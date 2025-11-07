namespace Security.Models.DTOS
{
    public record UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Specialty { get; set; }  
    }
}
