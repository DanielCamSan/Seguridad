using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        public Guid HospitalId { get; init; }
        public string Name { get; set; }
        public string Specialty { get; set; }

    }
}
