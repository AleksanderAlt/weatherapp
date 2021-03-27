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
using WeatherApp.Models;

namespace WeatherApp
{
    public class FutureWeatherInfoAdapter : BaseAdapter<FutureWeatherInfo.List>
    {
        List<FutureWeatherInfo.List> _items;
        Activity _context;

        public FutureWeatherInfoAdapter(Activity context, List<FutureWeatherInfo.List> items)
        {
            _context = context;
            _items = items;
        }

        public override FutureWeatherInfo.List this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.weather_row_layout, null);
            view.FindViewById<TextView>(Resource.Id.futureTempTextView).Text = $"Air {_items[position].main.temp.ToString()} °C";
            view.FindViewById<TextView>(Resource.Id.futureFeelsLikeTextView).Text = $"Feels like {_items[position].main.feels_like.ToString()} °C";
            //view.FindViewById<ImageView>(Resource.Id.weatherImageView).SetImageResource(_items[position].weather.icon);
            return view;
        }
    }
}