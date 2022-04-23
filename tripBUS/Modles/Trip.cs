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

        // נרמול לא תקין למצוא תיאור
        public int countBus{ get; set;}
        public int countStudent { get; set; }
        public int countGroup { get; set; }

        public Trip() { }
        public Trip(string tripName, string ManegerEmail, string tripDescription, string place,string classAge, DateTime StartDate, DateTime endDate)
        {
            tripCode = 0;
            this.tripName = tripName;
            this.maengerEmail = ManegerEmail;
            this.classAge = classAge;
            this.tripDescription = tripDescription;
            this.place = place;
            this.StartDate = StartDate;
            this.EndDate = endDate;
        }

        public Trip(int tripCode, string ManegerEmail, string tripName, string tripDescription,string place, string classAge, DateTime StartDate, DateTime endDate, int studentCount, int groupCount, int busCount)
        {
            this.tripCode = tripCode;
            this.maengerEmail= ManegerEmail;
            this.tripName = tripName;
            this.tripDescription = tripDescription;
            this.place = place;
            this.classAge= classAge;
            this.StartDate = StartDate;
            this.EndDate = endDate;
            this.countGroup = groupCount;
            this.countBus = busCount;
            this.countStudent = studentCount;
        }

        public string ToStringUpdate()
        {
            //(TripName, TripDescription, ManegerEmail, Place, ClassAge, TripStartDateDay, TripStartDateMonth, TripStartDateYear, TripEndDateDay, TripEndDateMonth, TripEndDateYear)
            return $"TripName='{this.tripName}',TripDescription='{this.tripDescription}', Place = '{this.place}', ClassAge='{this.classAge}', TripStartDateDay='{this.StartDate.Day}', TripStartDateMonth='{this.StartDate.Day}',TripStartDateYear='{this.StartDate.Day}',TripEndDateDay='{this.StartDate.Day}', TripEndDateMonth='{this.StartDate.Day}',TripEndDateYear='{this.StartDate.Day}'";
        }
        public override string ToString()
        {
            if(tripCode == 0)
            {
                return $"('{this.tripName}', '{this.tripDescription}','{this.maengerEmail}','{this.place}','{this.classAge}', '{this.StartDate.Day}', '{this.StartDate.Month}', '{this.StartDate.Year}', '{this.EndDate.Day}', '{this.EndDate.Month}', '{this.EndDate.Year}')";
            }
            return $"('{this.tripCode}','{this.maengerEmail}','{this.tripName}', '{this.tripDescription}','{this.place}','{this.classAge}', '{this.StartDate.Day}', '{this.StartDate.Month}', '{this.StartDate.Year}', '{this.EndDate.Day}', '{this.EndDate.Month}', '{this.EndDate.Year}')";
        }
    }
}