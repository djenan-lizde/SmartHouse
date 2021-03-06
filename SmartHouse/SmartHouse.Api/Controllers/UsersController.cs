﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Api.Services;
using SmartHouse.Models.Models;
using SmartHouse.Models.Requests;
using System;

namespace SmartHouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registration")]
        [AllowAnonymous]
        public IActionResult UserRegister([FromBody] UserRegistration userRequest)
        {
            try
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
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                var user = _userService.Authenticate(model);

                if (user == null)
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
