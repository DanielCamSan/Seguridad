using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record DoctorResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public HospitalResponseDto Hospital { get; set; }
    }
}
