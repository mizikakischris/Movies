using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace Movie.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var logger = NLogBuilder.ConfigureNLog($"NLog.config").GetCurrentClassLogger();
            LogManager.ThrowExceptions = true;
            LogManager.ThrowConfigExceptions = true;
            try
            {
                logger.Info("Movies Web Api Started");
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Host terminated unexpectedly");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.UseStartup<Startup>()
                          .ConfigureLogging(logging =>
                             {
                                 logging.ClearProviders();
                             })
                      .CaptureStartupErrors(true);

                }).UseNLog();
       
    }
}
