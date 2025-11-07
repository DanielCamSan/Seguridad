using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateHospitalDto
    {
        [Required]
        public required string Name { get; set; }
        public required string Address { get; set; }
        public int Type { get; set; }
    }
}
