using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "Sign Up")]
    public class ManegerSignUpActivity : Activity
    {
        TextView haveAccountTV;
        Button SignUpBtn;
        Spinner KidometSpinner;
        EditText firstNameET, lastNameET, schoolIdET, emailET, phoneET, passwordET, passworConET;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup_layout);

            haveAccountTV = FindViewById<TextView>(Resource.Id.tv_have_account);
            haveAccountTV.Click += HaveAccountTV_Click;

            SignUpBtn = FindViewById<Button>(Resource.Id.btn_signup);
            SignUpBtn.Click += SignUpBtn_Click;

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

            
            
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            //crate connaction
            try
            {
                if (ValidVaribols())
                {
                    DataHelper.AddTeamMember((new TeamMember(firstNameET.Text, lastNameET.Text, schoolIdET.Text, ((string)KidometSpinner.SelectedItem), phoneET.Text, emailET.Text, passwordET.Text)),this);
                    Intent data = new Intent();
                    data.PutExtra("Click", true);
                    SetResult(Result.Ok, data);
                    Finish();
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
            if (firstNameET.Text == "" || firstNameET.Text == null || !firstNameET.Text.All(Char.IsLetter))
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
            if (lastNameET.Text == "" || lastNameET.Text == null || !lastNameET.Text.All(Char.IsLetter))
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
            if (schoolIdET.Text == "" || schoolIdET.Text == null)
            {
                schoolIdET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                schoolIdET.Error = "must fill it";
                valid = false;
            }
            else
            {
                if (schoolIdET.Text.Length != 6 || schoolIdET.Text.All(char.IsDigit))
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

            // email valid
            if (!CheckDataMail(emailET.Text) || emailET.Text == "")
            {
                emailET.Text = null;
                emailET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                emailET.Error = "not valid email";
                valid = false;
            }
            else
            {
                if (DataHelper.ManegerHasEmail(emailET.Text,this)==1)
                {
                    emailET.Text = null;
                    emailET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                    emailET.Error = "there is email";
                    valid = false;
                }
                else
                {
                    emailET.SetError("", null);
                    emailET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                    emailET.Error = null;
                }
            }

            //valid phone
            if (phoneET.Text.Length != 9 || !phoneET.Text.All(char.IsDigit))
            {
                if (phoneET.Text.Length != 10 && !phoneET.Text.StartsWith("0"))
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
                    phoneET.Text = phoneET.Text.Substring(1);
                }
            }
            else
            {
                phoneET.SetError("", null);
                phoneET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                phoneET.Error = null;
            }

            //Pasword Valid
            if (ValidatePassword(passwordET.Text))
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
            if (validConditions == 1) return false;
            return true;


        }

        //have an account - move to login Activity
        private void HaveAccountTV_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(ManegerLoginActivity)));
            Finish();
        }
    }
}