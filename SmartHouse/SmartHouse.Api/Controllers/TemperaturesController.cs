using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        private readonly IData<Temperature> _service;

        public TemperaturesController(IData<Temperature> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetTemperatures()
        {
            return Ok(_service.Get());
        }

        //[HttpGet("Filter")]
        //public IActionResult GetTemperaturesCondition(TemperatureSearchRequest temperatureSearchRequest)
        //{
        //    var query = _service.GetByCondition(x => x.DateAdded.Day == temperatureSearchRequest.Day
        //        && x.DateAdded.Month == temperatureSearchRequest.Month && x.DateAdded.Year == temperatureSearchRequest.Year);
        //    return Ok(query);
        //}

        [HttpGet("CurrentTemperature")]
        public IActionResult GetLastTemperature()
        {
            try
            {
                var lastTemperature = _service.GetLastTemperature();
                return Ok(lastTemperature);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult PostTemeprature(Temperature temperature)
        {
            try
            {
                return Ok(_service.Insert(temperature));
            }
            catch (System.Exception)
            {
                return StatusCode(400);
            }
        }
    }
}
