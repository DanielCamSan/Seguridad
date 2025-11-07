using Microsoft.AspNetCore.Mvc;
using Security.Models.DTOS;
using Security.Models.DTOS.Security.Models.DTOS;
using Security.Services;

namespace Security.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var id = await _service.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), new { id }, null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var (ok, response) = await _service.LoginAsync(dto);
            if (!ok || response is null) return Unauthorized();
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
        {
            var (ok, response) = await _service.RefreshAsync(dto);
            if (!ok || response is null) return Unauthorized();
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
         
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var ok = await _service.LogoutAsync(userId);
            if (!ok) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Logout exitoso. Refresh token invalidado." });
        }
    }
}
