using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int Type { get; set; }

        Guid HospitalId { get; set; }
    }
}
