using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;
        public DoctorRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
        }

        public async  Task DeleteDoctorAsync(Guid id)
        {

            var doctor = await _db.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _db.Doctors.Remove(doctor);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _db.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _db.Doctors.Update(doctor);
            await _db.SaveChangesAsync();

        }
    }
}
