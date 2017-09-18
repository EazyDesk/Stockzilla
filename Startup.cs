using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Stockzilla.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stockzilla
{
    public class Startup
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StockzillaUser, IdentityRole>(config=>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 3;
                config.Password.RequireLowercase = false;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            })
            .AddEntityFrameworkStores<StockzillaContext>();

            services.AddSingleton(_config);
            services.AddDbContext<Models.StockzillaContext>();
            services.AddScoped<Stockzilla.Models.StockzillaRespository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "products", action = "list" }
                    );
            });

        }
    }
}
