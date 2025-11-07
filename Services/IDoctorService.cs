using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor?> Get(Guid id);
        Task<Doctor> Create(CreateDoctorDto dto);
        Task<bool> Update(Guid id, UpdateDoctorDto dto);
        Task<bool> Delete(Guid id);
    }
}
