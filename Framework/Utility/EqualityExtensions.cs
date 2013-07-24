using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Utility
{
    /// <summary>
    /// Nullable implies that a null collection is equal to an empy collection.
    /// Collection implies order should be ignored.
    /// </summary>
    public static class EqualityExtensions
    {
        private class FuncEqualityComparer<T>
            : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> _comparer;

            public FuncEqualityComparer(Func<T, T, bool> comparer)
            {
                _comparer = comparer;
            }

            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                return _comparer(x, y);
            }

            int IEqualityComparer<T>.GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> e)
        {
            return !e.GetEnumerator().MoveNext();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> e)
        {
            return e == null || e.IsEmpty();
        }

        public static bool NullableSequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            if (first == null)
                return second.IsNullOrEmpty();
            if (second == null)
                return first.IsEmpty();
            return first.SequenceEqual(second, comparer);
        }

        public static bool NullableSequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return first.NullableSequenceEqual(second, new FuncEqualityComparer<T>(comparer));
        }

        public static bool NullableSequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
                return second.IsNullOrEmpty();
            if (second == null)
                return first.IsEmpty();
            return first.SequenceEqual(second);
        }

        public static bool CollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            var list = new List<T>(second);
            foreach (var value in first)
            {
                var found = false;
                for (var index = 0; index < list.Count; index++)
                {
                    if (comparer == null ? Equals(value, list[index]) : comparer.Equals(value, list[index]))
                    {
                        list.RemoveAt(index);
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return list.Count == 0;
        }

        public static bool CollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return CollectionEqual(first, second, new FuncEqualityComparer<T>(comparer));
        }

        public static bool CollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return CollectionEqual(first, second, (IEqualityComparer<T>) null);
        }

        public static bool NullableCollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            if (first == null)
                return second.IsNullOrEmpty();
            if (second == null)
                return first.IsEmpty();
            return first.CollectionEqual(second, comparer);
        }

        public static bool NullableCollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return first.NullableCollectionEqual(second, new FuncEqualityComparer<T>(comparer));
        }

        public static bool NullableCollectionEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
                return second.IsNullOrEmpty();
            if (second == null)
                return first.IsEmpty();
            return first.CollectionEqual(second);
        }

        public static int GetCollectionHashCode<T>(this IEnumerable<T> collection)
        {
            var hashCode = 0;
            if (collection == null)
                return hashCode;

            foreach (var value in collection)
            {
                var obj = value as object;
                if (obj != null)
                    hashCode ^= obj.GetHashCode();
            }

            return hashCode;
        }

        public static bool NullableCollectionContains<T>(this IEnumerable<T> collection, T value, IEqualityComparer<T> comparer)
        {
            if (collection == null)
                return false;

            foreach (var item in collection)
            {
                if (comparer == null ? Equals(value, item) : comparer.Equals(value, item))
                    return true;
            }

            return false;
        }

        public static bool NullableCollectionContains<T>(this IEnumerable<T> collection, T value, Func<T, T, bool> comparer)
        {
            return collection.NullableCollectionContains(value, new FuncEqualityComparer<T>(comparer));
        }

        public static bool NullableCollectionContains<T>(this IEnumerable<T> collection, T value)
        {
            return collection.NullableCollectionContains(value, (IEqualityComparer<T>)null);
        }


        public static bool CollectionContains<T>(this IEnumerable<T> collection, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            foreach (var value in values)
            {
                var found = false;
                foreach (var item in collection)
                {
                    if (comparer == null ? Equals(value, item) : comparer.Equals(value, item))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public static bool CollectionContains<T>(this IEnumerable<T> collection, IEnumerable<T> values)
        {
            return collection.CollectionContains(values, null);
        }
    }
}
