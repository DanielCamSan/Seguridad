namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required Guid HospitalId { get; set; }
    }
}
