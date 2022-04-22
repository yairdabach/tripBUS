using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using tripBUS.Helpers;
using tripBUS.Modles;
using Android.Content;
using AndroidX.AppCompat.App;


namespace tripBUS
{
    [Activity(Label = "ViewStudentLIst")]
    public class ViewStudentListActivity : AppCompatActivity
    {
        private int busNum, tripCode, year; 
        string schoolId;
        private ListView StudentLV;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.layout_empty;
            stub.Inflate();

            // Create your application here
            View PerentLayout = FindViewById(Resource.Id.layout_empty);
            (FindViewById<TextView>(Resource.Id.tv_empty)).Visibility = ViewStates.Gone;

            busNum = Intent.GetIntExtra("busNum", 0);
            tripCode = Intent.GetIntExtra("tripCode", 0);
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");


            List<Student> students = DataHelper.GetStudentByBus(busNum, tripCode, year, schoolId, this);

            StudentAdapter studentAdapter = new StudentAdapter(this, tripCode, year, schoolId, students, false);

            StudentLV = new ListView(this);
            StudentLV.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ((LinearLayout)PerentLayout).AddView(StudentLV);

            StudentLV.Adapter = studentAdapter;

        }
    }
}