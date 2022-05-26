using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tripBUS.Helpers
{
    [Service(Label = "FirstService")]//write service to menifest file 
    [IntentFilter(new String[] { "com.yourname.FirstService" })]
    public class FirstService : Service
    {
        IBinder binder;//null not in bagrut 
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            // start your service logic here

            Sound.mp = MediaPlayer.Create(this, Resource.Raw.bgmusic);
            Sound.mp.Start();
            // Return the correct StartCommandResult for the type of service you are building
            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            binder = new FirstServiceBinder(this);
            return binder;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (Sound.mp != null)
            {
                Sound.mp.Stop();
                Sound.mp.Release();
                Sound.mp = null;
            }

        }
    }


    public class FirstServiceBinder : Binder
    {
        readonly FirstService service;

        public FirstServiceBinder(FirstService service)
        {
            this.service = service;
        }

        public FirstService GetFirstService()
        {
            return service;
        }
    }

    public static class Sound
    {
        public static MediaPlayer mp;
    }
}