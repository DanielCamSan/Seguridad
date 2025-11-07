using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;
using System.Security.Claims; // Importante para ClaimTypes

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HospitalController : ControllerBase
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
        [Authorize] // <--- Cambio: Autorización gestionada dentro del método
        public async Task<IActionResult> UpdateHospital([FromBody] UpdateHospitalDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            // EJERCICIO 3: Lógica de Autorización
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null) return Unauthorized();

            var hospital = await _service.GetOne(id);
            if (hospital is null) return NotFound();

            // Si NO es Admin Y el ID del token no coincide con el AdminId del hospital
            if (!User.IsInRole("Admin") && hospital.AdminId != Guid.Parse(userIdClaim))
            {
                return Forbid(); // Devuelve 403 Forbidden
            }
            // FIN EJERCICIO 3

            try
            {
                var updatedHospital = await _service.UpdateHospital(dto, id);
                return Ok(updatedHospital);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize] // <--- Cambio: Autorización gestionada dentro del método
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            // EJERCICIO 3: Lógica de Autorización
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null) return Unauthorized();

            var hospital = await _service.GetOne(id);
            if (hospital is null) return NoContent();

            // Si NO es Admin Y el ID del token no coincide con el AdminId del hospital
            if (!User.IsInRole("Admin") && hospital.AdminId != Guid.Parse(userIdClaim))
            {
                return Forbid(); // Devuelve 403 Forbidden
            }
            // FIN EJERCICIO 3

            await _service.DeleteHospital(id);
            return NoContent();
        }
    }
}