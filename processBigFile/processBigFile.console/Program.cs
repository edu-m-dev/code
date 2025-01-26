using processBigFile.console;

var cts = new CancellationTokenSource();

var lines = await new ReadableFile("./file.txt").ReadAllLinesAsync(cts.Token);
foreach (var line in lines)
{
    Console.WriteLine(line);
}
Console.ReadLine();
