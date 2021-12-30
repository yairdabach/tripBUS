using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tripBUS.Helpers
{
    [BroadcastReceiver(Enabled = true, Exported = true, Permission = "android.permission.RECEIVE_SMS")]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class SmsReceiver : Android.Content.BroadcastReceiver
    {
        public SmsReceiver() {}
        public override void OnReceive(Context context, Intent intent)
        {
       
            if (intent.Action.Equals("android.provider.Telephony.SMS_RECEIVED"))
            {

                Toast.MakeText(context, "u got sms", ToastLength.Long).Show();
                startBroadCast(intent, context);
                
            }
        }


        private void startBroadCast(Intent intent, Context context)
        {
            SmsMessage[] messages = null;

            String strMessage = "";



            if (intent.HasExtra("pdus"))

            {

                var pdus = (Java.Lang.Object[])intent.Extras.Get("pdus");
                messages = new SmsMessage[pdus.Length];

                String number = "";

                for (int i = 0; i < messages.Length; i++)
                {

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                    {
                        String format = intent.GetStringExtra("format");
                        messages[i] = SmsMessage.CreateFromPdu((byte[])pdus[i], format);
                    }
                    else
                    {
                        messages[i] = SmsMessage.CreateFromPdu((byte[])pdus[i]);
                    }

                    number = messages[i].OriginatingAddress;
                    strMessage += "SMS From: " + messages[i].OriginatingAddress;
                    strMessage += " : ";
                    strMessage += messages[i].MessageBody;

                }


                if (strMessage.Length > 0)
                {
                    Console.WriteLine(strMessage);
                }//end of if



            }
        }
    }
}