using System;
using System.Collections.Generic;
// Note: HashCode is in the 'System' namespace, so it's available automatically

/// <summary>
/// Demonstrates the use of the HashCode.Combine polyfill in a custom class.
/// This class represents a composite key used for tracking product variants.
/// </summary>
public class ProductKey
{
    public string ProductId { get; }
    public string Color { get; }
    public int Size { get; }

    public ProductKey(string productId, string color, int size)
    {
        ProductId = productId;
        Color = color;
        Size = size;
    }

    /// <summary>
    /// Mandatory override: Checks if all key properties are equal.
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ProductKey)obj;

        // Ensure string comparisons handle case sensitivity as needed
        return ProductId.Equals(other.ProductId, StringComparison.OrdinalIgnoreCase) &&
               Color.Equals(other.Color, StringComparison.OrdinalIgnoreCase) &&
               Size == other.Size;
    }

    /// <summary>
    /// Mandatory override: Generates a fast, well-distributed hash code
    /// using the clean HashCode.Combine polyfill.
    /// </summary>
    public override int GetHashCode()
    {
        return HashCode.Combine(ProductId, Color, Size);
    }
}

public class UsageDemo
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Setting up Product Inventory (Dictionary) ---");
        var inventory = new Dictionary<ProductKey, int>();

        // Add multiple products (the inventory setup)
        var productA = new ProductKey("SHIRT-001", "Blue", 42);
        inventory.Add(productA, 150);
        Console.WriteLine($"Added: A (SHIRT-001, Blue, 42) - Stock: 150. Hash: {productA.GetHashCode()}");

        var productB = new ProductKey("SHIRT-001", "Red", 42);
        inventory.Add(productB, 80);
        Console.WriteLine($"Added: B (SHIRT-001, Red, 42) - Stock: 80. Hash: {productB.GetHashCode()}");

        var productC = new ProductKey("PANTS-005", "Black", 32);
        inventory.Add(productC, 25);
        Console.WriteLine($"Added: C (PANTS-005, Black, 32) - Stock: 25. Hash: {productC.GetHashCode()}");

        Console.WriteLine($"\n--- Searching for Product A Key ---");

        // Create a new key with identical values to search for Product A
        var aSearchKey = new ProductKey("SHIRT-001", "Blue", 42);

        Console.WriteLine($"Search Key Hash: {aSearchKey.GetHashCode()}");
        Console.WriteLine($"(We expect the search hash to match Product A's hash for a fast lookup)");

        // The dictionary search logic
        if (inventory.TryGetValue(aSearchKey, out int stock))
        {
            Console.WriteLine($"\nSUCCESS! Found matching product using HashCode.Combine and Equals.");
            Console.WriteLine($"Details: {aSearchKey.ProductId}, {aSearchKey.Color}, {aSearchKey.Size}");
            Console.WriteLine($"Stock found: {stock}");
        }
        else
        {
            Console.WriteLine("\nFAILURE! Could not find product in inventory.");
        }
    }
}