using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHouse.Models.Models
{
    public class Temperature
    {
        [Key]
        public int Id { get; set; }
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public int Humidity { get; set; }
        public int HeatIndex { get; set; }
        public DateTime DateAdded { get; set; }

        [ForeignKey(nameof(HomeAddress))]
        public int HomeAddressId { get; set; }
        public HomeAddress HomeAddress { get; set; }
    }
}
