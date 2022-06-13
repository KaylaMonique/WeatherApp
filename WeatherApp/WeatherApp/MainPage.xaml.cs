using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static WeatherApp.API_Info;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private async Task<openWeatherData> GetWeatherData()
        {
            var location = await Geolocation.GetLocationAsync();
            var latitude = location.Latitude;
            var longitude = location.Longitude;

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&appid=27c5cd68ffd4b9cc8e225cd37435b08b");

            var weatherData = JsonConvert.DeserializeObject<openWeatherData>(response);

            return weatherData;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var data = await GetWeatherData();

            BindingContext = data;
        }
    }
}
