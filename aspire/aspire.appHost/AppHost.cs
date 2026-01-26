var builder = DistributedApplication.CreateBuilder(args);

// the string you pass to .AddDatabase("code") in AppHost must match the name you use in GetConnectionString("code").
var sql = builder.AddSqlServer("code-sqlserver")
                 .AddDatabase("code");

var migrations = builder.AddProject<Projects.chores_migrations>("chores-migrations")
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithReference(sql)
    .WaitFor(sql);

builder.AddProject<Projects.chores_webapi>("chores-webapi")
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithReference(sql)
    .WaitFor(migrations);

builder.Build().Run();
