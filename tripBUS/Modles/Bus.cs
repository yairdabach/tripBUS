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
    public class Bus
    {
        public int busNum { set; get; }
        public int tripNum { set; get; }
        public string schoolId { set; get; }
        public string BusName { set; get; }
        public int countStudent { set; get; }

        public List<Attendance> attendances { set; get; }
        public Bus(int busnum, int tripcode, string schoolId)
        {
            busNum = busnum;
            tripNum = tripcode;
            this.schoolId = schoolId;
            countStudent = 0;
        }

        public Bus(int busnum, int tripcode, string schoolId, string BusName, int count)
        {
            busNum = busnum;
            tripNum = tripcode;
            this.schoolId = schoolId;
            this.BusName = BusName;
            this.countStudent = count;
        }
    }
}