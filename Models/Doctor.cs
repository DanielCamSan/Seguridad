namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Speciallity { get; set; }

        public int HospitalId { get; set; }

        public Hospital? Hospital { get; set; }

    }
}
