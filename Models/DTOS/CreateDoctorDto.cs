using System.ComponentModel.DataAnnotations;
namespace Security.Models.DTOS
{
    public record CreateDoctorDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Speciality { get; init; }

        [Required]
        public Guid HospitalId { get; set; }
         

    }
}