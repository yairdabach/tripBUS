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
    [Activity(Label = "GroupEditActivity")]
    public class EditGroupActivity : AppCompatActivity
    {
        EditText GroupName, GroupNum;
        ListView groupStudent , classStudent;
        Trip trip;
        Spinner busSpr ,teamMemberSpr,classSpr;
        int tripCode, groupNum,year;
        string schoolId;
        bool IsExist;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
        List<Student> currentList, DeleteStudent, AddStudent;
        List<string> studentInAtherGroup;
        List<TeamMember> teamMembers;
        Group group;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.group_layout;
            stub.Inflate();

            groupNum = Intent.GetIntExtra("groupNum", 2);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");

            SupportActionBar.Title = "Group "+ groupNum;

            trip = DataHelper.GetTripByCode(tripCode, this);
            group = DataHelper.GetGroup(groupNum, tripCode, year, schoolId, this);
            if (group == null)
            {
                group = new Group();
                group.GroupNum = groupNum;
                group.SchoolId = schoolId;
                group.tripCode = tripCode;
                group.students = new List<Student>();
                IsExist = false;
            }
            else
            {
                IsExist = true;
            }
            currentList = new List<Student>();
            foreach (var student in group.students)
            {
                currentList.Add(student);
            }
            DeleteStudent = new List<Student>();
            AddStudent = new List<Student>();

            studentInAtherGroup = DataHelper.GetAllGroupStudent(groupNum, tripCode, this);

            GroupName = FindViewById<EditText>(Resource.Id.et_Gname_Group);
            GroupNum = FindViewById<EditText>(Resource.Id.et_Gnum_Group);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.group_fav);
            classSpr = FindViewById<Spinner>(Resource.Id.spnr_class_group);
            teamMemberSpr = FindViewById<Spinner>(Resource.Id.spnr_teammember_group);
            busSpr = FindViewById<Spinner>(Resource.Id.spnr_bus_group);

            List<string> busNum = new List<string>();
            busNum.Add("");
            List<Bus> busList = DataHelper.GetAllBuss(group.tripCode, group.SchoolId, this);
            int index = 0;
            foreach (Bus bus in busList)
            {
                busNum.Add(bus.busNum + " | " + bus.BusName);
                if (bus.busNum == group.BusNumber)
                {
                    index = busNum.IndexOf(bus.busNum + " | " + bus.BusName);
                }
            }
            busSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, busNum);
            busSpr.SetSelection(index);

            List<string> teamMemberString = new List<string>();
            teamMembers = DataHelper.GetTripTeamMembersWhoNotConnected(tripCode, schoolId, this);
            index = 0;

            teamMembers.Add(group.teamMember);
            teamMemberString.Add(" ");
            foreach (TeamMember teamMember in teamMembers)
            {
                teamMemberString.Add(teamMember.firstName +" "+ teamMember.lastName);
                if (teamMember != null && group.teamMember != null)
                {
                    if (group.teamMember.email == teamMember.email)
                    {
                        index = busNum.IndexOf(teamMember.firstName + " " + teamMember.lastName);
                    }
                }
                
            }
            teamMemberSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, teamMemberString);
            teamMemberSpr.SetSelection(index);


            groupStudent = new ListView(this);

            GroupNum.Text = groupNum.ToString();
            GroupName.Text = group.Name;

            List<Student> tempo = new List<Student>();
            foreach (var student in group.students)
            {
                tempo.Add(student);
            }

            if (group.students.Count > 0)
            {
                groupStudent.Adapter = new StudentAdapter(this,tripCode,year,schoolId, tempo, false,2);
            }
            else
            {
                groupStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, new List<Student>(), false, 2);
            }
            (FindViewById<LinearLayout>(Resource.Id.ll_lv_groupStudent_viewgroup_layout)).AddView(groupStudent);

            GroupNum.Enabled = false;

            //creat listview class
            classStudent = new ListView(this);
            (FindViewById<LinearLayout>(Resource.Id.ll_lv_classStudent_viewgroup_layout)).AddView(classStudent);
            //sppiner adapter
            List<int> classInschool = (DataHelper.GetClassAgeInYear(schoolId, trip.StartDate.Year, this))[int.Parse(trip.classAge)];
            List<string> classList = new List<string>();

            if (classInschool != null)
            { 
                foreach (int classNum in classInschool)
                {
                    classList.Add(trip.classAge + "/" + classNum.ToString());
                }
            }

            classSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, classList);
            classSpr.ItemSelected += ClassSpr_ItemSelected;
            ClassSpr_ItemSelected(classSpr, null);

            editFAB.SetImageResource(Android.Resource.Drawable.IcMenuSave);
            editFAB.Click += EditFAB_Click;
        }

        private void ClassSpr_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (!classSpr.Adapter.IsEmpty)
            {
                List<Student> students = DataHelper.GetStudentClassAgeInYear(int.Parse(((string)classSpr.SelectedItem).Split("/")[0]), int.Parse(((string)classSpr.SelectedItem).Split("/")[1]), schoolId, trip.StartDate.Year, this);
                int pos = students.Count;
                if (students == null)
                {
                   students = new List<Student>();
                }
                for (int i = 0; i < students.Count; i++)
                {
                    for (int j = 0; j < currentList.Count; j++)
                    {
                        try
                        {
                            if (students[i].Id == currentList[j].Id)
                            {
                                students.Remove(students[i]);
                            }
                        }
                        catch (Exception ex) { }
                    }
                }

                for (int i = 0; i < students.Count; i++)
                {
                    for (int j = 0; j < studentInAtherGroup.Count; j++)
                    {
                        try
                        {
                            if (students[i].Id == studentInAtherGroup[j])
                            {
                                students.Remove(students[i]);
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                classStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, students, false, 1);
            }
            
        }  

        public void RemoveFromGroup(Student student)
        {
            List<Student> temp;
            
            ((StudentAdapter)groupStudent.Adapter).RemoveItem(student);
            if (int.Parse(((string)classSpr.SelectedItem).Split("/")[1])==student.ClassNum && ((string)classSpr.SelectedItem).Split("/")[0] == student.ClassAge.ToString())
            {
                temp = ((StudentAdapter)classStudent.Adapter).objects;
                temp.Add(student);
                classStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, temp, false, 1);
            }
            
            temp = ((StudentAdapter)groupStudent.Adapter).objects;
            temp.Remove(student);
            groupStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, temp, false, 2);

            bool inGroup = false;
            foreach (Student studentIngroup in group.students)
            {
                if (studentIngroup.Id == student.Id)
                {
                    inGroup = true;
                }
            }

            if (inGroup)
            {
                DeleteStudent.Add(student);
            }
            else
            {
                AddStudent.Remove(student);
            }
        }

        public void AddToGroup(Student student)
        {
            //currentList.Add(student);
            List<Student> temp;
            if (classStudent.Adapter != null)
            {
                temp = ((StudentAdapter)classStudent.Adapter).objects;
                temp.Remove(student);
                classStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, temp, false, 1);
            }
            if (groupStudent.Adapter != null)
            {
                temp = ((StudentAdapter)groupStudent.Adapter).objects;
                temp.Add(student);
                groupStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, temp, false, 2);
                //groupStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, temp, false, 2);

            }

            bool inGroup = false;
            foreach (Student studentIngroup in group.students)
            {
                if (studentIngroup.Id == student.Id)
                {
                    inGroup = true;
                }
            }

            if (inGroup)
            {
                DeleteStudent.Remove(student);
            }
            else
            {
                AddStudent.Add(student);
            }
        }

        private void EditFAB_Click(object sender, EventArgs e)
        {
            group.Name = GroupName.Text;
            if (busSpr.SelectedItem.ToString() == "")
            {
                group.BusNumber = 667;
            }
            else
            {
                group.BusNumber = int.Parse((busSpr.SelectedItem.ToString().Split(" | "))[0]);
            }

            if (group.teamMember != null)
            {
                DataHelper.UpdateTeamMemberGroup(tripCode, 667, group.teamMember.email, this);
            }
            

            if (teamMemberSpr.SelectedItem.ToString() == " ")
            {
                group.teamMember = null;
            }
            else
            {
                group.teamMember = teamMembers[teamMemberSpr.SelectedItemPosition-1];
                DataHelper.UpdateTeamMemberGroup(tripCode, groupNum, group.teamMember.email, this);
            }

            //spiiner
            if (IsExist)
            {
                group.amoountOfstudent = currentList.Count;
                DataHelper.UpdateGroup(group, AddStudent, DeleteStudent, this);
                FinishActivity(1);
                Finish();
            }
            else
            {

                DataHelper.AddNewGroup(group, AddStudent, this);
                Intent intent = new Intent(this, typeof(ViewGroupActivity));
                Bundle b = new Bundle();
                b.PutInt("groupNum", group.GroupNum);
                b.PutInt("tripCode", tripCode);
                b.PutInt("year", year);
                b.PutString("SchoolId", group.SchoolId);
                intent.PutExtras(b);
                StartActivity(intent);
                Finish();
            }
        }
    }
}