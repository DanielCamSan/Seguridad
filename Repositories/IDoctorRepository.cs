using Security.Models;

namespace Security.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetOne(Guid id);
        Task Add(Doctor doctor);
        Task Update(Doctor doctor);
        Task Delete(Doctor doctor);
    }
}
