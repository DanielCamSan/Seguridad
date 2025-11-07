using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IHospitalRepository _repo;
        public DoctorService(IHospitalRepository repo)
        {
            _repo = repo;
        }
        public async Task <Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty
            };
            await _repo.Add(doctor);
            return doctor;
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Hospital> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }
        public async Task<Hospital> UpdateHospital(UpdateHospitalDto dto, Guid id)
        {
            Hospital? hospital = await GetOne(id);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");

            hospital.Name = dto.Name;
            hospital.Address = dto.Address;
            hospital.Type = dto.Type;

            await _repo.Update(hospital);
            return hospital;
        }
        public async Task DeleteHospital(Guid id)
        {
            Hospital? hospital = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (hospital == null) return;
            await _repo.Delete(hospital);
        }
    }
}
