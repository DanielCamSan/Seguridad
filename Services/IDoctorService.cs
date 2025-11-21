using Security.Models.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Security.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponse>> GetAllAsync();
        Task<DoctorResponse> GetByIdAsync(int id);
        Task AddAsync(CreateDoctorDto dto);
        Task UpdateAsync(int id, UpdateDoctorDto dto);
        Task DeleteAsync(int id);
    }
}
