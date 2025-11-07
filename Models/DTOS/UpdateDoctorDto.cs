namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public Guid HospitalId { get; set; }
    }
}
