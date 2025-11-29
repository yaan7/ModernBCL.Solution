using System;
using Xunit;
using ModernBCL.Core.Hashing;

namespace ModernBCL.Core.Tests.Hashing
{
    public class HashAccumulator64UnitTests
    {
        [Fact]
        public void Combine_SameInputs_ProducesSameHash()
        {
            ulong h1 = HashAccumulator64.Combine(1, 2, 3);
            ulong h2 = HashAccumulator64.Combine(1, 2, 3);

            Assert.Equal(h1, h2);
        }

        [Fact]
        public void Combine_DifferentInputs_ProducesDifferentHash()
        {
            ulong h1 = HashAccumulator64.Combine(1, 2, 3);
            ulong h2 = HashAccumulator64.Combine(1, 2, 4);

            Assert.NotEqual(h1, h2);
        }

        [Fact]
        public void Combine_OrderMatters()
        {
            ulong h1 = HashAccumulator64.Combine(1, 2, 3, 4);
            ulong h2 = HashAccumulator64.Combine(4, 3, 2, 1);

            Assert.NotEqual(h1, h2);
        }

        [Fact]
        public void Combine_ParamsArray_Works()
        {
            ulong h1 = HashAccumulator64.Combine(new object[] { 1, "x", 3 });
            ulong h2 = HashAccumulator64.Combine(1, "x", 3);

            Assert.Equal(h1, h2);
        }

        [Fact]
        public void Combine_NullValues()
        {
            ulong h1 = HashAccumulator64.Combine(null, 1, null, 2);
            ulong h2 = HashAccumulator64.Combine(null, 1, null, 2);

            Assert.Equal(h1, h2);
        }

        [Fact]
        public void ToHash64_ChangesAfterAdd()
        {
            var acc = HashAccumulator64.Create();
            ulong h1 = acc.ToHash64();

            acc.Add(123);
            ulong h2 = acc.ToHash64();

            Assert.NotEqual(h1, h2);
        }

        [Fact]
        public void Add_MultipleValues_UpdatesState()
        {
            var acc = HashAccumulator64.Create();
            acc.Add(1);
            acc.Add(2);

            ulong h = acc.ToHash64();

            Assert.NotEqual(0UL, h);
        }

        [Fact]
        public void Combine_GenericOverloads_AllWork()
        {
            Assert.NotEqual(0UL, HashAccumulator64.Combine(1));
            Assert.NotEqual(0UL, HashAccumulator64.Combine(1, 2));
            Assert.NotEqual(0UL, HashAccumulator64.Combine(1, 2, 3));
            Assert.NotEqual(0UL, HashAccumulator64.Combine(1, 2, 3, 4));
            Assert.NotEqual(0UL, HashAccumulator64.Combine(1, 2, 3, 4, 5));
        }
    }
}
