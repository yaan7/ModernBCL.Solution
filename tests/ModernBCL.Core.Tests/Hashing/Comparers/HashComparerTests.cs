using System;
using Xunit;
using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class HashComparerTests
    {
        [Fact]
        public void EqualObjects_ShouldBeEqual()
        {
            var comparer = new HashComparer<Person>(p => new object[] { p.Name, p.Age });

            Assert.True(comparer.Equals(
                new Person("A", 10),
                new Person("A", 10)));
        }

        [Fact]
        public void Hash_ShouldMatchPolyfill()
        {
            var person = new Person("A", 20);
            var comparer = new HashComparer<Person>(p => new object[] { p.Name, p.Age });

            int expected = HashCode.Combine(person.Name, person.Age);
            int actual = comparer.GetHashCode(person);

            Assert.Equal(expected, actual);
        }
    }
}
