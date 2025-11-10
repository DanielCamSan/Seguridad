namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Speciallity { get; set; } = string.Empty;

        public Guid HospitalId { get; set; }

        public Hospital? Hospital { get; set; }

    }
}
