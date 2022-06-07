using System;
using System.Linq;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.AppBar;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "Activity1")]
    public class UpdateDitlesActivity : AppCompatActivity
    {
        Button UpdateBtn;
        Spinner KidometSpinner;
        EditText firstNameET, lastNameET, schoolIdET, emailET, phoneET, passwordET, passworConET;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Update Account";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.signup_layout;
            stub.Inflate();
            (FindViewById<TextView>(Resource.Id.singupTV)).Text = "Update";

            // Create your application here

            (FindViewById<TextView>(Resource.Id.tv_have_account)).Visibility = ViewStates.Invisible;

            UpdateBtn = FindViewById<Button>(Resource.Id.btn_signup);
            UpdateBtn.Click += UpdateBtn_Click; ;
            UpdateBtn.Text = "Update";

            KidometSpinner = FindViewById<Spinner>(Resource.Id.spnr_Kidomet_signup);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.phone_kidomet, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            KidometSpinner.Adapter = adapter;

            firstNameET = FindViewById<EditText>(Resource.Id.et_first_signup);
            lastNameET = FindViewById<EditText>(Resource.Id.et_last_signup);
            schoolIdET = FindViewById<EditText>(Resource.Id.et_school_signup);
            emailET = FindViewById<EditText>(Resource.Id.et_email_signup);
            phoneET = FindViewById<EditText>(Resource.Id.et_phone_signup);
            passwordET = FindViewById<EditText>(Resource.Id.et_password_signup);
            passworConET = FindViewById<EditText>(Resource.Id.et_passwordcon_signup);

            firstNameET.Text = Helpers.SavedData.loginMember.firstName;
            lastNameET.Text = Helpers.SavedData.loginMember.lastName;
            schoolIdET.Text = Helpers.SavedData.loginMember.schoolID;
            emailET.Text = Helpers.SavedData.loginMember.email;
            emailET.Enabled= false;
            phoneET.Text = Helpers.SavedData.loginMember.phone;
            passwordET.Text = "00000000";
            passworConET.Text = "00000000";


            for (int i = 0; i < adapter.Count; i++)
            {
                if (((string)(adapter.GetItem(i))) == Helpers.SavedData.loginMember.kidomet)
                {
                    KidometSpinner.SetSelection(i);
                }
            }

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidVaribols())
                {
                    TeamMember teamMemberTemp = new TeamMember();
                    Helpers.SavedData.loginMember.CopyTeamMember(teamMemberTemp);
                    teamMemberTemp.firstName = firstNameET.Text;
                    teamMemberTemp.lastName = lastNameET.Text;
                    teamMemberTemp.phone = phoneET.Text;
                    teamMemberTemp.schoolID = schoolIdET.Text;
                    teamMemberTemp.email = emailET.Text;
                    teamMemberTemp.kidomet = ((string)KidometSpinner.SelectedItem);
                    if (passwordET.Text != "00000000")
                    {
                        teamMemberTemp.setPassword(passwordET.Text);
                    }
                    if(DataHelper.UpdateTeamMember(teamMemberTemp,this))
                    {
                        Helpers.SavedData.loginMember= teamMemberTemp;
                        FinishActivity(0);
                        Finish();
                    }
                    


                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "er try agin", ToastLength.Long).Show();
            }
        }

        private bool ValidVaribols()
        {
            bool valid = true;


            //check valid Fname
            if (firstNameET.Text == "" || firstNameET.Text == null )
            {
                firstNameET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                firstNameET.Error = "must fill it";
                valid = false;
            }
            else
            {
                firstNameET.SetError("", null);
                firstNameET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                firstNameET.Error = null;
            }

            //check valid last name
            if (lastNameET.Text == "" || lastNameET.Text == null)
            {
                lastNameET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                lastNameET.Error = "must fill it";
                valid = false;
            }
            else
            {
                lastNameET.SetError("", null);
                lastNameET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                lastNameET.Error = null;
            }

            //check valid school id
            if (schoolIdET.Text == "" || lastNameET.Text == null)
            {
                schoolIdET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                schoolIdET.Error = "must fill it";
                valid = false;
            }
            else
            {
                if (schoolIdET.Text.Length != 6 || lastNameET.Text.All(char.IsDigit))
                {
                    schoolIdET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                    schoolIdET.Error = "Not valid school id";
                    valid = false;
                }
                else
                {
                    schoolIdET.SetError("", null);
                    schoolIdET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                    schoolIdET.Error = null;
                }
            }

            //valid phone
            if (phoneET.Text.Length != 9 || !phoneET.Text.All(char.IsDigit))
            {
                phoneET.Text = null;
                phoneET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                phoneET.Error = "not valid phone";
                valid = false;
            }
            else
            {
                phoneET.SetError("", null);
                phoneET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                phoneET.Error = null;
            }

            //Pasword Valid
            if (passwordET.Text != "00000000")
            {
                if (!ValidatePassword(passwordET.Text))
                {
                    //At least onelower case letter,
                    //At least oneupper case letter,
                    //At least onenumber
                    //At least 8characters length
                    passwordET.Text = null;
                    passwordET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                    passwordET.Error = "At least onelower case letter, \n At least oneupper case letter \n At least onenumber \n At least 8characters length";
                    valid = false;
                    passworConET.Text = "";
                }
                else
                {
                    passwordET.SetError("", null);
                    passwordET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                    passwordET.Error = null;
                    if (passworConET.Text == passwordET.Text)
                    {
                        passworConET.SetError("", null);
                        passworConET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                        passworConET.Error = null;
                    }
                    else
                    {
                        passworConET.Text = null;
                        passworConET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                        passworConET.Error = "Password don't match";
                        valid = false;
                        passworConET.Text = "";
                    }
                }
            }
            
            return valid;
        }


        private bool CheckDataMail(string text)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(text);
            if (match.Success)
                return true;
            else
                return false;
        }

        private static bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions != 3) return false;
            return true;


        }
        
    }
}