using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class CreateDoctorDto
    {
        [Required]
        public Guid HospitalId { get; init; }

        [Required]
        public string Name { get; set; }
        public string Specialty { get; set; }
    }
}
