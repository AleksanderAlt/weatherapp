using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class RemoteDataService
    {
        public async Task<WeatherInfo> GetCityWeather(string city)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={Constants.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherInfo>(content);
            }
            else
            {
                throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString()); // How to handle this exception? City not found
            }
        }
        public async Task<Bitmap> GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(url);
                if (msg.IsSuccessStatusCode)
                {
                    using (var stream = await msg.Content.ReadAsStreamAsync())
                    {
                        var bitmap = await BitmapFactory.DecodeStreamAsync(stream);
                        return bitmap;
                    }
                }
            }
            return null;
        }
        public async Task<WeatherInfo> GetWeatherDesc(string city)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={Constants.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherInfo>(content);
            }
            else
            {
                throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString()); // How to handle this exception? City not found
            }
        }
        public async Task<FutureWeatherInfo.Root> GetCityFutureWeather(string city)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={Constants.ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var a = JsonConvert.DeserializeObject<FutureWeatherInfo.Root>(content);
                return a;
            }
            else
            {
                throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString()); // How to handle this exception? City not found
            }
        }
    }
}