namespace Security.Models
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Habitat { get; set; }
        public string ConservationStatus { get; set; }
    }
}