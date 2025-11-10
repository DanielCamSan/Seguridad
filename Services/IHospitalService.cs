using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalResponseDto>> GetAll();
        Task<HospitalResponseDto> GetOne(Guid id);
        Task<HospitalResponseDto> CreateHospital(CreateHospitalDto dto, Guid userId);
        Task<HospitalResponseDto> UpdateHospital(UpdateHospitalDto dto, Guid id,Guid userId);
        Task DeleteHospital(Guid id,Guid userId);
    }
}
