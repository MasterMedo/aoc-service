using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoc.core.codes;
using aoc.core.helper;
using aoc.core.solutions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace aoc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            // add custom services
            services.AddTransient<IHelper, helper.Helper>();
            services.AddTransient<ICodesService, codes.CodesService>();
            services.AddTransient<ISolutionsService, solutions.SolutionsService>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("basic", new ApiKeyScheme
                {
                    Description = "username e.g. medo, password e.g. medo",
                    Type = "basic"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "basic", Enumerable.Empty<string>() }
                });
                c.SwaggerDoc("v1", new Info {Title = "Advent Of Code", Version = "v1"});
                c.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Advent Of Code";
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Advent Of Code");
                c.RoutePrefix = string.Empty;
            });
            app.UseMiddleware<Authentication.Authentication>();
            app.UseCors(b => b.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
