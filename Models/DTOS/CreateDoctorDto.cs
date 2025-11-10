using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; set; }
        public string Speciallity { get; set; }

        public Guid HospitalId { get; set; }

    }
}
