using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LinkMe.Environment;

namespace LinkMe.Framework.Utility
{
    public static class MiscUtils
    {
        public static T Clone<T>(T source)
            where T : class, ICloneable
        {
            return (source == null ? null : (T)source.Clone());
        }

        public static IList<T> CopyCollection<T>(IEnumerable<T> source)
            where T : class
        {
            return (source == null ? null : new List<T>(source));
        }

        public static bool ByteArraysEqual(byte[] data1, byte[] data2)
        {
            if (ReferenceEquals(data1, data2))
                return true;

            // If either but not both are null, they're not equal.
            if (data1 == null || data2 == null)
                return false;
            if (data1.Length != data2.Length)
                return false;

            for (int i = 0; i < data1.Length; i++)
            {
                if (data1[i] != data2[i])
                    return false;
            }

            return true;
        }

        public static T[] ConcatArrays<T>(params T[][] arrays)
        {
            if (arrays == null || arrays.Length == 0)
                return null;

            int length = 0;

            // Work out the total length.
            foreach (T[] array in arrays)
            {
                if (array != null)
                {
                    length += array.Length;
                }
            }

            // Copy into the new array.

            var combined = new T[length];

            int index = 0;
            foreach (T[] array in arrays)
            {
                if (array != null)
                {
                    Array.Copy(array, 0, combined, index, array.Length);
                    index += array.Length;
                }
            }

            return combined;
        }

        public static T[] GetArraySubset<T>(T[] array, int offset, int count)
        {
            if (array == null)
                return null;

            count = Math.Min(count, array.Length - offset);
            if (count <= 0)
                return new T[0];

            var subset = new T[count];
            Array.Copy(array, offset, subset, 0, count);

            return subset;
        }

        public static IList<T> GetListSubset<T>(IList<T> list, int offset, int count)
        {
            if (list == null)
                return null;

            count = Math.Min(count, list.Count - offset);
            if (count <= 0)
                return new T[0];

            var subset = new T[count];
            for (int i = 0; i < count; i++)
            {
                subset[i] = list[i + offset];
            }

            return subset;
        }

        public static T[] TruncateArray<T>(T[] array, int maximumLength)
        {
            if (array == null || array.Length <= maximumLength)
                return array;

            var newArray = new T[maximumLength];
            Array.Copy(array, newArray, maximumLength);

            return newArray;
        }

        public static string GetExceptionMessageTree(Exception exception)
        {
            if (exception == null)
                return null;

            var sb = new StringBuilder(exception.Message);

            for (Exception ex = exception.InnerException; ex != null; ex = ex.InnerException)
            {
                sb.Append(System.Environment.NewLine);
                sb.Append("---> ");
                sb.Append(ex.Message);
            }

            return sb.ToString();
        }

        public static string GetFullExceptionText(Exception exception)
        {
            const string separatorStr = "\r\n\r\n";

            if (exception == null)
                return null;

            var sb = new StringBuilder();

            for (Exception ex = exception; ex != null; ex = ex.InnerException)
            {
                sb.Insert(0, separatorStr);
                sb.Insert(separatorStr.Length, string.Format("[{0}: {1}]\r\n{2}",
                                                             ex.GetType().Name, ex.Message, ex.StackTrace));
            }

            sb.Remove(0, separatorStr.Length);

            return sb.ToString();
        }

        public static void AddToListT<T, TV>(IList<TV> source, IList<T> target) where TV : T
        {
            foreach (TV o in source)
            {
                target.Add(o);
            }
        }

        /// <summary>
        /// Returns and IList containing new elements in current that don't exist in previous.
        /// </summary>
        public static IList NewListItems(IList currentList, IList previousList)
        {
            // Setup previous hash for fast lookup
            var previousHashtable = new Hashtable(previousList.Count);
            foreach (object previousItem in previousList)
            {
                previousHashtable.Add(previousItem, null);
            }

            // Find new items
            IList newItems = new ArrayList(currentList.Count);
            foreach (object currentItem in currentList)
            {
                if (!previousHashtable.Contains(currentItem)) newItems.Add(currentItem);
            }

            return newItems;
        }

        /// <summary>
        /// Performs a shallow copy of an array - just like Array.Clone(), but strongly typed.
        /// </summary>
        public static T[] CloneArray<T>(T[] array)
        {
            if (array == null)
                return null;

            var cloned = new T[array.Length];
            array.CopyTo(cloned, 0);

            return cloned;
        }

        public static T[] CollectionToArray<T>(ICollection<T> collection)
        {
            if (collection == null)
                return null;

            var array = new T[collection.Count];
            collection.CopyTo(array, 0);

            return array;
        }

        /// <summary>
        /// Copies all the objects in a generic list of a derived type to a new list of a base type,
        /// effectively "downcasting" the list. Eg. copies from IList&lt;ApplicationException&gt; into
        /// a new IList&lt;Exception&gt;.
        /// </summary>
        public static IList<TB> DowncastIList<TB, TD>(IList<TD> derivedList)
            where TD : TB
        {
            if (derivedList == null)
                return null;

            var baseList = new List<TB>(derivedList.Count);
            for (int i = 0; i < baseList.Count; i++)
            {
                baseList.Add(derivedList[i]);
            }

            return baseList;
        }

