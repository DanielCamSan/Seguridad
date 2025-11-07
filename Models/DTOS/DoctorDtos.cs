// Models/DTOS/DoctorDtos.cs
using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public record CreateDoctorDto(
        [Required] string Name,
        [Required] string Specialty,
        [Required] Guid HospitalId
    );

    public record UpdateDoctorDto(
        [Required] string Name,
        [Required] string Specialty,
        [Required] Guid HospitalId
    );

    public record DoctorDto(
        Guid Id,
        string Name,
        string Specialty,
        Guid HospitalId
    );
}