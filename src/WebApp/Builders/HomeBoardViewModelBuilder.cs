using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HomeBoard.Models;
using HomeBoard.Models.Trains;
using HomeBoard.WebApp.Services;

namespace HomeBoard.WebApp.Builders
{
    public class HomeBoardViewModelBuilder
    {
        private const string TimeFormat = "dddd MMMM dd, yyyy - h:mm:ss tt";
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
                Weather = await GetWeatherData(),
                Trains = await GetTrainsData()
            };
        }

        private async Task<WeatherViewModel> GetWeatherData()
        {
            var weather = await _weatherService.GetCurrentWeather();
            var viewModel = new WeatherViewModel
            {
                LastUpdated = weather.LocalTime.ToString(TimeFormat),
                Temperature = weather.Values.Temperature,
                FeelsLike = weather.Values.FeelsLike,
                MaxTemp = weather.Values.MaxTemp,
                MinTemp = weather.Values.MinTemp,
                CityName = weather.CityName,
                Description = weather.WeatherStats.FirstOrDefault()?.Description
            };

            return viewModel;
        }

        private async Task<TrainsViewModel> GetTrainsData()
        {
            var trains = await _trainsService.GetStationBoard();
            var viewModel = new TrainsViewModel
            {
                LastUpdated = trains.TimestampParsed.ToString(TimeFormat),
                Services = AddServices(trains.Services)
            };

            return viewModel;
        }

        private IEnumerable<ServiceViewModel> AddServices(List<Service> services)
        {
            return services.Select(s => new ServiceViewModel
            {
                Time = FormatTimestampWithDate(s.DepartTime.Timestamp),
                Destination = s.EndStation.Name,
                Expected = s.ExpectedDepartStatus.Time,
                Platform = s.Platform.Number,
                Coaches = s.CoachesCount,
                LastReport = BuildLastReport(s.LastReport)
            });
        }

        private string BuildLastReport(LastReport report)
        {
            var time = FormatTimestampWithoutDate(report.Time);

            if (string.IsNullOrEmpty(report.DestinationStation))
            {
                return $"Currently at {report.OriginStation} ({time})";
            }

            return $"Between {report.OriginStation} and {report.DestinationStation} ({time})";
        }

        private string FormatTimestampWithDate(string inputTime)
        {
            var parsedDate = DateTime.ParseExact(inputTime, "yyyyMMddHHmmss", null);
            return parsedDate.ToString("HH:mm");
        }

        private string FormatTimestampWithoutDate(string inputTime)
        {
            var parsedDate = DateTime.ParseExact(inputTime, "HHmm", CultureInfo.InvariantCulture);
            return parsedDate.ToString("HH:mm");
        }
    }
}