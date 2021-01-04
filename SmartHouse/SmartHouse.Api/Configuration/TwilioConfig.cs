namespace SmartHouse.Api.Configuration
{
    public class TwilioConfig
    {
        public string TwilioAccountSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string PhoneNumberTo { get; set; }
        public string PhoneNumberFrom { get; set; }
    }
}
