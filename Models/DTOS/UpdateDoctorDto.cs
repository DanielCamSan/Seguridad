using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        [Required]
        public string Name { get; init; } = string.Empty;
        public string Specialty { get; init; } = string.Empty; 
        public Guid HospitalId { get; init;  }
    }
}
