using System;

namespace SmartHouse.Models.Requests
{
    public class TemperatureSearchRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
