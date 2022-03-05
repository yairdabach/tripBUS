using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Telephony.Gsm;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/fabicon", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button BtnManegerLogin;
        Button BtnTeamMember;
        TextView TVSignUp;
        Android.Content.ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            BtnManegerLogin = FindViewById<Button>(Resource.Id.button_login_manager);
            BtnManegerLogin.Click += ManegerLogin_Click;

            TVSignUp = FindViewById<TextView>(Resource.Id.tv_signup);
            TVSignUp.Click += TVSignUp_Click;

            BtnTeamMember = FindViewById<Button>(Resource.Id.button_login_team);
            BtnTeamMember.Click += BtnTeamMember_Click;

            sp = this.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);
            SavedData.SetSharedPreferencesRefrence(sp);

            if (sp.GetString("email", null) != null && sp.GetString("password", null) != null)
            {
                DataHelper.Login(sp.GetString("email", null), sp.GetString("password", null), this);
            }

            if (SavedData.loginMember != null)
            {
                Intent menegerLogin = new Intent(this, typeof(MangerMainActivity));
                StartActivity(menegerLogin);
                Finish();
            }
        }

        private void BtnTeamMember_Click(object sender, EventArgs e)
        {
            //Intent menegerLogin = new Intent(this, typeof(MangerMainActivity));
            //StartActivity(menegerLogin);
        }

        private void TVSignUp_Click(object sender, System.EventArgs e)
        {
            Intent menegerLogin = new Intent(this, typeof(ManegerSignUpActivity));
            StartActivityForResult(menegerLogin, 1);
        }

        //manager login screen
        private void ManegerLogin_Click(object sender, System.EventArgs e)
        {
            Intent menegerLogin = new Intent(this, typeof(ManegerLoginActivity));
            StartActivityForResult(menegerLogin, 2);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                try
                {
                    if (data.GetBooleanExtra("Click", false))
                    {
                        // save to shere prefrence
                        Helpers.SavedData.loginMember.SetSp(sp);
                        Toast.MakeText(this, "Signup", ToastLength.Long).Show();

                    }
                    else
                        Toast.MakeText(this, "You signup Secsesfuly", ToastLength.Long).Show();
                }
                catch  { }

            }
            if (requestCode == 2)
            {

                try
                {
                    if (data.GetBooleanExtra("Save", false))
                    {
                        // save to shere prefrence
                        Helpers.SavedData.loginMember.SetSp(sp);
                        Intent menegerLogin = new Intent(this, typeof(MangerMainActivity));
                        StartActivity(menegerLogin);
                        Finish();
                    }
                    else
                    {
                        Intent menegerLogin = new Intent(this, typeof(MangerMainActivity));
                        StartActivity(menegerLogin);
                        Finish();
                    }

                }
                catch (Exception ex) { }

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}