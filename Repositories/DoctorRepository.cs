using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Doctor>> GetAll() => await _context.Doctors.ToListAsync(); 

        public async Task<Doctor> GetOne(Guid id) => await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id );
        public async Task Add(Doctor doctor) => await _context.Doctors.AddAsync(doctor); 
        public async Task Update(Doctor doctor) =>  _context.Doctors.Update(doctor);
        public async Task Delete(Doctor doctor) => _context.Doctors.Remove(doctor);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync(); 
    }
}