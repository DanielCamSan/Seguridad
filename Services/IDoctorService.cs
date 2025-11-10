using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAll();
        Task<DoctorResponseDto> GetOne(Guid id);
        Task<DoctorResponseDto> CreateDoctor(CreateDoctorDto dto);
        Task<DoctorResponseDto> UpdateDoctor(UpdateDoctorDto dto, Guid id);
        Task DeleteDoctor(Guid id);
    }
}
