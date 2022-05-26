using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using tripBUS.Helpers;
using tripBUS.Modles;
using Android.Content;
using AndroidX.AppCompat.App;
using Android.Runtime;

namespace tripBUS
{
    [Activity(Label = "ClassManegmentActivity")]
    public class ClassManegmentActivity : AppCompatActivity
    {
        ListView StudentLV;
        int classAge, year, classNum;
        string SchoolID;
        View PerentLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //set screen
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.layout_empty;
            stub.Inflate();

            // Create your application here
            PerentLayout = FindViewById(Resource.Id.layout_empty);
            (FindViewById<TextView>(Resource.Id.tv_empty)).Visibility = ViewStates.Gone;

            // get info from prev activity
            classAge = Intent.GetIntExtra("ClassAge", 0);
            SchoolID = Intent.GetStringExtra("SchoolId");
            year = Intent.GetIntExtra("year", 0);
            classNum = Intent.GetIntExtra("ClassNum", 0);

            // set title activity
            SupportActionBar.Title = "Class: " + classAge + "-" + classNum + " | " + year;

            //get class student and set adapter
            List<Student> students = DataHelper.GetStudentClassAgeInYear(classAge, classNum, SavedData.loginMember.schoolID, year, this);

            StudentAdapter studentAdapter = new StudentAdapter(this, year, SchoolID,students, true);

            StudentLV = new ListView(this);
            StudentLV.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ((LinearLayout) PerentLayout).AddView(StudentLV);
            
            StudentLV.Adapter = studentAdapter;

            
        }

        public void StartActivity(Intent intent)
        {
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            List<Student> students = DataHelper.GetStudentClassAgeInYear(classAge, classNum, SavedData.loginMember.schoolID, year, this);

            StudentAdapter studentAdapter = new StudentAdapter(this, year, SchoolID, students, true);

            StudentLV = new ListView(this);
            StudentLV.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ((LinearLayout)PerentLayout).AddView(StudentLV);

            StudentLV.Adapter = studentAdapter;
        }
    }
}