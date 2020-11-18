using System;

namespace SmartHouse.Models.Requests
{
    public class TemperatureSearchRequest
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
