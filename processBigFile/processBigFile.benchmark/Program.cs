using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using processBigFile.console;

BenchmarkRunner.Run<ReadAllLinesSyncVsAsync>();
public class ReadAllLinesSyncVsAsync
{
    [Benchmark(Baseline = true)]
    public string[] ReadAllLines() => new ReadableFile("./file.txt").ReadAllLines();

    [Benchmark]
    public async Task<string[]> ReadAllLinesAsync()
    {
        var cts = new CancellationTokenSource();
        return await new ReadableFile("./file.txt").ReadAllLinesAsync(cts.Token);
    }
}
