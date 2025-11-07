using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) { _repo = repo; }

        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                HospitalId = d.HospitalId
            });
        }

        public async Task<DoctorDto?> GetOneAsync(Guid id)
        {
            var d = await _repo.GetByIdAsync(id);
            return d == null ? null : new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                HospitalId = d.HospitalId
            };
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
        {
            var entity = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };
            await _repo.AddAsync(entity);

            return new DoctorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Specialty = entity.Specialty,
                HospitalId = entity.HospitalId
            };
        }

        public async Task<DoctorDto?> UpdateAsync(Guid id, UpdateDoctorDto dto)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;

            d.Name = dto.Name;
            d.Specialty = dto.Specialty;
            d.HospitalId = dto.HospitalId;

            await _repo.UpdateAsync(d);

            return new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                HospitalId = d.HospitalId
            };
        }

        public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
    }
}
