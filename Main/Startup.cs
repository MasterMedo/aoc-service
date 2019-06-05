using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Authorization;
using Main.Core.AdventOfCode;
using Main.Core.FakeProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // add custom services
            services.AddTransient<IAdventOfCodeService, AdventOfCode.AdventOfCodeService>();
            services.AddTransient<IFakeService, FakeProject.FakeService>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("basic", new ApiKeyScheme
                {
                    Description = "Basic AuthorizationBean using Username and Password",
                    Type = "basic"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "basic", Enumerable.Empty<string>() }
                });
                c.SwaggerDoc("v1", new Info {Title = "Middleware", Version = "v1"});
                c.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Middleware";
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Middleware");
                c.RoutePrefix = string.Empty;
            });
            app.UseMiddleware<Authentication.Authentication>();
            app.UseCors(b => b.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
