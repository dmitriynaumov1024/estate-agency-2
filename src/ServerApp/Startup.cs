using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServerApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // Here, site assets are served
            app.UseStaticFiles ("", "wwwroot");

            // Here, content is served
            app.UseStaticFiles ("/cdn", "cdn");

            // Here, Web API is served
            app.Map ( "/api", app => {
                app.UseRouting();
                app.UseSession();
                // Nested branches!
                app.UseEndpoints ( endpoints => {
                    endpoints.MapGet ("/person", Handlers.GetPerson);
                    endpoints.MapGet ("/object", Handlers.GetObject);

                    endpoints.MapPost ("/login", Handlers.PostLogin);
                    endpoints.MapPost ("/signup", Handlers.PostSignup);
                });
                // Fallback
                app.Run (Handlers.ApiHandler);
            });

            // Here, index.html is served. 
            // Some routes e.g. "/list" are handled on client side.
            app.Run (Handlers.DefaultGet);
        }
    }
}
