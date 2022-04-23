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
    public class AttendanceAdapter : BaseAdapter<tripBUS.Modles.Attendance>
    {
        Android.Content.Context context;
        public List<Attendance> objects;
        bool edit;

        public AttendanceAdapter(Android.Content.Context context, System.Collections.Generic.List<Attendance> objects, bool edit)
        {
            this.context = context;
            this.objects = objects;
            this.edit = edit;
        }


        public List<Attendance> GetList()
        {
            return this.objects;
        }

        public override long GetItemId(int position)
        {
            return position;

        }

        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override Attendance this[int position]
        {
            get { return this.objects[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            LayoutInflater layoutInflater = null;
            if (context is ViewBusActivity)
            {
                layoutInflater = ((ViewBusActivity)context).LayoutInflater;
            }

            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.attendance_adapter_layout, parent, false);
            (view.FindViewById<TextView>(Resource.Id.tv_des_adpterAtten)).Text = objects[position].descriotionCheek;
            (view.FindViewById<TextView>(Resource.Id.tv_date_adpterAtten)).Text = objects[position].DateTime.ToString("g");

            if (edit)
            {
                Button b = view.FindViewById<Button>(Resource.Id.del_attendent_fav);
                TagClass holder = null;
                if (b != null)
                    holder = b.Tag as TagClass;
                if (holder == null)
                {
                    holder = new TagClass();
                    holder.position = position;
                    b.Tag = holder;
                }
                b.Click += AttendanceAdapter_Click;
            }
            else
            {
                view.FindViewById<Button>(Resource.Id.del_attendent_fav).Visibility = ViewStates.Gone;
                view.Click += View_Click;
            }
            

            return view;

        }

        private void AttendanceAdapter_Click(object sender, EventArgs e)
        {
            ((ViewBusActivity)context).delateAttendace(objects[((sender as View).Tag as TagClass).position]);
        }

        private void View_Click(object sender, EventArgs e)
        {
            ((ViewBusActivity)context).OpenAttendece((((View)sender).FindViewById<TextView>(Resource.Id.tv_date_adpterAtten)).Text);
        }
    }
    internal class TagClass : Java.Lang.Object
    {
        public int position;

    }
}