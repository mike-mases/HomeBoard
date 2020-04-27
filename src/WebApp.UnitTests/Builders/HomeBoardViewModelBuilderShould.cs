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
using HomeBoard.Models;
using Microsoft.Extensions.Options;
using HomeBoard.Models.Configuration;

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
            var options = Substitute.For<IOptions<TrainsConfiguration>>();
            options.Value.Returns(new TrainsConfiguration { Destinations = new List<string> { "MID" } });
            _builder = new HomeBoardViewModelBuilder(_weatherService, _trainsService, options);
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
        public async Task AddWeatherIconField()
        {
            var result = await _builder.BuildViewModel();

            result.Weather.Icon.Should().Be("sample-icon");
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
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();

            result.Trains.Services.Should().HaveCount(1);
        }

        [Test]
        public async Task AddTimeField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Time.Should().Be("13:30");
        }

        [Test]
        public async Task AddDestinationField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Destination.Name.Should().Be("Action City");
        }

        [Test]
        public async Task AddDestinationDurationField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Destination.Duration.Should().Be(53);
        }

        [Test]
        public async Task AddDestinationDurationFieldIfCallingPointOfInterest()
        {
            var board = ReadTestData<StationBoard>("single-train-callingpoint-destination");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Destination.Duration.Should().Be(9);
        }

        [Test]
        public async Task AddDestinationIfCallingPointOfInterest()
        {
            var board = ReadTestData<StationBoard>("single-train-callingpoint-destination");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Destination.Name.Should().Be("Midtown");
        }

        [Test]
        public async Task AddExpectedField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Expected.Should().Be("On Time");
        }

        [Test]
        public async Task AddPlatformField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Platform.Should().Be("2");
        }

        [Test]
        public async Task AddLastReportInbetweenStations()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.LastReport.Should().Be("Between Station One and Station Two (13:15)");
        }

        [Test]
        public async Task AddLastReportAtStation()
        {
            var board = ReadTestData<StationBoard>("single-train-at-station");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.LastReport.Should().Be("Currently at Waitington (09:45)");
        }

        [Test]
        public async Task HandleLastReportMissingTime()
        {
            var board = ReadTestData<StationBoard>("single-train-missing-last-report-time");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.LastReport.Should().Be("Currently at Waitington ()");
        }

        [Test]
        public async Task HandleNoLastReport()
        {
            var board = ReadTestData<StationBoard>("single-train-no-last-report");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.LastReport.Should().Be("Train location unknown");
        }

        [Test]
        public async Task AddCoachesField()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.Coaches.Should().Be(8);
        }

        [Test]
        public async Task AddSpecialAnnouncements()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            var expected = board.SpecialNotices.Notices.Select(n => n.Text);
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var trains = result.Trains;

            trains.SpecialAnnouncements.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AddCallingAt()
        {
            var board = ReadTestData<StationBoard>("single-train-input");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var service = result.Trains.Services.FirstOrDefault();

            service.CallingAt.Should().Contain("Thoughtsville 13:30 (Exp. 13:34)");
        }

        [Test]
        public async Task AddMultipleServices()
        {
            var board = ReadTestData<StationBoard>("multiple-trains-input");
            var expected = ReadTestData<TrainsViewModel>("multiple-trains-expected");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var trains = result.Trains;

            trains.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task OrderServicesByDepartTime()
        {
            var board = ReadTestData<StationBoard>("unordered-services-input");
            var expected = ReadTestData<TrainsViewModel>("unordered-services-expected");
            _trainsService.GetStationBoard().Returns(board);

            var result = await _builder.BuildViewModel();
            var trains = result.Trains;

            trains.Services.Should().BeEquivalentTo(expected.Services,
            options => options.WithStrictOrdering());
        }

        private T ReadTestData<T>(string fileName)
        {
            var jsonString = File.ReadAllText($"./TestData/ViewModel/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        private void CreateStationBoard(List<Service> services = null, List<SpecialNotice> notices = null)
        {
            _trainsService.GetStationBoard().Returns(
                new StationBoard
                {
                    Timestamp = "16/04/2020 13:43:05",
                    Services = services != null ? services : new List<Service>(),
                    SpecialNotices = new SpecialNotices
                    {
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
                        new WeatherStatistics{ Description = "Perfect Weather", Icon = "sample-icon" },
                        new WeatherStatistics{ Description = "Terrible Weather", Icon = "sample-icon" }
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