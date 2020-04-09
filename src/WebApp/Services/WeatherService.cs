using System;
using System.Threading.Tasks;
using HomeBoard.Models.Configuration;
using HomeBoard.Models.Weather;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace HomeBoard.WebApp.Services
{
    public class WeatherService : IWeatherService
    {
        private const string CacheKey = "CurrentWeather";
        private readonly IRestClient _client;
        private readonly WeatherConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(
            IRestClient client,
            IOptions<WeatherConfiguration> config,
            IMemoryCache cache,
            ILogger<WeatherService> logger)
        {
            _config = config.Value;
            _client = client;
            _cache = cache;
            _logger = logger;
        }

        public async Task<WeatherResponse> GetCurrentWeather()
        {
            var content = await _cache.GetOrCreateAsync(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_config.CacheTimeoutSeconds);
                return GetFromWeatherService();
            });

            return content;
        }

        private async Task<WeatherResponse> GetFromWeatherService()
        {
            var response = await _client.ExecuteGetAsync(BuildRequest());

            if (!response.IsSuccessful)
            {
                _logger.LogError($"Call to service failed for " +
                                $"lat: {_config.Latitude}, " +
                                $"lon: {_config.Longitude}, " +
                                $"units: {_config.Units}, " +
                                $"key: {_config.ApiKey}, " +
                                $"uri: {_config.ActionUri}");
                return new WeatherResponse();
            }

            return DeserialiseWeatherReponse(response.Content);
        }

        private WeatherResponse DeserialiseWeatherReponse(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<WeatherResponse>(jsonString);
            }
            catch (JsonReaderException e)
            {
                _logger.LogError(e, "Weather service JSON deserialise failure.");
                return new WeatherResponse();
            }
        }

        private RestRequest BuildRequest()
        {
            var request = new RestRequest(_config.ActionUri, Method.GET);
            request.AddQueryParameter("lat", _config.Latitude.ToString());
            request.AddQueryParameter("lon", _config.Longitude.ToString());
            request.AddQueryParameter("units", _config.Units);
            request.AddQueryParameter("appid", _config.ApiKey);

            return request;
        }
    }
}