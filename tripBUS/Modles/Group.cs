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
    public class Group
    {
        public int GroupNum { get; set; }
        public string Name { get; set; }
        public int tripCode { get; set; }
        public string SchoolId { get; set; }
        public string TeamMemberEmail { get; set; }
        public TeamMember teamMember { get; set; }
        public List<Student> students { get; set; }

        public int BusNumber { get; set; } = 667;
        public int amoountOfstudent { get; set; } = 0; // להגדיר Set משלו


        public Group() { teamMember = new TeamMember(); }
        public Group(int GroupNum, string name, int tripcode, string schoolId, TeamMember teamMember)
        {
            this.GroupNum = GroupNum;
            this.Name = name;
            this.tripCode = tripcode;
            this.SchoolId = schoolId;
            this.teamMember = teamMember;
            BusNumber = 667;
            amoountOfstudent = 0;
            students = new List<Student>();

        }

        public Group(int GroupNum, string name, int tripcode, string schoolId, string teamMember, int amoountOfstudent)
        {
            this.GroupNum = GroupNum;
            this.Name = name;
            this.tripCode = tripcode;
            this.SchoolId = schoolId;
            this.TeamMemberEmail = teamMember;
            this.amoountOfstudent = amoountOfstudent;
            BusNumber = 667;
            students = new List<Student>();

        }

        public Group(int GroupNum, string name, int tripcode, string schoolId, string teamMember, int amoountOfstudent, int BusNumber)
        {
            this.GroupNum = GroupNum;
            this.Name = name;
            this.tripCode = tripcode;
            this.SchoolId = schoolId;
            this.TeamMemberEmail = teamMember;
            this.amoountOfstudent = amoountOfstudent;
            students = new List<Student>(); 
            this.BusNumber = BusNumber;

        }


        public override string ToString()
        {
            return $"({GroupNum},'{Name}',{tripCode},'{SchoolId}','{teamMember.email}',{BusNumber})";
        }
    }
}