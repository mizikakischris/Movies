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
                //CreateHostBuilder(args).Build().Run();
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
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                        serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        serverOptions.Limits.MinRequestBodyDataRate =
                            new MinDataRate(bytesPerSecond: 100,
                                gracePeriod: TimeSpan.FromSeconds(10));
                        serverOptions.Limits.MinResponseDataRate =
                            new MinDataRate(bytesPerSecond: 100,
                                gracePeriod: TimeSpan.FromSeconds(10));
                        serverOptions.Limits.KeepAliveTimeout =
                            TimeSpan.FromMinutes(2);
                        serverOptions.Limits.RequestHeadersTimeout =
                            TimeSpan.FromMinutes(1);
                    })
                    .UseStartup<Startup>()
                          .ConfigureLogging(logging =>
                             {
                                 logging.ClearProviders();
                             })
                      .CaptureStartupErrors(true);

                }).UseNLog();
       
    }
}
