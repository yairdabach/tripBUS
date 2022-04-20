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
        int tripCode, groupNum;
        string schoolId;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
        List<Student> currentList, DeleteStudent, AddStudent;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Group ";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.group_layout;
            stub.Inflate();

            groupNum = Intent.GetIntExtra("groupNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            schoolId = Intent.GetStringExtra("SchoolId");

            trip = DataHelper.GetTripByCode(tripCode, this);
            Group group = DataHelper.GetGroup(groupNum, tripCode, schoolId, this);
            currentList = group.students;
            DeleteStudent = new List<Student>();
            AddStudent = new List<Student>();

            GroupName = FindViewById<EditText>(Resource.Id.et_Gname_Group);
            GroupNum = FindViewById<EditText>(Resource.Id.et_Gnum_Group);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.group_fav);
            classSpr = FindViewById<Spinner>(Resource.Id.spnr_class_group);

            groupStudent = new ListView(this);

            GroupNum.Text = groupNum.ToString();
            GroupName.Text = group.Name;

            if (group.students.Count > 0)
            {
                groupStudent.Adapter = new StudentAdapter(this, group.students, false,2);
            }

            (FindViewById<LinearLayout>(Resource.Id.ll_lv_groupStudent_viewgroup_layout)).AddView(groupStudent);

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
                foreach (Student studentClass in students)
                {
                    foreach(Student studentCurrent in currentList)
                    {
                        if (studentClass.Id == studentCurrent.Id)
                        {
                            students.Remove(studentClass);
                        }
                    }
                }
                classStudent.Adapter = new StudentAdapter(this, students, false, 1);
            }
            
        }  

        public void RemoveFromGroup(Student student)
        {
            List<Student> temp;
            currentList.Remove(student);
            DeleteStudent.Add(student);
            ((StudentAdapter)groupStudent.Adapter).RemoveItem(student);
            if (int.Parse(((string)classSpr.SelectedItem).Split("/")[1])==student.ClassNum && ((string)classSpr.SelectedItem).Split("/")[0] == student.ClassAge.ToString())
            {
                temp = ((StudentAdapter)classStudent.Adapter).objects;
                temp.Add(student);
                classStudent.Adapter = new StudentAdapter(this, temp, false, 1);
            }
            
            temp = ((StudentAdapter)groupStudent.Adapter).objects;
            temp.Add(student);
            groupStudent.Adapter = new StudentAdapter(this, temp, false, 2);
        }

        public void AddToGroup(Student student)
        {
            currentList.Add(student);
            List<Student> temp = ((StudentAdapter)classStudent.Adapter).objects;
            temp.Remove(student);
            classStudent.Adapter = new StudentAdapter(this, temp, false, 1);
            temp = ((StudentAdapter)groupStudent.Adapter).objects;
            temp.Add(student);
            groupStudent.Adapter = new StudentAdapter(this, temp, false, 2);
        }

        private void EditFAB_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}