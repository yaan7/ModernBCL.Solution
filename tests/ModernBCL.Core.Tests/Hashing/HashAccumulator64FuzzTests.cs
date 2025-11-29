using System;
using System.Collections.Generic;
using Xunit;
using ModernBCL.Core.Hashing;

namespace ModernBCL.Core.Tests.Hashing
{
    public class HashAccumulator64FuzzTests
    {
        private readonly Random _rnd = new Random();

        // ============================================================
        // 1. Determinism
        // ============================================================

        [Fact]
        public void Fuzz_Deterministic()
        {
            for (int i = 0; i < 4000; i++)
            {
                int a = _rnd.Next();
                int b = _rnd.Next();

                ulong h1 = HashAccumulator64.Combine(a, b);
                ulong h2 = HashAccumulator64.Combine(a, b);

                Assert.Equal(h1, h2);
            }
        }

        // ============================================================
        // 2. Order sensitivity
        // ============================================================

        [Fact]
        public void Fuzz_OrderMatters()
        {
            for (int i = 0; i < 3000; i++)
            {
                int a = _rnd.Next();
                int b = _rnd.Next();
                int c = _rnd.Next();
                int d = _rnd.Next();

                ulong h1 = HashAccumulator64.Combine(a, b, c, d);
                ulong h2 = HashAccumulator64.Combine(d, c, b, a);

                Assert.NotEqual(h1, h2);
            }
        }

        // ============================================================
        // 3. Sequence fuzz test — extremely low collision rate
        // ============================================================

        [Fact]
        public void Fuzz_Sequence_HashChangesWithEachValue()
        {
            int collisions = 0;
            const int iterations = 2000;

            for (int i = 0; i < iterations; i++)
            {
                int length = _rnd.Next(2, 50);

                var seq1 = new int[length];
                var seq2 = new int[length];

                for (int j = 0; j < length; j++)
                {
                    seq1[j] = _rnd.Next();
                    seq2[j] = seq1[j];
                }

                int index = _rnd.Next(length);
                seq2[index]++;

                var acc1 = HashAccumulator64.Create();
                var acc2 = HashAccumulator64.Create();

                foreach (var v in seq1) acc1.Add(v);
                foreach (var v in seq2) acc2.Add(v);

                if (acc1.ToHash64() == acc2.ToHash64())
                    collisions++;
            }

            // 64-bit → expect near-zero collisions
            Assert.True(collisions < 2, $"Too many collisions: {collisions}");
        }

        // ============================================================
        // 4. Avalanche test
        // ============================================================

        [Fact]
        public void Fuzz_Avalanche()
        {
            int collisions = 0;

            for (int i = 0; i < 6000; i++)
            {
                int x = _rnd.Next();
                int y = x + 1;

                ulong h1 = HashAccumulator64.Combine(x);
                ulong h2 = HashAccumulator64.Combine(y);

                if (h1 == h2)
                    collisions++;
            }

            Assert.True(collisions < 2, $"Avalanche too weak: {collisions}");
        }

        // ============================================================
        // 5. Distribution test
        // ============================================================

        [Fact]
        public void Fuzz_Distribution()
        {
            const int n = 20000;
            var seen = new HashSet<ulong>();
            int collisions = 0;

            for (int i = 0; i < n; i++)
            {
                ulong h = HashAccumulator64.Combine(i, i * 2, i * 3);

                if (!seen.Add(h))
                    collisions++;
            }

            Assert.True(collisions < 3, $"Too many distribution collisions: {collisions}");
        }

        // ============================================================
        // 6. Mixed-type fuzzing
        // ============================================================

        [Fact]
        public void Fuzz_MixedTypes()
        {
            int collisions = 0;

            for (int i = 0; i < 3000; i++)
            {
                object[] values =
                {
                    _rnd.Next(),
                    _rnd.NextDouble(),
                    Guid.NewGuid(),
                    "S" + _rnd.Next(),
                    (byte)_rnd.Next(0, 255),
                    DateTime.Now.AddMilliseconds(_rnd.Next(0, 20000)),
                    _rnd.Next(0, 2) == 1,
                    (char)_rnd.Next(65, 90)
                };

                ulong h1 = HashAccumulator64.Combine(values);

                values[1] = values[1].ToString();

                ulong h2 = HashAccumulator64.Combine(values);

                if (h1 == h2)
                    collisions++;
            }

            Assert.True(collisions < 3, $"Too many mixed-type collisions: {collisions}");
        }

        // ============================================================
        // 7. Null-handling test
        // ============================================================

        [Fact]
        public void Fuzz_NullValues()
        {
            int collisions = 0;

            for (int i = 0; i < 2000; i++)
            {
                object[] values1 =
                {
                    null,
                    123,
                    null,
                    "x",
                    null
                };

                object[] values2 =
                {
                    null,
                    123,
                    null,
                    "x",
                    null
                };

                ulong h1 = HashAccumulator64.Combine(values1);
                ulong h2 = HashAccumulator64.Combine(values2);

                Assert.Equal(h1, h2);

                values2[0] = "changed";

                ulong h3 = HashAccumulator64.Combine(values2);

                if (h1 == h3)
                    collisions++;
            }

            Assert.True(collisions < 2, $"Null-handling collision spike: {collisions}");
        }
    }
}
