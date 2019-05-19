using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Main.Authentication
{
    public class Authentication
    {
        private readonly RequestDelegate _next;

        public Authentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string auth = context.Request.Headers["Authorization"];
            if (auth != null && auth.StartsWith("Basic"))
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var encoded = auth.Substring(6);
                var decoded = encoding.GetString(Convert.FromBase64String(encoded));

                var separator = decoded.IndexOf(":", StringComparison.Ordinal);
                var username = decoded.Substring(0, separator);
                var password = decoded.Substring(separator+1);

                var json = Encoding.Default.GetString(Properties.Resources.users).Substring(1);
                var users = JsonConvert.DeserializeObject<List<User>>(json);

                var user = users.FirstOrDefault(x => x.username == username && x.password == password);
                if (user != null)
                {
                    context.Items["username"] = username;
                    await _next.Invoke(context);
                    return;
                }
            }
            context.Response.StatusCode = 401;
        }
    }
}
