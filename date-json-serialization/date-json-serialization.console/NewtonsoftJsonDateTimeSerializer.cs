using Newtonsoft.Json;

namespace date_json_serialization.console;

public class NewtonsoftJsonDateTimeSerializer<T>
{
    public T? Deserialize(string json, string dateFormat)
    {
        var options = new JsonSerializerSettings();
        options.Converters.Add(new NewtonsoftJsonDateTimeConverter(dateFormat));

        return JsonConvert.DeserializeObject<T>(json, options);
    }
}
