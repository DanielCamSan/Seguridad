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
        public async Task<(bool ok, string message)> UpdateHospital(UpdateHospitalDto dto, Guid id, Guid userId)
        {
            Hospital? hospital = await GetOne(id);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");
            if (hospital.AdminId != userId)
                return (false, "No tienes permiso para editar este hospital");

            hospital.Name = dto.Name;
            hospital.Address = dto.Address;
            hospital.Type = dto.Type;

            await _repo.Update(hospital);
            return (true, "Hospital actualizado correctamente");
        }
        public async Task<(bool ok, string message)> DeleteHospital(Guid id, Guid userId)
        {
            Hospital? hospital = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (hospital == null) return (false, "Hospital no encontrado");
            if (hospital.AdminId != userId)
                return (false, "No tienes permiso para eliminar este hospital");
            await _repo.Delete(hospital);
            return (true, "Hospital eliminado");
        }
    }
}
