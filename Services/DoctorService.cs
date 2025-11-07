using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctors;
        private readonly IHospitalRepository _hospitals;
        public DoctorService(IDoctorRepository doctors, IHospitalRepository hospitals)
        {
            _doctors = doctors;
            _hospitals = hospitals;
        }
        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            
            var hospital = await _hospitals.GetOne(dto.HospitalId);
            if (hospital is null)
                throw new KeyNotFoundException("Hospital no existe.");

            var alreadyHasDoctor = await _doctors.ExistsByHospitalId(dto.HospitalId);
            if (alreadyHasDoctor)
                throw new InvalidOperationException("Este Hospital ya tiene un doctor asignado.");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                HospitalId = hospital.Id,
                Name = dto.Name.Trim(),
                Specialty = dto.Specialty.Trim()
            };

            await _doctors.Add(doctor);
            return doctor;
        }

        public async Task DeleteDoctor(Guid id)
        {
            Doctor? doctor = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (doctor == null) return;
            await _doctors.Delete(doctor);
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _doctors.GetAll();
        }

        public async Task<Doctor> GetOne(Guid id)
        {
            return await _doctors.GetOne(id);
        }

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? doctor = await GetOne(id);
            if (doctor == null) throw new Exception("doctor doesnt exist.");

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;

            await _doctors.Update(doctor);
            return doctor;
        }
    }
}
