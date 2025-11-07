namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public Guid HospitalId { get; set; }
        public Hospital? Hospital { get; set; }
    }
}