using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace minimal_webapi
{
    public static class JsonSerializerSettingsConfigurator
    {
        public static void Configure(JsonSerializerSettings settings)
        {
            settings.Converters.Add(new StringEnumConverter());
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        }
    }
}