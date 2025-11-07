namespace Security.Models
{
    public class Hospital
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public int Type { get; set; }
    }
}
