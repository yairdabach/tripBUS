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
            conn.Close();
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

        public static void AddTeamMember(TeamMember teamMember, Context context)
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
                stringBuilder.Append(@"SET " + teamMember.ToStringUpdate());
                stringBuilder.Append(@"Where Email='" + teamMember.email + "'");
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
        public static TeamMember Login(string email, string password, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where Email='" + email + "' AND Password='" + password + "'";
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
                        ((IDataRecord)reader).GetValue(6).ToString());
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
                string sqlQuer = @"SELECT * FROM [dbo].[TeamMember] Where Email='" + email.Replace(" ","") + "' AND schoolId='" + schoolId + "'";
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


        public static string GetPhoneByEmail(string email, Context context)
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
                stringBuilder.Append("SET Password='" + password + "'");
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

        public static List<TeamMember> GetAllSchoolTeamMember(string schoolId, Context context)
        {
           var list = new List<TeamMember>();
            try
            {
                string sqlQuer = @$"SELECT * FROM [dbo].[TeamMember] Where schoolId='{schoolId}' ";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new TeamMember(
                        ((IDataRecord)reader).GetValue(0).ToString(),
                        ((IDataRecord)reader).GetValue(1).ToString(),
                        ((IDataRecord)reader).GetValue(2).ToString(),
                        ((IDataRecord)reader).GetValue(3).ToString(),
                        ((IDataRecord)reader).GetValue(4).ToString(),
                        ((IDataRecord)reader).GetValue(5).ToString()));
                    }
                }

            }
            catch { }
            return list;
        }

        public static List<TeamMember> GetAllTripTeamMember(int tripCode, string schoolId, Context context)
        {
            var emails = new List<string>();
            var list = new List<TeamMember>();
            try
            {
                string sqlQuer = @$"SELECT teamMemberId FROM [dbo].[TeamMeaberTrip] Where tripCode={tripCode} ";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        emails.Add(reader.GetValue(0).ToString());
                    }
                }
                foreach (var email in emails)
                {
                    list.Add(GetTeamMember(email, schoolId, context));
                }
            }
            catch { }
            return list;
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
                    trip.tripCode = CangeToInt(((IDataRecord)reader).GetValue(0).ToString());
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
        public static List<Trip> GetAllTrips(string memberEmail, Context context)
        {
            List<Trip> trips = new List<Trip>();
            try
            {
                string sqlQuer = @$"SELECT * FROM [dbo].[Trip] Where ManegerEmail='{memberEmail}' ";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trips.Add(new Trip(
                            CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                            ((IDataRecord)reader).GetValue(2).ToString(),
                            ((IDataRecord)reader).GetValue(1).ToString(),
                            ((IDataRecord)reader).GetValue(3).ToString(),
                            ((IDataRecord)reader).GetValue(10).ToString(),
                            ((IDataRecord)reader).GetValue(11).ToString(),
                            new DateTime(CangeToInt(((IDataRecord)reader).GetValue(6).ToString()), CangeToInt(((IDataRecord)reader).GetValue(5).ToString()), CangeToInt(((IDataRecord)reader).GetValue(4).ToString())),
                            new DateTime(CangeToInt(((IDataRecord)reader).GetValue(9).ToString()), CangeToInt(((IDataRecord)reader).GetValue(8).ToString()), CangeToInt(((IDataRecord)reader).GetValue(7).ToString())),
                            CangeToInt(((IDataRecord)reader).GetValue(12).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(13).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(14).ToString())));
                    }
                }
            }
            catch (Exception ex) { }
            return trips;
        }

        public static List<Trip> GetAllTripsInYear(string memberEmail, int year, Context context)
        {
            List<Trip> trips = new List<Trip>();
            try
            {
                string sqlQuer = @$"SELECT * FROM [dbo].[Trip] Where TripStartDateYear={year} And ManegerEmail='{memberEmail}' ";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trips.Add(new Trip(
                            CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                            ((IDataRecord)reader).GetValue(2).ToString(),
                            ((IDataRecord)reader).GetValue(1).ToString(),
                            ((IDataRecord)reader).GetValue(3).ToString(),
                            ((IDataRecord)reader).GetValue(10).ToString(),
                            ((IDataRecord)reader).GetValue(11).ToString(),
                            new DateTime(CangeToInt(((IDataRecord)reader).GetValue(6).ToString()), CangeToInt(((IDataRecord)reader).GetValue(5).ToString()), CangeToInt(((IDataRecord)reader).GetValue(4).ToString())),
                            new DateTime(CangeToInt(((IDataRecord)reader).GetValue(9).ToString()), CangeToInt(((IDataRecord)reader).GetValue(8).ToString()), CangeToInt(((IDataRecord)reader).GetValue(7).ToString())),
                            CangeToInt(((IDataRecord)reader).GetValue(12).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(13).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(14).ToString())));
                    }
                }
            }
            catch (Exception ex) { }
            return trips;
        }
        public static Trip GetTripByCode(int code, Context context)
        {
            try
            {
                string sqlQuer = @"SELECT * FROM [dbo].[Trip] Where TripCode='" + code.ToString() + "'";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Trip trip = new Trip(
                         CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                         ((IDataRecord)reader).GetValue(2).ToString(),
                         ((IDataRecord)reader).GetValue(1).ToString(),
                         ((IDataRecord)reader).GetValue(3).ToString(),
                         ((IDataRecord)reader).GetValue(10).ToString(),
                         ((IDataRecord)reader).GetValue(11).ToString(),
                         new DateTime(CangeToInt(((IDataRecord)reader).GetValue(6).ToString()), CangeToInt(((IDataRecord)reader).GetValue(5).ToString()), CangeToInt(((IDataRecord)reader).GetValue(4).ToString())),
                         new DateTime(CangeToInt(((IDataRecord)reader).GetValue(9).ToString()), CangeToInt(((IDataRecord)reader).GetValue(8).ToString()), CangeToInt(((IDataRecord)reader).GetValue(7).ToString())),
                         CangeToInt(((IDataRecord)reader).GetValue(12).ToString()),
                         CangeToInt(((IDataRecord)reader).GetValue(13).ToString()),
                         CangeToInt(((IDataRecord)reader).GetValue(14).ToString()));
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

        public static int UpdateNumGroup(int tripcode, int add, Context context)
        {
            try
            {
                Trip trip = GetTripByCode(tripcode, context);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countGroups='{trip.countGroup+add}'");
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countGroup+add;
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
        public static int UpdateNumStusent(int tripcode, int add, Context context)
        {
            try
            {
                Trip trip = GetTripByCode(tripcode, context);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countStudent='{trip.countStudent + add}'");
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countStudent+add;
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
        public static int UpdateNumBus(int tripcode, int add, Context context)
        {
            try
            {
                Trip trip = GetTripByCode(tripcode, context);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("UPDATE [dbo].[Trip]");
                stringBuilder.Append(@"SET " + $"countBus='{trip.countBus+add}'");
                stringBuilder.Append(@"Where ManegerEmail='" + trip.maengerEmail + "' AND TripCode='" + trip.tripCode + "'");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return trip.countBus+add;
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

                    while (reader.Read())
                    {
                        int classAge = CangeToInt(((IDataRecord)reader).GetValue(1).ToString());
                        int ClassNum = CangeToInt(((IDataRecord)reader).GetValue(0).ToString());
                        if (classAges[classAge] == null)
                        {
                            classAges[classAge] = new List<int>();
                        }
                        classAges[classAge].Add(ClassNum);
                        Console.WriteLine(classAge + "|" + ClassNum);
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
                            CangeToInt(((IDataRecord)reader).GetValue(6).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(2).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(3).ToString())
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

        internal static void AddStudent(Student student, Context context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO [dbo].[Student] (StudentId, LerningYear,SchoolId, LastName, FirstName,ClassAge,classNum)");
            stringBuilder.Append(@"VALUES " + $"('{student.Id}',{student.LerningYear},'{student.School_ID}','{student.Last_Name}','{student.First_Name}',{student.ClassAge},{student.ClassNum})");
            createConacation();
            var cmd = new SqlCommand(stringBuilder.ToString(), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        internal static void DelStudent(string id, string school_ID, int lerningYear, Context context)
        {
            string comand = $"DELETE [dbo].[Student] where StudentId = '{id}' And LerningYear = {lerningYear} And SchoolId = '{school_ID}'";
            createConacation();
            var cmd = new SqlCommand(comand, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            foreach(TeamMember member in GetAllSchoolTeamMember(school_ID,context))
            {
                foreach (Trip item in GetAllTripsInYear(member.email, lerningYear,context))
                {
                    comand = $"DELETE [dbo].[StudentToGroup] where StudentId = '{id}' And TripCode={item.tripCode}";
                    createConacation();
                    cmd = new SqlCommand(comand, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        internal static void UpdateStudent(Student student, Context context)
        {
            string comand = $"Update [dbo].[Student] SET LastName = '{student.Last_Name}', FirstName = '{student.First_Name}', ClassAge = {student.ClassAge}, classNum = {student.ClassNum} where StudentId = '{student.Id}' And LerningYear = {student.LerningYear} And SchoolId = '{student.School_ID}'";
            createConacation();
            var cmd = new SqlCommand(comand, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static Student GetStudentById(string id, string schoolId, int year, Context context)
        {
            string sqlQuer = @$"SELECT * FROM [dbo].[Student] Where StudentID = '{id}' And LerningYear = {year} And SchoolID='{schoolId}'";
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
                            CangeToInt(((IDataRecord)reader).GetValue(6).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(2).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(3).ToString())
                            );
                if (((IDataRecord)reader).GetValue(7).ToString() == "0")
                    return temp;
                conn.Close();
            }
            return null;
        }

        public static List<Student> GetStudentByGroup(int groupNum, int tripCode, int year, string schoolID, Context context)
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
                        Student temp = GetStudentById(student, schoolID, year, context);
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

        public static List<string> GetAllGroupStudent(int groupNum, int tripCode, Context context)
        {
            List<string> students = new List<string>();
            try
            {
                string sqlQuer = $@"Select studentID from [dbo].[StudentToGroup] Where ( Not (groupNum = {groupNum}) ) And tripCode={tripCode}";
                createConacation();
                var cmd = new SqlCommand(sqlQuer, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        students.Add(((IDataRecord)reader).GetValue(0).ToString());
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
        public static int MinYearSchool(string schoolId, Context context)
        {
            int year = 0;
            string sqlQuer = @$"SELECT Min(LerningYear) FROM [dbo].[Student] Where SchoolID='{schoolId}'";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                year = CangeToInt(((IDataRecord)reader).GetValue(0).ToString());
            }
            if (year ==0)
            {
                year = DateTime.Today.Year;
            }
            return year;
        }

        //GroupFun
        public static Group GetGroup(int GroupNum, int tripcode, int year, string schoolID, Context context)
        {
            Group group = null;
            string sqlQuer = @"SELECT * FROM [dbo].[TripGroup] Where groupNum=" + GroupNum + " AND tripCode = " + tripcode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                //create group from info
                group = new Group(
                            CangeToInt(((IDataRecord)reader).GetValue(1).ToString()),
                            ((IDataRecord)reader).GetValue(3).ToString(),
                            CangeToInt(((IDataRecord)reader).GetValue(2).ToString()),
                            ((IDataRecord)reader).GetValue(0).ToString(),
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            CangeToInt(((IDataRecord)reader).GetValue(5).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(6).ToString())
                            );
                conn.Close();
                group.teamMember = GetTeamMember(group.TeamMemberEmail, group.SchoolId, context);
                group.students = GetStudentByGroup(GroupNum, tripcode, year, schoolID, context);
            }
            conn.Close();
            return group;
        }
        public static int GetGroupBus(int GroupNum, int tripcode, string schoolID, Context context)
        {
            string sqlQuer = @"SELECT BusNum FROM [dbo].[TripGroup] Where groupNum=" + GroupNum + " AND tripCode = " + tripcode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int i = CangeToInt(((IDataRecord)reader).GetValue(0).ToString());
                conn.Close();
                return i;
            }
            conn.Close();
            return 667;
        }

        public static List<Group> GetAllGroups(int tripcode, string schoolID, Context context)
        {
            List<Group> groups = new List<Group>();
            string sqlQuer = @"SELECT * FROM [dbo].[TripGroup] Where tripCode = " + tripcode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //create group from info
                while (reader.Read())
                {
                    groups.Add(new Group(
                                CangeToInt(((IDataRecord)reader).GetValue(1).ToString()),
                                ((IDataRecord)reader).GetValue(3).ToString(),
                                CangeToInt(((IDataRecord)reader).GetValue(2).ToString()),
                                ((IDataRecord)reader).GetValue(0).ToString(),
                                ((IDataRecord)reader).GetValue(4).ToString(),
                                CangeToInt(((IDataRecord)reader).GetValue(5).ToString()),
                                CangeToInt(((IDataRecord)reader).GetValue(6).ToString())
                                ));
                }

            }
            conn.Close();
            return groups;
        }

        public static void AddNewGroup(Group group, List<Student> students, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                stringBuilder.Append("INSERT INTO [dbo].[TripGroup] (groupNum, groupName,tripCode, schoolID, teamMember,BusNum)");
                stringBuilder.Append(@"VALUES " + group.ToString());
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                GroupStudentChange(students, new List<Student>(), students.Count, group, context);
                UpdateNumGroup(group.tripCode, 1,  context);
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

        public static void UpdateGroup(Group group, List<Student> AddStudent, List<Student> RemoveStudent, Context context)
        {
            try
            {
                string email;
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                if (group.teamMember == null)
                {
                    email = "";
                }
                else
                {
                    email = group.teamMember.email;
                }
                string comand = $"Update [dbo].[TripGroup] SET groupName='{group.Name}', teamMember ='{email}', BusNum ={group.BusNumber},countStudent={group.amoountOfstudent + AddStudent.Count - RemoveStudent.Count} Where groupNum = {group.GroupNum} AND tripCode={group.tripCode}";
                createConacation();
                var cmd = new SqlCommand(comand, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                GroupStudentChange(AddStudent, RemoveStudent , group.amoountOfstudent, group, context);
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

        public static void GroupStudentChange(List<Student> Add, List<Student> Del, int amount, Group group, Context context)
        {
            if (Add.Count>0)
            {
                foreach (Student student in Add)
                {
                    AddStudentToGroup(student, group.GroupNum, group.tripCode, context);
                }
            }
            if (Del.Count > 0)
            {
                foreach (Student student in Del)
                {
                    DeleatStudentToGroup(student, group.GroupNum, group.tripCode, context);
                }
            }
            UpdateNumStusent(group.tripCode, Add.Count - Del.Count, context);

        }
        public static void AddStudentToGroup(Student student,int GroupNum,int tripCode, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"INSERT INTO [dbo].[StudentToGroup] (groupNum, studentID ,tripCode , schoolID)");
                stringBuilder.Append(@$"VALUES({GroupNum}, '{student.Id}' ,{tripCode} , '{student.School_ID}')");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
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

        public static void DeleatStudentToGroup(Student student, int GroupNum, int tripCode, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"DELETE FROM [dbo].[StudentToGroup] Where groupNum={GroupNum} And tripCode= {tripCode} And studentID='{student.Id}'");
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

        //Bus Fun

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
                            CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(1).ToString()),
                            ((IDataRecord)reader).GetValue(2).ToString(),
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            CangeToInt(((IDataRecord)reader).GetValue(3).ToString())
                            );
            }
            conn.Close();
            return bus;
        }

        public static void AddNewBus(Bus bus, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                stringBuilder.Append("INSERT INTO [dbo].[Bus] (busNum, tripCode, schoolId, studentCount, BusNmae)");
                stringBuilder.Append($@"VALUES ({bus.busNum}, {bus.tripNum}, '{bus.schoolId}', {bus.countStudent}, '{bus.BusName}')" );
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                UpdateNumBus(bus.tripNum, 1, context);
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
        public static void UpdateBusInfo(Bus bus, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                string comand = $"Update [dbo].[Bus] SET BusNmae='{bus.BusName}' Where busNum = {bus.busNum} AND tripCode={bus.tripNum}";
                createConacation();
                var cmd = new SqlCommand(comand, conn);
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

        public static List<Bus> GetAllBuss(int tripcode, string schoolID, Context context)
        {
            List<Bus> buss = new List<Bus>();
            string sqlQuer = @"SELECT * FROM [dbo].[Bus] Where tripCode = " + tripcode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //create group from info
                while (reader.Read())
                {
                    buss.Add( new Bus(
                            CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                            CangeToInt(((IDataRecord)reader).GetValue(1).ToString()),
                            ((IDataRecord)reader).GetValue(2).ToString(),
                            ((IDataRecord)reader).GetValue(4).ToString(),
                            CangeToInt(((IDataRecord)reader).GetValue(3).ToString())
                            ));
                }

            }
            conn.Close();
            return buss;
        }
        public static List<Student> GetStudentByBus(int busNum, int tripCode, int year, string schoolID, Context context)
        {
            List<int> GroupNums = new List<int>();
            List<Student> students = new List<Student>();
            string sqlQuer = @"SELECT groupNum FROM [dbo].[TripGroup] Where busNum=" + busNum + " AND tripCode = " + tripCode;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    GroupNums.Add(CangeToInt(((IDataRecord)reader).GetValue(0).ToString()));
                }
                conn.Close();
                foreach (int i in GroupNums)
                {
                    foreach (Student student in GetStudentByGroup(i, tripCode, year, schoolID, context))
                    {
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        ///Atendece
        public static void AddAtendace(Attendance attendance,StudentAttendance student, Context context)
        {
            try
            {
                int attend = student.isAttendance ? 1 : 0;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"INSERT INTO [dbo].[Attendance] (tripCode, schoolID ,busNum , dateOfCheek, descriptionCheek, studentID, attend)");
                stringBuilder.Append(@$"VALUES ({attendance.tripCode}, '{attendance.schoolID}' ,{attendance.busNum} , '{attendance.DateTime.ToString("u").Replace("Z", "")}','{attendance.descriotionCheek}','{student.Id}',{attend})");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
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

        public static void UpdateAtendeceInfo(Attendance attendance, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                string comand = $"Update [dbo].[Attendance] SET descriptionCheek='{attendance.descriotionCheek}' Where busNum = {attendance.busNum} AND tripCode={attendance.tripCode} And dateOfCheek ='{attendance.DateTime.ToString("u").Replace("Z", "")}'";
                Console.WriteLine(comand);
                createConacation();
                var cmd = new SqlCommand(comand, conn);
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

        public static void UpdateAtendeceStudent(Attendance attendance,StudentAttendance student, Context context)
        {
            try
            {
                int attend = student.isAttendance ? 1 : 0;
                StringBuilder stringBuilder = new StringBuilder();
                //({GroupNum},{Name},{tripCode},{SchoolId},{TeamMemberEmail})";
                string comand = $"Update [dbo].[Attendance] SET attend='{attend}' Where studentID='{student.Id}' AND busNum = {attendance.busNum} AND tripCode={attendance.tripCode} And dateOfCheek ='{attendance.DateTime.ToString("u").Replace("Z", "")}'";
                Console.WriteLine(comand);
                createConacation();
                var cmd = new SqlCommand(comand, conn);
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

        public static List<Attendance> GetAllAttendaceForBus(int tripCode, int busNum, Context context)
        {
            //Select   dateOfCheek , descriptionCheek From [dbo].[Attendance] where tripCode=1 AND busNum=1 Group By dateOfCheek, descriptionCheek
            List<Attendance> attendances = new List<Attendance>();
            string sqlQuer = @$"Select dateOfCheek , descriptionCheek From [dbo].[Attendance] where tripCode={tripCode} AND busNum={busNum} Group By dateOfCheek, descriptionCheek";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    attendances.Add(new Attendance(
                        tripCode,
                        busNum,
                        DateTime.Parse(((IDataRecord)reader).GetValue(0).ToString()),
                        ((IDataRecord)reader).GetValue(1).ToString())
                        );
                }
            }   
            return attendances;
        }
        public static Attendance GetAttendace(int tripCode, int busNum, DateTime dateTime, int year, string schoolID, Context context)
        {
            Attendance attendance = null;
            string sqlQuer = @$"SELECT * FROM [dbo].[Attendance] Where busNum={busNum}  AND tripCode = { tripCode } And dateOfCheek='{dateTime.ToString("u").Replace("Z", "")}'";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    if (attendance == null)
                    {
                        attendance = new Attendance(
                                        CangeToInt(((IDataRecord)reader).GetValue(0).ToString()),
                                        ((IDataRecord)reader).GetValue(1).ToString(),
                                        CangeToInt(((IDataRecord)reader).GetValue(2).ToString()),
                                        DateTime.Parse(((IDataRecord)reader).GetValue(3).ToString()),
                                        ((IDataRecord)reader).GetValue(4).ToString()
                                        );
                        attendance.Attendances = new List<StudentAttendance>();
                    }

                    attendance.Attendances.Add(new StudentAttendance(
                        ((IDataRecord)reader).GetValue(5).ToString(),
                         Convert.ToBoolean(CangeToInt(((IDataRecord)reader).GetValue(6).ToString())))
                        );
                    Console.WriteLine(Convert.ToBoolean(CangeToInt(((IDataRecord)reader).GetValue(6).ToString())));

                }
                conn.Close();

                for (int i = 0; i < attendance.Attendances.Count; i++)
                {

                    attendance.Attendances[i] = new StudentAttendance(GetStudentById(attendance.Attendances[i].Id, schoolID, year, context), attendance.Attendances[i].isAttendance);
                }

            }
            return attendance;
        }

        public static void DeleatAttendace(Attendance attendance, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"DELETE FROM [dbo].[Attendance] Where busNum={attendance.busNum} And tripCode= {attendance.tripCode} And dateOfCheek='{attendance.DateTime.ToString("u").Replace("Z", "")}'");
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

        //TeamMemberTripFun
        public static void UpdateTeamMemberTrip(List<TeamMember> Add,List<TeamMember > Del, int tripCode, Context context)
        {
            foreach (var teamMember in Add) { AddTeamMemberToTrip(teamMember, tripCode, context); }
            foreach (var teamMember in Del) { DeleatTeamMemberFromTrip(teamMember, tripCode, context); }
        }

        public static void AddTeamMemberToTrip(TeamMember teamMember,int tripCode, Context context)
        {
            try
            {
                
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"INSERT INTO [dbo].[TeamMeaberTrip] (teamMemberId, tripCode, schoolID,groupNum)");
                stringBuilder.Append(@$"VALUES ('{teamMember.email}',{tripCode},'{teamMember.schoolID}',667)");
                createConacation();
                var cmd = new SqlCommand(stringBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();
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

        public static void DeleatTeamMemberFromTrip(TeamMember teamMember, int tripCode, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(@$"DELETE FROM [dbo].[TeamMeaberTrip] Where teamMemberId='{teamMember.email}' And tripCode= {tripCode}");
                createConacation();
                Console.WriteLine(stringBuilder.ToString());
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

        public static void UpdateTeamMemberGroup(int TripCode, int groupNum,string TeamMemberEmail, Context context)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                string comand = $"Update [dbo].[TeamMeaberTrip] SET groupNum={groupNum} Where teamMemberId='{TeamMemberEmail}' AND tripCode={TripCode}";
                Console.WriteLine(comand);
                createConacation();
                var cmd = new SqlCommand(comand, conn);
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

        public static List<TeamMember> GetTripTeamMembers(int tripCode, string schoolId, Context context)
        {
            List<string> emails = new List<string>();
            List<TeamMember> teamMembers = new List<TeamMember>();
            string sqlQuer = @$"SELECT teamMemberId FROM [dbo].[TeamMeaberTrip] Where tripCode={tripCode}" ;
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    emails.Add((((IDataRecord)reader).GetValue(0).ToString()));
                }
                conn.Close();
                foreach (string email in emails)
                {
                    teamMembers.Add(GetTeamMember(email, schoolId,context));
                }
            }
            return teamMembers;
        }

        public static int GetGroupTripTeamMembers(string email,int tripCode, string schoolId, Context context)
        {
            List<string> emails = new List<string>();
            List<TeamMember> teamMembers = new List<TeamMember>();
            string sqlQuer = @$"SELECT groupNum FROM [dbo].[TeamMeaberTrip] Where tripCode={tripCode} And teamMemberId='{email}'";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { 
                reader.Read();
                int i = int.Parse(((IDataRecord)reader).GetValue(0).ToString());
                conn.Close();
                return i;
            }
            return 667;
        }

        public static TeamMember LoginTripTeamMembers(string email, int tripCode, string schoolId, Context context)
        {
            
            string sqlQuer = @$"SELECT teamMemberId FROM [dbo].[TeamMeaberTrip] Where tripCode={tripCode} And teamMemberId='{email}'";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return (GetTeamMember(email, schoolId, context)); 
            }
            return null;
        }
        public static List<TeamMember> GetTripTeamMembersWhoNotConnected(int tripCode, string schoolId, Context context)
        {
            List<string> emails = new List<string>();
            List<TeamMember> teamMembers = new List<TeamMember>();
            string sqlQuer = @$"SELECT teamMemberId FROM [dbo].[TeamMeaberTrip] Where tripCode={tripCode} And groupNum=667";
            createConacation();
            var cmd = new SqlCommand(sqlQuer, conn);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    emails.Add((((IDataRecord)reader).GetValue(0).ToString()));
                }
                conn.Close();
                foreach (string email in emails)
                {
                    teamMembers.Add(GetTeamMember(email, schoolId, context));
                }
            }
            return teamMembers;
        }
        //DateTime.Parse(reader.GetValue(0).ToString());
        public static bool IfStudentConectedToGroup(string studentId, string schoolId, int year)
        {
            return true;
        }

        private static int CangeToInt(string text)
        {
            if (string.IsNullOrEmpty(text)) { return 0; }
            else { return Convert.ToInt32(text); }
        }
    }


}