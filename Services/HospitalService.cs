using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Routing.Matching;
using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepo;
        private readonly IUserRepository _userRepo;
        public HospitalService(IHospitalRepository hospitalRepo, IUserRepository userRepo)
        {
            _hospitalRepo = hospitalRepo;
            _userRepo = userRepo;
        }
        public async Task<HospitalResponseDto> CreateHospital(CreateHospitalDto dto, Guid userId)
        {
            User admin = await _userRepo.GetById(userId);
            
            var hospital = new Hospital
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Type = dto.Type,
                AdminId = userId,
                Admin = admin
            };
            var hospitalResponse = new HospitalResponseDto
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Type = hospital.Type,
                Admin = new UserResponse
                {
                    Id = hospital.Admin.Id.ToString(),
                    Email = hospital.Admin.Email,
                    Username = hospital.Admin.Username,
                }
            };
            
            admin.HospitalId = hospital.Id;
            admin.Hospital = hospital;
                
            
            await _hospitalRepo.Add(hospital);
            await _userRepo.UpdateAsync(admin);
            return hospitalResponse;
        }

        public async Task<IEnumerable<HospitalResponseDto>> GetAll()
        {
            IEnumerable<Hospital> hospitals = await _hospitalRepo.GetAll();
            return hospitals.Select(h=> new HospitalResponseDto()
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Type = h.Type,
                Admin = new UserResponse
                {
                    Id = h.AdminId.ToString(),
                    Email = h.Admin.Email,
                    Username = h.Admin.Username,
                }
            });
        }

        public async Task<HospitalResponseDto> GetOne(Guid id)
        {
            Hospital hospital = await _hospitalRepo.GetOne(id);
            
            return new  HospitalResponseDto()
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Type = hospital.Type,
                Admin = new UserResponse
                {
                    Id = hospital.Admin.Id.ToString(),
                    Email = hospital.Admin.Email,
                    Username = hospital.Admin.Username,
                }
            };
        }
        public async Task<HospitalResponseDto> UpdateHospital(UpdateHospitalDto dto, Guid id, Guid userId)
        {
            Hospital? hospital = await _hospitalRepo.GetOne(id);
            if (hospital == null) throw new Exception("Hospital doesnt exist.");
            if (hospital.AdminId != userId) throw new UnauthorizedAccessException();

            hospital.Name = dto.Name;
            hospital.Address = dto.Address;
            hospital.Type = dto.Type;

            await _hospitalRepo.Update(hospital);
            return new HospitalResponseDto()
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address,
                Type = hospital.Type,
                Admin = new UserResponse
                {
                    Id = hospital.Admin.Id.ToString(),
                    Email = hospital.Admin.Email,
                    Username = hospital.Admin.Username,
                }
            };
        }
        public async Task DeleteHospital(Guid id, Guid userId)
        {
            
            Hospital? hospital = (await _hospitalRepo.GetAll()).FirstOrDefault(h => h.Id == id);
            if (hospital == null) return;
            if (hospital.AdminId != userId) throw new UnauthorizedAccessException();

            await _hospitalRepo.Delete(hospital);
        }
    }
}
