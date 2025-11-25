using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var book = await _service.GetOne(id);
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var book = await _service.CreateBook(dto);
            return CreatedAtAction(nameof(GetOne), new { id = book.Id }, book);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var book = await _service.UpdateBook(dto, id);
            return CreatedAtAction(nameof(GetOne), new { id = book.Id }, book);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteBook(id);
            return NoContent();
        }
    }
}