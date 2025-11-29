using System;
using ModernBCL.Core.Hashing;

namespace HashingSample
{
    public sealed class Person
    {
        public string Name { get; }
        public int Age { get; }

        public Person(string n, int a)
        {
            Name = n;
            Age = a;
        }

        public override int GetHashCode()
        {
            // Modern, .NET-style GetHashCode (polyfilled)
            return HashCode.Combine(Name, Age);
        }
    }

    public static class HashingModelSample
    {
        public static void Run()
        {
            Console.WriteLine("=== Hashing Model Sample ===");

            var p1 = new Person("Alice", 30);
            var p2 = new Person("Alice", 30);

            Console.WriteLine("Hashes equal? " + (p1.GetHashCode() == p2.GetHashCode()));
        }
    }
}
