using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using CarsXamarinAndroid.Adapter;
using CarsXamarinAndroid.Model;
using CarsXamarinAndroid.Services;

namespace CarsXamarinAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        ImageView searchButton;
        EditText searchText;
        List<Car> cars;
        CarAdapter adapter;
        CarService service;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            recyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);

            searchButton = (ImageView)FindViewById(Resource.Id.searchButton);
            searchText = (EditText)FindViewById(Resource.Id.searchText);

            service = new CarService();
            cars = System.Threading.Tasks.Task.Run(() => service.GetAll())?.Result;

            searchText.TextChanged += OnSearchTextChanged;
            SetupRecycleView();

        }

        public void OnSearchTextChanged(object sender, TextChangedEventArgs eventArgs)
        {
            var searchList = cars.FindAll(x => x.Name.ToLower().Contains(searchText.Text.ToLower().Trim()));
            adapter = new CarAdapter(searchList, this);
            recyclerView.SetAdapter(adapter);
        }

        private void SetupRecycleView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            adapter = new CarAdapter(cars, this);
            recyclerView.SetAdapter(adapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
