using System.Runtime.CompilerServices;

namespace ModernBCL.Core.Hashing
{
    /// <summary>
    /// A robust, deterministic, order-sensitive non-cryptographic hash accumulator
    /// designed as a safe replacement for System.HashCode for .NET Framework 4.8/4.8.1.
    /// </summary>
    public struct HashAccumulator
    {
        // ============================================================
        // 1. SEEDS (strong starting state)
        // ============================================================

        private const int Seed1 = unchecked((int)0x811C9DC5);  // FNV offset basis
        private const int Seed2 = unchecked((int)0x9E3779B9);  // Golden ratio 2^32

        // ============================================================
        // 2. NAMED CONSTANTS FOR FINAL MIXING (non-commutative + avalanche)
        // ============================================================

        // Non-commutative merge constants
        private const uint MergePrime1 = 0x85EBCA6B; // Murmur3 C1
        private const uint MergePrime2 = 0xC2B2AE35; // Murmur3 C2

        // Final avalanche constants
        private const uint AvalanchePrime1 = 0x85EBCA6B;
        private const uint AvalanchePrime2 = 0xC2B2AE35;

        // ============================================================
        // 3. INTERNAL STATE
        // ============================================================

        private int _h1;
        private int _h2;
        private int _count;

        // ============================================================
        // 4. FACTORY
        // ============================================================

        public static HashAccumulator Create()
        {
            return new HashAccumulator
            {
                _h1 = Seed1,
                _h2 = Seed2,
                _count = 0
            };
        }

        // ============================================================
        // 5. ADDING VALUES
        // ============================================================

        /// <summary>
        /// Adds a value to the hash accumulator using alternating lanes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(T value)
        {
            int code = value?.GetHashCode() ?? 0;

            if ((_count & 1) == 0)
            {
                uint h = (uint)_h1;
                h = (h << 5) | (h >> 27); // rotate-left 5
                _h1 = unchecked((int)(h ^ (uint)code));
            }
            else
            {
                uint h = (uint)_h2;
                h = (h << 17) | (h >> 15); // rotate-left 17
                _h2 = unchecked((int)(h ^ (uint)code));
            }

            _count++;
        }

        // ============================================================
        // 6. FINAL MIX
        // ============================================================

        /// <summary>
        /// Finalizes the hash using non-commutative merge and a Murmur3-style avalanche.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToHashCode()
        {
            unchecked
            {
                uint h1 = (uint)_h1;
                uint h2 = (uint)_h2;

                // --- NON-COMMUTATIVE MERGE (order-sensitive) ---
                h1 = h1 * MergePrime1 + h2;
                h1 ^= (h2 * MergePrime2);

                // --- FINAL AVALANCHE ---
                h1 ^= h1 >> 13;
                h1 *= AvalanchePrime2;
                h1 ^= h1 >> 16;

                return (int)h1;
            }
        }

        // ============================================================
        // 7. UNIVERSAL SAFE COMBINE (simple version)
        // ============================================================

        /// <summary>
        /// Combines an arbitrary set of values (supports null, mixed types).
        /// </summary>
        public static int Combine(params object[] values)
        {
            var acc = Create();

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                    acc.Add(values[i]);
            }

            return acc.ToHashCode();
        }

        // ============================================================
        // 8. GENERIC COMBINE OVERLOADS (up to 8, matching Microsoft API)
        // ============================================================

        public static int Combine<T1>(T1 v1)
        {
            var acc = Create();
            acc.Add(v1);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2>(T1 v1, T2 v2)
        {
            var acc = Create();
            acc.Add(v1); acc.Add(v2);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3>(T1 a, T2 b, T3 c)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5>(
            T1 a, T2 b, T3 c, T4 d, T5 e)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d); acc.Add(e);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f); acc.Add(g);
            return acc.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h8)
        {
            var acc = Create();
            acc.Add(a); acc.Add(b); acc.Add(c); acc.Add(d);
            acc.Add(e); acc.Add(f); acc.Add(g); acc.Add(h8);
            return acc.ToHashCode();
        }
    }
}
