using System;
using System.ComponentModel;

// --- 1. Caller Argument Expression (Needed for ThrowHelper auto-naming) ---
// This enables the use of [CallerArgumentExpression] for auto-populating parameter names.
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}

// --- 2. Nullable Reference Type (NRT) Attributes (Needed for Code Analysis) ---
// These attributes are needed for the C# compiler to recognize NotNull/MaybeNull.
namespace System.Diagnostics.CodeAnalysis
{
    // Indicates that when a method returns, the parameter will not be null.
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class NotNullAttribute : Attribute { }

    // Indicates that when a method returns, the value might be null.
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class MaybeNullAttribute : Attribute { }
}