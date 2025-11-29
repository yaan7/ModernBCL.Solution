# 📘 ModernBCL.Core.BLC  
**Modern Polyfills for Legacy .NET Framework 4.8 / 4.8.1**

ModernBCL.Core.BLC brings high-performance, zero-allocation replacements for missing modern BCL APIs to legacy .NET Framework apps — enabling performance and clarity normally available only in .NET 7/8.

This library is especially useful for:

- Enterprise systems that must stay on **.NET Framework 4.8 / 4.8.1**
- Developers wanting **modern C# coding patterns** on legacy frameworks
- Teams needing **high-quality hashing** or **safe guard clauses**
- Projects migrating from .NET Framework → .NET Core but needing parity

---

# 🎯 **Project Goals**

### ModernBCL.Core.BLC provides:

### ✔ **High-performance hashing**
- Drop-in **System.HashCode** polyfill (for .NET Framework only)
- Modern, order-sensitive `HashCode.Combine()`
- `HashAccumulator` (32-bit) and `HashAccumulator64` (64-bit)
- Extremely low collision rates
- Zero allocations

### ✔ **Modern guard clauses**
- Clean replacement for repetitive null/empty/whitespace checks
- Fluent `Guard.Against()` API
- Polyfilled `[CallerArgumentExpression]` attribute
- No need to manually pass parameter names

### ✔ **Cleaner code, safer APIs**
- Modern BCL patterns for older frameworks
- Full parity with .NET 6/7/8 guard & hashing behavior (where appropriate)

---

# 🚀 **Installation**

Install via NuGet:

**.NET CLI**

```bash
dotnet add package ModernBCL.Core.BLC --version 1.2.0
```

**Visual Studio Package Manager Console**

```powershell
Install-Package ModernBCL.Core.BLC -Version 1.2.0
```

---

# 📦 **Compatibility**

| Target | Status | Notes |
|--------|--------|--------|
| **.NET Framework 4.8 / 4.8.1** | ✔ Supported | HashCode polyfill is active |
| **.NET 8+ / .NET Core** | ✔ Supported | Uses *native* System.HashCode (polyfill disabled) |
| **NuGet Package** | ✔ Multi-target | `net48; net481; net8.0` |

ModernBCL intelligently switches behavior:

- Under **net48/net481** → uses **ModernBCL HashCode polyfill**
- Under **net8+** → uses **native framework HashCode**

---

# 💻 Usage Examples

---

## 1. ⚡ **High-Performance Hash Codes**

```csharp
using System;

public class ProductKey
{
    public string ProductId { get; }
    public string Color { get; }
    public int Size { get; }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProductId, Color, Size);
    }
}
```

---

## 2. 🔐 **Modern Guard Clauses**

```csharp
using ModernBCL.Core.Guards;

public class DependencyContainer
{
    public DependencyContainer(object service, string clientName)
    {
        _service = Guard.Against(service).Null();
        _clientName = Guard.Against(clientName).NullOrWhiteSpace();
    }
}
```

---

# ⚙️ **How It Works**

### 🧮 HashCode Polyfill
- Only used under .NET Framework
- Fully deterministic
- Zero allocations
- Based on `HashAccumulator`

### 🛡 Guard Polyfills
- Provides `[CallerArgumentExpression]` for .NET Framework
- Modern fluent API on legacy runtimes

---

# 📊 Benchmarks Included

Benchmark results generated to:

```
BenchmarkDotNet.Artifacts/results/
```

Formats:
- `.html`
- `.md`
- `.csv`

---

# 🧾 **Changelog**

## **Version 1.2.0 (Latest)**  
✔ Added multi-targeting: `net48; net481; net8.0`  
✔ Added HashAccumulator64 (64-bit hashing)  
✔ Added comparer suite  
✔ Added full Guard API  
✔ Added fuzz tests  
✔ Added benchmarks  
✔ Improved HashCode polyfill  

## Version 1.1.1
✔ Support for both net48 and net481  

## Version 1.1.0
✔ ThrowHelper + polyfill attributes  

## Version 1.0.0
✔ Initial release with HashCode polyfill  

---

# 📄 License
MIT License.
