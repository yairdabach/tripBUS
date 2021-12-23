using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using tripBUS.Helpers;
using tripBUS.Modles;
using Xamarin.Essentials;

namespace tripBUS
{
    [Activity(Label = "Maneger Login")]
    public class ManegerLoginActivity : Activity
    {

        TextView forgetPasswordTvClick;
        EditText passwordET;
        EditText emailET;
        Button btnLogin;
        CheckBox cbRememberMe;
        Dialog ForgetPasswordDilog;
        string confirmCode;
        string emailstr;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_maneger_layout);

            forgetPasswordTvClick = FindViewById<TextView>(Resource.Id.tv_forget_password_click);
            forgetPasswordTvClick.Click += ForgetPassword_Click;

            passwordET = FindViewById<EditText>(Resource.Id.et_password_login);
            emailET= FindViewById<EditText>(Resource.Id.et_email_login);
            cbRememberMe = FindViewById<CheckBox>(Resource.Id.cb_remember_login);

            btnLogin = FindViewById<Button>(Resource.Id.btn_login);
            btnLogin.Click += BtnLogin_Click;
            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            TeamMember teamMember = DataHelper.Login(emailET.Text, passwordET.Text, this);
            if (teamMember != null)
            {
                Intent data = new Intent();
                data.PutExtra("Save", cbRememberMe.Checked);
                SetResult(Result.Ok,data);
                Finish();

            }
            else
            {
                passwordET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                passwordET.Error = "Wrong email or password";

                emailET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                emailET.Error = "must fill it";
            }
        }

        //crate forget Password click
        private void ForgetPassword_Click(object sender, EventArgs e)
        {
            createForgetDialog();
        }

        //crate forget Password dilog
        private void createForgetDialog()
        {
            ForgetPasswordDilog = new Dialog(this);
            ForgetPasswordDilog.SetContentView(Resource.Layout.forget_password_email_layout);
            ForgetPasswordDilog.Show();
            
            Button buttonEmailForget = (Button)ForgetPasswordDilog.FindViewById(Resource.Id.btn_recover_forget);
            buttonEmailForget.Click += ButtonEmailForget_Click;
        }

        private void ButtonEmailForget_Click(object sender, EventArgs e)
        {
            EditText email = ForgetPasswordDilog.FindViewById<EditText>(Resource.Id.et_email_forget);
            emailstr = email.Text;
            if (DataHelper.ManegerHasEmail(email.Text ,this)==1)
            {
                confirmCode = SMSHelper.SendSms("+972522401616");
                try
                {
                    string phone = DataHelper.GetPhoneByEmail(email.Text, this);
                    confirmCode = SMSHelper.SendSms(phone);
                    
                    ForgetPasswordDilog.SetContentView(Resource.Layout.forget_password_confirm_layout);
                    Button btnConfirmPassword = (Button)ForgetPasswordDilog.FindViewById(Resource.Id.btn_confirm_forget);
                    btnConfirmPassword.Click += BtnConfirmPassword_Click;
                }
                catch (Exception ex)
                {
                    //
                    
                }
                

            }
            else
            {
                Toast.MakeText(this,"Theres now email",ToastLength.Long).Show();
            }
        }

        private void BtnConfirmPassword_Click(object sender, EventArgs e)
        {
            if ((ForgetPasswordDilog.FindViewById<EditText>(Resource.Id.et_code_forget)).Text == confirmCode)
            {
                ForgetPasswordDilog.SetContentView(Resource.Layout.forget_password_new_layout);
                Button btnSetPassword = (Button)ForgetPasswordDilog.FindViewById(Resource.Id.btn_new_forget);
                btnSetPassword.Click += BtnSetPassword_Click;
            }
            else
                Toast.MakeText(this, "Wrong code", ToastLength.Long).Show();
        }

        private void BtnSetPassword_Click(object sender, EventArgs e)
        {
            EditText passwordForget = ForgetPasswordDilog.FindViewById<EditText>(Resource.Id.et_password_new_forget);
            EditText passwordForgetConfirm = ForgetPasswordDilog.FindViewById<EditText>(Resource.Id.et_password_CofirmPassword_forget);
            bool valid = true;
            if (!ValidatePassword(passwordForget.Text))
            {
                //At least onelower case letter,
                //At least oneupper case letter,
                //At least onenumber
                //At least 8characters length
                passwordForget.Text = null;
                passwordForget.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                passwordForget.Error = "At least onelower case letter, \n At least oneupper case letter \n At least onenumber \n At least 8characters length";
                valid = false;
                passwordForgetConfirm.Text = "";
            }
            else
            {
                passwordForget.SetError("", null);
                passwordForget.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                passwordForget.Error = null;
                if (passwordForgetConfirm.Text == passwordForget.Text)
                {
                    passwordForgetConfirm.SetError("", null);
                    passwordForgetConfirm.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                    passwordForgetConfirm.Error = null;
                }
                else
                {
                    passwordForgetConfirm.Text = null;
                    passwordForgetConfirm.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                    passwordForgetConfirm.Error = "Password don't match";
                    valid = false;
                    passwordForgetConfirm.Text = "";
                }
            }

            if (valid)
            {
                if(DataHelper.UpadtePhone(passwordForgetConfirm.Text,emailstr ,this))
                {
                    Toast.MakeText(this, "Password Change Secsesfly", ToastLength.Long).Show();
                    ForgetPasswordDilog.Dismiss();

                }
                else
                {
                    Toast.MakeText(this, "Eroore try agine", ToastLength.Long).Show();
                }
                
            }
        }

        private bool ValidatePassword(string passWord)
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
    }
}