using BenchmarkDotNet.Attributes;
using list_batch.bl;

namespace list_batch.console
{
    [MemoryDiagnoser]
    public class ListBatchBenchmarks
    {
        private readonly List<int> fullList = Enumerable.Range(0,1_000_000).ToList();
        private const int batchSize = 1_000;

        [Benchmark(Baseline = true)]
        public void BuildChunksWithIterationTest()
        {
            _ = Chunk.BuildChunksWithIteration(fullList, batchSize);
        }
        [Benchmark]
        public void BuildChunksWithRangeTest()
        {
            _ = Chunk.BuildChunksWithRange(fullList, batchSize);
        }
        [Benchmark]
        public void BuildChunksWithLinqTest()
        {
            _ = Chunk.BuildChunksWithLinq(fullList, batchSize);
        }
        [Benchmark]
        public void BuildChunksWithLinqAndYieldTest()
        {
            _ = Chunk.BuildChunksWithLinqAndYield(fullList, batchSize);
        }

    }
}
