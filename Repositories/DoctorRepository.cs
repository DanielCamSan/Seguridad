using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class DoctorRepository:IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext ctx) {
            _context = ctx;
        }

        public async Task Add(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetOne(Guid id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

    }
}
