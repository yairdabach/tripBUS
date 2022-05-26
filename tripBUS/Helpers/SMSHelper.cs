
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
            var accountSid = "AC66c98ec25c85b0ab9355d2918c166b22";
            var authToken = "7cc8df4fb539ee1b1d341d51d802867f";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber("+972522401616"));
            messageOptions.MessagingServiceSid = "MGf33a46c1c26224795718761507707b1c";
            string verifactioncode = (((new Random()).Next(111111, 9999999)).ToString());
            messageOptions.Body = "Your vrifaction code for TripBus is :" + verifactioncode;

            var message = MessageResource.Create(messageOptions);

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
