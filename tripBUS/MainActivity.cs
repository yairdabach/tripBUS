using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using tripBUS.Helpers;

namespace tripBUS
{
    [Activity(Label = "@string/app_name",Icon ="@drawable/fabicon" , Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button BtnManegerLogin;
        TextView TVSignUp;
        Android.Content.ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SavedData.loginMember = null;

            BtnManegerLogin = FindViewById<Button>(Resource.Id.button_login_manager);
            BtnManegerLogin.Click += ManegerLogin_Click;

            TVSignUp = FindViewById<TextView>(Resource.Id.tv_signup);
            TVSignUp.Click += TVSignUp_Click;



            sp = this.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);
            if (sp.GetString("email", null)!=null && sp.GetString("password", null) != null)
            {
                DataHelper.Login(sp.GetString("email", null), sp.GetString("password", null), this);
                Toast.MakeText(this, "You Login Secsesfuly", ToastLength.Long).Show();
            }

        }

        private void TVSignUp_Click(object sender, System.EventArgs e)
        {
            Intent menegerLogin = new Intent(this, typeof(ManegerSignUpActivity));
            StartActivityForResult(menegerLogin,1);
        }

        //manager login screen
        private void ManegerLogin_Click(object sender, System.EventArgs e)
        {
            Intent menegerLogin = new Intent(this,typeof(ManegerLoginActivity));
            StartActivityForResult(menegerLogin,2);
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                Toast.MakeText(this, "You Sign Up Secsesfuly", ToastLength.Long).Show();
            }
            if (requestCode == 2)
            {
                Toast.MakeText(this, "You Login Secsesfuly", ToastLength.Long).Show();
                if(data.GetBooleanExtra("Save", false))
                {
                    // save to shere prefrence
                    Helpers.SavedData.loginMember.SetSp(sp);
                    Toast.MakeText(this, "You Login Secsesfuly And Saved", ToastLength.Long).Show();

                }
            }
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }
    }
}