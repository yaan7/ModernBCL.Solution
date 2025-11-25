 ModernBCL.Core.BLC

Polyfills for Legacy .NET Framework 4.8

This library provides high-performance, zero-allocation polyfills for modern .NET features, enabling cleaner code and better performance in legacy .NET Framework applications (targeting net481).

🎯 Project Goal

The primary goal is to close critical feature gaps in .NET 4.8. By providing modern BCL (Base Class Library) types, developers maintaining large enterprise applications can:

Improve Performance: Use non-allocating hash generation (System.HashCode).

Modernize Code Style: Use concise guard clauses (ThrowHelper) instead of verbose null checks.

Ensure Correctness: Prevent hash collisions with order-sensitive combining.

🚀 Installation

Install the package via NuGet:

# Using the .NET CLI
dotnet add package ModernBCL.Core.BLC --version 1.1.0

# Using Package Manager Console (in Visual Studio)
Install-Package ModernBCL.Core.BLC -Version 1.1.0


Compatibility: This package targets .NET Framework 4.8.1 (compatible with 4.8+) and is designed to coexist with modern .NET applications.

💻 Usage Examples

1. High-Performance Hash Codes

Replace complex, manual prime-number math with the clean, static HashCode.Combine API.

using System; // HashCode is in the System namespace

public class ProductKey
{
    public string ProductId { get; }
    public string Color { get; }
    public int Size { get; }

    // ... Constructor and Equals implementation ...

    public override int GetHashCode()
    {
        // Combines up to 8 values.
        // This is non-allocating, order-sensitive, and cryptographically robust for dictionaries.
        return HashCode.Combine(ProductId, Color, Size); 
    }
}


2. Modern Argument Validation (Guard Clauses)

Simplify your constructor and method validation logic using ThrowHelper.

using ModernBCL.Core; // Namespace for ThrowHelper

public class DependencyContainer
{
    private readonly object _service;
    private readonly string _clientName;

    public DependencyContainer(object service, string clientName)
    {
        // Old .NET 4.8 way:
        // if (service == null) throw new ArgumentNullException(nameof(service));

        // ModernBCL way (Parameter name is automatically captured):
        _service = ThrowHelper.ThrowIfNull(service);
        
        // Validate strings (null, empty, or whitespace):
        _clientName = ThrowHelper.ThrowIfNullOrWhiteSpace(clientName);
    }
}


⚙️ How It Works

System.HashCode: Implements a standard mixing algorithm using two internal accumulators with distinct bit-rotations. This ensures that Combine(A, B) produces a different hash than Combine(B, A).

ThrowHelper: Uses the [CallerArgumentExpression] polyfill (included in this package) to automatically capture variable names, allowing you to write ThrowIfNull(arg) without manually passing nameof(arg).

📜 Changelog

Version 1.1.0

Added: ThrowHelper class with ThrowIfNull, ThrowIfNullOrEmpty, and ThrowIfNullOrWhiteSpace.

Added: Polyfill attributes for [CallerArgumentExpression] and [NotNull] to support modern syntax.

Fixed: Resolved hash distribution issues in HashCode.Combine.

Version 1.0.0

Added: Initial release with System.HashCode struct.