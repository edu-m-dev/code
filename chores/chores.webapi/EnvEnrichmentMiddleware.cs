namespace chores.webapi;

public class EnvEnrichmentMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _environmentName;

    public EnvEnrichmentMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _environmentName = env.EnvironmentName;
    }

    public async Task Invoke(HttpContext context, ILogger<EnvEnrichmentMiddleware> logger)
    {
        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["env"] = _environmentName
        }))
        {
            await _next(context);
        }
    }
}
