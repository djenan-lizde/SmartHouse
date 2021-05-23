using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;
using System;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpPost("save")]
        public IActionResult InsertConfig([FromBody] Models.Models.Configuration config)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(_configurationService.Insert(config));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("current")]
        public IActionResult GetConfig()
        {
            try
            {
                return Ok(_configurationService.GetLastT());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
