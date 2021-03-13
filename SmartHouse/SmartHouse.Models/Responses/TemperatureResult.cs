using SmartHouse.Models.Models;
using System.Collections.Generic;

namespace SmartHouse.Models.Responses
{
    public class TemperatureResult
    {
        public List<Temperature> Temperatures { get; set; }
        public float CelAvgTemperature { get; set; }
        public float FahAvgTemperature { get; set; }
    }
}
