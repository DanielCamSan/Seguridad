namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Speciality { get; set; }

        public Guid HospitalId { get; set; }
    }
}