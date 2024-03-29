﻿using Microsoft.AspNetCore.Mvc;
using Streaming.Controllers.Base;
using Streaming.Services;

namespace Streaming.Controllers
{
    [ApiController]
    [Route("stream")]
    public class HomeController : StreamBaseController
    {
        public HomeController(ILogger<HomeController> logger)
            : base(logger)
        { }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index(
            [FromServices] IStreamService streamService,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Requested stream");

            try
            {
                streamService.Stream((object obj) =>
                {
                    Send(obj);
                }, cancellationToken);

                return Streaming();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message + Environment.NewLine + exception.StackTrace);

                return StatusCode(500, "Couldn't stream data");
            }
        }
    }
}