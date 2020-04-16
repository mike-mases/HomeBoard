using System.Linq;
using System.Threading.Tasks;
using HomeBoard.Models;
using HomeBoard.WebApp.Services;

namespace HomeBoard.WebApp.Builders
{
    public class HomeBoardViewModelBuilder
    {
        private readonly IWeatherService _weatherService;
        private readonly ITrainsService _trainsService;

        public HomeBoardViewModelBuilder(IWeatherService weatherService, ITrainsService trainsService)
        {
            _weatherService = weatherService;
            _trainsService = trainsService;
        }

        public async Task<HomeBoardViewModel> BuildViewModel()
        {
            return new HomeBoardViewModel
            {
                Weather = await GetWeatherData()
            };
        }

        private async Task<WeatherViewModel> GetWeatherData()
        {
            var weather = await _weatherService.GetCurrentWeather();
            var viewModel = new WeatherViewModel
            {
                LastUpdated = weather.LocalTime.ToString("dddd MMMM dd, yyyy - h:mm:ss tt"),
                Temperature = weather.Values.Temperature,
                FeelsLike = weather.Values.FeelsLike,
                MaxTemp = weather.Values.MaxTemp,
                MinTemp = weather.Values.MinTemp,
                CityName = weather.CityName,
                Description = weather.WeatherStats.FirstOrDefault()?.Description
            };

            return viewModel;
        }
    }
}