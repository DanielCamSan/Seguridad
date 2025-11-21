using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Models;

namespace Security.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext _db;
        public AnimalRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Animal animal)
        {
            await _db.Animals.AddAsync(animal);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _db.Animals.ToListAsync();
        }

        public async Task<Animal?> GetOne(Guid id)
        {
            return await _db.Animals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Animal animal)
        {
            _db.Animals.Update(animal);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Animal animal)
        {
            _db.Animals.Remove(animal);
            await _db.SaveChangesAsync();
        }
    }
}