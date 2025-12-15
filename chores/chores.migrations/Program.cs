using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Build configuration from appsettings.json and environment variables
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)   // must exist
    .AddEnvironmentVariables()                         // allows overrides in CI/CD or Azure
    .Build();

// Get the connection string named "chores"
var connection = config.GetConnectionString("chores");

// Configure DbContext for SQL Server
var options = new DbContextOptionsBuilder<ChoresDbContext>()
    .UseSqlServer(connection, sqlOptions =>
        sqlOptions.MigrationsAssembly("chores.migrations"))
    .Options;

// Apply migrations and exit
using var db = new ChoresDbContext(options);
await db.Database.MigrateAsync();

Console.WriteLine("SQL Server migrations applied successfully.");
