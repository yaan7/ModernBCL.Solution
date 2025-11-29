using System;

namespace ModernBCL.Core.Guards
{
    /// <summary>
    /// Fluent guard for strongly-typed comparable values (int, double, DateTime, etc.).
    /// Used by Guard.Against&lt;T&gt;(T value, string paramName) where T : IComparable&lt;T&gt;.
    /// </summary>
    public sealed class GuardValue<T> where T : IComparable<T>
    {
        private readonly T _value;
        private readonly string _paramName;

        public GuardValue(T value, string paramName)
        {
            _value = value;
            _paramName = paramName;
        }

        /// <summary>
        /// Throws if value is null (for reference types).
        /// </summary>
        public GuardValue<T> Null()
        {
            if (Equals(_value, null))
                throw new ArgumentNullException(_paramName);

            return this;
        }

        /// <summary>
        /// Throws if the value is the default(T) value.
        /// </summary>
        public GuardValue<T> Default()
        {
            if (Equals(_value, default(T)))
                throw new ArgumentException(_paramName + " cannot be the default value.", _paramName);

            return this;
        }

        /// <summary>
        /// Throws if the value is less than the given minimum (value &lt; min).
        /// </summary>
        public GuardValue<T> LessThan(T min)
        {
            if (_value.CompareTo(min) < 0)
                throw new ArgumentOutOfRangeException(
                    _paramName,
                    _value,
                    string.Format("{0} must be >= {1}.", _paramName, min));

            return this;
        }

        /// <summary>
        /// Throws if the value is greater than the given maximum (value &gt; max).
        /// </summary>
        public GuardValue<T> GreaterThan(T max)
        {
            if (_value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(
                    _paramName,
                    _value,
                    string.Format("{0} must be <= {1}.", _paramName, max));

            return this;
        }

        /// <summary>
        /// Throws if the value is outside [min, max].
        /// </summary>
        public GuardValue<T> Between(T min, T max)
        {
            if (_value.CompareTo(min) < 0 || _value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(
                    _paramName,
                    _value,
                    string.Format("{0} must be between {1} and {2}.", _paramName, min, max));

            return this;
        }

        /// <summary>
        /// Special case: if T is string, throws when null/empty/whitespace.
        /// This is safe to call for non-string types; it just does nothing.
        /// </summary>
        public GuardValue<T> WhiteSpace()
        {
            if (_value == null)
                throw new ArgumentNullException(_paramName);

            var str = _value as object as string;
            if (str != null && string.IsNullOrWhiteSpace(str))
                throw new ArgumentException(_paramName + " cannot be empty or whitespace.", _paramName);

            return this;
        }
    }
}
