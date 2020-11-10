using System;

namespace SmartHouse.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public int Humidity { get; set; }
        public int HeatIndex { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
