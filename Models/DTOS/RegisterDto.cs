using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record RegisterDto
    {
        [Required]
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Role { get; set; } = "User";
    }
}
