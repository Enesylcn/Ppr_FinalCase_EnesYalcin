using Autofac;
using Autofac.Extensions.DependencyInjection;
using DigitalStore.Business.RegistrationServices;
using DigitalStore.WebApi;
using Serilog;

namespace Para.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        Log.Information("Application is starting...");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .UseServiceProviderFactory(new AutofacServiceProviderFactory()) 
           .ConfigureContainer<ContainerBuilder>(containerBuilder =>
           {
               containerBuilder.RegisterModule(new AutofacBusinessModule());
           })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });
}