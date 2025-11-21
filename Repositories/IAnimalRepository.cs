using Security.Models;

namespace Security.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAll();
        Task<Animal?> GetOne(Guid id);
        Task Add(Animal animal);
        Task Update(Animal animal);
        Task Delete(Animal animal);
    }
}