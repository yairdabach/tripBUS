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
        Spinner teamMemberSpr;
        int tripCode, groupNum;
        string schoolId;
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
            schoolId = Intent.GetStringExtra("SchoolId");

            Group group = DataHelper.GetGroup(groupNum, tripCode, schoolId, this);


            (FindViewById<LinearLayout>(Resource.Id.ll_studentclass_Glayout)).Visibility = ViewStates.Gone;

            GroupName = FindViewById<EditText>(Resource.Id.et_Gname_Group); 
            GroupNum = FindViewById<EditText>(Resource.Id.et_Gnum_Group);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.group_fav);

            groupStudent = new ListView(this);

            GroupName.Enabled = false;
            GroupNum.Enabled = false;

            GroupNum.Text = groupNum.ToString();
            GroupName.Text = group.Name;

            if (group.students.Count > 0)
            {

                groupStudent.Adapter = new StudentAdapter(this, group.students, false);

            }

            (FindViewById<LinearLayout>(Resource.Id.ll_lv_groupStudent_viewgroup_layout)).AddView(groupStudent);
            (FindViewById<LinearLayout>(Resource.Id.ll_groupStudent_viewgroup_layout)).LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

            editFAB.SetImageResource(Android.Resource.Drawable.IcMenuEdit);
            editFAB.Click += EditFAB_Click;
        }

        private void EditFAB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditGroupActivity));
            Bundle b = new Bundle();
            b.PutInt("groupNum", 1);
            b.PutInt("tripCode", tripCode);
            b.PutString("SchoolId", Helpers.SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivity(intent);
            
        }
    }
}