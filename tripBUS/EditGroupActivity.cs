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
        Spinner teamMemberSpr,classSpr;
        int tripCode, groupNum,year;
        string schoolId;
        bool IsExist;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
        List<Student> currentList, DeleteStudent, AddStudent;
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

            
            GroupName = FindViewById<EditText>(Resource.Id.et_Gname_Group);
            GroupNum = FindViewById<EditText>(Resource.Id.et_Gnum_Group);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.group_fav);
            classSpr = FindViewById<Spinner>(Resource.Id.spnr_class_group);

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
                if ((students != null || students.Count>0) && (currentList != null || currentList.Count > 0))
                {
                    foreach (Student studentClass in students)
                    {
                        foreach (Student studentCurrent in currentList)
                        {
                            if (studentClass.Id == studentCurrent.Id)
                            {
                                students.Remove(studentClass);
                                
                            }
                            
                        }
                        pos--;
                        if (pos == 0)
                        {
                            break;
                        }
                    }
                    classStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, students, false, 1);
                }
                else
                {
                    classStudent.Adapter = new StudentAdapter(this, tripCode, year, schoolId, new List<Student>(), false, 1);
                }
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