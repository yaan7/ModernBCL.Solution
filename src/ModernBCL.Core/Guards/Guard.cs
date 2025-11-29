using System;

namespace ModernBCL.Core.Guards
{
    /// <summary>
    /// Entry point for argument checking. Provides both classic static guard methods
    /// and a fluent guard API via Guard.Against(value, paramName).
    /// </summary>
    public static class Guard
    {
        // ------------------------------
        // Classic guard methods
        // ------------------------------

        public static void AgainstNull(object value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        public static void AgainstNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(paramName + " cannot be null, empty, or whitespace.", paramName);
        }

        public static void AgainstOutOfRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    string.Format("{0} must be between {1} and {2}.", paramName, min, max));
            }
        }

        public static void AgainstDefault<T>(T value, string paramName)
        {
            if (Equals(value, default(T)))
                throw new ArgumentException(paramName + " cannot be the default value.", paramName);
        }

        public static void AgainstNegative(int value, string paramName)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName, value, paramName + " cannot be negative.");
        }

        public static void AgainstZero(int value, string paramName)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException(paramName, value, paramName + " cannot be zero.");
        }

        public static void AgainstEmptyEnumerable(System.Collections.IEnumerable value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            var enumerator = value.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(paramName + " cannot be an empty collection.", paramName);
        }

        // ------------------------------
        // Fluent Guard DSL (object-based)
        // ------------------------------

        /// <summary>
        /// Fluent guard for object / string / collections.
        /// Example:
        /// Guard.Against(name, "name").Null().WhiteSpace();
        /// </summary>
        public static GuardClause Against(object value, string paramName)
        {
            return new GuardClause(value, paramName);
        }

        /// <summary>
        /// Fluent guard for comparable value types (int, double, DateTime, etc.)
        /// Example:
        /// Guard.Against(amount, "amount").LessThan(0).GreaterThan(100);
        /// </summary>
        public static GuardValue<T> Against<T>(T value, string paramName)
            where T : IComparable<T>
        {
            return new GuardValue<T>(value, paramName);
        }
    }
}
