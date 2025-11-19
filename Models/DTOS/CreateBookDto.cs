using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateBookDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
    }
}
