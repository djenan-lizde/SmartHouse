namespace SmartHouse.Models.Responses
{
    public class UserAuthenticationResult
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
