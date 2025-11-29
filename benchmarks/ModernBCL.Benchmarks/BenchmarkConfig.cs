using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Columns;

namespace ModernBCL.Benchmarks
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddJob(Job.ShortRun.WithWarmupCount(2).WithIterationCount(8));
            AddDiagnoser(MemoryDiagnoser.Default);
            AddColumn(
                StatisticColumn.Min,
                StatisticColumn.Mean,
                StatisticColumn.Max,
                StatisticColumn.StdDev,
                StatisticColumn.P95);
        }
    }
}
