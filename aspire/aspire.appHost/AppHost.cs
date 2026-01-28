using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// the string you pass to .AddDatabase("code") in AppHost must match the name you use in GetConnectionString("code").
var sql = builder.AddSqlServer("code-sqlserver")
                 .AddDatabase("code");

var migrations = builder.AddProject<Projects.chores_migrations>("chores-migrations")
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithReference(sql)
    .WaitFor(sql);

var webapiPort = builder.Configuration.GetValue<int>("ApiPort", 54196);

builder.AddProject<Projects.chores_webapi>("chores-webapi")
    .WithExternalHttpEndpoints() // keeps the default external endpoint
    .WithHttpsEndpoint(
        name: "fixed-https",       // must be unique
        port: webapiPort          // your fixed port
    )
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithEnvironment("AzureAd__Instance", "https://login.microsoftonline.com")
    .WithEnvironment("AzureAd__TenantId", "4ff270ac-352c-404c-ac7e-d6280a937f61")
    .WithEnvironment("AzureAd__ClientId", "697b17b5-9bcc-4504-b514-ec55f55e2f26")
    .WithEnvironment("AzureAd__ApiScope", "api://07d90b1e-4149-4147-ad04-382cd0c9c6f8")
    .WithReference(sql)
    .WaitFor(migrations);

builder.Build().Run();
