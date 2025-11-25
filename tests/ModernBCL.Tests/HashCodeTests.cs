using Microsoft.VisualStudio.TestTools.UnitTesting; // Uses MSTest attributes and Assert class
using System;

namespace ModernBCL.Tests
{
    [TestClass] // Indicates a class that contains test methods
    public class HashCodeTests
    {
        [TestMethod] // Indicates a test method
        public void Combine_TwoDifferentValues_ShouldProduceUniqueHash()
        {
            // Arrange
            string name = "UserA";
            int age = 30;

            // Act
            int hash1 = HashCode.Combine(name, age);
            int hash2 = HashCode.Combine("UserB", 30); // Different name

            // Assert
            Assert.AreNotEqual(hash1, hash2, "Hashes of different values should be different.");
        }

        [TestMethod] // Indicates a test method
        public void Combine_SameValues_ShouldProduceIdenticalHash()       {
            // Arrange
            string name = "WidgetX";
            double price = 99.99;
            bool isActive = true;

            // Act
            // Test 1: Combining the values
            int hashA = HashCode.Combine(name, price, isActive);

            // Test 2: Combining the exact same values (crucial for dictionary lookup)
            int hashB = HashCode.Combine("WidgetX", 99.99, true);

            // Assert
            Assert.AreEqual(hashA, hashB, "Hashes of identical values MUST be the same.");
        }

        [TestMethod] // Indicates a test method
        public void Combine_DifferentOrder_ShouldProduceDifferentHash()
        {
            // The internal mixing logic should depend on the order of arguments.

            // Arrange
            int val1 = 100;
            string val2 = "Test";

            // Act
            int hashOrder1 = HashCode.Combine(val1, val2);
            int hashOrder2 = HashCode.Combine(val2, val1);

            // Assert
            Assert.AreNotEqual(hashOrder1, hashOrder2, "Hash must be sensitive to argument order.");
        }

        [TestMethod] // Indicates a test method
        public void Combine_HandlesNulls_WithoutThrowing()
        {
            // Arrange
            string nullString = null;
            int number = 42;

            // Act & Assert (Should not throw exception)
            int hash = HashCode.Combine(number, nullString);

            // Assert: Hash should be generated and not zero (if non-null values exist)
            Assert.AreNotEqual(0, hash, "Hash code should be non-zero when mixing non-null values.");
        }
    }
}