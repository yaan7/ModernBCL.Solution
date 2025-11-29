using System;
using System.Collections.Generic;

namespace ModernBCL.Core.Hashing.Comparers
{
    public sealed class CompositeKeyComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object>[] _selectors;

        public CompositeKeyComparer(params Func<T, object>[] selectors)
        {
            if (selectors == null || selectors.Length == 0)
                throw new ArgumentException("At least one selector is required.", nameof(selectors));

            _selectors = selectors;
        }

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            foreach (var sel in _selectors)
            {
                var xv = sel(x);
                var yv = sel(y);

                if (!Equals(xv, yv))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            if (obj == null) return 0;

            var values = new object[_selectors.Length];
            for (int i = 0; i < _selectors.Length; i++)
                values[i] = _selectors[i](obj);

            return HashAccumulator.Combine(values);
        }
    }
}
