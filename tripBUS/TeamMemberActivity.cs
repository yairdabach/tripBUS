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
        List<TeamMember> listTeam = new List<TeamMember>(), add = new List<TeamMember>(), del = new List<TeamMember>();
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

            SupportActionBar.Title = "Trip "+ tripCode + " |  Team Members" ;
            schoolTeamMembers = DataHelper.GetAllSchoolTeamMember(schoolId, this);
            TripTeamMembers = DataHelper.GetTripTeamMembers(tripCode, SavedData.loginMember.schoolID, this);

            List<TeamMember> teamMembersTemp = new List<TeamMember>();
            foreach (TeamMember teamMember in schoolTeamMembers)
            {
                teamMembersTemp.Add(teamMember);
            }


            for (int i = 0; i < teamMembersTemp.Count; i++)
            {
                for (int j = 0; j < TripTeamMembers.Count; j++)
                {
                    if (teamMembersTemp[i].email == TripTeamMembers[j].email)
                    {
                        schoolTeamMembers.Remove(teamMembersTemp[i]);
                    }
                    if (teamMembersTemp[i].email == SavedData.loginMember.email)
                    {
                        schoolTeamMembers.Remove(teamMembersTemp[i]);
                    }
                }
            }

            for (int i = 0; i < TripTeamMembers.Count; i++)
            {
                listTeam.Add(TripTeamMembers[i]);
            }

            TripteamMembersLV = FindViewById<ListView>(Resource.Id.lv_tripTeamBember_teamMaeber_layout);
            SchoolTeamMemberLV = FindViewById<ListView>(Resource.Id.lv_schoolTeamBember_teamMaeber_layout);

            // 1 for trip,2 for school
            TripteamMembersLV.Adapter = new TeamMemberAdapter(this,listTeam,1);
            SchoolTeamMemberLV.Adapter = new TeamMemberAdapter(this,schoolTeamMembers,2);

            (FindViewById<Google.Android.Material.FloatingActionButton.FloatingActionButton>(Resource.Id.save_team_fav)).Click += TeamMemberActivity_Click; ;
        }

        private void TeamMemberActivity_Click(object sender, EventArgs e)
        {
            DataHelper.UpdateTeamMemberTrip(add, del, tripCode, this);
            Finish();
        }

        public void AddTripTeamMember(TeamMember teamMember)
        {
            if (del.Contains(teamMember))
            {
                del.Remove(teamMember);
                
            }
            else
            {
                add.Add(teamMember);
            }
            listTeam.Add(teamMember);

            schoolTeamMembers.Remove(teamMember);
            TripteamMembersLV.Adapter = new TeamMemberAdapter(this, listTeam, 1);
            SchoolTeamMemberLV.Adapter = new TeamMemberAdapter(this, schoolTeamMembers, 2);
        }

        public void DelateTripTeamMember(TeamMember teamMember)
        { 
            if(add.Contains(teamMember))
            {
                add.Remove(teamMember);
            }
            else
            {
                del.Add(teamMember);
            }
            listTeam.Remove(teamMember);
            schoolTeamMembers.Add(teamMember); 
            TripteamMembersLV.Adapter = new TeamMemberAdapter(this, listTeam, 1);
            SchoolTeamMemberLV.Adapter = new TeamMemberAdapter(this, schoolTeamMembers, 2);
        }


    }
}