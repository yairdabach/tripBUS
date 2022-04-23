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
    public class StudentAttendance : Student
    {


        public bool isAttendance { get; set; }
        public StudentAttendance(string id, string first_name, string last_name, string school_id, int lerning_year, int class_age, int class_num, bool _isAttendance) : base(id, first_name, last_name, school_id, lerning_year, class_age, class_num)
        {
            this.isAttendance = _isAttendance;
        }

        public StudentAttendance(string id,bool isAttendance): base(id)
        {
            this.isAttendance = isAttendance;
        }

        public StudentAttendance(Student s, bool isAttendance) : base(s)
        {
            this.isAttendance = isAttendance;
        }
    }
}