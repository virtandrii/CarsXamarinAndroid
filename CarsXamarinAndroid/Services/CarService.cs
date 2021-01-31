using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarsXamarinAndroid.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarsXamarinAndroid.Services
{
    class CarService
    {
        //public const string HostUrl = "http://192.168.0.104:3000";
        public const string HostUrl = "http://10.0.2.2:57598";
        private readonly string BaseUrl = $"{HostUrl}/api/cars";
        private HttpClient client;

        public CarService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 2560000;
        }

        public async Task<List<Car>> GetAll()
        {
            var items = new List<Car>();
            var uri = new Uri(BaseUrl);

            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    items = JsonConvert.DeserializeObject<List<Car>>(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return items;
        }

        public async Task<Car> Get(int id)
        {
            var item = new Car();
            var uri = new Uri($"{BaseUrl}/{id}");

            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Car>(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return item;
        }
    }
}