using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Models.Configuration;
using Models.Weather;
using RestSharp;

namespace WebApp.Services
{
    public class WeatherService
    {
        private readonly IRestClient _client;
        private readonly IOptions<WeatherConfiguration> _config;

        public WeatherService(IRestClient client, IOptions<WeatherConfiguration> config)
        {
            _config = config;
            _client = client;
        }

        public async Task<WeatherResponse> GetCurrentWeather()
        {
            await _client.ExecuteGetAsync(BuildRequest());
            return new WeatherResponse();
        }

        private RestRequest BuildRequest()
        {
            var parameters = _config.Value;
            return new RestRequest(parameters.ActionUri, Method.GET);
        }
    }
}