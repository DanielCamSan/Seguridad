namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public Guid HospitalId { get; set; }
    }
}
