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
    public class BusActivity : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        LinearLayout faterLayout;
        List<Bus> buss;
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

            //get data from prev activity
            SchoolID = Intent.GetStringExtra("SchoolId");
            tripCode = Intent.GetIntExtra("tripCode", 0);
            tripName = Intent.GetStringExtra("TripName");
            year = Intent.GetIntExtra("year",0);

            //activity title
            SupportActionBar.Title = "Buss | "+ tripName;

            //set scerrn
            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.layout_empty;
            stub.Inflate();

            //Add father dainamecky layout for floating button
            faterLayout = FindViewById<LinearLayout>(Resource.Id.layout_empty);

            var Frame = new FrameLayout(this);
            Frame.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.MatchParent);
            faterLayout.AddView(Frame);
            faterLayout= new LinearLayout(this);
            faterLayout.LayoutParameters = new FrameLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            Frame.AddView(faterLayout);
            faterLayout.Orientation = Orientation.Vertical;

            // set floting button
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

        // fun for desing sreen -> get all bus and crate for them buttons
        private void createScreen()
        {
            faterLayout.RemoveAllViewsInLayout();
            buttons = new List<Button>();
            buss = DataHelper.GetAllBuss(tripCode, SchoolID, this);
            int margin = 30;
            foreach (var bus in buss)
            {
                Button temp = new Button(this);
                temp.SetBackgroundResource(Resource.Drawable.button3_style);
                temp.Text = "Bus Num: "+ bus.busNum +" | "+ bus.BusName;
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

        //Add new bus function
        private void PlusFAB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ViewGroupActivity));
            Bundle b = new Bundle();
            int max = 0;
            foreach (Bus bus in buss)
            {
                if (max < bus.busNum)
                {
                    max = bus.busNum;
                }
            }
            Intent intenti = new Intent(this, typeof(ViewBusActivity));
            Bundle bu = new Bundle();
            bu.PutInt("busNum", max+1);
            bu.PutInt("Status", 3);
            bu.PutInt("tripCode", tripCode);
            bu.PutInt("year", year);
            bu.PutString("SchoolId", SchoolID);
            intenti.PutExtras(bu);
            StartActivityForResult(intenti, 1);
        }

        //open Bus info Activity
        private void Temp_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i]==sender)
                {
                    Intent intent = new Intent(this, typeof(ViewBusActivity));
                    Bundle b = new Bundle();
                    b.PutInt("busNum", buss[i].busNum);
                    b.PutInt("Status", 2);
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