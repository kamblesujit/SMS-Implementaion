using Microsoft.AspNetCore.Mvc;

using Twilio;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;

using Twilio.Types;



namespace SMS_Implementation.Controllers
{
   
    public class SMSController : TwilioController
    {
        private readonly IConfiguration _configuration;

        public SMSController( IConfiguration configuration)
        {
            
            _configuration = configuration;
            
        }

        public ActionResult SendSMS()
        {
            var accountSid = _configuration["ApiSettings:TwilioAccountSid"];
            var authToken = _configuration["ApiSettings:TwilioAuthToken"];

            
            TwilioClient.Init(accountSid, authToken);

            
            var to = new PhoneNumber(_configuration["ApiSettings:MyPhoneNumber"]);
            var from = new PhoneNumber("[Your Generated Number here]");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "This SMS is send from SMS_Implementation Asp.net Core Web application developed by Sujit S Kamble");
            return Content(message.Sid);

            
        }
        public ActionResult ReceiveSMS()
        {
            var response = new MessagingResponse();
            response.Message("This is Default SMS! Thanks for connecting");
            return TwiML(response);
        }
    }
}
