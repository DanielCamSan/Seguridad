namespace Security.Models.DTOS
{
    public class CreateDoctorDto
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int HospitalId { get; set; }
    }
}
