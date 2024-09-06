using System.Diagnostics.Metrics;

namespace adventureWorks.webApi;

public static class DiagnosticsConfig
{
    public const string ServiceName = "AdventureWorks";

    public static Meter Meter = new(ServiceName);

    public static Counter<int> SalesCounter = Meter.CreateCounter<int>("sales.count");
}
