using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) { _repo = repo; }

        public Task<IEnumerable<Doctor>> GetAll() => _repo.GetAll();

        public Task<Doctor?> GetOne(Guid id) => _repo.GetOne(id);

        public async Task<Doctor> Create(CreateDoctorDto dto)
        {
            var d = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };
            await _repo.Add(d);
            return d;
        }

        public async Task<Doctor?> Update(Guid id, UpdateDoctorDto dto)
        {
            var d = await _repo.GetOne(id);
            if (d == null) return null;
            d.Name = dto.Name;
            d.Specialty = dto.Specialty;
            d.HospitalId = dto.HospitalId;
            await _repo.Update(d);
            return d;
        }

        public async Task<bool> Delete(Guid id)
        {
            var d = await _repo.GetOne(id);
            if (d == null) return false;
            await _repo.Delete(d);
            return true;
        }
    }
}
