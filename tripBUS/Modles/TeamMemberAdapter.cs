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

        public TeamMemberAdapter(Context context)
        {
            this.context = context;
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
                (view.FindViewById<TextView>(Resource.Id.tv_empty)).Text = objects[position].firstName + " " + objects[position].lastName;
                view.Tag = holder;

            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

    }

    internal class Adapter1ViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
        TeamMember TeamMember;
    }
}