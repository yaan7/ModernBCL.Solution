using System;
using ModernBCL.Core.Guards;

namespace GuardSample
{
    /// <summary>
    /// Example domain model demonstrating how Guard protects constructors
    /// and ensures invalid objects are never created.
    /// </summary>
    public class Customer
    {
        public string Name { get; private set; }
        public bool IsVip { get; private set; }

        public Customer(string name, bool isVip)
        {
            // Classic static guard
            Guard.AgainstNullOrWhiteSpace(name, nameof(name));

            Name = name;
            IsVip = isVip;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, IsVip ? "VIP" : "Normal");
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== ModernBCL Guard API Demonstration ===\n");

            DemoClassicGuards();
            DemoFluentObjectGuards();
            DemoFluentTypedGuards();
            DemoInvalidCustomer();
            DemoInvalidAmount();

            Console.WriteLine("\nDone. Press any key to exit...");
            Console.ReadKey();
        }

        // ---------------------------------------------------------------------
        // 1. Classic Guard usage — simple, static, fast, and clean.
        // ---------------------------------------------------------------------
        private static void DemoClassicGuards()
        {
            Console.WriteLine("-> Classic Guards Demo");

            string productName = "Laptop";
            int price = 1200;

            Guard.AgainstNull(productName, nameof(productName));
            Guard.AgainstOutOfRange(price, 1, 10000, nameof(price));

            Console.WriteLine("Classic guards OK.\n");
        }

        // ---------------------------------------------------------------------
        // 2. Fluent Guard usage for objects + strings.
        // ---------------------------------------------------------------------
        private static void DemoFluentObjectGuards()
        {
            Console.WriteLine("-> Fluent Guard (Object / String) Demo");

            string username = "alice123";

            // Fluent object validation
            Guard.Against(username, nameof(username))
                 .Null()
                 .WhiteSpace(); // safe: username is NOT whitespace

            Console.WriteLine("Fluent object guards OK.\n");
        }

        // ---------------------------------------------------------------------
        // 3. Fluent Guard usage for typed values (numbers, dates, etc.)
        // ---------------------------------------------------------------------
        private static void DemoFluentTypedGuards()
        {
            Console.WriteLine("-> Fluent Guard (Typed Comparable Values) Demo");

            int amount = 500;

            // Validates: amount >= 0 and amount <= 100000
            Guard.Against(amount, nameof(amount))
                 .LessThan(0)       // ensures >= 0
                 .GreaterThan(100000); // ensures <= 100000

            // Guid example with Default rule:
            Guid id = Guid.NewGuid();

            Guard.Against(id, nameof(id))
                 .Default(); // ensures not Guid.Empty

            Console.WriteLine("Fluent typed guards OK.\n");
        }

        // ---------------------------------------------------------------------
        // 4. Customer constructor guard demo (invalid name triggers guard).
        // ---------------------------------------------------------------------
        private static void DemoInvalidCustomer()
        {
            Console.WriteLine("-> Invalid Customer Name Demo");

            try
            {
                // Empty name triggers Guard.AgainstNullOrWhiteSpace(...)
                var badCustomer = new Customer("", false);

                Console.WriteLine("Unexpected success: " + badCustomer);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Guard triggered (expected): " + ex.Message);
            }

            Console.WriteLine();
        }

        // ---------------------------------------------------------------------
        // 5. Demonstrates Guard protection inside business logic method.
        // ---------------------------------------------------------------------
        private static void DemoInvalidAmount()
        {
            Console.WriteLine("-> Invalid Amount Demo");

            var customer = new Customer("Bob", false);

            try
            {
                int result = CalculateDiscount(customer, -10); // invalid
                Console.WriteLine("Unexpected discount: " + result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Guard triggered (expected): " + ex.Message);
            }

            Console.WriteLine();
        }

        // ---------------------------------------------------------------------
        // Business logic method using Guard
        // ---------------------------------------------------------------------
        private static int CalculateDiscount(Customer customer, int amount)
        {
            // Classic Guard + Fluent Guard mixture
            Guard.AgainstNull(customer, nameof(customer));

            Guard.Against(amount, nameof(amount))
                 .LessThan(0)          // ensures amount >= 0
                 .GreaterThan(100000); // ensures amount <= 100000

            // Simple business logic
            if (customer.IsVip)
                return (int)(amount * 0.20); // VIP = 20% off

            return (int)(amount * 0.05); // normal = 5% off
        }
    }
}
