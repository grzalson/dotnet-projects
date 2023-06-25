using System.ComponentModel.DataAnnotations;

namespace MechanicKol.Models.DTOs
{
    public class CarPostDTO
    {
        [Required]
        public int IdMechanic { get; set; }
        [Required]
        public string RegistrationPlate { get; set; }
        [Required]
        public DateTime ProductionYear { get; set; } = DateTime.Now;
        [Required]
        public string Make { get; set; }
    }
}
