using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;
using Security.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;

    public DoctorService(IDoctorRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Doctor>> GetAll() => await _repo.GetAllAsync();
    public async Task<Doctor?> GetOne(Guid id) => await _repo.GetByIdAsync(id);
    public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
    {
        var doctor = new Doctor { Id = Guid.NewGuid(), Name = dto.Name, Specialty = dto.Specialty, HospitalId = dto.HospitalId };
        await _repo.AddAsync(doctor);
        return doctor;
    }
    public async Task<Doctor?> UpdateDoctor(UpdateDoctorDto dto, Guid id)
    {
        var doctor = await _repo.GetByIdAsync(id);
        if (doctor == null) return null;

        doctor.Name = dto.Name;
        doctor.Specialty = dto.Specialty;
        doctor.HospitalId = dto.HospitalId;

        await _repo.UpdateAsync(doctor);
        return doctor;
    }
    public async Task DeleteDoctor(Guid id) => await _repo.DeleteAsync(id);
}
