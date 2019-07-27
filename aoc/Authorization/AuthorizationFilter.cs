using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace aoc.Authorization
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var username = context.HttpContext.Items["username"].ToString();
                var controller = context.RouteData.Values["controller"].ToString();
                var method = context.RouteData.Values["action"].ToString();

                var json = File.ReadAllText("authorization.json");
                var authorizations = JsonConvert.DeserializeObject<List<AuthorizationBean>>(json);

                var auth = authorizations.FirstOrDefault(x => x.Username == username && x.Controller == controller);
                if (auth != null && (auth.Methods == null || auth.Methods.Count == 0 || auth.Methods.Contains(method)))
                    return;
            }
            catch
            {
                // logging
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
