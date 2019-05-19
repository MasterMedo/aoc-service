using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Authorization;
using Main.Core.AdventOfCode;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Main.Controllers
{
    [ApiController]    [Authorization]    [Route("v1/AdventOfCode")]    [Produces("application/json")]    public class AdventOfCodeController : ControllerBase
    {
        [HttpPost]
        [SwaggerResponse(200, "ok", typeof(AdventOfCodeResponse))]
        public AdventOfCodeResponse GetResult(AdventOfCodeRequest request)
        {
            var className = HttpContext.Items["className"].ToString();
            var assemblyName = HttpContext.Items["assemblyName"].ToString();
            var service = Activator.CreateInstance(Type.GetType(assemblyName + '.' + className + ", " + assemblyName));

            AdventOfCodeResponse result = null;

            try
            {
                result = ((IAdventOfCodeService) service).GetResult(request);
            }
            catch
            {
                // ignored
            }

            if (result == null)
                HttpContext.Response.StatusCode = 400;
            return result;
        }
    }
}
