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

        public async Task Add(Doctor doctor)
        {
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _db.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetOne(Guid id)
        {
            return await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Doctor doctor)
        {
            _db.Doctors.Update(doctor);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Doctor doctor)
        {
            _db.Doctors.Remove(doctor);
            await _db.SaveChangesAsync();
        }
    }
}
