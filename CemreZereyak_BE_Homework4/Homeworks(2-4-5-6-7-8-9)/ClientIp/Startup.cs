using ClientIp.Filters;
using ClientIp.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




using System.IO;
using System.Linq;


using NLog;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace ClientIp
{
    public class Startup
    { 
        public Startup(IConfiguration configuratin)
        {
            Configuration = configuratin;
        }

        public IConfiguration Configuration { get; }
    
    
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ClientIdCheckFilter>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add framework services.
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            GlobalDiagnosticsContext.Set("configDir", "C:\\git\\damienbod\\ClientIpAspNetCoreIIS\\Logs");
            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("DefaultConnection"));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMiddleware<AdminSafeListMiddleware>(Configuration["AdminSafeList"]);

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
