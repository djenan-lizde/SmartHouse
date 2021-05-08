using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;
using SmartHouse.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("filter/{date}")]
        public IActionResult GetTemperaturesCondition([FromRoute] DateTime date)
        {
            try
            {
                var query = _temperatureService.GetByCondition(x => x.DateAdded.Day == date.Day
                    && x.DateAdded.Month == date.Month && x.DateAdded.Year == date.Year);

                TemperatureResult temperatureResult = new TemperatureResult
                {
                    Temperatures = new List<Temperature>()
                };

                foreach (var item in query)
                {
                    temperatureResult.Temperatures.Add(item);
                }

                temperatureResult.CelAvgTemperature = query.Average(x => x.TemperatureCelsius);
                temperatureResult.FahAvgTemperature = query.Average(x => x.TemperatureFahrenheit);

                return Ok(temperatureResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("current")]
        public IActionResult GetLastTemperature()
        {
            try
            {
                return Ok(_temperatureService.GetLastT());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{temperatureCelsius}/{temperatureFahrenheit}/{humidity}/{heatIndex}")]
        public IActionResult AddTemeprature([FromRoute]int temperatureCelsius, [FromRoute] int temperatureFahrenheit, [FromRoute] int humidity, [FromRoute] int heatIndex)
        {
            try
            {
                var temprature = new Temperature
                {
                    DateAdded = DateTime.UtcNow,
                    TemperatureCelsius = temperatureCelsius,
                    HeatIndex = heatIndex,
                    Humidity = humidity,
                    TemperatureFahrenheit = temperatureFahrenheit
                };
                return Ok(_temperatureService.Insert(temprature));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
