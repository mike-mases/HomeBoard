using HomeBoard.Models.Configuration;
using HomeBoard.WebApp.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace HomeBoard.WebApp.Installers
{
    public static class ServicesInstaller
    {
        public static void ConfigureWeatherService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WeatherConfiguration>(config.GetSection("WeatherService"));
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

        public static void ConfigureTrainsService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TrainsConfiguration>(config.GetSection("TrainsService"));
            services.AddTransient<ITrainsService, TrainsService>(s =>
            {
                var options = s.GetService<IOptions<TrainsConfiguration>>();
                var logger = s.GetService<ILogger<TrainsService>>();
                var baseUrl = options.Value.BaseUrl;
                var client = new RestClient(baseUrl);
                var cache = s.GetService<IMemoryCache>();

                return new TrainsService(options, client, logger, cache);
            });
        }
    }
}