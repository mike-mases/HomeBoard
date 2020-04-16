using System.Threading.Tasks;
using FluentAssertions;
using HomeBoard.Models.Weather;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Services;
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

        private void CreateWeatherResponse()
        {
            _weatherService.GetCurrentWeather().Returns(
                new WeatherResponse
                {
                    Time = 1586937709,
                    Values = new WeatherValues
                    {
                        Temperature = 25.32
                    }
                }
            );
        }
    }
}