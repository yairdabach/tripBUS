using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tripBUS.Modles;

namespace tripBUS.Helpers
{
    public class DataHelper
    {
        
        private static SqlConnection conn;
        public static void createConacation()
        {
            if (conn == null)
            {
                conn = new SqlConnection(Constant.connectionString);
            }

            //conn.Open();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                //MySqlCommand cmd = new MySqlCommand();
            }
        }
        public static int ManegerHasEmail(string email, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where Email='" + email + "'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return 1;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return 2;
            }

        }
        public static void AddTeamMember (TeamMember teamMember,Context context )
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("INSERT INTO [dbo].[TeamMember] (FirstName, LastName, SchoolID, Kidomet, Phone, Email, Password)");
                stringBuilder.Append(@"VALUES " + teamMember.ToString());
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                
            }
        }
        public static TeamMember Login(string email,string password, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where Email='" + email + "' AND Password='"+password+"'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    TeamMember teamMember = new TeamMember(
                        ((IDataRecord)reader).GetValue(1).ToString(),
                        ((IDataRecord)reader).GetValue(0).ToString(),
                        ((IDataRecord)reader).GetValue(2).ToString(),
                        ((IDataRecord)reader).GetValue(3).ToString(),
                        ((IDataRecord)reader).GetValue(4).ToString(),
                        ((IDataRecord)reader).GetValue(5).ToString(),
                        ((IDataRecord)reader).GetValue(6).ToString() );
                    //SavedData.loginMember = teamMember;
                    conn.Close();
                    SavedData.loginMember = teamMember;
                    return teamMember;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }

        }
        public static string GetPhoneByEmail(string email,Context context)
        {
            try
            {
                string sqlQuer = @"SELECT Kidomet, Phone FROM [dbo].[TeamMember] Where Email='" + email + "'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    string phone = ((IDataRecord)reader).GetValue(1).ToString() + ((IDataRecord)reader).GetValue(0).ToString();

                    //SavedData.loginMember = teamMember;
                    conn.Close();
                    
                    return phone;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }
        }
        public static bool UpadtePhone(string password, string email, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[TeamMember]");
                stringBuilder.Append("SET Password='"+password+"'");
                stringBuilder.Append("WHERE Email='" + email + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                return false;

            }
        }
    }


}