namespace Security.Models
{
    public class Hospital
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }
        public Guid AdminId { get; set; }
        public User Admin { get; set; }
        
        public ICollection<Doctor> Doctors { get; set; }
    }
}
