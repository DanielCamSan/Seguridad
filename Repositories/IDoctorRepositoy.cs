using Security.Models;

namespace Security.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(Guid id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(Guid id);
    }
}
