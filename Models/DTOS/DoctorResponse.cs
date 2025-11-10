namespace Security.Models.DTOS
{
    public class DoctorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
    }
}
