using System;
using System.Runtime.CompilerServices;

namespace ModernBCL.Core.Hashing
{
    /// <summary>
    /// A high-performance, deterministic, non-cryptographic 64-bit hash accumulator.
    /// Provides extremely low collision probability.
    /// </summary>
    public struct HashAccumulator64
    {
        // ============================================================
        // Seeds (strong 64-bit)
        // ============================================================

        private const ulong Seed1 = 0x9E3779B97F4A7C15UL;  // Golden ratio 64
        private const ulong Seed2 = 0xC2B2AE3D27D4EB4FUL;  // xxHash prime

        // ============================================================
        // Mixing constants (xxHash / FarmHash style)
        // ============================================================

        private const ulong Prime1 = 0x9E3779B185EBCA87UL;
        private const ulong Prime2 = 0xC2B2AE3D27D4EB4FUL;
        private const ulong Prime3 = 0x165667B19E3779F9UL;

        // ============================================================
        // Internal state
        // ============================================================

        private ulong _h1;
        private ulong _h2;
        private int _count;

        // ============================================================
        // Factory
        // ============================================================

        public static HashAccumulator64 Create()
        {
            return new HashAccumulator64
            {
                _h1 = Seed1,
                _h2 = Seed2,
                _count = 0
            };
        }

        // ============================================================
        // Add
        // ============================================================

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(T value)
        {
            ulong code = (ulong)(value?.GetHashCode() ?? 0);

            if ((_count & 1) == 0)
            {
                // even → lane 1
                ulong h = _h1;
                h = RotateLeft64(h + code * Prime1, 31) * Prime2;
                _h1 = h;
            }
            else
            {
                // odd → lane 2
                ulong h = _h2;
                h = RotateLeft64(h + code * Prime2, 27) * Prime3;
                _h2 = h;
            }

            _count++;
        }

        // ============================================================
        // Final 64-bit mix (xxHash-like avalanche)
        // ============================================================

        public ulong ToHash64()
        {
            ulong h = _h1 ^ (_h2 * Prime3);

            h ^= ((ulong)_count) * Prime1;   // FIXED: explicit cast
            h *= Prime2;

            // strong avalanche
            h ^= h >> 29;
            h *= Prime3;
            h ^= h >> 32;

            return h;
        }

        // ============================================================
        // Helpers
        // ============================================================

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong RotateLeft64(ulong value, int bits)
        {
            return (value << bits) | (value >> (64 - bits));
        }

        // ============================================================
        // Combine(params)
        // ============================================================

        public static ulong Combine(params object[] values)
        {
            var acc = Create();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                    acc.Add(values[i]);
            }

            return acc.ToHash64();
        }

        // ============================================================
        // Generic overloads (fast path)
        // ============================================================

        public static ulong Combine<T1>(T1 v1)
        {
            var acc = Create();
            acc.Add(v1);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2>(T1 v1, T2 v2)
        {
            var acc = Create();
            acc.Add(v1); acc.Add(v2);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3>(T1 a, T2 b, T3 c)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3, T4, T5>(
            T1 a, T2 b, T3 c, T4 d, T5 e)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d); acc.Add(e);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3, T4, T5, T6>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3, T4, T5, T6, T7>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f); acc.Add(g);
            return acc.ToHash64();
        }

        public static ulong Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h8)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f); acc.Add(g); acc.Add(h8);
            return acc.ToHash64();
        }
    }
}
