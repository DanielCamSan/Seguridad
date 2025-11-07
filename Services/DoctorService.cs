using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) => _repo = repo;

        public Task<IEnumerable<Doctor>> GetAll() => _repo.GetAllAsync();
        public Task<Doctor?> Get(Guid id) => _repo.GetAsync(id);

        public async Task<Doctor> Create(CreateDoctorDto dto)
        {
            var d = new Doctor { Name = dto.Name, Specialty = dto.Specialty, HospitalId = dto.HospitalId };
            await _repo.AddAsync(d);
            return d;
        }

        public async Task<bool> Update(Guid id, UpdateDoctorDto dto)
        {
            if (!await _repo.ExistsAsync(id)) return false;
            var d = new Doctor { Id = id, Name = dto.Name, Specialty = dto.Specialty, HospitalId = dto.HospitalId };
            await _repo.UpdateAsync(d);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            if (!await _repo.ExistsAsync(id)) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
