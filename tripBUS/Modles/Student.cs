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
        string School_ID { get; }
        string First_Name { get; set; }
        string Last_Name { get; set; }
        int LerningYear { get; }
        int ClassAge { get; }
        int ClassNum { get; }

        public Student(string id, string first_name, string last_name, string school_id, int lerning_year)
        {
            this.Id = id;
            this.First_Name = first_name;
            this.Last_Name = last_name;
            this.School_ID = school_id;
            this.LerningYear = lerning_year;
        }

        // CREATE TABLE Student(
        //    LastName varchar(255),
        //    FirstName varchar(255),
	    //    ClassAge int,
	    //    ClassNum int,
        //    StudentID varchar(9),
	    //    SchoolID varchar(6),
        //    LerningYear int
        //    PRIMARY Key(StudentID, SchoolID, LerningYear)
        // );
    }
}