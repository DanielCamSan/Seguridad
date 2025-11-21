namespace Security.Models.DTOS
{
    public class UpdateAnimalDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Habitat { get; set; }
        public string ConservationStatus { get; set; }
    }
}
