using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "Activity1")]
    public class AttendanceActivity : AppCompatActivity
    {
        private int busNum;
        private int tripCode, year;
        string SchoolID;
        DateTime dateTime;
        EditText dateCheeckET, titleCheekET;
        ListView studentAttendeceLV;
        List<StudentAttendance> students;
        Attendance attendance;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Attendance";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.attendance_layout;
            stub.Inflate();

            busNum = Intent.GetIntExtra("busNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            string date = Intent.GetStringExtra("dateTime");
            year = Intent.GetIntExtra("year", 0);
            SchoolID = Intent.GetStringExtra("SchoolId");

            if (date !="" || date!=null)
            {
                dateTime = DateTime.Parse(date);
                attendance = DataHelper.GetAttendace(tripCode, busNum, dateTime, year, SchoolID, this);
            }
            else
            {
                dateTime = DateTime.Now;
                attendance = new Attendance(tripCode, busNum, dateTime);
            }

            studentAttendeceLV = FindViewById<ListView>(Resource.Id.lv_atendece_atendece);
            dateCheeckET = FindViewById<EditText>(Resource.Id.et_date_attendece);
            titleCheekET = FindViewById<EditText>(Resource.Id.et_title_attendece);

            studentAttendeceLV.Adapter = new StudentAdapter(this,tripCode,year,SchoolID, attendance.Attendances.Cast<Student>().ToList(), true, 3, attendance.Attendances);

            dateCheeckET.Text = dateTime.ToString("g");
            titleCheekET.Text = attendance.descriotionCheek;


        }
    }
}