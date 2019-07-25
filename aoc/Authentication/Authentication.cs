using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace aoc.Authentication
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
            try
            {
                string auth = context.Request.Headers["Authorization"];
                if (auth != null && auth.StartsWith("Basic"))
                {
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var encoded = auth.Substring(6);
                    var decoded = encoding.GetString(Convert.FromBase64String(encoded));

                    var separator = decoded.IndexOf(":", StringComparison.Ordinal);
                    var username = decoded.Substring(0, separator);
                    var password = decoded.Substring(separator + 1);

                    var json = File.ReadAllText("users.json");
                    var users = JsonConvert.DeserializeObject<List<User>>(json);

                    var user = users.FirstOrDefault(x => x.Username == username && x.Password == password);
                    if (user != null)
                    {
                        context.Items["username"] = username;
                        await _next.Invoke(context);
                        return;
                    }
                }
            }
            catch
            {
                // logging
            }

            context.Response.StatusCode = 401;
        }
    }
}
