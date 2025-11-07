using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _db;
        public DoctorRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Doctor>> GetAllAsync() =>
            await _db.Doctors.AsNoTracking().ToListAsync();

        public Task<Doctor?> GetAsync(Guid id) =>
            _db.Doctors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Doctor d)
        {
            _db.Doctors.Add(d);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor d)
        {
            _db.Doctors.Update(d);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var ghost = new Doctor { Id = id };
            _db.Attach(ghost);
            _db.Remove(ghost);
            await _db.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _db.Doctors.AnyAsync(x => x.Id == id);
    }
}
