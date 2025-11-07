using System.ComponentModel.DataAnnotations;

namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Specialty { get; set; } = string.Empty;

        // Clave foránea para la relación con Hospital (Ejercicio 3, si aplica)
        public Guid HospitalId { get; set; }
        public Hospital Hospital { get; set; } = default!;
    }
}