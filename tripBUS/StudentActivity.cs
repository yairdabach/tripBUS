using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "Student")]
    public class StudentActivity : AppCompatActivity
    {
        Spinner classAgeSpr, yearSpr;
        EditText StudentIdET, FirstNameET, LastNameET, ClassNumET;
        Student student;
        Button saveBtn;
        int status, year;
        string schoolId, studentId;

        //1- edit 2- add
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.studnet_mane_layout;
            stub.Inflate();
            

            // Create your application here

            status = Intent.GetIntExtra("Status", 0);
            studentId = Intent.GetStringExtra("student");
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");

            StudentIdET = FindViewById<EditText>(Resource.Id.et_studentId_student);
            FirstNameET = FindViewById<EditText>(Resource.Id.et_first_student);
            LastNameET = FindViewById<EditText>(Resource.Id.et_last_student);
            ClassNumET = FindViewById<EditText>(Resource.Id.et_classNum_student);
            classAgeSpr = FindViewById<Spinner>(Resource.Id.spnr_class_student);
            yearSpr = FindViewById<Spinner>(Resource.Id.spnr_year_student);
            saveBtn = FindViewById<Button>(Resource.Id.button_save_student);

            List<string> classes = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            
            
            List<int> years = new List<int>();
            years.Add(DataHelper.MinYearSchool(SavedData.loginMember.schoolID, this));
            int counter = 1;
            while (counter + years[0] <= DateTime.Today.Year)
            {
                years.Add(counter + years[0]);
                counter++;
            }

            yearSpr.Adapter = new ArrayAdapter<int>(this, Android.Resource.Layout.SimpleSpinnerItem, years);
            classAgeSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, classes);

            if (status == 1)
            {
                student = DataHelper.GetStudentById(studentId, schoolId, year, this);


                StudentIdET.Text = studentId;
                FirstNameET.Text = student.First_Name;
                LastNameET.Text = student.Last_Name;
                ClassNumET.Text = student.ClassNum.ToString();
                classAgeSpr.SetSelection(student.ClassAge - 1);
                yearSpr.SetSelection(student.LerningYear - years[0]);

                StudentIdET.Enabled = false;
                yearSpr.Enabled = false;
                saveBtn.Click += SaveBtn_Update_Click;
            }
            else
            {
                student = new Student();
                saveBtn.Click += SaveBtn_save_Click;
            }

        }

        private void SaveBtn_save_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                if (DataHelper.GetStudentById(StudentIdET.Text, schoolId, int.Parse(((string)yearSpr.SelectedItem)),this)!= null)
                {
                    StudentIdET.Text = null;
                    StudentIdET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                    StudentIdET.Error = "there is Such Id";
                }
                else
                {
                    try
                    {
                        DataHelper.AddStudent(new Student(StudentIdET.Text, FirstNameET.Text, LastNameET.Text, schoolId, int.Parse(((string)yearSpr.SelectedItem)), int.Parse(((string)classAgeSpr.SelectedItem)), int.Parse(ClassNumET.Text)), this);
                        Finish();
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this,"Errore", ToastLength.Long).Show();
                    }
                }
            }
        }

        bool validation()
        {
            var valid = true;
            if (!StudentIdET.Text.All(Char.IsDigit) || String.IsNullOrEmpty(StudentIdET.Text) || StudentIdET.Text.Length != 9)
            {
                //not
                StudentIdET.Text = null;
                StudentIdET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                StudentIdET.Error = "not valid ID";
                valid = false;
            }
            else
            {
                //valod
                StudentIdET.SetError("", null);
                StudentIdET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                StudentIdET.Error = null;
            }

            if (!String.IsNullOrEmpty(FirstNameET.Text) && FirstNameET.Text.All(Char.IsLetter))
            {
                //valid
                FirstNameET.SetError("", null);
                FirstNameET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                FirstNameET.Error = null;
            }
            else
            {
                FirstNameET.Text = null;
                FirstNameET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                FirstNameET.Error = "not valid name";
                valid = false;
            }

            if(!String.IsNullOrEmpty(LastNameET.Text) && LastNameET.Text.All(Char.IsLetter))
            {
                LastNameET.SetError("", null);
                LastNameET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                LastNameET.Error = null;
            }
            else
            {
                LastNameET.Text = null;
                LastNameET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                LastNameET.Error = "not valid name";
                valid = false;
            }

            if (ClassNumET.Text.All(Char.IsDigit) && ClassNumET.Text.Length<=2&& !String.IsNullOrEmpty(ClassNumET.Text))
            {
                //valid
                ClassNumET.SetError("", null);
                ClassNumET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                ClassNumET.Error = null;
            }
            else
            {
                //not valid
                ClassNumET.Text = null;
                ClassNumET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                ClassNumET.Error = "not valid name";
                valid = false;
            }

            return valid;
        }

        private void SaveBtn_Update_Click(object sender, EventArgs e)
        {
            if(validation())
            {
                student.First_Name = FirstNameET.Text;
                student.Last_Name = LastNameET.Text;
                student.ClassNum = int.Parse(ClassNumET.Text);
                student.ClassAge = int.Parse(((string)classAgeSpr.SelectedItem));

                try
                {
                    DataHelper.UpdateStudent(student, this);
                    Finish();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "erore", ToastLength.Long).Show();
                }
            }

        }
    }
}