using Azure.Monitor.OpenTelemetry.AspNetCore;
using chores.bl;
using chores.bl.ef;
using chores.webapi;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "chores api";
    config.Version = "v1";
});

// DbContext
var connection = builder.Configuration.GetConnectionString("code");
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<ChoresDbContext>(options =>
        options.UseInMemoryDatabase("code"));
}
else
{
    builder.Services.AddDbContext<ChoresDbContext>(options =>
        options.UseSqlServer(connection));
}

// IHttpContextAccessor for accessing the current principal in controllers
builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddScoped<IChoresService, ChoresService>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("code");
    options.SchemaName = "dbo";
    options.TableName = "chores_cache";
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

// MediatR - register all handlers in this assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Structured logging with OpenTelemetry
builder.Logging.AddOpenTelemetry(options =>
{
    options.ParseStateValues = true;
    options.IncludeScopes = true;
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddAttributes(
    [
        new KeyValuePair<string, object>("deployment.environment", builder.Environment.EnvironmentName)
    ]))
    .UseAzureMonitor();

var app = builder.Build();

// 1. Dimension middlewares FIRST
app.UseMiddleware<CorrelationIdEnrichmentMiddleware>();
app.UseMiddleware<EnvEnrichmentMiddleware>();

// 2. Diagnostics / developer tools
app.UseDeveloperExceptionPage();

// 3. API surface
app.UseOpenApi();
app.UseSwaggerUi();

// 4. Routing + controllers
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();

public partial class Program { } // make accessible for integration tests
