using System;
using System.Collections.Generic;

namespace ModernBCL.Core.Hashing.Comparers
{
    public sealed class SequenceHashComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        private readonly IEqualityComparer<T> _elementComparer;

        public SequenceHashComparer(IEqualityComparer<T> elementComparer = null)
        {
            _elementComparer = elementComparer ?? EqualityComparer<T>.Default;
        }

        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            using (var xe = x.GetEnumerator())
            using (var ye = y.GetEnumerator())
            {
                while (true)
                {
                    bool xn = xe.MoveNext();
                    bool yn = ye.MoveNext();

                    if (!xn && !yn) return true;
                    if (xn != yn) return false;

                    if (!_elementComparer.Equals(xe.Current, ye.Current))
                        return false;
                }
            }
        }

        public int GetHashCode(IEnumerable<T> seq)
        {
            if (seq == null) return 0;

            var acc = HashAccumulator.Create();
            foreach (var item in seq)
                acc.Add(item);

            return acc.ToHashCode();
        }
    }
}
