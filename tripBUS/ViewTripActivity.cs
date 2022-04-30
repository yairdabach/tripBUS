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
    [Activity(Label = "")]

    public class ViewTripActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        Trip trip;
        EditText TripCodeET,TripNameET, TripDescriptionET, TripStartDate, TripEndDate, TripPlace;
        Spinner TripClass;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.navbar_layout);
            // Create your application here

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_futer);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "";

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.trip_layout;
            stub.Inflate();

            Google.Android.Material.Navigation.NavigationView navigationView = FindViewById<Google.Android.Material.Navigation.NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_name)).Text = SavedData.loginMember.firstName + " " + SavedData.loginMember.lastName;
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_email)).Text = SavedData.loginMember.email;

            int tripCode = Intent.GetIntExtra("TripCode", 0);

            if (tripCode == 0)
            {
                Toast.MakeText(this, "ERROE", ToastLength.Long);
                Finish();
            }
            else
            {
                trip = DataHelper.GetTripByCode(tripCode, this);
            }

            // Create your application here
            
            TripCodeET = FindViewById<EditText>(Resource.Id.et_TripCode_trip);
            TripNameET = FindViewById<EditText>(Resource.Id.et_TripName_trip);
            TripDescriptionET = FindViewById<EditText>(Resource.Id.et_TripDes_trip);
            TripStartDate = FindViewById<EditText>(Resource.Id.et_startdate_trip);
            TripEndDate = FindViewById<EditText>(Resource.Id.et_end_trip);
            TripPlace = FindViewById<EditText>(Resource.Id.et_place_trip);
            TripClass = FindViewById<Spinner>(Resource.Id.spnr_class_trip);
            editFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.save_trip_fav);

            var items = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            TripClass.Adapter = adapter;

            TripCodeET.Enabled = false;
            TripNameET.Enabled = false;
            TripDescriptionET.Enabled = false;
            TripStartDate.Enabled = false;
            TripEndDate.Enabled = false;
            TripPlace.Enabled = false;
            TripClass.Enabled = false;
            (FindViewById<EditText>(Resource.Id.et_GroupCount_trip)).Enabled = false;
            (FindViewById<EditText>(Resource.Id.et_StudentCount_trip)).Enabled = false;
            (FindViewById<EditText>(Resource.Id.et_BusCount_trip)).Enabled = false;

            editFAB.SetImageResource(Android.Resource.Drawable.IcMenuEdit);
            TripCodeET.Text = trip.tripCode.ToString();
            TripNameET.Text = trip.tripName;
            TripDescriptionET.Text = trip.tripDescription;
            TripPlace.Text = trip.place;
            TripStartDate.Text = trip.StartDate.ToShortDateString();
            TripEndDate.Text = trip.EndDate.ToShortDateString();
            (FindViewById<EditText>(Resource.Id.et_GroupCount_trip)).Text = trip.countGroup.ToString()  ;
            (FindViewById<EditText>(Resource.Id.et_StudentCount_trip)).Text = trip.countStudent.ToString();
            (FindViewById<EditText>(Resource.Id.et_BusCount_trip)).Text = trip.countBus.ToString();

            for (int i = 0; i < adapter.Count; i++)
            {
                if (((string)(adapter.GetItem(i))) == trip.classAge)
                {
                    TripClass.SetSelection(i);
                }
            }

            editFAB.Click += EditFAB_Click;
            (FindViewById<Button>(Resource.Id.button_group_trip)).Click += ViewTripActivity_Click;
            (FindViewById<Button>(Resource.Id.button_bus_trip)).Click += ViewTripActivity_ClickBus; ;
        }

        private void ViewTripActivity_ClickBus(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BusActivity));
            Bundle b = new Bundle();
            b.PutString("TripName", trip.tripName);
            b.PutInt("tripCode", trip.tripCode);
            b.PutInt("year", trip.StartDate.Year);
            b.PutString("SchoolId", SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }

        private void ViewTripActivity_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GroupsActivity));
            Bundle b = new Bundle();
            b.PutString("TripName", trip.tripName);
            b.PutInt("tripCode", trip.tripCode);
            b.PutInt("year", trip.StartDate.Year);
            b.PutString("SchoolId", SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }

        private void EditFAB_Click(object sender, EventArgs e)
        {
            Intent UpTrAc = new Intent(this, typeof(UpdaterTripActivity));
            UpTrAc.PutExtra("TripCode", trip.tripCode);
            StartActivityForResult(UpTrAc, 0);
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
                trip = DataHelper.GetTripByCode(trip.tripCode, this);

                TripCodeET.Text = trip.tripCode.ToString();
                TripNameET.Text = trip.tripName;
                TripDescriptionET.Text = trip.tripDescription;
                TripPlace.Text = trip.place;
                TripStartDate.Text = trip.StartDate.ToShortDateString();
                TripEndDate.Text = trip.EndDate.ToShortDateString();

                for (int i = 0; i < TripClass.Adapter.Count; i++)
                {
                    if (((string)(TripClass.Adapter.GetItem(i))) == trip.classAge)
                    {
                        TripClass.SetSelection(i);
                    }
                }
            }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            // group activity open
            Intent intent = new Intent(this, typeof(ViewGroupActivity));
            Bundle b = new Bundle();
            b.PutInt("groupNum", 1);
            b.PutInt("tripCode", trip.tripCode);
            b.PutInt("year", trip.StartDate.Year);
            b.PutString("SchoolId", Helpers.SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivityForResult(intent,1);
            return true;
        }

    }
}