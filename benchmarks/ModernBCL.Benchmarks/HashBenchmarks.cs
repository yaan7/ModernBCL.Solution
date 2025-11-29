using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Hashing;


namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class HashBenchmarks
    {
        private readonly int a = 123;
        private readonly int b = 456;
        private readonly int c = 789;

        [Benchmark(Baseline = true)]
        public int NativeStringHash()
        {
            return "ModernBCL".GetHashCode();
        }

        [Benchmark]
        public int HashCodeCombine()
        {
            return HashCode.Combine(a, b, c);
        }

        [Benchmark]
        public int HashAccumulator_Combine()
        {
            return HashAccumulator.Combine(a, b, c);
        }

        [Benchmark]
        public ulong HashAccumulator64_Combine()
        {
            return HashAccumulator64.Combine(a, b, c);
        }

        [Benchmark]
        public int TupleHash()
        {
            return Tuple.Create(a, b, c).GetHashCode();
        }
    }
}
