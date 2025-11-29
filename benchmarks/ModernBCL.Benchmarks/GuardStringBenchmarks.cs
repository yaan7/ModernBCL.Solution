using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Guards;

namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class GuardStringBenchmarks
    {
        private readonly string valid = "ModernBCL";
        private readonly string invalid = "  ";

        [Benchmark]
        public void Check_Valid()
        {
            Guard.AgainstNullOrWhiteSpace(valid, "valid");
        }

        [Benchmark]
        public void Check_Invalid()
        {
            try { Guard.AgainstNullOrWhiteSpace(invalid, "invalid"); }
            catch { }
        }
    }
}
