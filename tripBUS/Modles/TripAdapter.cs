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

namespace tripBUS.Modles
{
    internal class TripAdapter : BaseAdapter
    {

        Context context;
        List<Trip> objects;

        public TripAdapter(List<Trip> objects,Context context)
        {
            this.context = context;
            this.objects = objects;
        }

        public override int Count { get { return objects.Count; } }

        public override Java.Lang.Object GetItem(int position) { return position; }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            TripAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as TripAdapterViewHolder;

            if (holder == null)
            {
                holder = new TripAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.trip_layout_lv, parent, false);
                holder.TitleTV = view.FindViewById<TextView>(Resource.Id.tv_tripName_triplv);
                holder.DateTV = view.FindViewById<TextView>(Resource.Id.tv_date_triplv);
                holder.PlaceAndAgeTV = view.FindViewById<TextView>(Resource.Id.tv_placeAge_triplv);

                holder.TitleTV.Text = objects[position].tripName;
                holder.DateTV.Text = $"Date: {objects[position].StartDate.ToString("d")}-{objects[position].EndDate.ToString("d")}";
                holder.PlaceAndAgeTV.Text = $"Age Class: {objects[position].classAge} Place: {objects[position].place}";

                holder.trip = objects[position];
                view.Tag = holder;

                view.Click += View_Click;
            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            
            TripAdapterViewHolder holder = ((View)sender).Tag as TripAdapterViewHolder;
            if (context is MangerMainActivity)
            {
                ((MangerMainActivity)context).OpenTripLayout(holder.trip.tripCode, SavedData.loginMember.schoolID);
            }
        }
    }

    internal class TripAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView TitleTV { get; set; }
        public TextView DateTV { get; set; }
        public TextView PlaceAndAgeTV { get; set; }

        public Trip trip { get; set; }
    }
}