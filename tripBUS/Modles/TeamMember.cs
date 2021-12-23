using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tripBUS.Modles
{
    public class TeamMember
    {
        string firstName { get; set; }
        string lastName { get; set; }
        string schoolID { get; set; }
        string kidomet { get; set; }
        string phone { get; set; }
        string email  { get; set; }

        private string password;

        public TeamMember() { }
        public TeamMember(string firstName,string lastName, string schoolID, string kidomet, string phone, string email,string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.schoolID = schoolID;
            this.kidomet = kidomet;
            this.phone = phone;
            this.email = email;
            this.password = password;
        }

        public void SetSp(Android.Content.ISharedPreferences sp)
        {
            var editor = sp.Edit();
            editor.PutString("email", this.email);
            editor.PutString("password", this.password);
            editor.Commit();
        }
        public override string ToString()
        {
            return $"('{this.firstName}','{this.lastName}', '{this.schoolID}', '{this.kidomet}', '{this.phone}', '{this.email}', '{this.password}')";
        }
    }
}