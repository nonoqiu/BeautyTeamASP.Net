using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Twilio;

namespace BeautyTeamWeb
{
    public class SmsService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var Twilio = new TwilioRestClient(
                Secrets.TwilioAccountSid,
                Secrets.TwilioAuthToken);
            if (message.Destination.Length == 11 && !message.Destination.Contains("+"))
            {
                message.Destination = "+86" + message.Destination;
            }
            var result = Twilio.SendMessage(Secrets.TwilioFromPhone, message.Destination, message.Body);
            Trace.TraceInformation(result.Status);
            await Task.FromResult(0);
        }
    }
}