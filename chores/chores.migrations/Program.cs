using chores.bl.ef;
using chores.migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connection = configuration.GetConnectionString("chores");
        services.AddDbContext<ChoresDbContext>(
            options => options.UseSqlite(connection, sqlOptions =>
                sqlOptions.MigrationsAssembly("chores.migrations")));
        services.AddHostedService<MigrationHostedService>();
    })
    .Build();

await host.RunAsync();
