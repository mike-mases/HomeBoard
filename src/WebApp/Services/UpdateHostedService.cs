using System;
using System.Threading;
using System.Threading.Tasks;
using HomeBoard.Models.Configuration;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HomeBoard.WebApp.Services
{
    public class UpdateHostedService : IHostedService, IDisposable
    {
        private const string SignalMethod = "homeBoardUpdate";
        private readonly IHubContext<HomeBoardUpdateHub> _hub;
        private readonly IHomeBoardViewModelBuilder _builder;
        private readonly ILogger<UpdateHostedService> _logger;
        private Timer _timer;
        private UpdateServiceConfiguration _config;

        public UpdateHostedService(
            IHubContext<HomeBoardUpdateHub> hub,
            IHomeBoardViewModelBuilder builder,
            ILogger<UpdateHostedService> logger,
            IOptions<UpdateServiceConfiguration> options)
        {
            _hub = hub;
            _builder = builder;
            _logger = logger;
            _config = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Update Hosted Service");
            _timer = new Timer(async (e) =>
            {
                await UpdateClients();
            }, null, 0, _config.RefreshMilliseconds);

            return Task.CompletedTask;
        }

        private async Task UpdateClients()
        {
            try
            {
                var viewModel = await _builder.BuildViewModel();
                await _hub.Clients.All.SendAsync(SignalMethod, viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error sending update to clients");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Hosted Service is stopping");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}