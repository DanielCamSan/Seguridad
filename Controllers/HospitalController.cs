using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;
using System.Security.Claims;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HospitalController:ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHospitals()
        {
            IEnumerable<Hospital> items = await _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var hospital = await _service.GetOne(id);
            return Ok(hospital);
        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateHospital([FromBody] CreateHospitalDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var hospital = await _service.CreateHospital(dto);
            return CreatedAtAction(nameof(GetOne), new { id = hospital.Id }, hospital);
        }
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateHospital([FromBody] UpdateHospitalDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized();

            var (ok, message) = await _service.UpdateHospital(dto,id, userId);
            if (!ok)
            {
                if (message.Contains("permiso"))
                    return Forbid();
                return NotFound(new { message });
            }
            return Ok(new { message });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized();

            var (ok, message) = await _service.DeleteHospital(id, userId);
            if (!ok)
            {
                if (message.Contains("permiso"))
                    return Forbid();
                return NotFound(new { message });
            }

            return Ok(new { message });
        }
    }
}
