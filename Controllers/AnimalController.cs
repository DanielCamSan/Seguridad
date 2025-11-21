using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;
        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals()
        {
            IEnumerable<Animal> items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var animal = await _service.GetOne(id);
            return Ok(animal);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateAnimal([FromBody] CreateAnimalDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var animal = await _service.CreateAnimal(dto);
            return CreatedAtAction(nameof(GetOne), new { id = animal.Id }, animal);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateAnimal([FromBody] UpdateAnimalDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var animal = await _service.UpdateAnimal(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = animal.Id }, animal);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteAnimal(id);
            return NoContent();
        }
    }
}