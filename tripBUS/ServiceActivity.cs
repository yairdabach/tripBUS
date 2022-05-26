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
using Android.Views.Animations;

namespace tripBUS
{
    [Activity(Label = "@string/app_name")]
    
    public class ServiceActivity : Activity
    {
        Button btnStop;
        Button btnStart;
        Animation animFadeIn, animFadeOut;
        Switch sw;
        bool vol;

        protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                SetContentView(Resource.Layout.service_latyout);

                btnStart = FindViewById<Button>(Resource.Id.btnStart);
                btnStop = FindViewById<Button>(Resource.Id.btnStop);
                btnStart.Click += BtnStart_Click;
                btnStop.Click += BtnStop_Click;

            animFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.anim1);
            animFadeOut = AnimationUtils.LoadAnimation(this, Resource.Animation.a2_animFadeOut);
            sw = FindViewById<Switch>(Resource.Id.sw);
            sw.CheckedChange += OnChekedChanged;
            vol = false;
        }

        private void OnChekedChanged(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked && Sound.mp!= null)
            {
                Sound.mp.SetVolume(0, 0);
                vol = true;
            }   
            else
            {
                Sound.mp.SetVolume(1, 1);
                vol = false;
            }   
        }

        private void BtnStop_Click(object sender, EventArgs e)
            {
                Intent intent = new Intent(this, typeof(Helpers.FirstService));
                StopService(intent);
                btnStart.StartAnimation(animFadeIn);
            btnStop.Visibility = ViewStates.Invisible;
            btnStart.Visibility = ViewStates.Visible;
            btnStop.StartAnimation(animFadeOut);
        }

            private void BtnStart_Click(object sender, EventArgs e)
            {
                Intent intent = new Intent(this, typeof(FirstService));
                StartService(intent);
                btnStart.StartAnimation(animFadeOut);
            btnStart.Visibility = ViewStates.Invisible;
            btnStop.Visibility = ViewStates.Visible;
            btnStop.StartAnimation(animFadeIn);
           
            try
            {
                if (vol)
                {
                    Sound.mp.SetVolume(0, 0);
                }
                else
                {
                    Sound.mp.SetVolume(1, 1);
                }
            }
            catch { }
             
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
