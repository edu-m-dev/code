using chores.bl;
using chores.bl.ef;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
var connection = builder.Configuration.GetConnectionString("chores");
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<ChoresDbContext>(options =>
        options.UseInMemoryDatabase("chores"));
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

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("chores-cache");
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

// MediatR - register all handlers in this assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDeveloperExceptionPage();
app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.Run();

public partial class Program { } // make accessible for integration tests
