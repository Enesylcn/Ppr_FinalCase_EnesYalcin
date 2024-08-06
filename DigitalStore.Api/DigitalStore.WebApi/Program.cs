using Autofac.Extensions.DependencyInjection;
using NLog.Web;
using System.Reflection.Metadata;

namespace DigitalStore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
        try
        {
            logger.Debug("init main");

            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, ex.Message);
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            //.UseSerilog()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
            })
           .UseNLog();  // NLog: Setup NLog for Dependency injection;
}