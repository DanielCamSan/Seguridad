using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repo;
        public HospitalService(IHospitalRepository repo)
        {
            _repo = repo;
        }
        public async Task<Hospital> CreateHospital(CreateHospitalDto dto)
        {
            var hospital = new Hospital
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Type = dto.Type
            };
            await _repo.Add(hospital);
            return hospital;
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Hospital> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }
        public async Task<Hospital> UpdateHospital(UpdateHospitalDto dto, Guid id, Guid userId)
        {
            Hospital? hospital = await GetOne(id);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");

            if (hospital.AdminId != userId) throw new UnauthorizedAccessException("You are not authorized to update this hospital.");

            hospital.Name = dto.Name;
            hospital.Address = dto.Address;
            hospital.Type = dto.Type;

            await _repo.Update(hospital);
            return hospital;
        }
        public async Task DeleteHospital(Guid id, Guid userId)
        {
            Hospital? hospital = await GetOne(id);
            if (hospital == null) return;

            if (hospital.AdminId != userId) throw new UnauthorizedAccessException("You are not authorized to delete this hospital.");

            await _repo.Delete(hospital);
        }
    }
}
