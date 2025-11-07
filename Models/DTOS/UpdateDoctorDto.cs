using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty; 
        public Guid HospitalId { get; set;  }
    }
}
