# Changelog

All notable changes to this project will be documented in this file.
The format is based on **Keep a Changelog**, and this project adheres to **Semantic Versioning**.

---

## [1.2.0] – 2025-11-27

### Added
- Multi-targeting support: **net48**, **net481**, and **net8.0**.
- Added **HashAccumulator64** (64-bit hashing) with extremely low collision rates.
- Added full comparer suite:
  - `ObjectHashComparer`
  - `EnumerableHashComparer`
  - `SequenceHashComparer`
- Added complete **Guard API** with fluent syntax:
  - `.Null()`
  - `.NullOrEmpty()`
  - `.NullOrWhiteSpace()`
  - `.OutOfRange()`
  - Numeric validations
- Added fuzz test suites for:
  - HashAccumulator (32-bit)
  - HashAccumulator64
  - Guard validation system
- Added benchmark project targeting **net8.0**, powered by BenchmarkDotNet.

### Changed
- Rebuilt HashCode polyfill using `HashAccumulator` for deterministic, order-sensitive hashing.
- Improved folder organization for clearer separation of polyfills, guards, hashing, comparers, tests, and benchmarks.
- Standardized testing to **xUnit** across the entire project.

### Fixed
- Resolved `System.HashCode` type conflict on .NET 8 by isolating polyfill with conditional compilation.
- Eliminated duplicate compile warnings by removing explicit <Compile Include> rules.
- Ensured deterministic null-handling behavior in hashing APIs.

---

## [1.1.1] – 2025-11-25

### Added
- Support both **net48** and **net481** target frameworks.

---

## [1.1.0] – 2025-11-25

### Added
- Added `Guard.ThrowIfNullOrEmpty` and `Guard.ThrowIfNullOrWhiteSpace`.
- Introduced `ValueTuple` polyfills for .NET Framework.

### Changed
- Updated XML comments and internal documentation.

### Fixed
- Resolved performance issue in `HashCode.Combine` under large input sets.

---

## [1.0.0] – 2025-11-25

### Added
- Initial release of the ModernBCL.Core.HashCode polyfill.
- Added `System.HashCode` struct with dual-accumulator mixing.
- Implemented alternating rotation hashing (5-bit and 17-bit).
- Included non-commutative `ToHashCode()` algorithm.
- Added `HashCode.Combine` overloads for 1–8 parameters.

### Fixed
- Resolved swapped-input collisions in hashing implementation.

