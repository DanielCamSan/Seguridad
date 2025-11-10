using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required,StringLength(100)]
        public string Name { get; set; }
        public string Speciality { get; set; }
        [Required]
        public Guid HospitalId { get; set; }
    }

    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public Guid HospitalId { get; set; }

        public string HospitalName { get; set; }

    }
}
