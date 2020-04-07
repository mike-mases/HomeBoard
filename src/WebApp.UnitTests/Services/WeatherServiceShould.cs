namespace WebApp.UnitTests.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Models.Configuration;
    using Models.Weather;
    using Newtonsoft.Json;
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
        private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IRestClient>();
            _config = Substitute.For<IOptions<WeatherConfiguration>>();
            _config.Value.Returns(new WeatherConfiguration
            {
                ActionUri = "testuri",
                Latitude = 2.1,
                Longitude = 4.2,
                Units = "metric",
                ApiKey = "testkey"
            });
            _cache = Substitute.For<IMemoryCache>();

            _service = new WeatherService(_client, _config, _cache);
            var jsonString = GetTestDataText("weather-api-response.json");
            _client.ExecuteGetAsync(Arg.Any<IRestRequest>()).ReturnsForAnyArgs(new RestResponse { Content = jsonString });
        }

        [Test]
        public async Task ReturnWeatherResponse()
        {
            var result = await _service.GetCurrentWeather();
            result.Should().NotBeNull();
        }

        [Test]
        public async Task CallTheWeatherApi()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Any<IRestRequest>());
        }

        [Test]
        public async Task BuildTheRequest()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Resource.Equals("testuri")));
        }

        [Test]
        public async Task AddLatitudeParameter()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Parameters.Contains(new Parameter("lat", "2.1", ParameterType.QueryString))));
        }

        [Test]
        public async Task AddLongitudeParameter()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Parameters.Contains(new Parameter("lon", "4.2", ParameterType.QueryString))));
        }

        [Test]
        public async Task AddUnitsParameter()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Parameters.Contains(new Parameter("units", "metric", ParameterType.QueryString))));
        }

        [Test]
        public async Task AddApiKeyParameter()
        {
            await _service.GetCurrentWeather();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Parameters.Contains(new Parameter("appid", "testkey", ParameterType.QueryString))));
        }

        [Test]
        public async Task PopulateResponseObject()
        {
            var jsonString = GetTestDataText("weather-api-response.json");
            var expected = JsonConvert.DeserializeObject<WeatherResponse>(jsonString);

            var result = await _service.GetCurrentWeather();

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task SetMemoryCacheWhenResultRetrieved(){
            await _service.GetCurrentWeather();

            _cache.Received(1).CreateEntry(Arg.Is("CurrentWeather"));
        }

        [Test]
        public async Task NotCallWeatherServiceWithCacheHit(){
            _cache.TryGetValue("CurrentWeather", out _).Returns(true).AndDoes(x => {
                x[1] = new WeatherResponse();
            });
            await _service.GetCurrentWeather();

            await _client.ReceivedWithAnyArgs(0).ExecuteGetAsync(Arg.Any<IRestRequest>());
        }

        private string GetTestDataText(string fileName)
        {
            return File.ReadAllText($"./TestData/{fileName}");
        }
    }
}