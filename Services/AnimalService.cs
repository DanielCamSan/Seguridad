using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repo;
        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo;
        }

        public async Task<Animal> CreateAnimal(CreateAnimalDto dto)
        {
            var animal = new Animal
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Species = dto.Species,
                Age = dto.Age,
                Habitat = dto.Habitat,
                ConservationStatus = dto.ConservationStatus
            };
            await _repo.Add(animal);
            return animal;
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Animal> GetOne(Guid id)
        {
            var animal = await _repo.GetOne(id);
            if (animal == null) throw new Exception("Animal doesn't exist.");
            return animal;
        }

        public async Task<Animal> UpdateAnimal(UpdateAnimalDto dto, Guid id)
        {
            Animal? animal = await GetOne(id);
            if (animal == null) throw new Exception("Animal doesn't exist.");

            animal.Name = dto.Name;
            animal.Species = dto.Species;
            animal.Age = dto.Age;
            animal.Habitat = dto.Habitat;
            animal.ConservationStatus = dto.ConservationStatus;

            await _repo.Update(animal);
            return animal;
        }

        public async Task DeleteAnimal(Guid id)
        {
            Animal? animal = await _repo.GetOne(id);
            if (animal == null) return;
            await _repo.Delete(animal);
        }
    }
}