using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Hashing;


namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HashAccumulatorVsPolyfillBenchmarks
    {
        private readonly string s1 = "Modern";
        private readonly string s2 = "BCL";

        [Benchmark(Baseline = true)]
        public int HashCodePolyfill()
        {
            return HashCode.Combine(s1, s2);
        }

        [Benchmark]
        public int Accumulator()
        {
            var acc = HashAccumulator.Create();
            acc.Add(s1);
            acc.Add(s2);
            return acc.ToHashCode();
        }
    }
}
