using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
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

namespace tripBUS
{
    [Activity(Label = "Student Manegmant")]
    public class StudentManegmantActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, Android.Views.View.IOnClickListener
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        LinearLayout faterLayout;
        Spinner sprLerningYear;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.navbar_layout);
            // Create your application here

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_futer);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Student Menegmant";


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_name)).Text = SavedData.loginMember.firstName + " " + SavedData.loginMember.lastName;
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_email)).Text = SavedData.loginMember.email;


            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.student_menegment;
            stub.Inflate();

            faterLayout = FindViewById<LinearLayout>(Resource.Id.lily_Perent_StudentMenegment);

            sprLerningYear = FindViewById<Spinner>(Resource.Id.spnr_LerningYear_StudentMenegment);

            createScreen();
            
        }

        private void createScreen()
        {
            faterLayout.RemoveAllViews();
            List<int>[] classAge =DataHelper.GetClassAgeInYear(SavedData.loginMember.schoolID, 2022, this);
            if (classAge!=null)
            {
                
                for (int i = 0; i < classAge.Length; i++)
                {
                    if (classAge[i] != null)
                    {
                        var scrollTemp = new HorizontalScrollView(this);
                        scrollTemp.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent);

                        LinearLayout linearTemp = new LinearLayout(this);
                        linearTemp.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                        linearTemp.Orientation = Orientation.Vertical;
                        scrollTemp.AddView(linearTemp);

                        TextView classAgeTV = new TextView(this);
                        classAgeTV.Text = "Class Age : " + i;
                        classAgeTV.LayoutParameters=  new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                        classAgeTV.TextSize = 18;
           
                        linearTemp.AddView(classAgeTV);

                        linearTemp = new LinearLayout(this);
                        linearTemp.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                        linearTemp.Orientation = Orientation.Horizontal;
                        ((LinearLayout)(scrollTemp.GetChildAt(0))).AddView(linearTemp);

                        foreach(var classNum in classAge[i])
                        {
                            Button buttonClass = new Button(this);
                            buttonClass.Text = i.ToString()+"-"+ classNum.ToString();
                            buttonClass.LayoutParameters = new LinearLayout.LayoutParams(200, 200);
                            buttonClass.SetBackgroundResource(Resource.Drawable.button3_style);
                            
                            buttonClass.SetOnClickListener(this);
                            linearTemp.AddView(buttonClass);
                        }

                        faterLayout.AddView(scrollTemp);
                    }
                }
            }

        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
            if (v is Button)
            {
                string[] classinfo = ((Button)v).Text.Split('-');

                Intent ClassMenAc = new Intent(this, typeof(ClassManegmentActivity));
                ClassMenAc.PutExtra("ClassAge", int.Parse(classinfo[0]));
                ClassMenAc.PutExtra("ClassNum", int.Parse(classinfo[1]));
                ClassMenAc.PutExtra("year", 2022);
                ClassMenAc.PutExtra("schoolId", SavedData.loginMember.schoolID);
                StartActivity(ClassMenAc);
            }
        }
    }
}