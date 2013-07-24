using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Framework.Utility.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Randomise<T>(this IEnumerable<T> enumerable)
        {
            // Fisher–Yates / Knuth shuffle.

            var array = enumerable.ToArray();
            var random = new Random();
            var n = array.Length;
            
            while (n > 1)
            {
                var k = random.Next(n);
                --n;

                // Swap array[n] with array[k].

                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
        }

        public static IQueryable<T> SelectRange<T>(this IQueryable<T> queryable, Range range)
        {
            if (range.Skip != 0)
                queryable = queryable.Skip(range.Skip);
            if (range.Take != null)
                queryable = queryable.Take(range.Take.Value);
            return queryable;
        }

        public static IEnumerable<T> SelectRange<T>(this IEnumerable<T> enumerable, Range range)
        {
            if (range.Skip != 0)
                enumerable = enumerable.Skip(range.Skip);
            if (range.Take != null)
                enumerable = enumerable.Take(range.Take.Value);
            return enumerable;
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list.ToList());
        }

        public static ReadOnlyDictionary<K, E> ToReadOnlyDictionary<K, E>(this IDictionary<K, E> dictionary)
        {
            return new ReadOnlyDictionary<K, E>(dictionary);
        }

        public static ReadOnlyDictionary<K, E> ToReadOnlyDictionary<T, K, E>(this IEnumerable<T> list, Func<T, K> keySelector, Func<T, E> elementSelector)
        {
            return new ReadOnlyDictionary<K, E>(list.ToDictionary(keySelector, elementSelector));
        }

        public static ReadOnlyDictionary<K, E> ToReadOnlyDictionary<K, E>(this IEnumerable<Tuple<K, E>> list)
        {
            return new ReadOnlyDictionary<K, E>(list.ToDictionary(x => x.Item1, x => x.Item2));
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T t)
        {
            var index = 0;
            foreach (var currentT in enumerable)
            {
                if (Equals(currentT, t))
                    return index;
                ++index;
            }

            return -1;
        }

        public static int IndexOf<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> selector, T2 t)
        {
            var index = 0;
            foreach (var currentT in enumerable)
            {
                if (Equals(selector(currentT), t))
                    return index;
                ++index;
            }

            return -1;
        }
    }
}
