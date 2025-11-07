using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService (IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Speciallity = dto.Speciallity,
                HospitalId = dto.HospitalId
            };
            await _repo.Add(doctor);
            return doctor;
        }

        public async Task <IEnumerable<Doctor>> GetAll()
        {
            return await _repo.GetAll();
        }
        public async Task<Doctor> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? doctor = await GetOne(id);
            if (doctor == null) throw new Exception("Doctor doesnt exists");

           doctor.Name = dto.Name;
            doctor.Speciallity = dto.Speciallity;
            doctor.HospitalId = dto.HospitalId;
            
            await _repo.Update(doctor);
            return doctor;


        }

        public async Task DeleteDoctor(Guid id)
        {
            Doctor? doctor = (await GetAll()).FirstOrDefault(d=> d.Id == id);
            if (doctor == null) return;
            await _repo.Delete(doctor);
        }

    }
}
