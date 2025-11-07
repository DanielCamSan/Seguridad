using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models.DTOS;
using Security.Models.DTOS.Security.Models.DTOS;
using Security.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


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
        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrWhiteSpace(sub)) return Unauthorized();
            if (!Guid.TryParse(sub, out var userId)) return Unauthorized();

            await _service.LogoutAsync(userId);
            return NoContent();
        }
    }
}
