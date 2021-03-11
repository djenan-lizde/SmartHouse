using System;

namespace SmartHouse.Models.Requests
{
    public class TemperatureSearchRequest
    {
        public DateTime DateFilter { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
