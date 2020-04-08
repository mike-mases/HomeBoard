using System;
using System.Threading.Tasks;
using HomeBoard.Models.Confguration;
using HomeBoard.Models.Trains;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Homeboard.WebApp.Services
{
    public class TrainsService
    {
        private readonly TrainsConfiguration _config;
        private readonly IRestClient _client;
        private readonly ILogger _logger;

        public TrainsService(IOptions<TrainsConfiguration> options, IRestClient client, ILogger<TrainsService> logger)
        {
            _config = options.Value;
            _client = client;
            _logger = logger;
        }

        public async Task<StationBoard> GetStationBoard()
        {
            return new StationBoard();
        }
    }
}