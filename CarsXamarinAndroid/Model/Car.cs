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

namespace CarsXamarinAndroid.Model
{
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string Price { get; set; }

        public string EngineType { get; set; }

        public string EngineCapacity { get; set; }

        public string Color { get; set; }

        public byte[] Image { get; set; }

        public DateTime? PublishYear { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}