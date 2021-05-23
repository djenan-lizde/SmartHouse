using System.ComponentModel.DataAnnotations;

namespace SmartHouse.Models.Models
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit { get; set; }
    }
}
