using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HomeBoard.Models;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Hubs;
using HomeBoard.WebApp.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace HomeBoard.WebApp.UnitTests.Services
{
    [TestFixture]
    public class UpdateHostedServiceShould
    {
        private IHubContext<HomeBoardUpdateHub> _hub;
        private IHomeBoardViewModelBuilder _builder;
        private ILogger<UpdateHostedService> _logger;
        private UpdateHostedService _service;
        private IClientProxy _clientProxy;
        private HomeBoardViewModel _model;

        [SetUp]
        public void Setup()
        {
            _model = new HomeBoardViewModel();
            _hub = Substitute.For<IHubContext<HomeBoardUpdateHub>>();
            _builder = Substitute.For<IHomeBoardViewModelBuilder>();
            _builder.BuildViewModel().Returns(_model);
            _logger = Substitute.For<ILogger<UpdateHostedService>>();
            var clients = Substitute.For<IHubClients>();
            _clientProxy = Substitute.For<IClientProxy>();
            _hub.Clients.Returns(clients);
            clients.All.Returns(_clientProxy);
            _service = new UpdateHostedService(_hub, _builder, _logger);
        }

        [Test]
        public async Task CallBuilderWhenServiceIsStarted()
        {
            await _service.StartAsync(CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            await _service.StopAsync(CancellationToken.None);

            await _builder.Received(1).BuildViewModel();
        }

        [Test]
        public async Task SendUpdateToClients()
        {
            await _service.StartAsync(CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            await _service.StopAsync(CancellationToken.None);

            await _clientProxy.Received(1).SendCoreAsync(
                "homeBoardUpdate",
                Arg.Is<object[]>(o => o.Contains(_model)));
        }

        [Test]
        public async Task HandleExceptions()
        {
            _builder.BuildViewModel().Throws<Exception>();

            Func<Task> act = async () =>
            {
                await _service.StartAsync(CancellationToken.None);
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                await _service.StopAsync(CancellationToken.None);
            };

            await act.Should().NotThrowAsync<Exception>();
        }

        [Test]
        public async Task NotUpdateClientsWhenException()
        {
            _builder.BuildViewModel().Throws<Exception>();

            Func<Task> act = async () =>
            {
                await _service.StartAsync(CancellationToken.None);
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                await _service.StopAsync(CancellationToken.None);
            };

            await _clientProxy.Received(0).SendCoreAsync(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [Test]
        public async Task LogException()
        {
            _builder.BuildViewModel().Throws<Exception>();

            await _service.StartAsync(CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            await _service.StopAsync(CancellationToken.None);

            _logger.Received(1).LogError(Arg.Any<Exception>(), "Error sending update to clients");
        }

        [Test]
        public async Task LogWhenServiceStarted()
        {
            await _service.StartAsync(CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            await _service.StopAsync(CancellationToken.None);

            _logger.Received(1).LogInformation("Starting Update Hosted Service");
        }

        [Test]
        public async Task LogWhenServiceStopped()
        {
            await _service.StartAsync(CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(0.1));
            await _service.StopAsync(CancellationToken.None);

            _logger.Received(1).LogInformation("Update Hosted Service is stopping");
        }
    }
}