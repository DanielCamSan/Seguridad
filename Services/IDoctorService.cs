using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor?> GetOne(Guid id);
        Task<Doctor> Create(CreateDoctorDto dto);
        Task<Doctor?> Update(Guid id, UpdateDoctorDto dto);
        Task<bool> Delete(Guid id);
    }
}
