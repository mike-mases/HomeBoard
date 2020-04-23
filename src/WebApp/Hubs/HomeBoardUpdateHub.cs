using System;
using System.Threading.Tasks;
using HomeBoard.WebApp.Builders;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HomeBoard.WebApp.Hubs
{
    public class HomeBoardUpdateHub : Hub
    {
        private const string SignalMethod = "homeBoardUpdate";
        private readonly IHomeBoardViewModelBuilder _builder;
        private readonly ILogger<HomeBoardUpdateHub> _logger;

        public HomeBoardUpdateHub(IHomeBoardViewModelBuilder builder, ILogger<HomeBoardUpdateHub> logger)
        {
            _builder = builder;
            _logger = logger;
        }

        public async Task UpdateHomeBoard()
        {
            try
            {
                var viewModel = await _builder.BuildViewModel();
                await Clients.All.SendAsync(SignalMethod, viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error sending update");
            }
        }
    }
}