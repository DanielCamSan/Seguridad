using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDoctors()
        {
            var items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOneDoctor(Guid id)
        {
            var doctor = await _service.GetOne(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var doctor = await _service.CreateDoctor(dto);
            return CreatedAtAction(nameof(GetOneDoctor), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var doctor = await _service.UpdateDoctor(dto, id);
            if(doctor == null) return NotFound();
            return Ok(doctor);
 
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _service.DeleteDoctor(id);
            return NoContent();
        }
    }
}