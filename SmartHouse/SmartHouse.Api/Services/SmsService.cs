using Microsoft.Extensions.Options;
using SmartHouse.Api.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SmartHouse.Api.Services
{
    public interface ISmsService
    {
        MessageResource SendSms();
    }
    public class SmsService : ISmsService
    {
        private readonly IOptions<TwilioConfig> _options;
        public SmsService(IOptions<TwilioConfig> options)
        {
            _options = options;
        }
        public MessageResource SendSms()
        {
            string accountSid = _options.Value.TwilioAccountSid;
            string authToken = _options.Value.TwilioAuthToken;

            TwilioClient.Init(accountSid, authToken);

            var from = new PhoneNumber(_options.Value.PhoneNumberFrom);
            var to = new PhoneNumber(_options.Value.PhoneNumberTo);

            var message = MessageResource.Create(
                body: "Kako je Armando?",
                from: from,
                to: to
            );
            return message;
        }
    }
}
