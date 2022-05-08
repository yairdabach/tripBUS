using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
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
        FloatingActionButton BusFab;
        List<Attendance> Del = new List<Attendance>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.bus_layout;
            stub.Inflate();

            Status = Intent.GetIntExtra("Status", 0);
            busNum = Intent.GetIntExtra("busNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");


            SupportActionBar.Title = "Bus num:" +busNum;

            if(Status == 4)
            {
                FindViewById<FloatingActionButton>(Resource.Id.bus_fav).Visibility = ViewStates.Gone;
                Status = 2;
            }

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
            BusFab = FindViewById<FloatingActionButton>(Resource.Id.bus_fav);

            BusNameET.Text = bus.BusName;

            if (Status == 2)
            {
                BusNameET.Enabled = false;
                ViewStudentBTN.Click += ViewStudentBTN_Click;
                atendesLV.Adapter = new AttendanceAdapter(this, DataHelper.GetAllAttendaceForBus(tripCode, busNum, this), false);
                BusFab.SetImageResource(Android.Resource.Drawable.IcMenuEdit);
                BusFab.Click += BusFabEdit_Click;
                AddAttendeceBTN.Click += AddAttendeceBTN_Click; 
            }
            else
            {
                AddAttendeceBTN.SetBackgroundColor( Android.Graphics.Color.Gray);
                ViewStudentBTN.SetBackgroundColor(Android.Graphics.Color.Gray);
                if (Status == 1)
                {
                    atendesLV.Adapter = new AttendanceAdapter(this, DataHelper.GetAllAttendaceForBus(tripCode, busNum, this), true);
                    BusFab.Click += BusFabUpdate_Click;
                }
                else
                {
                    BusFab.Click += BusFabAdd_Click;
                }
                BusFab.SetImageResource(Android.Resource.Drawable.IcMenuSave);
            }

        }

        public void delateAttendace(Attendance attendance)
        {
           
            var list = new List<Attendance>();
            foreach (var item in (atendesLV.Adapter as AttendanceAdapter).objects)
            {
                list.Add(item);
            }
            list.Remove(attendance);
            atendesLV.Adapter = new AttendanceAdapter(this, list, true);
            Del.Add(attendance);
        }

        private void AddAttendeceBTN_Click(object sender, EventArgs e)
        {
            OpenAttendece("");
        }

        private void BusFabAdd_Click(object sender, EventArgs e)
        {
            bus.BusName = BusNameET.Text;
            DataHelper.AddNewBus(bus, this);

            Intent intent = new Intent(this, typeof(ViewBusActivity));
            Bundle b = new Bundle();
            b.PutInt("busNum", bus.busNum);
            b.PutInt("Status", 2);
            b.PutInt("tripCode", tripCode);
            b.PutInt("year", year);
            b.PutString("SchoolId", bus.schoolId);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }

        private void BusFabUpdate_Click(object sender, EventArgs e)
        {
            bus.BusName = BusNameET.Text;
            DataHelper.UpdateBusInfo(bus, this);
            foreach (var item in Del)
            {
                DataHelper.DeleatAttendace(item, this);
            }
            Finish();
        }

        private void BusFabEdit_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewBusActivity));
            Bundle b = new Bundle();
            b.PutInt("busNum", busNum);
            b.PutInt("Status", 1);
            b.PutInt("tripCode", tripCode);
            b.PutInt("year", year);
            b.PutString("SchoolId", schoolId);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }

        public void OpenAttendece(string time)
        {
            Intent intent = new Intent(this, typeof(AttendanceActivity));
            intent.PutExtra("tripCode", tripCode);
            intent.PutExtra("busNum", bus.busNum);
            intent.PutExtra("dateTime", time);
            intent.PutExtra("year", year);
            intent.PutExtra("SchoolId", schoolId);
            StartActivityForResult(intent, 0);
        }

        private void ViewStudentBTN_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewStudentListActivity));
            intent.PutExtra("tripCode", tripCode);
            intent.PutExtra("busNum",busNum);
            intent.PutExtra("Status", 2);
            intent.PutExtra("year", year);
            intent.PutExtra("SchoolId", Helpers.SavedData.loginMember.schoolID);
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            bus = DataHelper.GetBusInfo(busNum, tripCode, schoolId, this);
            BusNameET.Text = bus.BusName;

            if (Status == 2)
            {
                BusNameET.Enabled = false;
                ViewStudentBTN.Click += ViewStudentBTN_Click;
                atendesLV.Adapter = new AttendanceAdapter(this, DataHelper.GetAllAttendaceForBus(tripCode, busNum, this), false);
                BusFab.SetImageResource(Android.Resource.Drawable.IcMenuEdit);
                BusFab.Click += BusFabEdit_Click;
                ViewStudentBTN.Click += ViewStudentBTN_Click;
            }
        }
    }
}