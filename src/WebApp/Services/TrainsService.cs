using System.Threading.Tasks;
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
            return new StationBoard();
        }
    }
}