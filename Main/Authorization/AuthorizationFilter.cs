using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Main.Authorization
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var username = context.HttpContext.Items["username"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();
            var method = context.RouteData.Values["action"].ToString();

            var json = Encoding.Default.GetString(Properties.Resources.authorization).Substring(1);
            var authorizations = JsonConvert.DeserializeObject<List<AuthorizationBean>>(json);

            var authorization = authorizations.FirstOrDefault(x => x.Username == username && x.Controller == controller);
            if (authorization != null && (authorization.PermittedMethods.Count == 0 || authorization.PermittedMethods.Contains(method)))
            {
                context.HttpContext.Items["assemblyName"] = authorization.AssemblyName;
                context.HttpContext.Items["className"] = authorization.ClassName;
                return;
            }
            context.HttpContext.Response.Headers["WW-Authenticate"] = "Basic";
            context.Result = new UnauthorizedResult();
        }
    }
}
