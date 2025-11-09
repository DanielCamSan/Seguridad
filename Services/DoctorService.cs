using Security.Models;
using Security.Repositories;
using Security.Models.DTOS;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IHospitalRepository _hospitalRepo; 

        public DoctorService(IDoctorRepository repo, IHospitalRepository hospitalRepo)
        {
            _repo = repo;
            _hospitalRepo = hospitalRepo;
        }

        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null) throw new Exception("Hospital ID is invalid.");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };
            await _repo.Add(doctor);
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors() => await _repo.GetAll();

        public async Task<Doctor?> GetDoctorById(Guid id) => await _repo.GetOne(id);

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            var doctor = await _repo.GetOne(id);
            if (doctor == null) throw new Exception("Doctor not found.");

            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null) throw new Exception("New Hospital ID is invalid.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.HospitalId = dto.HospitalId;

            await _repo.Update(doctor);
            return doctor;
        }

        public async Task DeleteDoctor(Guid id)
        {
            var doctor = await _repo.GetOne(id);
            if (doctor == null) return;
            await _repo.Delete(doctor);
        }
    }
}