using BenchmarkDotNet.Running;
using list_batch.console;

var summary = BenchmarkRunner.Run<ListBatchBenchmarks>();