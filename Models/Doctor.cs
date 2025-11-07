namespace Security.Models
{
    public class Doctor
    {
        public Guid id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public int HospitalId { get; set; }
    }
}



