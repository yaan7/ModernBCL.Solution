using System.Collections.Generic;
using Xunit;
using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class DictionaryKeyComparerTests
    {
        [Fact]
        public void Dictionary_ShouldUsePolyfill()
        {
            var comparer = new DictionaryKeyComparer<Person>(
                p => new object[] { p.Name, p.Age }
            );

            var dict = new Dictionary<Person, string>(comparer);

            var p1 = new Person("X", 10);
            var p2 = new Person("X", 10);

            dict[p1] = "OK";

            Assert.True(dict.ContainsKey(p2));
        }
    }
}
