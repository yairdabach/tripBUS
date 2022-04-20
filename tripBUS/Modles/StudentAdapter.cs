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
        bool edit;
        int classOrGroup;


        public StudentAdapter(Android.Content.Context context, System.Collections.Generic.List<Student> objects,bool edit)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = 0;
        }

        public StudentAdapter(Android.Content.Context context, System.Collections.Generic.List<Student> objects, bool edit, int classOrGroup)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
            this.classOrGroup = classOrGroup;
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


            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.student_layout, parent, false);
            TextView tvTitle = view.FindViewById<TextView>(Resource.Id.tv_studentLayout);

            tripBUS.Modles.Student temp = objects[position];

            if (edit)
            {
                (view.FindViewById<Button>(Resource.Id.del_student_fav)).Click += delegate
                {
                    DataHelper.DelStudent(objects[position].Id, objects[position].School_ID, objects[position].LerningYear);
                };
                (view.FindViewById<Button>(Resource.Id.edit_student_fav)).Click += delegate
                {

                };
            }
            else
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
            

            if (temp != null)
            {

                tvTitle.Text = temp.First_Name + " " + temp.Last_Name + " | " + temp.Id;
            }

            return view;

        }

        private void View_Click(object sender, EventArgs e)
        {
            string id = (((View)sender).FindViewById<TextView>(Resource.Id.tv_studentLayout).Text.Split(" | "))[1];
            Student temp = DataHelper.GetStudentById(id, context);
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
}