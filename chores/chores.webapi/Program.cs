using chores.bl;
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

// MediatR - register all handlers in this assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.Run();

public partial class Program { } // make accessible for integration tests
