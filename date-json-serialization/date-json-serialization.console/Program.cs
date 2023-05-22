using Newtonsoft.Json;

while (true)
{
    Console.WriteLine("type date");
    string? dateString = Console.ReadLine();

    var json = $$"""
            {
                "Date": "{{dateString}}",
            } 
            """;
    Console.WriteLine($"json: {json}");
    // 1. System.Text.Json
    var dateContainer = System.Text.Json.JsonSerializer.Deserialize<DateContainer>(json,
        options: new System.Text.Json.JsonSerializerOptions
        {
            AllowTrailingCommas = true,
        });
    Console.WriteLine($"object: {dateContainer}");
    // 2. Newtonsoft.Json
    dateContainer = JsonConvert.DeserializeObject<DateContainer>(json);
    Console.WriteLine($"object: {dateContainer}");
}
