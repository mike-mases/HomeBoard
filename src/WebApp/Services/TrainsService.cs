using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HomeBoard.Models.Configuration;
using HomeBoard.Models.Trains;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace HomeBoard.WebApp.Services
{
    public class TrainsService : ITrainsService
    {
        private const string CacheKey = "StationBoard";
        private readonly TrainsConfiguration _config;
        private readonly IRestClient _client;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public TrainsService(
            IOptions<TrainsConfiguration> options,
            IRestClient client,
            ILogger<TrainsService> logger,
            IMemoryCache cache)
        {
            _config = options.Value;
            _client = client;
            _logger = logger;
            _cache = cache;
        }

        public async Task<StationBoard> GetStationBoard()
        {
            var content = await _cache.GetOrCreateAsync(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_config.CacheTimeoutSeconds);
                return GetStationBoardFromService();
            });

            return content;
        }

        private async Task<StationBoard> GetStationBoardFromService()
        {
            var result = await _client.ExecuteGetAsync(BuildRequest());

            if (!result.IsSuccessful)
            {
                _logger.LogError($"Call to XML feed failed for station ID: {_config.StationId}");
                return new StationBoard();
            }

            return DeserialiseStationBoard(result.Content);
        }

        private StationBoard DeserialiseStationBoard(string content)
        {
            try
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                var deserializer = new XmlSerializer(typeof(StationBoard));
                var board = deserializer.Deserialize(stream) as StationBoard;
                FilterTrains(board);
                return board;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, "Trains Service XML deserialise failure.");
                return new StationBoard();
            }
        }

        private void FilterTrains(StationBoard board)
        {
            var endStations = _config.Destinations;

            if (!endStations.Any())
            {
                return;
            }

            board.Services = board.Services.Where(
                s => endStations.Contains(s.EndStation.Crs) ||
                endStations.Intersect(s.EndStationCallingPoints.CallingPoint.Select(c => c.Crs)).Any())
                .ToList();
        }

        private RestRequest BuildRequest()
        {
            var uri = $"{_config.StationId}.xml";
            return new RestRequest(uri, Method.GET);
        }
    }
}