using date_json_serialization.console;

while (true)
{
    Console.WriteLine("type date and its format");

    var input = Console.ReadLine()?.Split(" ");
    string? dateString = input?.Take(1).FirstOrDefault();
    if (dateString is null)
    {
        Console.WriteLine("provide a date");
        continue;
    }
    string? dateFormat = input?.Skip(1).Take(1).FirstOrDefault();
    if (dateFormat is null)
    {
        Console.WriteLine("provide a date format");
        continue;
    }

    var json = $$"""
            {
                "Date": "{{dateString}}",
            } 
            """;
    Console.WriteLine($"json: {json}");
    try
    {
        var stjDateContainer = new SystemTextJsonDateTimeSerializer<DateContainer>().Deserialize(json, dateFormat);
        Console.WriteLine($"System.Text.Json object: {stjDateContainer}");
        var njDateContainer = new NewtonsoftJsonDateTimeSerializer<DateContainer>().Deserialize(json, dateFormat);
        Console.WriteLine($"Newtonsoft.Json object: {njDateContainer}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
