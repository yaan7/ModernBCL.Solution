using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModernBCL.Core.Hashing.Comparers
{
    public sealed class StructuralHashComparer<T> : IEqualityComparer<T>
    {
        private static readonly MemberInfo[] _members =
            typeof(T)
                .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property)
                .ToArray();

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            foreach (var m in _members)
            {
                object xv = GetValue(m, x);
                object yv = GetValue(m, y);

                if (!Equals(xv, yv))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            if (obj == null) return 0;

            var values = new object[_members.Length];
            for (int i = 0; i < _members.Length; i++)
                values[i] = GetValue(_members[i], obj);

            return HashAccumulator.Combine(values);
        }

        private static object GetValue(MemberInfo m, object o)
        {
            if (m is PropertyInfo p) return p.GetValue(o, null);
            if (m is FieldInfo f) return f.GetValue(o);
            return null;
        }
    }
}
