using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _ctx;
        public DoctorRepository(AppDbContext ctx) { _ctx = ctx; }

        public Task<List<Doctor>> GetAllAsync() =>
            _ctx.Doctors.AsNoTracking().ToListAsync();

        public Task<Doctor?> GetByIdAsync(Guid id) =>
            _ctx.Doctors.FirstOrDefaultAsync(d => d.Id == id);

        public async Task AddAsync(Doctor doctor)
        {
            _ctx.Doctors.Add(doctor);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _ctx.Doctors.Update(doctor);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var d = await _ctx.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (d != null)
            {
                _ctx.Doctors.Remove(d);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
