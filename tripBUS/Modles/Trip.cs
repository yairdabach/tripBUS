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
    public class Trip
    {
        public int tripCode { get; set; }
        public string tripName { get; set; }
        public string tripDescription { get; set; }
        public string classAge { get; set; }
        public string place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string maengerEmail { get; set; }

        public Trip() { }
        public Trip(string tripName, string ManegerEmail, string tripDescription, string place,string classAge, DateTime StartDate, DateTime endDate)
        {
            this.tripName = tripName;
            this.maengerEmail = ManegerEmail;
            this.classAge = classAge;
            this.tripDescription = tripDescription;
            this.place = place;
            this.StartDate = StartDate;
            this.EndDate = endDate;
            

        }

        public Trip(int tripCode, string ManegerEmail, string tripName, string tripDescription,string place, string classAge, DateTime StartDate, DateTime endDate)
        {
            this.tripCode = tripCode;
            this.maengerEmail= ManegerEmail;
            this.tripName = tripName;
            this.tripDescription = tripDescription;
            this.place = place;
            this.classAge= classAge;
            this.StartDate = StartDate;
            this.EndDate = endDate;
        }

        public override string ToString()
        {
            if(tripCode == 0)
            {
                return $"('{this.maengerEmail}','{this.tripName}', '{this.tripDescription}','{this.place}','{this.classAge}', '{this.StartDate.Day}', '{this.StartDate.Month}', '{this.StartDate.Year}', '{this.EndDate.Day}', '{this.EndDate.Month}', '{this.EndDate.Year}')";
            }
            return $"('{this.tripCode}','{this.maengerEmail}','{this.tripName}', '{this.tripDescription}','{this.place}','{this.classAge}', '{this.StartDate.Day}', '{this.StartDate.Month}', '{this.StartDate.Year}', '{this.EndDate.Day}', '{this.EndDate.Month}', '{this.EndDate.Year}')";
        }
    }
}