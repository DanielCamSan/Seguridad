using Security.Models;

namespace Security.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetAsync(Guid id);
        Task AddAsync(Doctor d);
        Task UpdateAsync(Doctor d);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
