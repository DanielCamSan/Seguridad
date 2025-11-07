using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHospitalRepository _hospitals;
        public DoctorService(IDoctorRepository doctorRepository, IHospitalRepository hospitalRepository)
        {
            _doctorRepository = doctorRepository;
            _hospitals = hospitalRepository;
        }
        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            var hospital = await _hospitals.GetOne(dto.HospitalId);
            if (hospital is null)
                throw new KeyNotFoundException("Hospital not found");
            var doct = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId

            };
            await _doctorRepository.AddDoctorAsync(doct);
            return doct;
        }

        public async Task DeleteDoctor(Guid id)
        {
            Doctor? doctor = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (doctor == null) return;
            await _doctorRepository.DeleteDoctorAsync(id);

        }

        public async  Task<IEnumerable<Doctor>> GetAll()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<Doctor> GetOne(Guid id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? doctor = await GetOne(id);
            if (doctor == null) throw new Exception("Doctor doesnt exist.");
            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            await _doctorRepository.UpdateDoctorAsync(doctor);
            return doctor;
        }
    }
}
