var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.chores_webapi>("chores-webapi");

builder.Build().Run();
