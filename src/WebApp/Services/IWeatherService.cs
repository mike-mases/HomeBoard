using System.Threading.Tasks;
using Models.Weather;

namespace WebApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeather();
    }
}