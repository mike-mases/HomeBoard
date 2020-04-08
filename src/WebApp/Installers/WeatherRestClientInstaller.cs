using Homeboard.Models.Configuration;
using Homeboard.WebApp.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace HomeBoard.WebApp.Installers
{
    public static class ServicesInstaller
    {
        public static void ConfigureWeatherService(this IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>(s =>
            {
                var options = s.GetService<IOptions<WeatherConfiguration>>();
                var logger = s.GetService<ILogger<WeatherService>>();
                var baseUrl = options.Value.BaseUrl;
                var client = new RestClient(baseUrl);
                var cache = s.GetService<IMemoryCache>();

                return new WeatherService(client, options, cache, logger);
            });
        }
    }
}