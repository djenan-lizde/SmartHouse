using System.ComponentModel.DataAnnotations;

namespace SmartHouse.Models.Requests
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
