using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    [Activity(Label = "TeamMemberActivity")]
    public class TeamMemberActivity : AppCompatActivity
    {
        ListView TripteamMembersLV, SchoolTeamMemberLV;
        List<TeamMember> schoolTeamMembers, TripTeamMembers;
        int year, tripCode;
        string schoolId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.title_layout);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar_Bar);
            SetSupportActionBar(toolbar);

            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stubBar);
            stub.LayoutResource = Resource.Layout.team_member_layout;
            stub.Inflate();

            tripCode = Intent.GetIntExtra("tripCode", 0);
            year = Intent.GetIntExtra("year", 0);
            schoolId = Intent.GetStringExtra("SchoolId");

            SupportActionBar.Title = "Trip "+ tripCode;
            schoolTeamMembers = DataHelper.GetAllSchoolTeamMember(schoolId, this);
            TripTeamMembers = DataHelper.GetAllTripTeamMember(tripCode, schoolId, this);


            foreach (var schoolteamMember in schoolTeamMembers)
            {
                foreach (var tripteamMember in TripTeamMembers)
                {
                    if (schoolteamMember.email == tripteamMember.email)
                    {
                        schoolTeamMembers.Remove(schoolteamMember);
                    }
                }
            }

            TripteamMembersLV = FindViewById<ListView>(Resource.Id.lv_tripTeamBember_teamMaeber_layout);
            SchoolTeamMemberLV = FindViewById<ListView>(Resource.Id.lv_schoolTeamBember_teamMaeber_layout);

            //TripteamMembersLV.
        }
    }
}