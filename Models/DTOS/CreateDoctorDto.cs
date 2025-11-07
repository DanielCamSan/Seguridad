using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; init; } = string.Empty;
        public string Specialty { get; init; } = string.Empty; 
        public Guid HospitalId { get; init ;  }
    }
}
