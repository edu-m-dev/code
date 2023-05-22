using Newtonsoft.Json.Converters;

namespace date_json_serialization.console;

public class NewtonsoftJsonDateTimeConverter : IsoDateTimeConverter
{
    public NewtonsoftJsonDateTimeConverter(string dateFormat)
    {
        DateTimeFormat = dateFormat;
    }
}
