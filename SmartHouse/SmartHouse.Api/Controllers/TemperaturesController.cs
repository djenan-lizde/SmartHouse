using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models;
using SmartHouse.Models.Requests;

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
            return Ok(_service.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetLastTemperature(int Id)
        {
            try
            {
                var lastTemperature = _service.Get(Id);
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

        [HttpGet("Filter")]
        public IActionResult GetTemperaturesByFilter(TemperatureSearchRequest searchRequest)
        {
            var query = _service.Get();

            if (searchRequest.DateFrom.HasValue)
            {
                query = query.Where(x => x.DateAdded == searchRequest.DateFrom);
            }
            if (searchRequest.DateTo.HasValue)
            {
                query = query.Where(x => x.DateAdded == searchRequest.DateTo);
            }
            return Ok(query);
        }
    }
}
