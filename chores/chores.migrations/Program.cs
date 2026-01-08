using System.Diagnostics;
using System.Diagnostics.Metrics;
using Azure.Monitor.OpenTelemetry.Exporter;
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

// -------------------------------
// 1. Configuration
// -------------------------------
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .AddCommandLine(args) // expects --env dev|uat etc
    .Build();

// env comes from command line: --env dev
var environment = config["env"] ?? "unknown";
var aiConnectionString = config["AzureMonitor:ConnectionString"];

// -------------------------------
// 2. OpenTelemetry Resource
// -------------------------------
var resource = ResourceBuilder.CreateDefault()
    .AddService(
        serviceName: "chores-migrations",
        serviceVersion: config["BUILD_VERSION"] ?? "unknown")
    .AddAttributes(new[]
    {
        new KeyValuePair<string, object>("deployment.environment", environment)
    });

// -------------------------------
// 3. ActivitySource for spans
// -------------------------------
using var activitySource = new ActivitySource("chores-migrations");

// -------------------------------
// 4. Metrics: migration duration
// -------------------------------
var meter = new Meter("chores-migrations");
var migrationDuration = meter.CreateHistogram<double>("migration.duration.ms");

// -------------------------------
// 5. Tracing pipeline
// -------------------------------
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(resource)
    .AddSource("chores-migrations")
    .AddAzureMonitorTraceExporter(o =>
    {
        o.ConnectionString = aiConnectionString;
    })
    .Build();

// -------------------------------
// 6. Logging pipeline (structured logs)
// -------------------------------
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(o =>
    {
        o.SetResourceBuilder(resource);
        o.IncludeScopes = true;
        o.ParseStateValues = true;
        o.AddAzureMonitorLogExporter(options =>
        {
            options.ConnectionString = aiConnectionString;
        });
    });
});

var logger = loggerFactory.CreateLogger("Migrations");

// -------------------------------
// 7. Run migrations with full observability
// -------------------------------
using var activity = activitySource.StartActivity("ApplyMigrations", ActivityKind.Internal);

var sw = Stopwatch.StartNew();

logger.LogInformation("Starting migrations for environment {env}", environment);

// DB connection
var connection = config.GetConnectionString("chores");

var options = new DbContextOptionsBuilder<ChoresDbContext>()
    .UseSqlServer(connection, sqlOptions =>
        sqlOptions.MigrationsAssembly("chores.migrations"))
    .Options;

using var db = new ChoresDbContext(options);
await db.Database.MigrateAsync();

sw.Stop();

// Record metric
migrationDuration.Record(sw.ElapsedMilliseconds);

// Structured log
logger.LogInformation(
    "SQL Server migrations applied successfully in {Duration} ms for environment {env}",
    sw.ElapsedMilliseconds,
    environment
);
