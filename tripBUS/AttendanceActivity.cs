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
        string date;
        DateTime dateTime;
        EditText dateCheeckET, titleCheekET;
        Button saveBtn;
        ListView studentAttendeceLV;
        List<StudentAttendance> students;
        List<StudentAttendance> change, Add;
        Attendance attendance;
        Trip trip;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            //set layout and desing 
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Attendance";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.attendance_layout;
            stub.Inflate();

            //get info from prev activity
            busNum = Intent.GetIntExtra("busNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            date = Intent.GetStringExtra("dateTime");
            year = Intent.GetIntExtra("year", 0);
            SchoolID = Intent.GetStringExtra("SchoolId");

            // get trip
            trip = DataHelper.GetTripByCode(tripCode, this);

            // if date is empty -> new Attendace else get the attendace from db
            if (!string.IsNullOrEmpty(date))
            {
                dateTime = DateTime.Parse(date);
                attendance = DataHelper.GetAttendace(tripCode, busNum, dateTime, year, SchoolID, this);
                if (attendance.Attendances == null)
                {
                    attendance.Attendances = new List<StudentAttendance>();
                }
            }
            else
            {
                dateTime = DateTime.Now;
                attendance = new Attendance(tripCode,SchoolID, busNum, dateTime);
                attendance.Attendances = new List<StudentAttendance>();
            }

            // set list and adapters fot attendace
            students = new List<StudentAttendance>();
            Add = new List<StudentAttendance>();
            change = new List<StudentAttendance>();
            List<Student> tempStudent = DataHelper.GetStudentByBus(busNum,tripCode,year, SchoolID, this);
            if ( tempStudent.Count ==0)
            {
                Finish();
            }
            for (int i = 0; i < tempStudent.Count; i++)
            {
                bool inAttenace = false;
                for (int j = 0; j < attendance.Attendances.Count; j++)
                {
                    if (tempStudent[i].Id == attendance.Attendances[j].Id)
                    {
                        inAttenace = true;
                        students.Add(attendance.Attendances[j]);
                    }
                }
                if (!inAttenace)
                {
                    var s = new StudentAttendance(tempStudent[i], false);
                    students.Add(s);
                    Add.Add(s);
                }
            }

            // find views
            studentAttendeceLV = FindViewById<ListView>(Resource.Id.lv_atendece_atendece);
            dateCheeckET = FindViewById<EditText>(Resource.Id.et_date_attendece);
            titleCheekET = FindViewById<EditText>(Resource.Id.et_title_attendece);
            saveBtn = FindViewById<Button>(Resource.Id.btn_save_attendace);
           
            //set Adapter
            studentAttendeceLV.Adapter = new StudentAdapter(this,tripCode,year,SchoolID, students.Cast<Student>().ToList(), true, 3, students);

            //set et Text
            dateCheeckET.Text = dateTime.ToString("g");
            titleCheekET.Text = attendance.descriotionCheek;

            // set on click function
            if (string.IsNullOrEmpty(date))
                dateCheeckET.Click += DateCheeckET_Click;
            else
            {
                dateCheeckET.Enabled = false;
                dateCheeckET.Hint = "date of check";
            }

            saveBtn.Click += SaveBtn_Click;
        }

        // click on save 
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(date) && ((dateTime.CompareTo(trip.StartDate) < 0 || dateTime.CompareTo(trip.EndDate) > 0)))
            {
                // un valid date
                Toast.MakeText(this, "invalid date", ToastLength.Long).Show();
            }
            else
            {
                //set attendace
                attendance.descriotionCheek = titleCheekET.Text;
                attendance.DateTime = dateTime;

                //change student attendace
                for (int i = 0; i < Add.Count; i++)
                {
                    DataHelper.AddAtendace(attendance, Add[i], this);
                }
                for (int i = 0; i < change.Count; i++)
                {
                    DataHelper.UpdateAtendeceStudent(attendance, change[i], this);
                }
                DataHelper.UpdateAtendeceInfo(attendance, this);
            }
            Finish();
            

        }

        //ope date dilog
        private void DateCheeckET_Click(object sender, EventArgs e)
        {
            DatePickerDialog datePickerDialog = new DatePickerDialog(this,OnDateSet,dateTime.Year, dateTime.Month, dateTime.Day);
            datePickerDialog.Show();
        }

        //set date and open time dilog
        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            if (e.Date.CompareTo(trip.StartDate)<0 || e.Date.CompareTo(trip.EndDate) > 0)
            {
                Toast.MakeText(this,"invalid date", ToastLength.Long).Show();
                DateCheeckET_Click(null, null);
            }
            else
            {
                dateTime = new DateTime(e.Date.Year, e.Date.Month, e.Date.Day, 0, 0, 0);
                TimePickerDialog timePickerDialog = new TimePickerDialog(this, OnTimeSet, DateTime.Now.Hour, DateTime.Now.Minute, true);
                timePickerDialog.Show(); 
            }
        }

        //set time
        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            TimeSpan t = new TimeSpan(e.HourOfDay, e.Minute, 0);
            dateTime.Add(t);
            dateCheeckET.Text = dateTime.ToString("g");
        }

        // change attendace | the fun calld by adapter
        public void ChageAttendace(int postion)
        {
            string id = students[postion].Id;
            if (!string.IsNullOrEmpty(date))
            {
                bool inList = false;
                for (int i = 0; i < Add.Count && !inList; i++)
                {
                    if (id == Add[i].Id)
                    {
                        Add[i].isAttendance = students[postion].isAttendance;
                        inList = true;
                    }
                }
                if (!inList)
                {
                    for (int i = 0; i < change.Count && !inList; i++)
                    {
                        if (id == change[i].Id)
                        {
                            inList = true;
                            change.RemoveAt(i);
                        }
                    }
                    if (!inList)
                    {
                        change.Add(students[postion]);
                    }
                }
            }
        }
    }
}