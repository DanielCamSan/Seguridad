using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class CreateAnimalDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Species { get; set; }
        public int Age { get; set; }
        public string Habitat { get; set; }
        public string ConservationStatus { get; set; }
    }
}
