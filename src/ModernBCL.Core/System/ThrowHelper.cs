using System;
using System.Runtime.CompilerServices;

namespace System
{
    // NOTE: This class provides a polyfill for the modern BCL Argument validation 
    // methods (e.g., ArgumentNullException.ThrowIfNull), which significantly reduces 
    // boilerplate code for checking parameters in .NET Framework 4.8.

    /// <summary>
    /// Provides concise, modern methods for validating arguments, eliminating the 
    /// need for manual 'if (argument == null) throw new...' checks.
    /// </summary>
    public static class ThrowHelper
    {
        // --- 1. ThrowIfNull<T>(T argument) ---

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="paramName">The name of the parameter. This is automatically 
        /// populated by the compiler in modern C#, but is explicitly needed for .NET 4.8.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull<T>(
            [System.Diagnostics.CodeAnalysis.NotNull] T argument,
            [CallerArgumentExpression("argument")] string paramName = null)
            where T : class // Only works on reference types
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        // --- 2. ThrowIfNullOrEmpty(string argument) ---

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the string is null, 
        /// or an <see cref="ArgumentException"/> if the string is empty.
        /// </summary>
        /// <param name="argument">The string argument to validate.</param>
        /// <param name="paramName">The name of the parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNullOrEmpty(
            [System.Diagnostics.CodeAnalysis.NotNull] string argument,
            [CallerArgumentExpression("argument")] string paramName = null)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (argument.Length == 0)
            {
                throw new ArgumentException("The string cannot be empty.", paramName);
            }
        }

        // --- 3. ThrowIfNullOrWhiteSpace(string argument) ---

        /// <summary>
        /// Throws an exception if the string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="argument">The string argument to validate.</param>
        /// <param name="paramName">The name of the parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNullOrWhiteSpace(
            [System.Diagnostics.CodeAnalysis.NotNull] string argument,
            [CallerArgumentExpression("argument")] string paramName = null)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("The string cannot be null, empty, or consist only of white-space characters.", paramName);
            }
        }
    }
}