using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) => _repo = repo; 

        public async Task<IEnumerable<Doctor>> GetAll() => await _repo.GetAll();
        public async Task<Doctor> GetOneDoctor(Guid id) => await _repo.GetOne(id); 
        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            var doc = new Doctor
            {
                Name = dto.Name.Trim(),
                Specialty = dto.Specialty.Trim(),
                HospitalId = dto.HospitalId
            };
            await _repo.Add(doc);
            await _repo.SaveChangesAsync();
            return doc; 
        }
        public async Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id)
        {
            Doctor? exist = await GetOneDoctor(id);
            if (exist is null) throw new ArgumentException("Doctor doesnt exist");

            exist.Name = dto.Name;
            exist.Specialty = dto.Specialty;
            exist.HospitalId = dto.HospitalId;

            await _repo.Update(exist);
            await _repo.SaveChangesAsync();
            return exist; 
            
        }
        public async Task Delete(Guid id)
        {
            Doctor? exist = await GetOneDoctor(id);
            if (exist is null) throw new ArgumentException("Doctor doesnt exist");

            await _repo.Delete(exist);
            await _repo.SaveChangesAsync(); 
        }
    }
}