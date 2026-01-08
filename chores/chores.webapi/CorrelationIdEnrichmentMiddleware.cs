using System.Diagnostics;

public class CorrelationIdEnrichmentMiddleware
{
    private readonly RequestDelegate _next;
    private const string HeaderName = "X-Correlation-ID";

    public CorrelationIdEnrichmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<CorrelationIdEnrichmentMiddleware> logger)
    {
        // Determine correlation ID
        var correlationId =
            context.Request.Headers.TryGetValue(HeaderName, out var cid)
                ? cid.ToString()
                : Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();

        // Make it available to downstream components
        context.Items["CorrelationId"] = correlationId;

        // Add it to the response for client visibility
        context.Response.Headers[HeaderName] = correlationId;

        // --- FULL OBSERVABILITY ENRICHMENT ---
        var activity = Activity.Current;

        if (activity != null)
        {
            // 1. Add as span attribute (shows up in traces/spans)
            activity.SetTag("correlationId", correlationId);

            // 2. Add as baggage (propagates to downstream services)
            activity.AddBaggage("correlationId", correlationId);
        }

        // --- LOG ENRICHMENT ---
        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["correlationId"] = correlationId
        }))
        {
            await _next(context);
        }
    }
}
