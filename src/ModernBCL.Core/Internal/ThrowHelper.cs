using System;
using System.Diagnostics;

namespace ModernBCL.Core.Internal
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal static class ThrowHelper
    {
        internal static void ThrowArgumentNull(string paramName)
            => throw new ArgumentNullException(paramName);

        internal static void ThrowArgumentOutOfRange(string paramName)
            => throw new ArgumentOutOfRangeException(paramName);

        internal static void ThrowInvalidOperation(string message)
            => throw new InvalidOperationException(message);
    }
}