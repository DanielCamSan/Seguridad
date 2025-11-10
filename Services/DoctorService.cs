using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;
using System.Numerics;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHospitalRepository _hospitalRepository;

        public DoctorService(IDoctorRepository doctorRepository, IHospitalRepository hospitalRepository)
        {
            _doctorRepository = doctorRepository;
            _hospitalRepository = hospitalRepository;
        }   
        public async Task<DoctorDto> create(CreateDoctorDto dto)
        {
            var hospital = await _hospitalRepository.GetOne(dto.HospitalId);

            if (hospital==null) throw new Exception("Hospital not found");
            var doctor = new Doctor
            {
                Name = dto.Name,
                Speciality = dto.Speciality,
                Hospital = hospital
            };
            await _doctorRepository.createDoctorAsync(doctor);
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Speciality = doctor.Speciality,
                HospitalId = doctor.HospitalId,
                HospitalName= hospital.Name
            };
        }

        public async Task delete(Guid id)
        {
            var doctor = await _doctorRepository.getDoctorById(id);
            if (doctor == null) throw new Exception("Doctor not found");
            await _doctorRepository.DeleteDoctorAsync(doctor);
        }

        public async Task<IEnumerable<DoctorDto>> getAll()
        {
           var doctors=await _doctorRepository.getDoctors();
            return doctors.Select(doctor => new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Speciality = doctor.Speciality,
                HospitalId = doctor.HospitalId,
                HospitalName = doctor.Hospital.Name
            }).ToList();
        }

        public async Task<DoctorDto> getById(Guid id)
        {
           var doctor= await _doctorRepository.getDoctorById(id);
            if (doctor == null) throw new Exception("Doctor not found");
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Speciality = doctor.Speciality,
                HospitalId = doctor.HospitalId,
                HospitalName =doctor.Hospital.Name
            };


        }

        public async Task<DoctorDto> update(Guid id, UpdateDoctorDto dto)
        {
            var doctor=await  _doctorRepository.getDoctorById(id);
            if (doctor == null) throw new Exception("Doctor not found");
            doctor.Name = dto.Name;
            doctor.Speciality = dto.Speciality;
            doctor.HospitalId = dto.HospitalId;
            await _doctorRepository.UpdateDoctorAsync(doctor);
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Speciality = doctor.Speciality,
                HospitalId = doctor.HospitalId,
                HospitalName = doctor.Hospital.Name
            };
        }
    }
}
