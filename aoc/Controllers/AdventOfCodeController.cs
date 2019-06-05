using System;
using Main.Authorization;
using Main.Core.AdventOfCode;
using Microsoft.AspNetCore.Mvc;
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
        public AdventOfCodeController(IAdventOfCodeService service)
        {
            _service = service;
        }
        [HttpPost]
        [SwaggerResponse(200, "Request successful!", typeof(AdventOfCodeResponse))]
        [SwaggerResponse(406, "The day or year is not valid", typeof(ProblemDetails))]
        [SwaggerResponse(501, "The day is not yet implemented :/", typeof(ProblemDetails))]
        public object GetResult(AdventOfCodeRequest request)
        {
            try
            {
                return _service.GetResult(request);
            }
            catch (AdventOfCodeException e)
            {
                HttpContext.Response.StatusCode = e.Code;
                return new ProblemDetails { Status = e.Code, Title = e.Title, Detail = e.Message };
            }
            catch (Exception e)
            {
                // logging

                HttpContext.Response.StatusCode = 500;
                return new ProblemDetails { Status = 500, Title = "Internal server error.", Detail = e.Message };
            }
        }
    }
}
