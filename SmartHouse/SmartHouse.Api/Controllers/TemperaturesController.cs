﻿using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;

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

        //[HttpGet("Filter")]
        //public IActionResult GetTemperaturesCondition(TemperatureSearchRequest temperatureSearchRequest)
        //{
        //    var query = _service.GetByCondition(x => x.DateAdded.Day == temperatureSearchRequest.Day
        //        && x.DateAdded.Month == temperatureSearchRequest.Month && x.DateAdded.Year == temperatureSearchRequest.Year);
        //    return Ok(query);
        //}

        [HttpGet("currenttemperature")]
        public IActionResult GetLastTemperature()
        {
            try
            {
                var lastTemperature = _temperatureService.GetLastT();
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
                return Ok(_temperatureService.Insert(temperature));
            }
            catch (System.Exception)
            {
                return StatusCode(400);
            }
        }
    }
}
