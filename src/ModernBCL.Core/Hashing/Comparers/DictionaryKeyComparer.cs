using System;
using System.Collections.Generic;

namespace ModernBCL.Core.Hashing.Comparers
{
    public sealed class DictionaryKeyComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object[]> _keySelector;

        public DictionaryKeyComparer(Func<T, object[]> keySelector)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        }

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            var a = _keySelector(x);
            var b = _keySelector(y);

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
            return HashAccumulator.Combine(_keySelector(obj));
        }
    }
}
