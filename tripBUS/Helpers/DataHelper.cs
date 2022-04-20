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
    /// <summary>
    /// DDL -  Data Layer
    /// </summary>
    public class DataHelper
    {
        
        private static SqlConnection conn;
        public static void createConacation()
        {
            if (conn == null)
            {
                conn = new SqlConnection(Constant.connectionString);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        //TeamMember function
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

        internal static List<Attendance> GetAllAtendece(int busNum, int tripCode, string schoolId, ViewBusActivity viewBusActivity)
        {
            //throw new NotImplementedException();
            return null;
        }

        internal static void DelStudent(string id, string school_ID, int lerningYear)
        {
            throw new NotImplementedException();
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
        public static bool UpdateTeamMember(TeamMember teamMember, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[TeamMember]");
                stringBuilder.Append(@"SET " +teamMember.ToStringUpdate() );
                stringBuilder.Append(@"Where Email='" + teamMember.email+"'");
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
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return false;
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
                        ((IDataRecord)reader).GetValue(0).ToString(),
                        ((IDataRecord)reader).GetValue(1).ToString(),
                        ((IDataRecord)reader).GetValue(2).ToString(),
                        ((IDataRecord)reader).GetValue(3).ToString(),
                        ((IDataRecord)reader).GetValue(4).ToString(),
                        ((IDataRecord)reader).GetValue(5).ToString(),
                        ((IDataRecord)reader).GetValue(6).ToString() );
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
        public static TeamMember GetTeamMember(string email, string schoolId, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where Email='" + email + "' AND schoolId='" + schoolId + "'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    TeamMember teamMember = new TeamMember(
                        ((IDataRecord)reader).GetValue(0).ToString(),
                        ((IDataRecord)reader).GetValue(1).ToString(),
                        ((IDataRecord)reader).GetValue(2).ToString(),
                        ((IDataRecord)reader).GetValue(3).ToString(),
                        ((IDataRecord)reader).GetValue(4).ToString(),
                        ((IDataRecord)reader).GetValue(5).ToString());
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

        //Trip Function
        public static void CreateNewTrip(Trip trip, Context context)
        {
            try
            {
                // add trip to table
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("INSERT INTO [dbo].[Trip] (TripName, TripDescription, ManegerEmail ,Place,ClassAge, TripStartDateDay, TripStartDateMonth, TripStartDateYear, TripEndDateDay, TripEndDateMonth, TripEndDateYear)");
                stringBuilder.Append(@"VALUES " + trip.ToString());
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();

                // get trip code
                string sqlQuer = @"SELECT MAX(TripCode) From [dbo].[Trip] Where ManegerEmail='" + trip.maengerEmail + "'";
                createConacation();
                cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.HasRows)
                {
                    trip.tripCode= int.Parse(((IDataRecord)reader).GetValue(0).ToString());
                    conn.Close();
                }
                else
                {
                    throw new NotImplementedException();
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

            }
        }

        //public static List<Trip> GetAllTripsByEmail(string manegerEmail, Context context)
        //{
        //    try
        //    {
        //        string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where ManegerEmail='" + manegerEmail + "' AND Password='" + password + "'";
        //        createConacation();
        //        var cmd = new SqlCommand(sqlQuer, conn);
        //        var reader = cmd.ExecuteReader();
        //        reader.Read();
        //        if (reader.HasRows)
        //        {
        //            TeamMember teamMember = new TeamMember(
        //                ((IDataRecord)reader).GetValue(0).ToString(),
        //                ((IDataRecord)reader).GetValue(1).ToString(),
        //                ((IDataRecord)reader).GetValue(2).ToString(),
        //                ((IDataRecord)reader).GetValue(3).ToString(),
        //                ((IDataRecord)reader).GetValue(4).ToString(),
        //                ((IDataRecord)reader).GetValue(5).ToString(),
        //                ((IDataRecord)reader).GetValue(6).ToString());
        //            conn.Close();
        //            SavedData.loginMember = teamMember;
        //            return teamMember;
        //        }
        //        else
        //        {
        //            conn.Close();
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //            //MySqlCommand cmd = new MySqlCommand();
        //        }
        //        Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
        //        return null;
        //    }
        //}
        public static Trip GetTripByCode(int code, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[Trip] Where TripCode='" + code.ToString() + "'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                   Trip trip = new Trip(
                        int.Parse(((IDataRecord)reader).GetValue(0).ToString()),
                        ((IDataRecord)reader).GetValue(2).ToString(),
                        ((IDataRecord)reader).GetValue(1).ToString(),
                        ((IDataRecord)reader).GetValue(3).ToString(),
                        ((IDataRecord)reader).GetValue(10).ToString(),
                        ((IDataRecord)reader).GetValue(11).ToString(),
                        new DateTime(int.Parse(((IDataRecord)reader).GetValue(6).ToString()), int.Parse(((IDataRecord)reader).GetValue(5).ToString()), int.Parse(((IDataRecord)reader).GetValue(4).ToString())),
                        new DateTime(int.Parse(((IDataRecord)reader).GetValue(9).ToString()), int.Parse(((IDataRecord)reader).GetValue(8).ToString()), int.Parse(((IDataRecord)reader).GetValue(7).ToString())),
                        int.Parse(((IDataRecord)reader).GetValue(12).ToString()),
                        int.Parse(((IDataRecord)reader).GetValue(13).ToString()),
                        int.Parse(((IDataRecord)reader).GetValue(14).ToString()));
                    conn.Close();
                    return trip;

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
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }

        }
        public static bool UpdateTrip(Trip trip, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + trip.ToStringUpdate());
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
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
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return false;
            }
        }

        public static int UpdateNumGroup(Trip trip, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countGroup='{trip.countGroup}'" );
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countGroup;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }
        public static int UpdateNumStusent(Trip trip, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countStudent='{trip.countStudent}'");
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countStudent;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }
        public static int UpdateNumBus(Trip trip, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countBus='{trip.countBus}'");
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countBus;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return -1;
            }
        }
        //Student function
        public static List<int>[] GetClassAgeInYear(string schoolID, int year, Context context)
        {
            List<int>[] classAges = new List<int>[13];
            try
            {
                string sqlQuer = $@"Select ClassNum,ClassAge from [dbo].[Student] Group By ClassAge,SchoolID,LerningYear,ClassNum,Show Having (SchoolID='{schoolID}' And LerningYear={year} And Show=0) ORDER BY ClassAge,ClassNum";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    
                    while(reader.Read())
                    {
                        int classAge = int.Parse(((IDataRecord)reader).GetValue(1).ToString());
                        int ClassNum = int.Parse(((IDataRecord)reader).GetValue(0).ToString());
                        if(classAges[classAge]== null)
                        {
                            classAges[classAge] = new List<int>();
                        }
                        classAges[classAge].Add(ClassNum);
                        Console.WriteLine(classAge+"|"+ ClassNum);
                    }
                    
                    conn.Close();
                    return classAges;
                }
                else
                {
                    conn.Close();
                    return classAges;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }
            
        }
        public static List<Student> GetStudentClassAgeInYear(int classAge, int classNum, string schoolID, int year, Context context)
        {
            List<Student> students = new List<Student>();
            try
            {
                string sqlQuer = $@"Select * from [dbo].[Student] Where (SchoolID='{schoolID}' And LerningYear={year} And Show=0 And  ClassNum={classNum} And ClassAge={classAge})";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    Student temp;
                    while (reader.Read())
                    {
                        temp = new Student(
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            ((IDataRecord)reader).GetValue(1).ToString(),
                            ((IDataRecord)reader).GetValue(0).ToString(),
                            ((IDataRecord)reader).GetValue(5).ToString(),
                            int.Parse(((IDataRecord)reader).GetValue(6).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(2).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(3).ToString())
                            );
                        students.Add(temp);
                    }

                    conn.Close();
                    return students;
                }
                else
                {
                    conn.Close();
                    return students;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }

        }

        public static Student GetStudentById(string id,Context context)
        {
            string sqlQuer = @"SELECT * FROM [dbo].[Student] Where studentID = '" + id + "'";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                Student temp = new Student(
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            ((IDataRecord)reader).GetValue(1).ToString(),
                            ((IDataRecord)reader).GetValue(0).ToString(),
                            ((IDataRecord)reader).GetValue(5).ToString(),
                            int.Parse(((IDataRecord)reader).GetValue(6).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(2).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(3).ToString())
                            );
                if (((IDataRecord)reader).GetValue(7).ToString()=="0")
                    return temp;
                conn.Close();
            }
            return null;
        }
        
        public static List<Student> GetStudentByGroup(int groupNum, int tripCode, Context context)
        {
            List<string> studentId = new List<string>();
            List<Student> students = new List<Student>();
            try
            {
                string sqlQuer = $@"Select studentID from [dbo].[StudentToGroup] Where (groupNum= {groupNum} And tripCode={tripCode})";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        studentId.Add(((IDataRecord)reader).GetValue(0).ToString()); 
                    }

                    conn.Close();

                    foreach (var student in studentId)
                    {
                        Student temp = GetStudentById(student, context);
                        if (temp != null)
                        {
                            students.Add(temp);
                        }
                    }
                    return students;
                }
                else
                {
                    conn.Close();
                    return students;
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
                return null;
            }

        }
        //GroupFun
        public static Group GetGroup(int GroupNum, int tripcode, string schoolID , Context context)
        {
            Group group = null;
            string sqlQuer = @"SELECT * FROM [dbo].[TripGroup] Where groupNum=" + GroupNum  + " AND tripCode = " + tripcode ;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                //create group from info
                group = new Group(
                            int.Parse(((IDataRecord)reader).GetValue(1).ToString()),
                            ((IDataRecord)reader).GetValue(3).ToString(),
                            int.Parse(((IDataRecord)reader).GetValue(2).ToString()),
                            ((IDataRecord)reader).GetValue(0).ToString(),
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            int.Parse(((IDataRecord)reader).GetValue(5).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(6).ToString())
                            );
                conn.Close ();
                group.teamMember = GetTeamMember(group.TeamMemberEmail, group.SchoolId, context);
                group.students = GetStudentByGroup(GroupNum, tripcode, context);
            }
            conn.Close();
            return group;
        }

        public static Bus GetBusInfo(int busNum, int tripcode, string schoolID, Context context)
        {
            Bus bus = null;
            string sqlQuer = @"SELECT * FROM [dbo].[Bus] Where busNum=" + busNum + " AND tripCode = " + tripcode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                //create group from info
                bus = new Bus(
                            int.Parse(((IDataRecord)reader).GetValue(0).ToString()),
                            int.Parse(((IDataRecord)reader).GetValue(1).ToString()),
                            ((IDataRecord)reader).GetValue(2).ToString(),
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            int.Parse(((IDataRecord)reader).GetValue(3).ToString())
                            );
            }
            conn.Close();
            return bus;
        }


        public static bool IfStudentConectedToGroup(string studentId, string schoolId, int year)
        {
            return true;
        }
    }


}