using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarsXamarinAndroid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsXamarinAndroid
{
    [Activity(Label = "CarDetailsActivity")]
    public class CarDetailsActivity : Activity
    {
        TextView nameDetailView;
        TextView companyNameDetailView;
        TextView priceDetailView;
        TextView colorDetailView;
        TextView engineTypeDetailView;
        TextView engineCapacityDetailView;
        TextView publishYearDetailView;

        ImageView imageDetailView;

        CarService service;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            service = new CarService();

            var carId = Intent.Extras.GetInt("car_details");

            var car = await service.Get(carId);

            SetContentView(Resource.Layout.car_details_activity);

            nameDetailView = (TextView)FindViewById(Resource.Id.carNameDetail);
            companyNameDetailView = (TextView)FindViewById(Resource.Id.carCompanyNameDetail);
            colorDetailView = (TextView)FindViewById(Resource.Id.carColorDetail);
            priceDetailView = (TextView)FindViewById(Resource.Id.carPriceDetail);
            engineTypeDetailView = (TextView)FindViewById(Resource.Id.carEngineTypeDetail);
            engineCapacityDetailView = (TextView)FindViewById(Resource.Id.carEngineCapacityDetail);
            publishYearDetailView = (TextView)FindViewById(Resource.Id.carPublishYearDetail);
            imageDetailView = (ImageView)FindViewById(Resource.Id.carProfileImageEdit);

            if (car != null)
            {
                imageDetailView.SetImageBitmap(BitmapFactory.DecodeByteArray(car.Image, 0, car.Image.Length));
            }

            nameDetailView.Text = car?.Name;
            companyNameDetailView.Text = car?.CompanyName;
            colorDetailView.Text = car?.Color;
            priceDetailView.Text = car?.Price;
            engineTypeDetailView.Text = car?.EngineType;
            engineCapacityDetailView.Text = car?.EngineCapacity;

            publishYearDetailView.Text = car != null ? car?.PublishYear.Value.Date.ToString() : DateTime.Now.ToString();

        }
    }
}