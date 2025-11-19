namespace Security.Models.DTOS
{
    public record UpdateBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
    }
}
