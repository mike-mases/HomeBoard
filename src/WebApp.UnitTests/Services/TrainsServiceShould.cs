using Homeboard.WebApp.Services;
using HomeBoard.Models.Confguration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using RestSharp;

namespace Homeboard.WebApp.UnitTests.Services
{
    [TestFixture]
    public class TrainsServiceShould
    {
        private IOptions<TrainsConfiguration> _options;
        private IRestClient _client;
        private ILogger<TrainsService> _logger;
        private TrainsService _service;

        [SetUp]
        public void Setup()
        {
            _options = Substitute.For<IOptions<TrainsConfiguration>>();
            _options.Value.Returns(new TrainsConfiguration());
            _client = Substitute.For<IRestClient>();
            _logger = Substitute.For<ILogger<TrainsService>>();

            _service = new TrainsService(_options, _client, _logger);
        }
    }
}