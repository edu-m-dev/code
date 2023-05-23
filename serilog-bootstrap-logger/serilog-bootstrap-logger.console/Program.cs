using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateBootstrapLogger();

try
{
    await Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, configuration) => configuration
                .WriteTo.Console()
                // But, we have access to configuration and services from the host
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services))
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ConsoleHostedService>();
        })
        .RunConsoleAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}
