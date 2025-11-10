using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; set; }
        public string Specialty { get; set; }
        public Guid HospitalId { get; set; }
    }
}
