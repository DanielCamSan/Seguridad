using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetOne(Guid id);
        Task<Doctor> CreateHospital(CreateDoctorDto dto);
        Task<Doctor> UpdateHospital(UpdateDoctorDto dto, Guid id);
        Task DeleteHospital(Guid id);
    }
}
