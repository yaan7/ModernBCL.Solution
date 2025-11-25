using System;
using System.Runtime.CompilerServices;

namespace System
{
    // NOTE: This implementation is a lightweight, non-cryptographic polyfill
    // of the modern System.HashCode struct, designed to work optimally 
    // within the constraints of .NET Framework 4.8.

    /// <summary>
    /// A lightweight implementation of System.HashCode used to combine 
    /// the hash codes of multiple values into a single hash code.
    /// </summary>
    public struct HashCode
    {
        // Using common prime seed values to ensure good hash distribution.
        private const int Seed1 = unchecked((int)0x811C9DC5); // FNV-1a prime
        private const int Seed2 = unchecked((int)0x902F1905);

        private int _hash1;
        private int _hash2;
        private int _count;

        public static HashCode Combine()
        {
            HashCode h = new HashCode();
            h._hash1 = Seed1;
            h._hash2 = Seed2;
            h._count = 0;
            return h;
        }

        public void Add<T>(T value)
        {
            int code = (value == null) ? 0 : value.GetHashCode();

            if ((_count % 2) == 0)
            {
                // Use a non-commutative mix for _hash1 (left rotation 5 + XOR)
                _hash1 = (int)(((uint)_hash1 << 5) | ((uint)_hash1 >> 27)) ^ code;
            }
            else
            {
                // Use a different non-commutative mix for _hash2 (left rotation 17 + XOR)
                _hash2 = (int)(((uint)_hash2 << 17) | ((uint)_hash2 >> 15)) ^ code;
            }

            _count++;
        }

        /// <summary>
        /// FINAL MIX FIX: Ensures the output is highly sensitive to which value was mixed into _hash1 versus _hash2.
        /// </summary>
        public int ToHashCode()
        {
            // We use a robust, non-commutative finalizer that is common in hash functions.

            unchecked
            {
                uint h1 = (uint)_hash1;
                uint h2 = (uint)_hash2;

                // 1. Incorporate h2 into h1 using XOR.
                h1 ^= h2;

                // 2. Add the golden ratio constant (an unrelated prime constant) for better mixing.
                h1 += 0x9e3779b9;

                // 3. Rotate 16 bits to thoroughly smear the bits across the 32-bit integer space.
                h1 = (h1 << 16) | (h1 >> 16);

                // 4. Add h2 back to incorporate both accumulators non-commutatively.
                h1 += h2;

                // We return the final mixed result, ignoring the final count for simplicity 
                // as the mix is already strong.
                return (int)h1;
            }
        }

        // Static Helper Methods (up to T8) for clean usage - unchanged.
        public static int Combine<T1>(T1 value1) => (value1 == null) ? 0 : value1.GetHashCode();

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            hash.Add(value4);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            hash.Add(value4);
            hash.Add(value5);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            hash.Add(value4);
            hash.Add(value5);
            hash.Add(value6);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            hash.Add(value4);
            hash.Add(value5);
            hash.Add(value6);
            hash.Add(value7);
            return hash.ToHashCode();
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
        {
            var hash = Combine();
            hash.Add(value1);
            hash.Add(value2);
            hash.Add(value3);
            hash.Add(value4);
            hash.Add(value5);
            hash.Add(value6);
            hash.Add(value7);
            hash.Add(value8);
            return hash.ToHashCode();
        }
    }
}