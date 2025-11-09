using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class CreateDoctorDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        public Guid HospitalId { get; set; }
    }
}
