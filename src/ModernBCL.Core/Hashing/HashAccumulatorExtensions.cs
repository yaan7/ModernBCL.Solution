using System;

namespace ModernBCL.Core.Hashing
{
    /// <summary>
    /// Fluent extensions for HashAccumulator.
    /// </summary>
    public static class HashAccumulatorExtensions
    {
        public static HashAccumulator AddFluent<T>(this HashAccumulator acc, T value)
        {
            acc.Add(value);
            return acc;
        }

        /// <summary>
        /// Computes a hash for an arbitrary sequence.
        /// </summary>
        public static int CombineSequence<T>(this HashAccumulator acc, params T[] values)
        {
            if (values != null)
            {
                foreach (var v in values)
                    acc.Add(v);
            }
            return acc.ToHashCode();
        }
    }
}
