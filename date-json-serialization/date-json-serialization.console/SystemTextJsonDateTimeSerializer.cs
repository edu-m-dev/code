namespace date_json_serialization.console;

public class SystemTextJsonDateTimeSerializer<T>
{
    public T? Deserialize(string json, string dateFormat)
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            AllowTrailingCommas = true,
        };
        options.Converters.Add(new SystemTextJsonDateTimeConverter(dateFormat));

        return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
    }
}
