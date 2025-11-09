using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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

        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

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

            var hospital = await _service.GetOne(id);
            if (hospital == null) return NotFound();

            if (hospital.AdminId != CurrentUserId)
            {
                return Forbid();
            }

            var updatedHospital = await _service.UpdateHospital(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = updatedHospital.Id }, updatedHospital);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var hospital = await _service.GetOne(id);
            if (hospital == null) return NoContent();

            if (hospital.AdminId != CurrentUserId)
            {
                return Forbid();
            }

            await _service.DeleteHospital(id);
            return NoContent();
        }
    }
}
