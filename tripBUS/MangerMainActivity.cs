using System;
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
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using tripBUS.Helpers;

namespace tripBUS
{
    [Activity(Label = "MangerMainActivity")]
    public class MangerMainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.maneger_layout);
            // Create your application here
            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_futer);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Future trips";

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_name)).Text = SavedData.loginMember.firstName + " "+SavedData.loginMember.lastName;
            ((navigationView.GetHeaderView(0)).FindViewById<TextView>(Resource.Id.nav_head_email)).Text = SavedData.loginMember.email;
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
            return true;
        }

        
    }
}