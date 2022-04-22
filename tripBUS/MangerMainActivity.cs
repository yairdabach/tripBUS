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
using tripBUS.Modles;

namespace tripBUS
{
    [Activity(Label = "MangerMainActivity")]
    public class MangerMainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        ListView tripsLV;
        List<Trip> allTrips;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.navbar_layout);
            // Create your application here

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_futer);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Future trips";

            
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.futer_trips_layout;
            stub.Inflate();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_name)).Text = SavedData.loginMember.firstName + " "+SavedData.loginMember.lastName;
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_email)).Text = SavedData.loginMember.email;

            allTrips = DataHelper.GetAllTrips(SavedData.loginMember.email, this);
            string email = SavedData.loginMember.email;
            List<Trip> futerTrips = new List<Trip>();

            foreach (Trip trip in allTrips)
            {
                if (trip.StartDate.CompareTo(DateTime.Now)>=0)
                {
                    futerTrips.Add(trip);
                }
            }

            tripsLV = FindViewById<ListView>(Resource.Id.lv_trips_futer);
            tripsLV.Adapter = new TripAdapter(allTrips, this);
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (menuItem.ItemId == Resource.Id.nav_logout)
            {
                SavedData.loginMember = null;
                SavedData.DeleteSp();
                Intent MainAc = new Intent(this, typeof(MainActivity));
                StartActivity(MainAc);
                Finish();
            }
            if (menuItem.ItemId == Resource.Id.nav_new_trip)
            {
                Intent NewTrAc = new Intent(this, typeof(NewTripActivity));
                StartActivity(NewTrAc);
                return true;
            }
            if (menuItem.ItemId == Resource.Id.nav_update)
            {
                Intent UpAc = new Intent(this, typeof(UpdateDitlesActivity));
                StartActivityForResult(UpAc, 0);
                return true;
            }
            if (menuItem.ItemId == Resource.Id.nav_past_trips)
            {
                Intent PasdtAc = new Intent(this, typeof(ViewTripActivity));
                PasdtAc.PutExtra("TripCode", 1);
                PasdtAc.PutExtra("SchoolId", SavedData.loginMember.schoolID);
                StartActivityForResult(PasdtAc, 0);
                return true;
            }
            if (menuItem.ItemId == Resource.Id.nav_student_menegment) 
            {
                Intent SudMenAc = new Intent(this, typeof(StudentManegmantActivity));
                SudMenAc.PutExtra("TripCode", 1);
                SudMenAc.PutExtra("SchoolId", SavedData.loginMember.schoolID);
                StartActivityForResult(SudMenAc, 0);
                return true;
            }
            return true;
        }

        public void OpenTripLayout(int TripCode, string schoolID)
        {
            Intent PasdtAc = new Intent(this, typeof(ViewTripActivity));
            PasdtAc.PutExtra("TripCode", TripCode);
            PasdtAc.PutExtra("SchoolId", schoolID);
            StartActivityForResult(PasdtAc, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_name)).Text = SavedData.loginMember.firstName + " " + SavedData.loginMember.lastName;
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_email)).Text = SavedData.loginMember.email;

        }


    }
}