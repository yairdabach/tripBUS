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
        int busNum, tripCode;
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
            schoolId = Intent.GetStringExtra("SchoolId");

            if (Status != 3)
            {
                bus = DataHelper.GetBusInfo(busNum, tripCode, schoolId, this);
                bus.attendances = DataHelper.GetAllAtendece(busNum, tripCode, schoolId, this);
            }
            else
            {
                bus = new Bus(busNum, tripCode, schoolId);
            }

            BusNameET = FindViewById<EditText>(Resource.Id.et_name_bus);
            AddAttendeceBTN = FindViewById<Button>(Resource.Id.button_NewAttendance_bus);
            ViewStudentBTN = FindViewById<Button>(Resource.Id.button_student_bus);

            if (Status == 2)
            {
                BusNameET.Enabled = false;

            }
            else
            {
                AddAttendeceBTN.SetBackgroundColor( Android.Graphics.Color.Gray);
                ViewStudentBTN.SetBackgroundColor(Android.Graphics.Color.Gray);
            }

        }
    }
}