using System.ComponentModel.DataAnnotations.Schema;

namespace Security.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public Guid HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
    }
}