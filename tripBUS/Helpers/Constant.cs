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

namespace tripBUS.Helpers
{
    public static class Constant
    {
        #region MYSQL CONACTION STRING
        public static string connectionString = "Server=tcp:tripbusserver.database.windows.net,1433;Initial Catalog = tripbus; Persist Security Info=False;User ID = admintripbus; Password=tripBus!; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        #endregion

    }
}