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
    public class Student
    {
        public string Id { get; }
        public string School_ID { get; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int LerningYear { get; }
        public int ClassAge { get; set; }
        public int ClassNum { get; set; }

        public Student(string id, string first_name, string last_name, string school_id, int lerning_year, int class_age, int class_num)
        {
            this.Id = id;
            this.First_Name = first_name;
            this.Last_Name = last_name;
            this.School_ID = school_id;
            this.LerningYear = lerning_year;
            this.ClassAge = class_age;
            this.ClassNum = class_num;
        }

        public Student(Student s)
        {
            if (s != null)
            {
                this.Id = s.Id;
                this.First_Name = s.First_Name;
                this.Last_Name = s.Last_Name;
                this.School_ID = s.School_ID;
                this.LerningYear = s.LerningYear;
                this.ClassAge = s.ClassAge;
                this.ClassNum = s.ClassNum;
            }
        }

        public Student(string Id) { this.Id = Id; }
        public Student() { }
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