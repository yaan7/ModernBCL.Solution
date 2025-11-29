using Xunit;
using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Hashing;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class SequenceHashComparerTests
    {
        [Fact]
        public void Hash_ShouldMatchPolyfillSequenceCombine()
        {
            var comparer = new SequenceHashComparer<int>();
            int[] input = { 1, 2, 3 };

            // --- EXPECTED VALUE USING ACCUMULATOR ---
            var acc = HashAccumulator.Create();
            acc.Add(1);
            acc.Add(2);
            acc.Add(3);
            int expected = acc.ToHashCode();

            // --- ACTUAL ---
            int actual = comparer.GetHashCode(input);

            Assert.Equal(expected, actual);
        }
    }
}
