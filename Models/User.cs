using System.ComponentModel.DataAnnotations;

namespace Security.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty; 
        public string Role { get; set; } = "User";//"User" | "Admin"
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }
        public string? CurrentJwtId { get; set; }

        public Hospital? hospital { get; set;  }

    }
}
