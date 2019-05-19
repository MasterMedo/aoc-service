using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Authorization;
using Main.Core.FakeProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [ApiController]
    [Authorization]
    [Route("v1/fake")]
    [Produces("application/json")]
    public class FakeController : ControllerBase
    {
        [HttpPost]
        public FakeResponse FakeBoy(FakeRequest request)
        {
            var className = HttpContext.Items["ClassName"].ToString();
            var assemblyName = HttpContext.Items["AssemblyName"].ToString();

            var service = Activator.CreateInstance(Type.GetType(assemblyName + '.' + className + ", " + assemblyName));
            return ((IFakeService) service).FakeMethod(request);
        }
    }
}
