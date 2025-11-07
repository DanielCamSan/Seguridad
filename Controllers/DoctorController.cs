using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        // GET (User y Admin)
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET {id} (User y Admin)
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var dto = await _service.GetOneAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        // POST (solo Admin)
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
        }

        // PUT (solo Admin)
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return CreatedAtAction(nameof(GetOne), new { id = updated.Id }, updated);
        }

        // DELETE (solo Admin)
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
