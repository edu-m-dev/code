var builder = DistributedApplication.CreateBuilder(args);

// the string you pass to .AddDatabase("chores") in AppHost must match the name you use in GetConnectionString("chores").
var sql = builder.AddSqlServer("chores-sqlserver")
                 .AddDatabase("chores");

var migrations = builder.AddProject<Projects.chores_migrations>("chores-migrations")
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithReference(sql)
    .WaitFor(sql);

var redis = builder.AddRedis("chores-cache");

builder.AddProject<Projects.chores_webapi>("chores-webapi")
    .WithEnvironment("AzureMonitor__ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/")
    .WithReference(sql)
    .WaitFor(migrations)
    .WithReference(redis)
    .WaitFor(redis);

builder.Build().Run();
