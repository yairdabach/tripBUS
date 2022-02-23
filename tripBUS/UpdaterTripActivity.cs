using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
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
    [Activity(Label = "UpdaterTripActivity")]
    public class UpdaterTripActivity : AppCompatActivity
    {
        EditText TripNameET, TripDescriptionET, TripStartDate, TripEndDate, TripPlace;
        Object Last;
        Spinner TripClass;
        DateTime startDate, endDate;
        Button btnStartDate, btnEndDate;
        Google.Android.Material.FloatingActionButton.FloatingActionButton saveFAB;
        Trip trip;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "New trip";

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.trip_layout;
            stub.Inflate();

            // Create your application here
            var TripCodeET = FindViewById<TextInputLayout>(Resource.Id.til_TripCode_trip);
            TripCodeET.Visibility = ViewStates.Invisible;

            TripNameET = FindViewById<EditText>(Resource.Id.et_TripName_trip);
            TripDescriptionET = FindViewById<EditText>(Resource.Id.et_TripDes_trip);
            TripStartDate = FindViewById<EditText>(Resource.Id.et_startdate_trip);
            TripEndDate = FindViewById<EditText>(Resource.Id.et_end_trip);
            TripPlace = FindViewById<EditText>(Resource.Id.et_place_trip);
            TripClass = FindViewById<Spinner>(Resource.Id.spnr_class_trip);
            saveFAB = FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.save_trip_fav);

            var items = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            TripClass.Adapter = adapter;

            int tripCode = Intent.GetIntExtra("TripCode", 0);

            if (tripCode == 0)
            {
                Toast.MakeText(this, "ERROE", ToastLength.Long);
            }
            else
            {
                trip = DataHelper.GetTripByCode(tripCode, this);
            }


            TripNameET.Text = trip.tripName;
            TripDescriptionET.Text = trip.tripDescription;
            TripPlace.Text = trip.place;
            TripStartDate.Text = trip.StartDate.ToShortDateString();
            TripEndDate.Text = trip.EndDate.ToShortDateString();

            for (int i = 0; i < adapter.Count; i++)
            {
                if (((string)(adapter.GetItem(i))) == trip.classAge)
                {
                    TripClass.SetSelection(i);
                }
            }

            btnEndDate = FindViewById<Button>(Resource.Id.btn_endDate);
            btnStartDate = FindViewById<Button>(Resource.Id.btn_startDate);

            btnEndDate.Click += TripDate_Click;
            btnStartDate.Click += TripDate_Click;
            saveFAB.Click += SaveFAB_Click;
        }

        private void SaveFAB_Click(object sender, EventArgs e)
        {
            bool valid = true;
            if (TripNameET.Text == "")
            {
                TripNameET.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                TripNameET.Error = "must fill it";
                valid = false;
            }
            else
            {
                TripNameET.SetError("", null);
                TripNameET.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                TripNameET.Error = null;
            }

            if (TripStartDate.Text == "")
            {
                TripStartDate.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                TripStartDate.Error = "must fill it";
                valid = false;
            }
            else
            {
                TripStartDate.SetError("", null);
                TripStartDate.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                TripStartDate.Error = null;
            }

            if (TripEndDate.Text == "" || DateTime.Compare(startDate, endDate) > 0)
            {
                TripEndDate.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                TripEndDate.Error = "you didn't fill or the date befor the start date";
                valid = false;
            }
            else
            {
                TripEndDate.SetError("", null);
                TripEndDate.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                TripEndDate.Error = null;
            }

            if (TripPlace.Text == "")
            {
                TripPlace.Background.SetColorFilter(Android.Graphics.Color.Red, PorterDuff.Mode.SrcAtop);
                TripPlace.Error = "you didn't fill or the date befor the start date";
                valid = false;
            }
            else
            {
                TripPlace.SetError("", null);
                TripPlace.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.Src);
                TripPlace.Error = null;
            }


            if (valid)
            {
                trip.tripName = TripNameET.Text;
                trip.tripDescription = TripDescriptionET.Text;
                trip.place = TripPlace.Text;
                trip.StartDate = startDate;
                trip.EndDate = endDate;
                trip.classAge = TripClass.SelectedItem.ToString();

                DataHelper.UpdateTrip(trip, this);
                FinishActivity(0);
                Finish();
            }

        }

        private void TripDate_Click(object sender, EventArgs e)
        {
            DateTime today;
            if (TripStartDate.Text == "")
                today = DateTime.Today;
            else
                today = startDate.Date;

            Last = sender;
            DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
            datePickerDialog.Show();

        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            if (Last == btnStartDate) { startDate = e.Date; TripStartDate.Text = e.Date.ToShortDateString(); Last = null; }
            else { if (Last == btnEndDate) { endDate = e.Date; TripEndDate.Text = e.Date.ToShortDateString(); Last = null; } }
        }
    }
}