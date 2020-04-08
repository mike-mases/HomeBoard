using System.Threading.Tasks;
using HomeBoard.Models.Weather;

namespace Homeboard.WebApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeather();
    }
}