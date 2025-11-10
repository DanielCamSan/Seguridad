using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IHospitalRepository _hospitals;

        public DoctorService(IDoctorRepository repo, IHospitalRepository hospitals)
        {
            _repo = repo;
            _hospitals = hospitals;
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Doctor> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            // Si tu CreateDoctorDto usa HospitalId, cambia dto.IdHospital por dto.HospitalId aquí y abajo.
            var hospital = await _hospitals.GetOne(dto.IdHospital);
            if (hospital is null) throw new KeyNotFoundException("Hospital no existe.");

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Speciality = dto.Speciality,
                HospitalId = dto.IdHospital
            };

            await _repo.Add(doctor);
            return doctor;
        }

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            var doctor = await _repo.GetOne(id);
            if (doctor is null) throw new KeyNotFoundException("Doctor doesnt exist.");

            doctor.Name = dto.Name;
            doctor.Speciality = dto.Speciality;

            if (dto.IdHospital != Guid.Empty && dto.IdHospital != doctor.HospitalId)
            {
                var hosp = await _hospitals.GetOne(dto.IdHospital);
                if (hosp is null) throw new KeyNotFoundException("Hospital no existe.");
                doctor.HospitalId = dto.IdHospital;
            }

            await _repo.Update(doctor);
            return doctor;
        }

        public async Task DeleteDoctor(Guid id)
        {
            var doctor = await _repo.GetOne(id);
            if (doctor is null) return;
            await _repo.Delete(doctor);
        }
    }
}
