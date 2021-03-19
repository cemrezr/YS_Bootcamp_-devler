using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace WebsiteStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Users\CZ\source\repos\week4-homework4.2-cemrezereyak\LogFile.txt")
                .CreateLogger();

            try
            {
                Log.Information("Starting up the service");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the service");
                return;
                
            }
            finally
            {
                Log.CloseAndFlush();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
            .UseSerilog();
    }
}
