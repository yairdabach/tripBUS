using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using tripBUS.Helpers;
namespace tripBUS
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    
    public class ServiceActivity : Activity
    {
        Button btnStop;
        Button btnStart;
        protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                SetContentView(Resource.Layout.service_latyout);

                btnStart = FindViewById<Button>(Resource.Id.btnStart);
                btnStop = FindViewById<Button>(Resource.Id.btnStop);
                btnStart.Click += BtnStart_Click;
                btnStop.Click += BtnStop_Click;
            }
            private void BtnStop_Click(object sender, EventArgs e)
            {
                Intent intent = new Intent(this, typeof(Helpers.FirstService));
                StopService(intent);

            }

            private void BtnStart_Click(object sender, EventArgs e)
            {
                Intent intent = new Intent(this, typeof(FirstService));
                StartService(intent);

            }


            private void FabOnClick(object sender, EventArgs eventArgs)
            {
                View view = (View)sender;
                Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            }

            public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
            {
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
    }
}
