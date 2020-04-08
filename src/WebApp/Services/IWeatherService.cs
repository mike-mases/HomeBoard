using System.Threading.Tasks;
using HomeBoard.Models.Weather;

namespace HomeBoard.WebApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetCurrentWeather();
    }
}