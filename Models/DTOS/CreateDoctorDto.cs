using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty; 
        public Guid HospitalId { get; set;  }
    }
}
