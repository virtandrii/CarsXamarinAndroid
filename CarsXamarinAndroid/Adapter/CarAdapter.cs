using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CarsXamarinAndroid.Model;
using System;
using System.Collections.Generic;

namespace CarsXamarinAndroid.Adapter
{
    class CarAdapter : RecyclerView.Adapter
    {
        public event EventHandler<CarAdapterClickEventArgs> ItemClick;
        public event EventHandler<CarAdapterClickEventArgs> ItemLongClick;
        List<Car> items;
        Context context;

        public CarAdapter(List<Car> data, Context context)
        {
            items = data;
            this.context = context;
        }

        public override int ItemCount => items != null ? items.Count : 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var car = items[position];

            var holder = viewHolder as CarAdapterViewHolder;

            holder.nameText.Text = car.Name;
            holder.colorText.Text = car.Color;
            holder.engineTypeText.Text = car.EngineType;
            holder.engineCapacityText.Text = car.EngineCapacity;
            holder.priceText.Text = $"{car.Price} color";

            holder.imageView.SetImageBitmap(BitmapFactory.DecodeByteArray(car.Image, 0, car.Image.Length));

            holder.ItemView.Click += delegate (object sender, EventArgs args)
            {
                var intent = new Intent(context, typeof(CarDetailsActivity));

                intent.PutExtra("car_details", car.Id);
                context.StartActivity(intent);
            };


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.car_row, parent, false);

            var vh = new CarAdapterViewHolder(itemView);
            return vh;
        }


    }

    public class CarAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView nameText { get; set; }

        public TextView colorText { get; set; }

        public TextView engineTypeText { get; set; }

        public TextView engineCapacityText { get; set; }

        public TextView priceText { get; set; }

        public ImageView imageView { get; set; }

        public CarAdapterViewHolder(View itemView) : base(itemView)
        {
            nameText = (TextView)itemView.FindViewById(Resource.Id.carNameText);
            colorText = (TextView)itemView.FindViewById(Resource.Id.carColorText);
            engineTypeText = (TextView)itemView.FindViewById(Resource.Id.carEngineTypeText);
            engineCapacityText = (TextView)itemView.FindViewById(Resource.Id.carEngineCapacityText);
            priceText = (TextView)itemView.FindViewById(Resource.Id.carPriceText);
            imageView = (ImageView)itemView.FindViewById(Resource.Id.carProfileImage);
        }
    }

    public class CarAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}

