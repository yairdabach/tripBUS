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

    public class TeamMemberViewTripActivity : AppCompatActivity
    {
        Trip trip;
        EditText TripCodeET,TripNameET, TripDescriptionET, TripStartDate, TripEndDate, TripPlace;
        Spinner TripClass;
        Google.Android.Material.FloatingActionButton.FloatingActionButton editFAB;
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        int tripCode, groupNum, busNum;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.navbar_layout);
            // Create your application here

            tripCode = Intent.GetIntExtra("TripCode", 0);
            groupNum = Intent.GetIntExtra("groupNum", 667);
            busNum = Intent.GetIntExtra("BusNum", 667);

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_futer);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Hello "+ SavedData.loginMember.firstName + "! | trip num: "+tripCode;

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.trip_layout;
            stub.Inflate();
            
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
            editFAB.Visibility = ViewStates.Gone;

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

            (FindViewById<Button>(Resource.Id.button_teamMembers_trip)).Visibility = ViewStates.Gone;

            if (groupNum != 667)
            {
                (FindViewById<Button>(Resource.Id.button_group_trip)).Click += ViewTripActivity_Click;
                (FindViewById<Button>(Resource.Id.button_group_trip)).Text = $"My Group | {groupNum}";
            }
            else
            {
                FindViewById<Button>(Resource.Id.button_group_trip).Visibility = ViewStates.Gone;
            }

            if (busNum != 667)
            {
                (FindViewById<Button>(Resource.Id.button_bus_trip)).Text = $"My Bus | {busNum}";
                (FindViewById<Button>(Resource.Id.button_bus_trip)).Click += ViewTripActivity_ClickBus;
            }
            else
            {
                (FindViewById<Button>(Resource.Id.button_bus_trip)).Visibility = ViewStates.Gone;
            }
            
            
        }

        private void ViewTripActivity_ClickBus(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewBusActivity));
            Bundle b = new Bundle();
            b.PutInt("busNum", busNum);
            b.PutInt("Status", 4);
            b.PutInt("tripCode", tripCode);
            b.PutInt("year", trip.StartDate.Year);
            b.PutString("SchoolId", SavedData.loginMember.schoolID);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }

        private void ViewTripActivity_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewGroupActivity));
            Bundle b = new Bundle();
            b.PutInt("groupNum", groupNum);
            b.PutInt("tripCode", tripCode);
            b.PutInt("year", trip.StartDate.Year);
            b.PutString("SchoolId", SavedData.loginMember.schoolID);
            b.PutInt("meneger", 1);
            intent.PutExtras(b);
            StartActivityForResult(intent, 0);
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

        }



    }
}