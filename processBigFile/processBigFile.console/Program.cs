using processBigFile.console;

var cts = new CancellationTokenSource();

var lines = (await new ReadableFile("./file.txt")
    .ReadAllLinesAsync(cts.Token))
    .RemoveBlankLines(cts.Token);
await foreach (var line in lines)
{
    Console.WriteLine(line);
}
Console.ReadLine();
