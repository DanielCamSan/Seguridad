namespace Security.Models.DTOS
{
    public class UpdateDoctorDto
    {
        public string Name { get; init; }
        public string Speciality { get; init; }
        public Guid IdHospital { get; init; }

    }
}
