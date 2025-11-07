using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IDoctorService
    {
        public interface IHospitalService
        {
            Task<IEnumerable<Doctor>> GetAll();
            Task<Doctor> GetOne(Guid id);
            Task<Doctor> CreateDoctor(CreateDoctorDto dto);
            Task<Hospital> UpdateDoctor(UpdateDoctorDto dto, Guid id);
            Task DeleteDoctor(Guid id);
        }
    }
}
