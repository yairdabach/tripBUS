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
    [Activity(Label = "GroupViewActivity")]
    public class ViewGroupActivity : AppCompatActivity
    {
        EditText GroupName, GroupNum;
        ListView groupStudent;
        Spinner teamMemberSpr, busSpr;
        int tripCode, groupNum,year;
        string schoolId;
        int meneger;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
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
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");
            meneger = Intent.GetIntExtra("meneger", 0);

            if (meneger == 1 )
            {
                FindViewById(Resource.Id.group_fav).Visibility = ViewStates.Gone;
            }

            Group group = DataHelper.GetGroup(groupNum, tripCode,year, schoolId, this);

            (FindViewById<LinearLayout>(Resource.Id.ll_studentclass_Glayout)).Visibility = ViewStates.Gone;

            GroupName = FindViewById<EditText>(Resource.Id.et_Gname_Group); 
            GroupNum = FindViewById<EditText>(Resource.Id.et_Gnum_Group);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.group_fav);
            busSpr = FindViewById<Spinner>(Resource.Id.spnr_bus_group);
            teamMemberSpr = FindViewById<Spinner>(Resource.Id.spnr_teammember_group);

            groupStudent = new ListView(this);

            GroupName.Enabled = false;
            GroupNum.Enabled = false;
            busSpr.Enabled = false;
            teamMemberSpr.Enabled = false;

            GroupNum.Text = groupNum.ToString();
            GroupName.Text = group.Name;

            Bus bus = DataHelper.GetBusInfo(group.BusNumber, group.tripCode, group.SchoolId, this);

            List<string> busNum = new List<string>() ;
            if (bus != null)
            {
                busNum.Add(bus.busNum + " | " + bus.BusName);
            }
            busSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, busNum);

            List<string> TeamMember = new List<string>();
            if (group.teamMember != null)
            {
                TeamMember.Add(group.teamMember.firstName+" "+ group.teamMember.lastName);
            }
            teamMemberSpr.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, TeamMember);


            if (group.students.Count > 0)
            {

                groupStudent.Adapter = new StudentAdapter(this,year,schoolId, group.students, false);

            }

            (FindViewById<LinearLayout>(Resource.Id.ll_lv_groupStudent_viewgroup_layout)).AddView(groupStudent);
            (FindViewById<LinearLayout>(Resource.Id.ll_groupStudent_viewgroup_layout)).LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

            editFAB.SetImageResource(Android.Resource.Drawable.IcMenuEdit);
            if (meneger ==1)
            {
                editFAB.Visibility = ViewStates.Gone;
            }
            else
            {
                editFAB.Click += EditFAB_Click;
            }
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);
            Group group = DataHelper.GetGroup(groupNum, tripCode, year, schoolId, this);
            GroupNum.Text = groupNum.ToString();
            GroupName.Text = group.Name;

            if (group.students.Count > 0)
            {

                groupStudent.Adapter = new StudentAdapter(this, year, schoolId, group.students, false);

            }
        }
        private void EditFAB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditGroupActivity));
            Bundle b = new Bundle();
            b.PutInt("groupNum", groupNum);
            b.PutInt("tripCode", tripCode);
            b.PutInt("year", year);
            b.PutString("SchoolId", Helpers.SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivityForResult(intent,1);
            
        }
    }
}