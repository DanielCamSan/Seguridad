using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

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
            if (hospital == null) throw new Exception("Hospital doesnt exist.");

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

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Doctor> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? doctor = await GetOne(id);
            if (doctor == null) throw new Exception("Doctor doesnt exist.");

            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.HospitalId = dto.HospitalId;

            await _repo.Update(doctor);
            return doctor;
        }

        public async Task DeleteDoctor(Guid id)
        {
            Doctor? doctor = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (doctor == null) return;
            await _repo.Delete(doctor);
        }
    }
}