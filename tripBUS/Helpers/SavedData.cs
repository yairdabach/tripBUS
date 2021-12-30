using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tripBUS.Modles;

namespace tripBUS.Helpers
{
    public static class SavedData
    {
        public static TeamMember loginMember;
        public static Queue<Context> savedActivities;
        public static Context context;
        private static Android.Content.ISharedPreferences sharedPreferencesRefrence;

        public static void SetSharedPreferencesRefrence(Android.Content.ISharedPreferences sp) { sharedPreferencesRefrence = sp; }
        public static void DeleteSp()
        {
            var editor = sharedPreferencesRefrence.Edit();
            editor.PutString("email", null);
            editor.PutString("password",null);
            editor.Commit();
        }

    }
}