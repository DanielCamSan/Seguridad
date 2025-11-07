using System.ComponentModel.DataAnnotations;

namespace Security.Models.DTOS
{
    public class UpdateDoctor
    {
        public string Name { get; set; }
        public Guid HospitalId { get; set; }
        public string Specialty { get; set; }
    }
}
