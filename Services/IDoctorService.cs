// Services/IDoctorService.cs
using Security.Models.DTOS;
using Security.Models;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllAsync();
        Task<DoctorDto?> GetOneAsync(Guid id);
        Task<DoctorDto> CreateAsync(CreateDoctorDto dto);
        Task<DoctorDto> UpdateAsync(UpdateDoctorDto dto, Guid id);
        Task DeleteAsync(Guid id);
    }
}