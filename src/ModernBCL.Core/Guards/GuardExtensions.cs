using ModernBCL.Core.Internal;

namespace ModernBCL.Core.Guards
{
    public static class GuardExtensions
    {
        /// <summary>
        /// Ensures the reference type is not null.
        /// </summary>
        public static void EnsureNotNull<T>(this T value, string paramName)
            where T : class
        {
            if (value is null)
                ThrowHelper.ThrowArgumentNull(paramName);
        }

        /// <summary>
        /// Ensures the condition is true.
        /// </summary>
        public static void Ensure(this bool condition, string paramName)
        {
            if (condition is false)
                ThrowHelper.ThrowArgumentOutOfRange(paramName);
        }
    }
}
