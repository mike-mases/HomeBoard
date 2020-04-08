using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBoard.WebApp.Services;
using HomeBoard.Models.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HomeBoard.Models.Trains;

namespace HomeBoard.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ITrainsService _trainsService;

        public WeatherResponse Weather { get; set; }
        public StationBoard Board { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger, 
            IWeatherService weatherService,
            ITrainsService trainsService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _trainsService = trainsService;
        }

        public async Task OnGet()
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress.ToString();
            _logger.LogInformation($"Weather page requested from IP {clientIp}");
            Weather = await _weatherService.GetCurrentWeather();
            Board = await _trainsService.GetStationBoard();
        }
    }
}
