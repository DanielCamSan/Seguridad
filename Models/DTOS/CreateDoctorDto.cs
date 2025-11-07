using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class CreateDoctorDto
    {
        [Required]
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int Type { get; set; }

        [Required]
        Guid HospitalId { get; set; }
    }
}
