using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.AppBar;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "Groups")]
    public class GroupsActivity : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        LinearLayout faterLayout;
        List<Group> groups;
        List<Button> buttons;
        string SchoolID, tripName;
        int tripCode,year;
        Google.Android.Material.FloatingActionButton.FloatingActionButton plusFAB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //set screen
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            // get info from prev actibity
            SchoolID = Intent.GetStringExtra("SchoolId");
            tripCode = Intent.GetIntExtra("tripCode", 0);
            tripName = Intent.GetStringExtra("TripName");
            year = Intent.GetIntExtra("year",0);

            SupportActionBar.Title = "Groups | "+ tripName;


            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.layout_empty;
            stub.Inflate();

            faterLayout = FindViewById<LinearLayout>(Resource.Id.layout_empty);

            //set dinamicly base for activity
            var Frame = new FrameLayout(this);
            Frame.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent);
            faterLayout.AddView(Frame);
            faterLayout= new LinearLayout(this);
            faterLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            Frame.AddView(faterLayout);
            faterLayout.Orientation = Orientation.Vertical;

            plusFAB = new FloatingActionButton(this);
            plusFAB.LayoutParameters = new FrameLayout.LayoutParams(150, 150, (GravityFlags.Right | GravityFlags.Bottom));
            ((FrameLayout.LayoutParams) plusFAB.LayoutParameters).SetMargins(50,50, 50, 50);
            plusFAB.SetImageResource(Android.Resource.Drawable.IcInputAdd);
            plusFAB.ImageTintList = Android.Content.Res.ColorStateList.ValueOf(Color.Rgb(255, 225, 225));
            Frame.AddView(plusFAB); 

            FindViewById<TextView>(Resource.Id.tv_empty).Visibility = ViewStates.Gone;

            createScreen();
            plusFAB.Click += PlusFAB_Click;
        }

        private void createScreen()
        {
            //desing button for goups
            faterLayout.RemoveAllViewsInLayout();
            buttons = new List<Button>();
            groups = DataHelper.GetAllGroups(tripCode, SchoolID, this);
            int margin = 30;
            foreach (var group in groups)
            {
                Button temp = new Button(this);
                temp.SetBackgroundResource(Resource.Drawable.button3_style);
                temp.Text = "Group Num: "+ group.GroupNum +" | "+ group.Name;
                var layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                layoutParams.SetMargins(100, margin, 100, 10);
                margin = 10;
                layoutParams.Gravity= GravityFlags.CenterHorizontal;
                temp.LayoutParameters = layoutParams;

                temp.Click += Temp_Click;

                buttons.Add(temp);
                faterLayout.AddView(temp);

            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            createScreen();
        }

        //open activity - new group
        private void PlusFAB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewGroupActivity));
            Bundle b = new Bundle();
            int max = 0;
            foreach (Group group in groups)
            {
                if (max < group.GroupNum)
                {
                    max = group.GroupNum;
                }
            }
            Intent intenti = new Intent(this, typeof(EditGroupActivity));
            Bundle bi = new Bundle();
            bi.PutInt("groupNum", max + 1);
            bi.PutInt("tripCode", tripCode);
            bi.PutInt("year", year);
            bi.PutString("SchoolId", Helpers.SavedData.loginMember.schoolID);
            intenti.PutExtras(bi);
            StartActivityForResult(intenti, 1);
        }

        // open group view screen
        private void Temp_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i]==sender)
                {
                    Intent intent = new Intent(this, typeof(ViewGroupActivity));
                    Bundle b = new Bundle();
                    b.PutInt("groupNum", groups[i].GroupNum);
                    b.PutInt("tripCode", tripCode);
                    b.PutInt("year", year);
                    b.PutString("SchoolId", SchoolID);
                    intent.PutExtras(b);
                    StartActivityForResult(intent, 0);
                }
            }
        }
    }
}