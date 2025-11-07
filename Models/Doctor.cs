using System;
using System.ComponentModel.DataAnnotations;

namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(120)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(120)]
        public string Specialty { get; set; } = default!;

        [Required]
        public Guid HospitalId { get; set; }
        public Hospital? Hospital { get; set; }
    }
}
