var builder = DistributedApplication.CreateBuilder(args);

// the string you pass to .AddDatabase("chores") in AppHost must match the name you use in GetConnectionString("chores").
var sql = builder.AddSqlServer("chores-sqlserver")
                 .AddDatabase("chores");

var migrations = builder.AddProject<Projects.chores_migrations>("chores-migrations")
    .WithReference(sql)
    .WaitFor(sql);

builder.AddProject<Projects.chores_webapi>("chores-webapi")
    .WithReference(sql)
    .WaitFor(migrations);

builder.Build().Run();
