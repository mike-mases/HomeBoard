namespace WebApp.UnitTests.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
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
        private ILogger<WeatherService> _logger;

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
            _logger = Substitute.For<ILogger<WeatherService>>();

            _service = new WeatherService(_client, _config, _cache, _logger);
            var jsonString = GetTestDataText("weather-api-response.json");
            _client.ExecuteGetAsync(Arg.Any<IRestRequest>()).ReturnsForAnyArgs
            (new RestResponse
            {
                Content = jsonString,
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });
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
        public async Task SetMemoryCacheWhenResultRetrieved()
        {
            await _service.GetCurrentWeather();

            _cache.Received(1).CreateEntry(Arg.Is("CurrentWeather"));
        }

        [Test]
        public async Task NotCallWeatherServiceWithCacheHit()
        {
            _cache.TryGetValue("CurrentWeather", out _).Returns(true).AndDoes(x =>
            {
                x[1] = new WeatherResponse();
            });
            await _service.GetCurrentWeather();

            await _client.ReceivedWithAnyArgs(0).ExecuteGetAsync(Arg.Any<IRestRequest>());
        }

        [Test]
        public async Task HandleUnsucessful()
        {
            _client.ExecuteGetAsync(Arg.Any<IRestRequest>()).ReturnsForAnyArgs(new RestResponse { StatusCode = HttpStatusCode.InternalServerError });
            var result = await _service.GetCurrentWeather();

            result.Should().BeEquivalentTo(new WeatherResponse());
        }

        [Test]
        public async Task HandleMalformedJson()
        {
            _client.ExecuteGetAsync(Arg.Any<IRestRequest>()).ReturnsForAnyArgs
            (new RestResponse
            {
                Content = "{malformedjson",
                StatusCode = HttpStatusCode.OK,
                ResponseStatus = ResponseStatus.Completed
            });

            await _service.Invoking(s => s.GetCurrentWeather()).Should().NotThrowAsync<JsonReaderException>();
        }

        [Test]
        public async Task LogUnsuccessfulRequests()
        {
            _client.ExecuteGetAsync(Arg.Any<IRestRequest>()).ReturnsForAnyArgs(new RestResponse { StatusCode = HttpStatusCode.InternalServerError });
            var result = await _service.GetCurrentWeather();

            // _logger.Received(1).Log(Arg.Is(LogLevel.Error), Arg.Is("Call to service failed for lat: 2.1, lon: 4.2, units: metric, key: testkey, uri: testuri"));
            _logger.Received(1).Log<object>(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            null,
            Arg.Any<Func<object, Exception, string>>()
            );
        }

        private string GetTestDataText(string fileName)
        {
            return File.ReadAllText($"./TestData/{fileName}");
        }
    }
}