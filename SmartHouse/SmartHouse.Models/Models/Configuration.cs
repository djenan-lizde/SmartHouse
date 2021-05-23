using System.ComponentModel.DataAnnotations;

namespace SmartHouse.Models.Models
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Temperature needs to be inserted between 0 and 50 degrees.")]
        public float TemperatureCelsius { get; set; }

        [Required]
        public float TemperatureFahrenheit { get; set; }
    }
}
