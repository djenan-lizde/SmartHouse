﻿namespace SmartHouse.Models.Requests
{
    public class UserRegistration
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        //home address table
        public int CityId { get; set; }
        public string Street { get; set; }
        public bool Active { get; set; }
    }
}