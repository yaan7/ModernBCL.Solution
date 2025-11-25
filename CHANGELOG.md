Changelog

All notable changes to this project will be documented in this file.

The format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

[1.1.0] - 2025-11-25

Added 

Added Guard.ThrowIfNullOrEmpty and Guard.ThrowIfNullOrWhiteSpace for parameter validation, mirroring modern C# patterns.

Introduced ValueTuple polyfills for .NET Framework compatibility.

Changed

Updated internal documentation and XML comments for enhanced Intellisense support.

Fixed

Resolved a minor performance issue in the HashCode.Combine implementation when dealing with large numbers of inputs.

[1.0.0] - 2025-11-25

Added

Initial public release of the ModernBCL.Core.HashCode polyfill for .NET Framework 4.8.

Introduced System.HashCode struct with internal hash accumulation streams (_hash1, _hash2).

Implemented robust Add<T> method with alternating rotation factors (5 and 17 bits) for inputs, ensuring order sensitivity.

Implemented non-commutative ToHashCode() finalizer mix using XOR, prime addition, and rotation (16 bits).

Included static helper methods HashCode.Combine for 1 up to 8 arguments.

Fixed

Resolved hash collision issue where swapping inputs resulted in identical hash codes, ensuring the implementation is highly sensitive to argument order.