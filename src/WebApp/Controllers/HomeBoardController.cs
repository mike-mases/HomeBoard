using System;
using System.Threading.Tasks;
using HomeBoard.WebApp.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeBoard.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeBoardController : ControllerBase
    {
        private readonly IHomeBoardViewModelBuilder _builder;
        private readonly ILogger<HomeBoardController> _logger;

        public HomeBoardController(IHomeBoardViewModelBuilder builder, ILogger<HomeBoardController> logger)
        {
            _builder = builder;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomeBoard()
        {
            try
            {
                var board = await _builder.BuildViewModel();
                return Ok(board);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while building response");
                return new StatusCodeResult(500);
            }
        }
    }
}