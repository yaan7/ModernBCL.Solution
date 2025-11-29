#if NET48 || NET481

using ModernBCL.Core.Hashing;

namespace System
{
    /// <summary>
    /// System.HashCode polyfill for .NET Framework 4.8 / 4.8.1.
    /// Delegates hashing logic to ModernBCL's HashAccumulator.
    /// </summary>
    public struct HashCode
    {
        private HashAccumulator _acc;

        public void Add<T>(T value) => _acc.Add(value);

        public int ToHashCode() => _acc.ToHashCode();

        public static int Combine<T1>(T1 v1)
            => HashAccumulator.Combine(v1);

        public static int Combine<T1, T2>(T1 v1, T2 v2)
            => HashAccumulator.Combine(v1, v2);

        public static int Combine<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
            => HashAccumulator.Combine(v1, v2, v3);

        public static int Combine<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
            => HashAccumulator.Combine(v1, v2, v3, v4);

        public static int Combine<T1, T2, T3, T4, T5>(
            T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
            => HashAccumulator.Combine(v1, v2, v3, v4, v5);

        public static int Combine<T1, T2, T3, T4, T5, T6>(
            T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6)
            => HashAccumulator.Combine(v1, v2, v3, v4, v5, v6);

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(
            T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7)
            => HashAccumulator.Combine(v1, v2, v3, v4, v5, v6, v7);

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8)
            => HashAccumulator.Combine(v1, v2, v3, v4, v5, v6, v7, v8);
    }
}

#endif
