static async Task<string[]> ReturnCollectionAsync()
{
    List<Task<string>> tasks = new List<Task<string>>();
    for (int i = 0; i < 5; i++)
    {
        var currentTask = GetStringAsync();
        tasks.Add(currentTask);
    }

    return await Task.WhenAll(tasks);
}

static async Task<string> GetStringAsync()
{
    var rnd = new Random();
    return $"Result string {rnd.Next()}";
}

var results = await ReturnCollectionAsync();
foreach (var result in results)
{
    Console.WriteLine(result);
}
Console.ReadLine();
