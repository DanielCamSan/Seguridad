using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorsController(IDoctorService service) => _service = service;

        // GET /api/doctors 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Doctor> items = await _service.GetAllDoctors();
            return Ok(items);
        }

        // GET /api/doctors/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var doctor = await _service.GetDoctorById(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        // POST /api/doctors 
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var doctor = await _service.CreateDoctor(dto);
                return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT /api/doctors/{id}
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var doctor = await _service.UpdateDoctor(dto, id);
                return CreatedAtAction(nameof(GetOne), new { id = doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE /api/doctors/{id} 
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteDoctor(id);
            return NoContent();
        }
    }
}