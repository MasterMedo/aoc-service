using System;
using Main.Authorization;
using Main.Core.FakeProject;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [ApiController]
    [Authorization]
    [Route("fake")]
    [Produces("application/json")]
    public class FakeController : ControllerBase
    {
        [HttpPost]
        public FakeResponse FakeBoy(FakeRequest request)
        {
            try
            {
                var service = HttpContext.Items["service"];
                return ((IFakeService) service).FakeMethod(request);
            }
            catch
            {
                // logging
            }

            HttpContext.Response.StatusCode = 400;
            return null;
        }
    }
}
