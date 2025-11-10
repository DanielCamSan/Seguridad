namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Speciallity { get; set; }

        public Guid HospitalId { get; set; }

    }
}
