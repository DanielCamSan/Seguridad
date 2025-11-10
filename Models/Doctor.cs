namespace Security.Models;

public class Doctor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public Guid HospitalId { get; set; }
    public Hospital Hospital { get; set; }
}