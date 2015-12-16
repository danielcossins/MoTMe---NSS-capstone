using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace MoTMe.Controllers
{
    public class SMS
    {
        private string AccountSid = "AC85f12f79e35f3ea3f1fb985df2ee99a9";
        private string AuthToken = "ef3e7e352e393b144f8b74a2ed6c24af";
        TwilioRestClient twilio;

        public SMS()
        {
            twilio = new TwilioRestClient(AccountSid, AuthToken);
        }

        public void SendSMS(string number, string content)
        {
            var message = twilio.SendMessage("+18017841260", number, content, "");
        }
    }
}