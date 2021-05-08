using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using System;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;
        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet("sendsms")]
        [AllowAnonymous]
        public IActionResult SendSms()
        {
            try
            {
                var message = _smsService.SendSms();
                return Ok(Content(message.Sid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
