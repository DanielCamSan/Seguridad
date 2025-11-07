using Security.Models;

namespace Security.Repositories
{
    public interface IDoctorRepository
    {
        Task Add(Doctor doctor);

        Task Update(Doctor doctor);
        Task Delete(Doctor doctor);
        Task<Doctor> GetOne(Guid id);
        Task<IEnumerable<Doctor>> GetAll();

    }
}
