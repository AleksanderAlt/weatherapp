using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using WeatherApp.Services;
using WeatherApp.Models;
using System.Collections.Generic;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var dataService = new RemoteDataService();

            var cityEditText = FindViewById<EditText>(Resource.Id.cityTextView);
            var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            var tempTextView = FindViewById<TextView>(Resource.Id.tempTextView);
            var weatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);
            var feelsLikeTextView = FindViewById<TextView>(Resource.Id.feelsLikeTextView);
            var listView = FindViewById<ListView>(Resource.Id.tempListView);

            searchButton.Click += async delegate
            {
                var city = cityEditText.Text.Trim();
                if (city != "")
                {
                    var data = await dataService.GetCityWeather(city);
                    var futureData = await dataService.GetCityFutureWeather(city);

                    tempTextView.Text = $"Air temperature {data.main.temp.ToString()} °C";
                    using (var bm = await dataService.GetImageFromUrl($"https://openweathermap.org/img/wn/{data.weather[0].icon}@2x.png"))
                        weatherImage.SetImageBitmap(bm);
                    feelsLikeTextView.Text = $"Feels like {data.main.feels_like.ToString()} °C";

                    listView.Adapter = new FutureWeatherInfoAdapter(this, futureData.list);

                    listView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
                    {
                        var desc = futureData.list[args.Position].weather[0].description.ToString();
                        Toast.MakeText(this, desc, ToastLength.Long).Show();
                    };
                }

            };


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}