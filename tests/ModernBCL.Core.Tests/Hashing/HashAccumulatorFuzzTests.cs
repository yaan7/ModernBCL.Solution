using System;
using Xunit;
using ModernBCL.Core.Hashing;

namespace ModernBCL.Core.Tests.Hashing
{
    public class HashAccumulatorFuzzTests
    {
        private readonly Random _rnd = new Random();

        [Fact]
        public void Fuzz_CompareWithPolyfill()
        {
            for (int i = 0; i < 2000; i++)
            {
                int a = _rnd.Next();
                int b = _rnd.Next();
                int c = _rnd.Next();

                int expected = HashCode.Combine(a, b, c);
                int actual = HashAccumulator.Combine(a, b, c);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Fuzz_IncrementalMatchesPolyfill()
        {
            for (int i = 0; i < 2000; i++)
            {
                int x = _rnd.Next();
                int y = _rnd.Next();

                var acc = HashAccumulator.Create();
                acc.Add(x);
                acc.Add(y);

                int expected = HashCode.Combine(x, y);
                int actual = acc.ToHashCode();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Fuzz_NullValuesMatchPolyfill()
        {
            for (int i = 0; i < 500; i++)
            {
                string s1 = (i % 2 == 0) ? null : ("STR" + _rnd.Next());
                string s2 = (i % 3 == 0) ? null : ("X" + _rnd.Next());

                int expected = HashCode.Combine(s1, s2);
                int actual = HashAccumulator.Combine(s1, s2);

                Assert.Equal(expected, actual);
            }
        }
    }
}
