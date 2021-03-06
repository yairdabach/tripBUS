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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string schoolID { get; set; }
        public string kidomet { get; set; }
        public string phone { get; set; }
        public string email  { get; set; }

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
        public TeamMember(string firstName, string lastName, string schoolID, string kidomet, string phone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.schoolID = schoolID;
            this.kidomet = kidomet;
            this.phone = phone;
            this.email = email;
        }
        public void CopyTeamMember(TeamMember m)
        {
            m.firstName = firstName;
            m.lastName = lastName;
            m.schoolID = schoolID;
            m.kidomet = kidomet;
            m.phone = phone;
            m.email = email;
            m.password = password;
        }

        public void setPassword(string pass)
        {
            password= pass;
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

        public string ToStringUpdate()
        {
            return $"FirstName='{this.firstName}',LastName='{this.lastName}', SchoolId = '{this.schoolID}', Kidomet='{this.kidomet}', Phone='{this.phone}', Password = '{this.password}'";
        }
    }
}