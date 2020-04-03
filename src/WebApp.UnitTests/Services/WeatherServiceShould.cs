namespace WebApp.UnitTests.Services
{
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using RestSharp;
    using WebApp.Services;

    [TestFixture]
    public class WeatherServiceShould
    {
        private IRestClient _client;
        private WeatherService _service;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IRestClient>();
            _service = new WeatherService(_client);
        }

        [Test]
        public void ReturnWeatherResponse(){
            var result = _service.GetCurrentWeather();
            result.Should().NotBeNull();
        }
    }
}