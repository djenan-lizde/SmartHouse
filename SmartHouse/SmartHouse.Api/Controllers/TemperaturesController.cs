using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;
using SmartHouse.Models.Requests;
using System;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        private readonly ITemperatureService _temperatureService;

        public TemperaturesController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        [HttpGet]
        public IActionResult GetTemperatures()
        {
            return Ok(_temperatureService.Get());
        }

        [HttpGet("filter")]
        public IActionResult GetTemperaturesCondition([FromQuery] TemperatureSearchRequest temperatureSearchRequest)
        {
            try
            {
                var query = _temperatureService.GetByCondition(x => x.DateAdded.Day == temperatureSearchRequest.Day
                    && x.DateAdded.Month == temperatureSearchRequest.Month && x.DateAdded.Year == temperatureSearchRequest.Year);
                return Ok(query);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("current")]
        public IActionResult GetLastTemperature()
        {
            try
            {
                var lastTemperature = _temperatureService.GetLastT();
                return Ok(lastTemperature);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult PostTemeprature([FromBody] Temperature temperature)
        {
            try
            {
                return Ok(_temperatureService.Insert(temperature));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
