using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Intrinsics.X86;
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
            var result = await _client.ExecuteGetAsync(BuildRequest());
            return DeserialiseStationBoard(result.Content);
        }

        private StationBoard DeserialiseStationBoard(string content){
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var deserializer = new XmlSerializer(typeof(StationBoard));
            var board = deserializer.Deserialize(stream) as StationBoard;
            FilterTrains(board);
            return board;
        }

        private void FilterTrains(StationBoard board){
            var endStations = _config.Destinations;

            if (!endStations.Any()){
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