namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }=Guid.NewGuid();

        public string Name { get; set; }

        public string Speciality { get; set; }

        public Hospital Hospital { get; set; } = default!;

        public Guid HospitalId { get; set; }
    }
}
