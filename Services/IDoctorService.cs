using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(Guid id);
        Task<Doctor> CreateDoctor(CreateDoctorDto dto);
        Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id);
        Task DeleteDoctor(Guid id);
    }

    public record CreateDoctorDto(string Name, string Specialty, Guid HospitalId);
    public record UpdateDoctorDto(string Name, string Specialty, Guid HospitalId);
}