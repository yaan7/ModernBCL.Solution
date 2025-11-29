using System;

namespace ModernBCL.Core.Guards
{
    /// <summary>
    /// Fluent guard for object values (string, collections, etc.).
    /// Used by Guard.Against(object, paramName).
    /// </summary>
    public sealed class GuardClause
    {
        private readonly object _value;
        private readonly string _paramName;

        public GuardClause(object value, string paramName)
        {
            _value = value;
            _paramName = paramName;
        }

        /// <summary>
        /// Throws if the value is null.
        /// </summary>
        public GuardClause Null()
        {
            if (_value == null)
                throw new ArgumentNullException(_paramName);

            return this;
        }

        /// <summary>
        /// Throws if the value is a string that is null, empty, or whitespace.
        /// </summary>
        public GuardClause WhiteSpace()
        {
            if (_value == null)
                throw new ArgumentNullException(_paramName);

            var str = _value as string;
            if (str != null && string.IsNullOrWhiteSpace(str))
                throw new ArgumentException(_paramName + " cannot be empty or whitespace.", _paramName);

            return this;
        }

        /// <summary>
        /// Throws if the value is a collection and is empty.
        /// </summary>
        public GuardClause EmptyEnumerable()
        {
            var col = _value as System.Collections.IEnumerable;
            if (col != null)
            {
                var enumerator = col.GetEnumerator();
                if (!enumerator.MoveNext())
                    throw new ArgumentException(_paramName + " cannot be an empty collection.", _paramName);
            }

            return this;
        }

        /// <summary>
        /// Throws if the value equals default(T). Caller must supply T explicitly.
        /// Example: Guard.Against(id, "id").Default&lt;Guid&gt;();
        /// </summary>
        public GuardClause Default<T>()
        {
            if (Equals(_value, default(T)))
                throw new ArgumentException(_paramName + " cannot be the default value.", _paramName);

            return this;
        }
    }
}
