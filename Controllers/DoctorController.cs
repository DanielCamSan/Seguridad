using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _service.GetAll();
            return Ok(doctors);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            var doctor = await _service.GetOne(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var doctor = await _service.CreateDoctor(dto);
            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var doctor = await _service.UpdateDoctor(dto, id);
            if (doctor == null) return NotFound();

            return Ok(doctor);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var existing = await _service.GetOne(id);
            if (existing == null) return NotFound();

            await _service.DeleteDoctor(id);
            return NoContent();
        }
    }
}
