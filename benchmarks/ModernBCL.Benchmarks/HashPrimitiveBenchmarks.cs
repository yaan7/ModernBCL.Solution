using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Hashing;


namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HashPrimitiveBenchmarks
    {
        private int x = 5000;
        private int y = 9999;

        [Benchmark(Baseline = true)]
        public int IntCombine()
        {
            return HashCode.Combine(x);
        }

        [Benchmark]
        public int TwoInts()
        {
            return HashCode.Combine(x, y);
        }

        [Benchmark]
        public int AccumulatorTwoInts()
        {
            var acc = HashAccumulator.Create();
            acc.Add(x);
            acc.Add(y);
            return acc.ToHashCode();
        }
    }
}
