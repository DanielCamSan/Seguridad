using Security.Models;
using Security.Models.DTOS;

namespace Security.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAll();
        Task<Animal> GetOne(Guid id);
        Task<Animal> CreateAnimal(CreateAnimalDto dto);
        Task<Animal> UpdateAnimal(UpdateAnimalDto dto, Guid id);
        Task DeleteAnimal(Guid id);
    }
}