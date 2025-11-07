using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DoctorController:ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllDoctors()
        {
            IEnumerable<Doctor> items = await _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        [Authorize (Roles ="User,Admin")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var doctor = await _service.GetOne(id);
            return Ok(doctor);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var doctor = await _service.CreateDoctor(dto);
            return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, doctor);
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var doctor = await _service.UpdateDoctor(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, doctor);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteDoctor(id);
            return NoContent();
        }
    }
}
