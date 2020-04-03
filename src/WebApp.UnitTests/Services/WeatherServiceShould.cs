namespace WebApp.UnitTests.Services
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using Models.Configuration;
    using NSubstitute;
    using NUnit.Framework;
    using RestSharp;
    using WebApp.Services;

    [TestFixture]
    public class WeatherServiceShould
    {
        private IRestClient _client;
        private WeatherService _service;
        private IOptions<WeatherConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IRestClient>();
            _config = Substitute.For<IOptions<WeatherConfiguration>>();
            _config.Value.Returns(new WeatherConfiguration{
                ActionUri = "testuri"
            });

            _service = new WeatherService(_client, _config);
        }

        [Test]
        public async Task ReturnWeatherResponse(){
            var result = await _service.GetCurrentWeather();
            result.Should().NotBeNull();
        }

        [Test]
        public async Task CallTheWeatherApi(){
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Any<IRestRequest>());
        }

        [Test]
        public async Task BuildTheRequest(){
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Resource.Equals("testuri")));
        }
    }
}