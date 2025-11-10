using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; init; }
        public string Speciality { get; init; }
        public Guid IdHospital { get; init; }

    }
}
