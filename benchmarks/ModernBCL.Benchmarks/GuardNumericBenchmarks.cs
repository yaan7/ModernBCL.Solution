using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Guards;

namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class GuardNumericBenchmarks
    {
        private readonly int value = 50;

        [Benchmark]
        public void RangeCheck_Valid()
        {
            Guard.AgainstOutOfRange(value, 0, 100, "value");
        }

        [Benchmark]
        public void RangeCheck_Invalid()
        {
            try { Guard.AgainstOutOfRange(value, 1000, 2000, "value"); }
            catch { }
        }
    }
}
