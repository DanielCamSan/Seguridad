using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        [Required]
        public string Name { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public Guid HospitalId { get; set; }
    }
}
