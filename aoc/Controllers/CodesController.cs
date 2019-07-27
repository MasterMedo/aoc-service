using System;
using aoc.Authorization;
using aoc.core.Exceptions;
using aoc.core.codes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace aoc.Controllers
{
    [ApiController]
    [Authorization]
    [Route("codes")]
    [Produces("application/json")]
    public class CodesController : ControllerBase
    {
        private readonly ICodesService _service;
        public CodesController(ICodesService service)
        {
            _service = service;
        }
        [HttpPost]
        [SwaggerResponse(200, "ok.", typeof(CodesResponse))]
        [SwaggerResponse(400, "4xx -> bad request.", typeof(ProblemDetails))]
        [SwaggerResponse(500, "5xx -> server error.", typeof(ProblemDetails))]
        public object GetResult(CodesRequest request)
        {
            try
            {
                return _service.GetCode(request);
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