using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service) => _service = service;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getAllDocs()
        {
            IEnumerable<Doctor> docs = await _service.GetAll();
            return Ok(docs);
        }
        [HttpGet("{id:guid}")]
        [Authorize]

        public async Task<IActionResult> getOneDoc(Guid id)
        {
            var doc = await _service.GetOneDoctor(id);
            return Ok(doc);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> createDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _service.CreateDoctor(dto);
            return CreatedAtAction(nameof(getOneDoc), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _service.UpdateDoctor(dto, id);
            return CreatedAtAction(nameof(getOneDoc), new { id = created.Id }, created);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _service.Delete(id);
            return NoContent(); 
        }

    }
}