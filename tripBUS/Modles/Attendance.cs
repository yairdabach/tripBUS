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
    public class Attendance
    {

        public int tripCode { get; set; }
        public string schoolID { get; set; }
        public int busNum { get; set; }
        public DateTime DateTime { get; set; }
        public string descriotionCheek { get; set; }

        public List<StudentAttendance> Attendances { get; set; } = new List<StudentAttendance>();
        public Attendance() { }

        public Attendance(int tripcode,string schoolID, int busNum, DateTime dateTime, string descriotionCheek)
        { 
            this.tripCode = tripcode;
            this.schoolID = schoolID;
            this.DateTime = dateTime;
            this.busNum = busNum;
            this.descriotionCheek = descriotionCheek;
        }

        public Attendance(int tripcode, int busNum, DateTime dateTime, string descriotionCheek)
        {
            this.tripCode = tripcode;
            this.DateTime = dateTime;
            this.busNum = busNum;
            this.descriotionCheek = descriotionCheek;
        }

        public Attendance(int tripcode, int busNum, DateTime dateTime)
        {
            this.tripCode = tripcode;
            this.DateTime = dateTime;
            this.busNum = busNum;
        }
    }
}