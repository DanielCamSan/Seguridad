using Microsoft.EntityFrameworkCore.Storage;
using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IHospitalRepository? _hospitalRepo;
        public DoctorService(IDoctorRepository doctorRepo,IHospitalRepository hospitalRepo)
        {
            _doctorRepo = doctorRepo;
            _hospitalRepo = hospitalRepo;
        }
        public async Task<DoctorResponseDto> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };
            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");
            doctor.Hospital = hospital;
            hospital.Doctors.Add(doctor);
            
            
            await _doctorRepo.Add(doctor);
            await _hospitalRepo.Update(hospital);
            return new DoctorResponseDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Hospital = new HospitalResponseDto
                {
                    Id = hospital.Id,
                    Name = hospital.Name,
                    Address = hospital.Address,
                    Type = hospital.Type
                }
            };
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAll()
        {
            IEnumerable<Doctor> doctors = await _doctorRepo.GetAll();
            return doctors.Select(d => new DoctorResponseDto()
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                Hospital = new HospitalResponseDto
                {
                    Id = d.Hospital.Id,
                    Name = d.Hospital.Name,
                    Address = d.Hospital.Address,
                    Type = d.Hospital.Type
                }
            });
        }

        public async Task<DoctorResponseDto> GetOne(Guid id)
        {
            Doctor doctor= await _doctorRepo.GetOne(id);
            return new  DoctorResponseDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Hospital = new HospitalResponseDto
                {
                    Id = doctor.Hospital.Id,
                    Name = doctor.Hospital.Name,
                    Address = doctor.Hospital.Address,
                    Type = doctor.Hospital.Type
                }
            };
        }
        
        public async Task<DoctorResponseDto> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? doctor = await _doctorRepo.GetOne(id);
            if (doctor == null) throw new Exception("Doctor doesnt exist.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.HospitalId = dto.HospitalId;

            await _doctorRepo.Update(doctor);
            return new DoctorResponseDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Hospital = new HospitalResponseDto
                {
                    Id = doctor.Hospital.Id,
                    Name = doctor.Hospital.Name,
                    Address = doctor.Hospital.Address,
                    Type = doctor.Hospital.Type
                }
            };
        }
        public async Task DeleteDoctor(Guid id)
        {
            Doctor? doctor = (await _doctorRepo.GetAll()).FirstOrDefault(h => h.Id == id);
            if (doctor == null) return;
            await _doctorRepo.Delete(doctor);
        }
    }
}
