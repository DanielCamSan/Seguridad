using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetOneDoctor(Guid id);
        Task<Doctor> CreateDoctor(CreateDoctorDto dto);
        Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id);
        Task Delete(Guid id); 
    }
}