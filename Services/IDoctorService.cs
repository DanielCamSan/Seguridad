using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> getAll();

        Task<DoctorDto> getById(Guid id);
        Task<DoctorDto> create(CreateDoctorDto dto);

        Task<DoctorDto> update(Guid id, UpdateDoctorDto dto);

        Task delete(Guid id);
    }
}
