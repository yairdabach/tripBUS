
using AspNetCore.Http.Extensions;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using Twilio;
using Twilio.TwiML;

using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Android.Telephony;

namespace tripBUS.Helpers
{
    public static class SMSHelper
    {
        
        public static string SendSms(string phone)
        {
            var accountSid = "AC3c897d2fbc0ef250c402e005366f3fa5";
            var authToken = "8bc1df40cff2d1da70bae4cddbf2a368";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(phone));
            messageOptions.MessagingServiceSid = "MG12eac243899b87c0fe01b89b356329a4";

            string verifactioncode = (((new Random()).Next(111111, 9999999)).ToString());
            messageOptions.Body = "Your vrifaction code for TripBus is :" + verifactioncode;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            return verifactioncode;

        }

        public static void Send()
        {
            SmsManager.Default.SendTextMessage("972522580810", null, "תתעלמי", null, null);
        }
        public static async Task Write(string text)
        {
            await File.WriteAllTextAsync("WriteText.txt", text);
        }
        
    }
}
