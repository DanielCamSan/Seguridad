using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorResponse>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return doctors.Select(d => new DoctorResponse
            {
                Id = d.Id,
                Name = d.Name,
                Specialty = d.Specialty,
                HospitalId = d.HospitalId,
                HospitalName = d.Hospital?.Name
            });
        }

        public async Task<DoctorResponse> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return null;

            return new DoctorResponse
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                HospitalId = doctor.HospitalId,
                HospitalName = doctor.Hospital?.Name
            };
        }
    }
}