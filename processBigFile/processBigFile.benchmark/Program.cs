using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using processBigFile.console;

BenchmarkRunner.Run<ReadAllLinesSyncVsAsync>();
public class ReadAllLinesSyncVsAsync
{
    [Benchmark(Baseline = true)]
    public IEnumerable<string> ReadAllLines() => new ReadableFile("./file.txt").ReadAllLines();

    [Benchmark]
    public async Task<IEnumerable<string>> ReadAllLinesAsync()
    {
        var cts = new CancellationTokenSource();
        return await new ReadableFile("./file.txt").ReadAllLinesAsync(cts.Token);
    }
}
