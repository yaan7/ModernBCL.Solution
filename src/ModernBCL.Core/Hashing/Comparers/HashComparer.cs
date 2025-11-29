using System;
using System.Collections.Generic;

namespace ModernBCL.Core.Hashing.Comparers
{
    public sealed class HashComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object[]> _selector;

        public HashComparer(Func<T, object[]> selector)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            var a = _selector(x);
            var b = _selector(y);

            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            if (obj == null) return 0;
            return HashAccumulator.Combine(_selector(obj));
        }
    }
}
