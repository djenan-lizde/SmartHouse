using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;

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
            var message = _smsService.SendSms();
            return Content(message.Sid);
        }
    }
}
