namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        public string Name { get; init; }
        public string Speciality { get; init; }
        public Guid HospitalId { get; init; }
    }
}