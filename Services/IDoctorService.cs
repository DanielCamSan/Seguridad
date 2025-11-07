using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllAsync();
        Task<DoctorDto?> GetOneAsync(Guid id);
        Task<DoctorDto> CreateAsync(CreateDoctorDto dto);
        Task<DoctorDto?> UpdateAsync(Guid id, UpdateDoctorDto dto);
        Task DeleteAsync(Guid id);
    }
}
