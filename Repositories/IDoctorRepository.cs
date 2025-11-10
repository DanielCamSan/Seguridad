using Security.Models;
using Security.Models.DTOS;

namespace Security.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> getDoctors();

        Task createDoctorAsync(Doctor doc);
        Task UpdateDoctorAsync(Doctor doc);

        Task DeleteDoctorAsync(Doctor id);
        Task<Doctor?> getDoctorById(Guid id);

    }
}
