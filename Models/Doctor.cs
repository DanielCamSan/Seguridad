using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Specialty { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }
    }
}
