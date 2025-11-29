using System;
using Xunit;
using ModernBCL.Core.Hashing.Comparers;
using ModernBCL.Core.Tests.Hashing.TestModels;

namespace ModernBCL.Core.Tests.Hashing.Comparers
{
    public class ComparerFuzzTests
    {
        private readonly Random _rnd = new Random();

        // 1. HashComparer64 – random equal object fuzz
        [Fact]
        public void Fuzz_HashComparer64_RandomObjects()
        {
            var comparer = new HashComparer64<Person>(person => new object[] { person.Name, person.Age });

            for (int i = 0; i < 2000; i++)
            {
                var name = "X" + _rnd.Next(1000);
                var age = _rnd.Next(100);

                var person1 = new Person(name, age);
                var person2 = new Person(name, age);

                Assert.True(comparer.Equals(person1, person2));
                Assert.Equal(comparer.GetHashCode(person1), comparer.GetHashCode(person2));
            }
        }

        // 2. Mutate field fuzz
        [Fact]
        public void Fuzz_MutateField_ShouldChangeEquality()
        {
            var comparer = new HashComparer64<Person>(person => new object[] { person.Name, person.Age });

            for (int i = 0; i < 2000; i++)
            {
                var age = _rnd.Next(100);
                var person1 = new Person("A", age);
                var person2 = new Person("A", age + 1);

                Assert.False(comparer.Equals(person1, person2));
            }
        }

        // 3. Mixed types fuzz
        [Fact]
        public void Fuzz_MixedTypes()
        {
            var comparer = new DictionaryKeyComparer<object>(value => new object[] { value });

            for (int i = 0; i < 2000; i++)
            {
                object value;
                int r = _rnd.Next(3);

                switch (r)
                {
                    case 0:
                        value = _rnd.Next();
                        break;
                    case 1:
                        value = "T" + _rnd.Next(1000);
                        break;
                    default:
                        value = Guid.NewGuid();
                        break;
                }

                int h1 = comparer.GetHashCode(value);
                int h2 = comparer.GetHashCode(value);

                Assert.Equal(h1, h2);
            }
        }

        // 4. Sequence comparer fuzz
        [Fact]
        public void Fuzz_SequenceComparer()
        {
            var comparer = new SequenceHashComparer<int>();

            for (int i = 0; i < 2000; i++)
            {
                var arr1 = new[] { _rnd.Next(), _rnd.Next(), _rnd.Next() };
                var arr2 = new[] { arr1[0], arr1[1], arr1[2] };

                Assert.True(comparer.Equals(arr1, arr2));
            }
        }
    }
}
