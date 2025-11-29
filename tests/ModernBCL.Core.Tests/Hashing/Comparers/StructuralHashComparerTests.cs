using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;
using System;
using Xunit;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class StructuralHashComparerTests
    {
        [Fact]
        public void StructuralHash_ShouldUsePolyfillCombine()
        {
            var p = new Person("X", 5);

            var comparer = new StructuralHashComparer<Person>();

            int expected = HashCode.Combine(p.Name, p.Age);
            int actual = comparer.GetHashCode(p);

            Assert.Equal(expected, actual);
        }
    }
}
