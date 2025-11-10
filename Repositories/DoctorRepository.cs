using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;
using Security.Models.DTOS;

namespace Security.Repositories
{

    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task createDoctorAsync(Doctor doc)
        {
            _context.Doctors.Add(doc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(Doctor doc)
        {
            _context.Doctors.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public Task<Doctor?> getDoctorById(Guid id)
        {
            return _context.Doctors.Include(d => d.Hospital).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Doctor>> getDoctors()
        {
            return await _context.Doctors.Include(d => d.Hospital).ToListAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doc)
        {
            _context.Doctors.Update(doc);
            await _context.SaveChangesAsync();

        }
    }
}
