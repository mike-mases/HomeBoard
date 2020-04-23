using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HomeBoard.Models;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace HomeBoard.WebApp.UnitTests.Hubs
{
    [TestFixture]
    public class HomeBoardUpdateHubShould
    {
        private HomeBoardUpdateHub _hub;
        private ILogger<HomeBoardUpdateHub> _logger;
        private IHomeBoardViewModelBuilder _builder;
        private IClientProxy _clientProxy;
        private HomeBoardViewModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new HomeBoardViewModel();
            _logger = Substitute.For<ILogger<HomeBoardUpdateHub>>();
            _builder = Substitute.For<IHomeBoardViewModelBuilder>();
            _builder.BuildViewModel().Returns(_model);
            _clientProxy = Substitute.For<IClientProxy>();
            var clients = Substitute.For<IHubCallerClients>();
            _hub = new HomeBoardUpdateHub(_builder, _logger);
            _hub.Clients = clients;
            clients.All.Returns(_clientProxy);
        }

        [Test]
        public async Task BuildViewModel()
        {
            await _hub.UpdateHomeBoard();

            await _builder.Received(1).BuildViewModel();
        }

        [Test]
        public async Task SendMessageToClients()
        {
            await _hub.UpdateHomeBoard();

            await _clientProxy.Received(1).SendCoreAsync("homeBoardUpdate", Arg.Is<object[]>(o => o.Contains(_model)));
        }

        [Test]
        public async Task HandleExceptions()
        {
            _builder.BuildViewModel().Throws(new Exception());
            await _hub.Invoking(h => h.UpdateHomeBoard()).Should().NotThrowAsync<Exception>();
        }

        [Test]
        public async Task NotUpdateClientsWhenException()
        {
            _builder.BuildViewModel().Throws(new Exception());
            await _hub.UpdateHomeBoard();

            await _clientProxy.Received(0).SendCoreAsync(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [Test]
        public async Task LogExceptions()
        {
            var exception = new Exception();
            _builder.BuildViewModel().Throws(exception);
            await _hub.UpdateHomeBoard();

            _logger.Received(1).LogError(exception, "Error sending update");
        }
    }
}