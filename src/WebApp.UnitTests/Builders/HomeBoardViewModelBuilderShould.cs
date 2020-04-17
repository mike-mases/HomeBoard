using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using HomeBoard.Models.Trains;
using HomeBoard.Models.Weather;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Services;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace HomeBoard.WebApp.UnitTests.Builders
{
    [TestFixture]
    public class HomeBoardViewModelBuilderShould
    {
        private HomeBoardViewModelBuilder _builder;
        private IWeatherService _weatherService;
        private ITrainsService _trainsService;

        [SetUp]
        public void Setup()
        {
            _weatherService = Substitute.For<IWeatherService>();
            _trainsService = Substitute.For<ITrainsService>();
            _builder = new HomeBoardViewModelBuilder(_weatherService, _trainsService);
            CreateWeatherResponse();
            CreateStationBoard();
        }

        [Test]
        public async Task ReturnViewModel()
        {
            var result = await _builder.BuildViewModel();

            result.Should().NotBeNull();
        }

        [Test]
        public async Task PopulateWeatherLastUpdatedField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.LastUpdated.Should().Be("Wednesday April 15, 2020 - 9:01:49 AM");
        }

        [Test]
        public async Task PopulateTemperatureField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.Temperature.Should().Be(25.32);
        }

        [Test]
        public async Task PopulateFeelsLikeField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.FeelsLike.Should().Be(23.1);
        }

        [Test]
        public async Task PopulateMaxTempField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.MaxTemp.Should().Be(27.54);
        }

        [Test]
        public async Task PopulateMinTempField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.MinTemp.Should().Be(10.34);
        }

        [Test]
        public async Task PopulateCityNameField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.CityName.Should().Be("Testville");
        }

        [Test]
        public async Task PopulateDescriptionFromFirstEntry()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.Description.Should().Be("Perfect Weather");
        }

        [Test]
        public async Task HandleNoDescriptionEntries()
        {
            CreateWeatherResponseWithoutStats();
            var result = await _builder.BuildViewModel();

            result.Weather.Description.Should().BeNull();
        }

        [Test]
        public async Task PopulateTrainsLastUpdatedField()
        {
            var result = await _builder.BuildViewModel();

            result.Trains.LastUpdated.Should().Be("Thursday April 16, 2020 - 1:43:05 PM");
        }

        [Test]
        public async Task AddTrainServiceToViewModel()
        {
            var board  = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();

            result.Trains.Services.Should().HaveCount(1);
        }

        [Test]
        public async Task AddTimeField()
        {
            var board  = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Time.Should().Be("13:30");
        }

        private T ReadTestData<T>(string fileName){
            var jsonString = File.ReadAllText($"./TestData/ViewModel/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        private void CreateStationBoard(List<Service> services = null, List<SpecialNotice> notices = null)
        {
            _trainsService.GetStationBoard().Returns(
                new StationBoard
                {
                    Timestamp = "16/04/2020 13:43:05",
                    Services = services != null? services : new List<Service>(),
                    SpecialNotices = new SpecialNotices {
                        Notices = notices != null ? notices : new List<SpecialNotice>()
                    }
                }
            );
        }

        private void CreateWeatherResponse()
        {
            _weatherService.GetCurrentWeather().Returns(
                new WeatherResponse
                {
                    Time = 1586937709,
                    Values = new WeatherValues
                    {
                        Temperature = 25.32,
                        FeelsLike = 23.1,
                        MaxTemp = 27.54,
                        MinTemp = 10.34
                    },
                    WeatherStats = new List<WeatherStatistics>
                    {
                        new WeatherStatistics{ Description = "Perfect Weather" },
                        new WeatherStatistics{ Description = "Terrible Weather"}
                    },
                    CityName = "Testville"
                }
            );
        }

        private void CreateWeatherResponseWithoutStats()
        {
            _weatherService.GetCurrentWeather().Returns(
                new WeatherResponse
                {
                    Time = 1586937709,
                    Values = new WeatherValues
                    {
                        Temperature = 25.32,
                        FeelsLike = 23.1,
                        MaxTemp = 27.54,
                        MinTemp = 10.34
                    },
                    WeatherStats = new List<WeatherStatistics>(),
                    CityName = "Testville"
                }
            );
        }
    }
}