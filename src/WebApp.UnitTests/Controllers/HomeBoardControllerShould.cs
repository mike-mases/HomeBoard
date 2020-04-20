using System;
using System.Threading.Tasks;
using FluentAssertions;
using HomeBoard.Models;
using HomeBoard.WebApp.Builders;
using HomeBoard.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace HomeBoard.WebApp.UnitTests.Controllers
{
    [TestFixture]
    public class HomeBoardControllerShould
    {
        private HomeBoardController _controller;
        private IHomeBoardViewModelBuilder _builder;
        private ILogger<HomeBoardController> _logger;

        [SetUp]
        public void Setup()
        {
            _builder = Substitute.For<IHomeBoardViewModelBuilder>();
            _builder.BuildViewModel().Returns(new HomeBoardViewModel());
            _logger = Substitute.For<ILogger<HomeBoardController>>();

            _controller = new HomeBoardController(_builder, _logger);
        }

        [Test]
        public async Task ReturnOkObjectResult()
        {
            var result = await _controller.GetHomeBoard();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task HandleExeptions()
        {
            _builder.BuildViewModel().Throws(new Exception());
            
            await _controller.Invoking(c => c.GetHomeBoard()).Should().NotThrowAsync<Exception>();
        }

        [Test]
        public async Task Return500StatusResponseIfException()
        {
            _builder.BuildViewModel().Throws(new Exception());
            
            var result = await _controller.GetHomeBoard();

            result.Should().BeOfType<StatusCodeResult>()
            .Which.StatusCode.Should().Be(500);
        }
    }
}