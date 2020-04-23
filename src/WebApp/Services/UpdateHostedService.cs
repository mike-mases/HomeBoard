using System;
using System.Threading;
using System.Threading.Tasks;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeBoard.WebApp.Services
{
    public class UpdateHostedService : IHostedService, IDisposable
    {
        private const string SignalMethod = "homeBoardUpdate";
        private readonly IHubContext<HomeBoardUpdateHub> _hub;
        private readonly IHomeBoardViewModelBuilder _builder;
        private readonly ILogger<UpdateHostedService> _logger;
        private Timer _timer;

        public UpdateHostedService(IHubContext<HomeBoardUpdateHub> hub, IHomeBoardViewModelBuilder builder, ILogger<UpdateHostedService> logger)
        {
            _hub = hub;
            _builder = builder;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Update Hosted service");
            _timer = new Timer(async (e) =>
            {
                _logger.LogInformation("Sending update");
                var viewModel = await _builder.BuildViewModel();
                await _hub.Clients.All.SendAsync(SignalMethod, viewModel);
            }, null, 0, 2000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}