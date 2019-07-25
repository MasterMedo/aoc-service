using System;
using Main.Authorization;
using Main.Core.AdventOfCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Main.Controllers
{
    [ApiController]
    [Authorization]
    [Route("AdventOfCode")]
    [Produces("application/json")]
    public class AdventOfCodeController : ControllerBase
    {
        private readonly IAdventOfCodeService _service;
        private readonly ILogger<AdventOfCodeController> _logger;
        public AdventOfCodeController(IAdventOfCodeService service, ILogger<AdventOfCodeController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        [SwaggerResponse(200, "Request successful!", typeof(AdventOfCodeResponse))]
        [SwaggerResponse(406, "The day or year is not valid", typeof(ProblemDetails))]
        [SwaggerResponse(501, "The day is not yet implemented :/", typeof(ProblemDetails))]
        public object GetResult(AdventOfCodeRequest request)
        {
            try
            {
                _logger.LogInformation("AdventOfCodeController.GetResult called.");
                return _service.GetResult(request);
            }
            catch (AdventOfCodeException e)
            {
                _logger.LogError("AdventOfCodeController.GetResult failed." + e.Message);
                HttpContext.Response.StatusCode = e.Code;
                return new ProblemDetails { Status = e.Code, Title = e.Title, Detail = e.Message };
            }
            catch (Exception e)
            {
                // logging
                _logger.LogError("AdventOfCodeController.GetResult failed." + e.Message);
                HttpContext.Response.StatusCode = 500;
                return new ProblemDetails { Status = 500, Title = "Internal server error.", Detail = e.Message };
            }
        }
    }
}
