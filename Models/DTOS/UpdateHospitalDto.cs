namespace Security.Models.DTOS
{
    public record UpdateHospitalDto
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public int Type { get; set; }
    }
}
