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
    internal class Student
    {
        string Id { get; }
        string First_Name { get; set; }
        string Last_Name { get; set; }
        string School_ID { get;  }

        public Student(string id, string first_name, string last_name, string school_id)
        {
            this.Id = id;
            this.First_Name = first_name;
            this.Last_Name = last_name;
            this.School_ID = school_id;
        }


    }
}