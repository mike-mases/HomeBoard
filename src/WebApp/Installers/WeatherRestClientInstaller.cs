using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Models.Configuration;
using RestSharp;

namespace WebApp.Installers
{
    public static class WeatherRestClientInstaller
    {
        public static void ConfigureWeatherRestClient(this IServiceCollection services)
        {
            services.AddTransient<IRestClient, RestClient>(s =>
            {
                var options = s.GetService<IOptions<WeatherConfiguration>>();
                var baseUrl = options.Value.BaseUrl;

                return new RestClient(baseUrl);
            });
        }
    }
}