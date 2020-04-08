using System.IO;
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
using HomeBoard.Models.Trains;
using System.Xml.Serialization;
using System.Net;

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
            _options.Value.Returns(new TrainsConfiguration
            {
                StationId = "testId"
            });
            _client = Substitute.For<IRestClient>();
            _logger = Substitute.For<ILogger<TrainsService>>();
            _cache = Substitute.For<IMemoryCache>();

            _service = new TrainsService(_options, _client, _logger, _cache);

            var content = GetTestDataStream("trains-feed-response.xml").ReadToEnd();
            _client.ExecuteGetAsync(Arg.Any<RestRequest>()).Returns(
                new RestResponse
                {
                    Content = content,
                    StatusCode = HttpStatusCode.OK,
                    ResponseStatus = ResponseStatus.Completed
                });
        }

        [Test]
        public async Task ReturnStationBoard()
        {
            var result = await _service.GetStationBoard();

            result.Should().NotBeNull();
        }

        [Test]
        public async Task CallTheTrainFeed()
        {
            await _service.GetStationBoard();
            await _client.Received(1).ExecuteGetAsync(Arg.Is<IRestRequest>(r => r.Resource.Equals("testId.xml")));
        }

        [Test]
        public async Task PopulateTheStationBoardObject()
        {
            var expected = GetTestData("trains-feed-response.xml");
            var result = await _service.GetStationBoard();

            result.Should().BeEquivalentTo(expected);
        }

        private StreamReader GetTestDataStream(string fileName)
        {
            return new StreamReader($"./TestData/{fileName}");
        }

        private StationBoard GetTestData(string fileName)
        {
            var stream = GetTestDataStream(fileName);
            var deserializer = new XmlSerializer(typeof(StationBoard));
            return deserializer.Deserialize(stream) as StationBoard;
        }
    }
}