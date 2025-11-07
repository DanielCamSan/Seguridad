using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protege todo el controlador (EJERCICIO 1)
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorsController(IDoctorService service) => _service = service;

        // GET: /api/doctors (Usuarios con rol 'User' pueden ver)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET: /api/doctors/{id} (Usuarios con rol 'User' pueden ver)
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var doctor = await _service.GetOneAsync(id);
            return doctor == null ? NotFound() : Ok(doctor);
        }

        // POST: /api/doctors (SOLO Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var doctor = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, doctor);
        }

        // PUT: /api/doctors/{id} (SOLO Admin)
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto, Guid id)
        {
            try
            {
                var doctor = await _service.UpdateAsync(dto, id);
                return Ok(doctor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: /api/doctors/{id} (SOLO Admin)
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}