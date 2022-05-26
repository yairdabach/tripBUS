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
using tripBUS.Helpers;
using tripBUS.Modles;

namespace tripBUS.Modles
{
    public class StudentAdapter : BaseAdapter<tripBUS.Modles.Student>
    {
        Android.Content.Context context;
        public List<Student> objects;
        public List<StudentAttendance> attendances;
        bool edit;
        int classOrGroup;
        int tripCode;
        int year;
        string SchoolID;
        private ClassManegmentActivity classManegmentActivity;
        private int schoolID;
        private List<Student> students;
        private bool v;

        public StudentAdapter(Android.Content.Context context,int tripcode, int year, string SchoolID, System.Collections.Generic.List<Student> objects,bool edit)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = 0;
            this.tripCode = tripcode;
            this.year = year;
            this.SchoolID = SchoolID;
        }

        public StudentAdapter(Android.Content.Context context, int year, string SchoolID, System.Collections.Generic.List<Student> objects, bool edit)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = 0;
            this.year = year;
            this.SchoolID = SchoolID;
        }


        public StudentAdapter(Android.Content.Context context, int tripcode, int year, string SchoolID, System.Collections.Generic.List<Student> objects, bool edit, int classOrGroup)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = classOrGroup;
            this.tripCode = tripcode;
            this.year = year;
            this.SchoolID = SchoolID;
        }

        public StudentAdapter(Android.Content.Context context, int tripcode, int year, string SchoolID, System.Collections.Generic.List<Student> objects, bool edit, int classOrGroup, List<StudentAttendance> attendances)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = classOrGroup;
            this.attendances = attendances;
        }



        public List<Student> GetStudents() { return objects; }

        public override Student this[int position]
        {
            get { return this.objects[position]; }
        }

        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
    
        public void RemoveItem(Student student)
        {
            if (this.objects.Contains(student)) { this.objects.Remove(student); }
        }
        public void AddItem(Student student)
        {
            if (!this.objects.Contains(student)) { this.objects.Add(student); }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            StudentAdapterViewHolder holder = null;
            LayoutInflater layoutInflater = null;
            if (context is ClassManegmentActivity)
            {
                layoutInflater = ((ClassManegmentActivity)context).LayoutInflater;
            }
            if (context is ViewGroupActivity)
            {
                layoutInflater = ((ViewGroupActivity)context).LayoutInflater;
            }
            if (context is EditGroupActivity)
            {
                layoutInflater = ((EditGroupActivity)context).LayoutInflater;
                
            }
            if (context is ViewStudentListActivity)
            {
                layoutInflater = ((ViewStudentListActivity)context).LayoutInflater;

            }
            if (context is AttendanceActivity)
            {
                layoutInflater = ((AttendanceActivity)context).LayoutInflater;

            }


            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.student_layout, parent, false);
            TextView tvTitle = view.FindViewById<TextView>(Resource.Id.tv_studentLayout);

            tripBUS.Modles.Student temp = objects[position];

            if (edit && classOrGroup!=3)
            {
               
                holder = view.Tag as StudentAdapterViewHolder;
                if (holder == null)
                {
                    holder = new StudentAdapterViewHolder();
                    holder.student = objects[position];
                    holder.pos = position;
                    view.Tag = holder;
                }


                (view.FindViewById<Button>(Resource.Id.del_student_fav)).Click += delegate
                {
                    DataHelper.DelStudent(objects[position].Id, objects[position].School_ID, objects[position].LerningYear, context);
                    objects.Remove(objects[position]);
                    ((ClassManegmentActivity)context).LayoutInflater.Inflate(Resource.Layout.student_layout, parent, false);
                    Toast.MakeText(context, "Student Delate", ToastLength.Short).Show();
                };
                (view.FindViewById<Button>(Resource.Id.edit_student_fav)).Click += delegate
                {
                    Intent intenti = new Intent(context, typeof(StudentActivity));
                    Bundle bi = new Bundle();
                    bi.PutString("student", (view.Tag as StudentAdapterViewHolder).student.Id);
                    bi.PutInt("year", (view.Tag as StudentAdapterViewHolder).student.LerningYear);
                    bi.PutInt("Status", 1);
                    bi.PutString("SchoolId", Helpers.SavedData.loginMember.schoolID);
                    intenti.PutExtras(bi);
                    ((ClassManegmentActivity) context).StartActivity(intenti);
                };
            }
            else if(classOrGroup != 3)
            {
                view.FindViewById<Button>(Resource.Id.del_student_fav).Visibility = ViewStates.Gone;
                view.FindViewById<Button>(Resource.Id.edit_student_fav).Visibility = ViewStates.Gone;
                view.SetBackgroundResource(Resource.Drawable.stroke);
                tvTitle.TextSize = 18;
                if (context is EditGroupActivity)
                {
                    tvTitle.TextSize = 12;
                    view.Click += View_Click;
                }
            }
            else
            {
                view.SetBackgroundResource(Resource.Drawable.stroke);
                view.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.WrapContent);
                view.FindViewById<Button>(Resource.Id.del_student_fav).Visibility = ViewStates.Gone;
                view.FindViewById<Button>(Resource.Id.edit_student_fav).Visibility = ViewStates.Visible;
                if (attendances[position].isAttendance)
                {
                    view.FindViewById<Button>(Resource.Id.edit_student_fav).SetBackgroundResource(Resource.Drawable.IsCheck);
                }
                else
                {
                    view.FindViewById<Button>(Resource.Id.edit_student_fav).SetBackgroundResource(Resource.Drawable.NotCheck);
                }
                
                if (view.FindViewById<Button>(Resource.Id.edit_student_fav) != null)
                    holder = view.Tag as StudentAdapterViewHolder;
                if (holder == null)
                {
                    holder = new StudentAdapterViewHolder();
                    holder.student = objects[position];
                    holder.pos = position;
                    view.FindViewById<Button>(Resource.Id.edit_student_fav).Tag = holder;
                }
                view.FindViewById<Button>(Resource.Id.edit_student_fav).Click += StudentAdapter_Click;

            }


            if (temp != null)
            {

                tvTitle.Text = temp.First_Name + " " + temp.Last_Name + " | " + temp.Id;
                
            }

            if (view != null)
                holder = view.Tag as StudentAdapterViewHolder;
            if (holder == null)
            {
                holder = new StudentAdapterViewHolder();
                holder.student = objects[position];
                holder.pos = position;
                view.Tag = holder;
            }
            return view;

        }

        private void StudentAdapter_Click(object sender, EventArgs e)
        {
            View view = sender as View;
            int position = ((StudentAdapterViewHolder) view.Tag).pos;
            attendances[position].isAttendance = !attendances[position].isAttendance;
            if (attendances[position].isAttendance)
            {
                view.FindViewById<Button>(Resource.Id.edit_student_fav).SetBackgroundResource(Resource.Drawable.IsCheck);
            }
            else
            {
                view.FindViewById<Button>(Resource.Id.edit_student_fav).SetBackgroundResource(Resource.Drawable.NotCheck);
            }
            ((AttendanceActivity)context).ChageAttendace(position);
        }

        private void View_Click(object sender, EventArgs e)
        {
            Student temp = ((StudentAdapterViewHolder)((View)sender).Tag).student;
            if (classOrGroup == 1)
            {
                ((EditGroupActivity)context).AddToGroup(temp);
            }
            if (classOrGroup == 2)
            {
                ((EditGroupActivity)context).RemoveFromGroup(temp);
            }
        }
    }
    internal class StudentAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public Student student { get; set; }
        public int pos { get; set; }

        public StudentAdapterViewHolder() { }
    }
}