using System;
using System.Threading.Tasks;
using Homeboard.Models.Configuration;
using HomeBoard.Models.Weather;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Homeboard.WebApp.Services
{
    public class WeatherService : IWeatherService
    {
        private const string CacheKey = "CurrentWeather";
        private readonly IRestClient _client;
        private readonly IOptions<WeatherConfiguration> _config;
        private readonly IMemoryCache _cache;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(
            IRestClient client, 
            IOptions<WeatherConfiguration> config, 
            IMemoryCache cache,
            ILogger<WeatherService> logger)
        {
            _config = config;
            _client = client;
            _cache = cache;
            _logger = logger;
        }

        public async Task<WeatherResponse> GetCurrentWeather()
        {
            var content = await _cache.GetOrCreateAsync(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_config.Value.CacheTimeoutSeconds);
                return GetFromWeatherService();
            });

            return content;
        }

        private async Task<WeatherResponse> GetFromWeatherService()
        {
            var response = await _client.ExecuteGetAsync(BuildRequest());

            if (!response.IsSuccessful)
            {
                var details = _config.Value;
                _logger.LogError($"Call to service failed for lat: {details.Latitude}, lon: {details.Longitude}, units: {details.Units}, key: {details.ApiKey}, uri: {details.ActionUri}");
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