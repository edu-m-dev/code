using Azure.Monitor.OpenTelemetry.AspNetCore;
using chores.bl;
using chores.bl.ef;
using chores.webapi;
using chores.webapi.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    config.AddSecurity("oauth2", new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.OAuth2,
        Flow = NSwag.OpenApiOAuth2Flow.AccessCode,
        AuthorizationUrl = $"{builder.Configuration["AzureAd:Instance"]}/{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize",
        TokenUrl = $"{builder.Configuration["AzureAd:Instance"]}/{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token",
        Scopes = new Dictionary<string, string>
        {
            { $"{builder.Configuration["AzureAd:ApiScope"]}/api.read", "Read access to Chores API" },
            { $"{builder.Configuration["AzureAd:ApiScope"]}/api.write", "Write access to Chores API" }
        }
    });

    config.OperationProcessors.Add(new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("oauth2"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{builder.Configuration["AzureAd:Instance"]}/{builder.Configuration["AzureAd:TenantId"]}/v2.0";
        options.Audience = builder.Configuration["AzureAd:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadAccess", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            return user.HasScope("api.read") || user.HasRole("Chores.Api.Read");
        }));

    options.AddPolicy("WriteAccess", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            return user.HasScope("api.write") || user.HasRole("Chores.Api.Write");
        }));
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

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IChoresService, ChoresService>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("code");
    options.SchemaName = "dbo";
    options.TableName = "chores_cache";
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

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

app.UseMiddleware<CorrelationIdEnrichmentMiddleware>();
app.UseMiddleware<EnvEnrichmentMiddleware>();

app.UseDeveloperExceptionPage();

app.UseOpenApi();
app.UseSwaggerUi(settings =>
{
    settings.OAuth2Client = new NSwag.AspNetCore.OAuth2ClientSettings
    {
        ClientId = builder.Configuration["AzureAd:ClientId"],
        ScopeSeparator = " ",
        ClientSecret = null,
        UsePkceWithAuthorizationCodeGrant = true
    };
});

app.MapDefaultEndpoints();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public partial class Program { }
