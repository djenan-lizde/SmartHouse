using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;
using SmartHouse.Models.Requests;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHomeAddressService _homeAddressService;

        public UsersController(IUserService userService,
            IHomeAddressService homeAddressService)
        {
            _userService = userService;
            _homeAddressService = homeAddressService;
        }

        [HttpPost("registration")]
        [AllowAnonymous]
        public User UserRegister(UserRegistration userRequest)
        {
            var obj = _userService.RegisterUser(userRequest);
            User user = new User
            {
                Id = obj.Id,
                Username = obj.Username,
                Email = obj.Email,
                LastName = obj.LastName,
                FirstName = obj.FirstName,
                JoinDate = obj.JoinDate,
                PhoneNumber = obj.PhoneNumber
            };
            var homeAddress = new HomeAddress
            {
                Active = true,
                CityId = userRequest.CityId,
                Street = userRequest.Street,
                UserId = user.Id
            };
            _homeAddressService.Insert(homeAddress);
            return user;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserLoginModel model)
        {
            var user = _userService.Authenticate(model);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(user);
        }
    }
}
