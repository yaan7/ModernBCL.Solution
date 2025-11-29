using System;
using Xunit;
using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class HashComparer64Tests
    {
        [Fact]
        public void EqualObjects_ShouldBeEqual()
        {
            var comparer = new HashComparer64<Person>(p => new object[] { p.Name, p.Age });

            Assert.True(comparer.Equals(
                new Person("A", 10),
                new Person("A", 10)));
        }

        [Fact]
        public void Hash_ShouldBeDeterministic()
        {
            var comparer = new HashComparer64<Person>(per => new object[] { per.Name, per.Age });
            var p = new Person("Bob", 33);

            Assert.Equal(comparer.GetHashCode(p), comparer.GetHashCode(p));
        }
    }
}
