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
    [Activity(Label = "ViewBusActivity")]
    public class ViewBusActivity : AppCompatActivity
    {
        //activity status | 1-edit 2-view 3-new
        Bus bus;
        int Status;
        int busNum, tripCode, year;
        string schoolId;
        ListView atendesLV;
        EditText BusNameET;
        Button AddAttendeceBTN, ViewStudentBTN;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Group ";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.bus_layout;
            stub.Inflate();

            Status = Intent.GetIntExtra("Status", 0);
            busNum = Intent.GetIntExtra("busNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");

            if (Status != 3)
            {
                bus = DataHelper.GetBusInfo(busNum, tripCode, schoolId, this);
                List<Attendance> temp = DataHelper.GetAllAttendaceForBus(tripCode, busNum, this);
                if(temp != null)
                {
                    bus.attendances = temp;
                }
                atendesLV = FindViewById<ListView>(Resource.Id.lv_atendece_bus);
                
            }
            else
            {
                bus = new Bus(busNum, tripCode, schoolId);
                
            }

            BusNameET = FindViewById<EditText>(Resource.Id.et_name_bus);
            AddAttendeceBTN = FindViewById<Button>(Resource.Id.button_NewAttendance_bus);
            ViewStudentBTN = FindViewById<Button>(Resource.Id.button_student_bus);
            

            BusNameET.Text = bus.BusName;

            if (Status == 2)
            {
                BusNameET.Enabled = false;
                ViewStudentBTN.Click += ViewStudentBTN_Click;
                atendesLV.Adapter = new AttendanceAdapter(this, DataHelper.GetAllAttendaceForBus(tripCode, busNum, this), false);
            }
            else
            {
                AddAttendeceBTN.SetBackgroundColor( Android.Graphics.Color.Gray);
                ViewStudentBTN.SetBackgroundColor(Android.Graphics.Color.Gray);
                if (Status == 1)
                {
                    ViewStudentBTN.Click += ViewStudentBTN_Click;
                    atendesLV.Adapter = new AttendanceAdapter(this, DataHelper.GetAllAttendaceForBus(tripCode, busNum, this), true);
                }
            }

        }

        public void OpenAttendece(string time)
        {
            Intent intent = new Intent(this, typeof(AttendanceActivity));
            intent.PutExtra("tripCode", tripCode);
            intent.PutExtra("busNum", 1);
            intent.PutExtra("dateTime", time);
            intent.PutExtra("year", year);
            intent.PutExtra("SchoolId", schoolId);
            StartActivity(intent);
        }

        private void ViewStudentBTN_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewStudentListActivity));
            intent.PutExtra("tripCode", tripCode);
            intent.PutExtra("busNum", 1);
            intent.PutExtra("Status", 2);
            intent.PutExtra("year", year);
            intent.PutExtra("SchoolId", Helpers.SavedData.loginMember.schoolID);
            StartActivity(intent);
        }
    }
}