        /// <summary>
        /// As silly as it is, we sometimes have to convert a weakly-typed list to a generic one.
        /// </summary>
        public static IList<T> ListToListT<T>(IList list)
        {
            if (list == null)
                return null;

            var generic = list as IList<T>;
            if (generic != null)
                return generic; // Great, it already supports the generic interface.

            generic = new List<T>(list.Count);

            foreach (T item in list)
            {
                generic.Add(item);
            }

            return generic;
        }

        /// <summary>
        /// ...and, even sillier, we have to go the other way, too.
        /// </summary>
        public static IList ListTToList<T>(IList<T> list)
        {
            return ListTToList(list, false);
        }

        public static IList ListTToList<T>(IList<T> list, bool alwaysCreateNew)
        {
            if (list == null)
                return null;

            if (!alwaysCreateNew)
            {
                var same = list as IList;
                if (same != null)
                    return same; // Great, it already supports the weakly-typed interface.
            }

            IList weak = new ArrayList(list.Count);

            foreach (T item in list)
            {
                weak.Add(item);
            }

            return weak;
        }

        public static IDictionary<T, object> ListToDictionary<T>(ICollection<T> list)
        {
            if (list == null)
                return null;

            var dictionary = new Dictionary<T, object>(list.Count);

            foreach (T item in list)
            {
                dictionary[item] = null; // Don't use Add() to allow for duplicates in the input list.
            }

            return dictionary;
        }

        /// <summary>
        /// Given a set of keys and a dictionary (map) of these keys to values returns a list of
        /// values in the same order as the keys. Missing keys are added to the <paramref name="missingKeys"/>
        /// parameter, if it is not null.
        /// </summary>
        /// <typeparam name="TK">Type of the keys.</typeparam>
        /// <typeparam name="TV">Type of the values.</typeparam>
        /// <param name="valueByKey">The dictionary from which values are read.</param>
        /// <param name="keys">The keys for which to return the values.</param>
        /// <param name="missingKeys">Any keys in <paramref name="keys"/> that were not found in
        /// <paramref name="valueByKey"/> are added to this list, if it is not null.</param>
        /// <returns>A list of values of the specified keys in the same order as the <paramref name="keys"/>
        /// list.</returns>
        public static TV[] OrderResults<TK, TV>(IDictionary<TK, TV> valueByKey, ICollection<TK> keys,
                                             IList<TK> missingKeys)
        {
            var ordered = new TV[keys.Count];

            int i = 0;
            foreach (TK key in keys)
            {
                TV result;
                if (valueByKey.TryGetValue(key, out result))
                {
                    ordered[i] = result;
                }
                else if (missingKeys != null)
                {
                    missingKeys.Add(key);
                }
                ++i;
            }

            return ordered;
        }

        /// <summary>
        /// Removes duplicates from the specified list without making assumptions about item order.
        /// </summary>
        public static void RemoveDuplicates(IList list, IEqualityComparer comparer)
        {
            if (list == null || list.Count <= 1)
                return;

            var dict = new Hashtable(list.Count, comparer);

            int i = 0;
            while (i < list.Count)
            {
                if (dict.ContainsKey(list[i]))
                {
                    list.RemoveAt(i);
                }
                else
                {
                    dict.Add(list[i++], null);
                }
            }
        }

        /// <summary>
        /// Return all the items that appear in both <paramref name="orderedList" /> and <paramref name="otherList" />
        /// in the same order as they appear in <paramref name="orderedList" />. Duplicates are allowed in
        /// both. If there are duplicates in <paramref name="orderedList" /> then the returned list may also
        /// contain duplicates.
        /// </summary>
        public static List<T> GetOrderedListIntersection<T>(IList<T> orderedList, IList<T> otherList)
        {
            if (orderedList == null || otherList == null)
                return null;

            IDictionary<T, object> otherDictionary = ListToDictionary(otherList);

            var subsetOrdered = new List<T>(Math.Min(orderedList.Count, otherList.Count));
            foreach (T item in orderedList)
            {
                if (otherDictionary.ContainsKey(item))
                {
                    subsetOrdered.Add(item);
                }
            }

            return subsetOrdered;
        }

        public static string ObfuscateEmailAddress(string emailAddress)
        {
            if (emailAddress == null)
                return null;

            if (emailAddress.EndsWith("@" + RuntimeEnvironment.TestEmailDomain, StringComparison.CurrentCultureIgnoreCase))
                return emailAddress;

            return emailAddress.Replace("@", "+") + "@" + RuntimeEnvironment.TestEmailDomain;
        }

        public static void SwapItems<T>(IList<T> list, int index1, int index2)
        {
            var item1 = list[index1];
            list[index1] = list[index2];
            list[index2] = item1;
        }
    }
}