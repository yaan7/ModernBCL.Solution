using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;
using System;
using Xunit;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class CompositeKeyComparerTests
    {
        [Fact]
        public void CompositeHash_ShouldUsePolyfill()
        {
            var p = new Person("AAA", 30);

            var c = new CompositeKeyComparer<Person>(
                x => x.Name,
                x => x.Age
            );

            int expected = HashCode.Combine(p.Name, p.Age);
            int actual = c.GetHashCode(p);

            Assert.Equal(expected, actual);
        }
    }
}
