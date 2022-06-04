using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static WeatherApp.API_Info;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private double _temperature;

        public double Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;

                OnPropertyChanged();
            }
        }


        private Main _weather;

        public Main WeatherInformation
        {
            get { return _weather; }
            set
            {
                _weather = value;

                OnPropertyChanged();
            }
        }

        public openWeatherData AllWeather
        {
            get;
            set;
        }

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = AllWeather;
        }

        public class Weather
        {
            public string Temp { get; set; }
            public string Date { get; set; }
            public string Icon { get; set; }
        }
        private async Task GetWeatherData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string response = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&appid=27c5cd68ffd4b9cc8e225cd37435b08b}");

            var weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject<openWeatherData>(response);

            if (weatherData != null)
            {
                AllWeather = weatherData;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await GetWeatherData();
        }
    }
}
