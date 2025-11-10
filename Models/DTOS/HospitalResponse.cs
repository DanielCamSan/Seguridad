using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record HospitalResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }
        public UserResponse Admin { get; set; }
    }
}
