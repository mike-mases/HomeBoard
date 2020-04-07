using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Models.Configuration;
using Models.Weather;
using Newtonsoft.Json;
using RestSharp;

namespace WebApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IRestClient _client;
        private readonly IOptions<WeatherConfiguration> _config;
        private readonly IMemoryCache _cache;

        public WeatherService(IRestClient client, IOptions<WeatherConfiguration> config, IMemoryCache cache)
        {
            _config = config;
            _client = client;
            _cache = cache;
        }

        public async Task<WeatherResponse> GetCurrentWeather()
        {
            var response = await _client.ExecuteGetAsync(BuildRequest());
            var content = JsonConvert.DeserializeObject<WeatherResponse>(response.Content);
            _cache.Set<WeatherResponse>("CurrentWeather", content);

            return content;
        }

        private RestRequest BuildRequest()
        {
            var parameters = _config.Value;
            var request = new RestRequest(parameters.ActionUri, Method.GET);
            request.AddQueryParameter("lat", parameters.Latitude.ToString());
            request.AddQueryParameter("lon", parameters.Longitude.ToString());
            request.AddQueryParameter("units", parameters.Units);
            request.AddQueryParameter("appid", parameters.ApiKey);

            return request;
        }
    }
}