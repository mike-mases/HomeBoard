using Models.Weather;
using RestSharp;

namespace WebApp.Services
{
    public class WeatherService
    {
        private readonly IRestClient _client;

        public WeatherService(IRestClient client)
        {
            _client = client;
        }

        public object GetCurrentWeather()
        {
            return new WeatherResponse();
        }
    }
}