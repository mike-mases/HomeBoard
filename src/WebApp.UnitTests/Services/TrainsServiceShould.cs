using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using RestSharp;
using FluentAssertions;
using System.Threading.Tasks;
using HomeBoard.Models.Configuration;
using HomeBoard.WebApp.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HomeBoard.WebApp.UnitTests.Services
{
    [TestFixture]
    public class TrainsServiceShould
    {
        private IOptions<TrainsConfiguration> _options;
        private IRestClient _client;
        private ILogger<TrainsService> _logger;
        private TrainsService _service;
        private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            _options = Substitute.For<IOptions<TrainsConfiguration>>();
            _options.Value.Returns(new TrainsConfiguration());
            _client = Substitute.For<IRestClient>();
            _logger = Substitute.For<ILogger<TrainsService>>();
            _cache = Substitute.For<IMemoryCache>();

            _service = new TrainsService(_options, _client, _logger, _cache);
        }

        [Test]
        public async Task ReturnStationBoard()
        {
            var result = await _service.GetStationBoard();

            result.Should().NotBeNull();
        }
    }
}