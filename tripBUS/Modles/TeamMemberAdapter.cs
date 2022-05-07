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

namespace tripBUS.Modles
{
    internal class TeamMemberAdapter : BaseAdapter
    {

        Context context;
        List<TeamMember> objects;
        int status;
        public TeamMemberAdapter(Context context , List<TeamMember> objects, int ststus)
        {
            this.context = context;
            this.objects = objects;
            this.status = ststus;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count { get { return objects.Count; } }

        public TeamMember this[int index]
        {
            get { return objects[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            Adapter1ViewHolder holder = null;

            if (view != null)
                holder = view.Tag as Adapter1ViewHolder;

            if (holder == null)
            {
                holder = new Adapter1ViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.layout_empty, parent, false);
                view.SetBackgroundResource(Resource.Drawable.stroke);
                (view.FindViewById<TextView>(Resource.Id.tv_empty)).Text =  "  " + objects[position].firstName + " " + objects[position].lastName;
                holder.TeamMember = objects[position];
                view.Tag = holder;
                view.Click += View_Click;

            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            if (status ==1)
            {
                (context as TeamMemberActivity).DelateTripTeamMember(((sender as View).Tag as Adapter1ViewHolder).TeamMember);
            }
            if (status ==2)
            {
                (context as TeamMemberActivity).AddTripTeamMember(((sender as View).Tag as Adapter1ViewHolder).TeamMember);
            }
        }
    }

    internal class Adapter1ViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
        public TeamMember TeamMember;
    }
}