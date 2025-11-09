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
           // var hospital = await _service.UpdateHospital(dto, id);
            //return CreatedAtAction(nameof(GetOne), new { id = hospital.Id }, hospital);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(ClaimTypes.PrimarySid)?.Value
                              ?? User.FindFirst(ClaimTypes.Name)?.Value;

            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid currentUserId))
            {
                return Unauthorized();
            }

            var hospital = await _service.GetOne(id);
            if (hospital == null) return NotFound();

            if (hospital.AdminUserId != currentUserId)
            {
                return Forbid();
            }

            try
            {
                var updatedHospital = await _service.UpdateHospital(dto, id);
                return CreatedAtAction(nameof(GetOne), new { id = updatedHospital.Id }, updatedHospital);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteHospital(Guid id)
        {
            //await _service.DeleteHospital(id);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst(ClaimTypes.PrimarySid)?.Value
                              ?? User.FindFirst(ClaimTypes.Name)?.Value;

            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid currentUserId))
            {
                return Unauthorized();
            }

            
            var hospital = await _service.GetOne(id);
            if (hospital == null) return NoContent();

            
            if (hospital.AdminUserId != currentUserId)
            {
                return Forbid();
            }
            await _service.DeleteHospital(id);
            return NoContent();
        }

    }
}
