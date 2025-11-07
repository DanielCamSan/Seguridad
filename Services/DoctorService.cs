using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IHospitalRepository _hospitalRepo;

        public DoctorService(IDoctorRepository doctorRepo, IHospitalRepository hospitalRepo)
        {
            _doctorRepo = doctorRepo;
            _hospitalRepo = hospitalRepo;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {
            var doctors = await _doctorRepo.GetAllAsync();
            return doctors.Select(d => new DoctorResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                HospitalId = d.HospitalId,
                HospitalName = d.Hospital.Name
            });
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(Guid id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return null;

            return new DoctorResponseDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                HospitalId = doctor.HospitalId,
                HospitalName = doctor.Hospital.Name
            };
        }

        public async Task<DoctorResponseDto> CreateAsync(CreateDoctorDto dto)
        {
            // Verificar que el hospital existe
            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null)
                throw new KeyNotFoundException($"Hospital with id {dto.HospitalId} not found.");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Specialty = dto.Specialty,
                HospitalId = dto.HospitalId
            };

            await _doctorRepo.AddAsync(doctor);

            return new DoctorResponseDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                HospitalId = doctor.HospitalId,
                HospitalName = hospital.Name
            };
        }

        public async Task<DoctorResponseDto> UpdateAsync(Guid id, UpdateDoctorDto dto)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with id {id} not found.");

            // Verificar que el hospital existe
            var hospital = await _hospitalRepo.GetOne(dto.HospitalId);
            if (hospital == null)
                throw new KeyNotFoundException($"Hospital with id {dto.HospitalId} not found.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.HospitalId = dto.HospitalId;

            await _doctorRepo.UpdateAsync(doctor);

            return new DoctorResponseDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                HospitalId = doctor.HospitalId,
                HospitalName = hospital.Name
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return;

            await _doctorRepo.DeleteAsync(doctor);
        }
    }
}