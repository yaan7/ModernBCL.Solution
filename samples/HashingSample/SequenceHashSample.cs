using System;
using ModernBCL.Core.Hashing;
using ModernBCL.Core.Hashing.Comparers;

namespace HashingSample
{
    public static class SequenceHashSample
    {
        public static void Run()
        {
            Console.WriteLine("=== Sequence Hash Sample ===");

            int[] values = { 10, 20, 30 };

            var comparer = new SequenceHashComparer<int>();
            int seqHash = comparer.GetHashCode(values);

            Console.WriteLine("SequenceHashComparer = " + seqHash);

            // Manual incremental equivalent
            var acc = HashAccumulator.Create();

            foreach (var v in values)
                acc.Add(v);

            Console.WriteLine("Manual accumulator = " + acc.ToHashCode());
        }
    }
}
