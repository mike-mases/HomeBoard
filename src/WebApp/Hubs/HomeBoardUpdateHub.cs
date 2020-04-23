using System.Threading.Tasks;
using HomeBoard.WebApp.Builders;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HomeBoard.WebApp.Hubs
{
    public class HomeBoardUpdateHub : Hub
    {
        private readonly IHomeBoardViewModelBuilder _builder;
        private readonly ILogger<HomeBoardUpdateHub> _logger;

        public HomeBoardUpdateHub(IHomeBoardViewModelBuilder builder, ILogger<HomeBoardUpdateHub> logger)
        {
            _builder = builder;
            _logger = logger;
        }

        public async Task UpdateHomeBoard()
        {
            var viewModel = await _builder.BuildViewModel();
            await Clients.All.SendAsync("homeBoardUpdate", viewModel);
        }
    }
}