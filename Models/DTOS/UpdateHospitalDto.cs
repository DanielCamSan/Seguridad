namespace Security.Models.DTOS
{
    public record UpdateHospitalDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }

        public Guid AdminUserId { get; set; } = Guid.Empty; 
    }
}
