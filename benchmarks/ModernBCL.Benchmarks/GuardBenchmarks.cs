using System;
using BenchmarkDotNet.Attributes;
using ModernBCL.Core.Guards;

namespace ModernBCL.Benchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class GuardBenchmarks
    {
        private readonly string good = "Modern";
        private readonly int age = 35;

        [Benchmark(Baseline = true)]
        public void NoGuard()
        {
            var x = good;
            var y = age;
        }

        [Benchmark]
        public void ClassicStringGuard()
        {
            Guard.AgainstNullOrWhiteSpace(good, "good");
        }

        [Benchmark]
        public void ClassicNumericGuard()
        {
            Guard.AgainstOutOfRange(age, 1, 100, "age");
        }

        [Benchmark]
        public void FluentStringGuard()
        {
            Guard.Against(good, "good")
                 .Null()
                 .WhiteSpace();
        }

        [Benchmark]
        public void FluentNumericGuard()
        {
            Guard.Against(age, "age")
                 .LessThan(1)
                 .GreaterThan(100);
        }
    }
}
