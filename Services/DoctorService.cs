// Services/DoctorService.cs
using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) => _repo = repo;

        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
        {
            var doctors = await _repo.GetAllAsync();
            return doctors.Select(d => new DoctorDto(d.Id, d.Name, d.Specialty, d.HospitalId));
        }

        public async Task<DoctorDto?> GetOneAsync(Guid id)
        {
            var d = await _repo.GetOneAsync(id);
            return d == null ? null : new DoctorDto(d.Id, d.Name, d.Specialty, d.HospitalId);
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };
            await _repo.AddAsync(doctor);
            return new DoctorDto(doctor.Id, doctor.Name, doctor.Specialty, doctor.HospitalId);
        }

        public async Task<DoctorDto> UpdateAsync(UpdateDoctorDto dto, Guid id)
        {
            var doctor = await _repo.GetOneAsync(id);
            if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.HospitalId = dto.HospitalId;

            await _repo.UpdateAsync(doctor);
            return new DoctorDto(doctor.Id, doctor.Name, doctor.Specialty, doctor.HospitalId);
        }

        public async Task DeleteAsync(Guid id)
        {
            var doctor = await _repo.GetOneAsync(id);
            if (doctor == null) return;
            await _repo.DeleteAsync(doctor);
        }
    }
}