using System;
using Xunit;
using ModernBCL.Core.Hashing;

namespace ModernBCL.Core.Tests.Hashing
{
    public class HashAccumulatorTests
    {
        [Fact]
        public void Combine_ShouldMatchPolyfill()
        {
            int a = 123, b = 456, c = 789;

            int expected = HashCode.Combine(a, b, c);
            int actual = HashAccumulator.Combine(a, b, c);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IncrementalAdd_ShouldMatchPolyfill()
        {
            var acc = HashAccumulator.Create();
            acc.Add(123);
            acc.Add(456);

            int expected = HashCode.Combine(123, 456);
            int actual = acc.ToHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombineNullValues_ShouldMatchPolyfill()
        {
            string s1 = null;
            string s2 = "test";

            int expected = HashCode.Combine(s1, s2);
            int actual = HashAccumulator.Combine(s1, s2);

            Assert.Equal(expected, actual);
        }
    }
}
