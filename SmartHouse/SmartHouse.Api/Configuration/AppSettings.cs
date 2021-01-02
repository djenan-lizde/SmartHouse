namespace SmartHouse.Api.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int TokenDuration { get; set; }
    }
}
