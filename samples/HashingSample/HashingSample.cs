using System;
using ModernBCL.Core.Hashing;

namespace HashingSample
{
    public static class HashingSample
    {
        public static void Run()
        {
            Console.WriteLine("=== Hashing Sample ===");

            int id = 123;
            string name = "ModernBCL";
            DateTime dt = DateTime.UtcNow;

            // Standard .NET HashCode API (polyfilled in .NET 4.8)
            int h1 = HashCode.Combine(id, name, dt);
            Console.WriteLine("HashCode.Combine = " + h1);

            // Incremental hashing using HashAccumulator
            var acc = HashAccumulator.Create();
            acc.Add(id);
            acc.Add(name);
            acc.Add(dt);

            int h2 = acc.ToHashCode();
            Console.WriteLine("HashAccumulator = " + h2);

            // 64-bit hashing
            ulong big = HashAccumulator64.Combine(id, name, dt);
            Console.WriteLine("HashAccumulator64 = " + big);

            int h3 = HashCode.Combine(id, name, dt);
            bool equal = HashCode.Equals(h1, h3);
            Console.WriteLine("HashCode H1 Equal to H3 ? " + equal);
        }
    }
}
