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
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]

        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorService.getAll();
            return doctors is null ? new NotFoundResult() : Ok(doctors);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = await _doctorService.getById(id);
            return doctor is null ? new NotFoundResult() : Ok(doctor);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        {
            var doctor = await _doctorService.create(dto);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorDto dto)
        {
            var doctor=await _doctorService.update(id, dto);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _doctorService.delete(id);
            return NoContent();
        }

    }
}
