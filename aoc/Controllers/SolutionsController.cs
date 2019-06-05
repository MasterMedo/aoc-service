using System;
using aoc.Authorization;
using aoc.core.solutions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace aoc.Controllers
{
    [ApiController]
    [Authorization]
    [Route("AdventOfCode")]
    [Produces("application/json")]
    public class SolutionsController : ControllerBase
    {
        private readonly ISolutionsService _service;
        public SolutionsController(ISolutionsService service)
        {
            _service = service;
        }
        [HttpPost]
        [SwaggerResponse(200, "Request successful!", typeof(SolutionsResponse))]
        [SwaggerResponse(406, "The day or year is not valid", typeof(ProblemDetails))]
        [SwaggerResponse(501, "The day is not yet implemented :/", typeof(ProblemDetails))]
        public object GetResult(SolutionsRequest request)
        {
            try
            {
                return _service.GetSolution(request);
            }
            catch (SolutionsException e)
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
