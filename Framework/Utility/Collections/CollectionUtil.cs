using System.Collections;
using System.Collections.Specialized;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Collections
{
	public sealed class CollectionUtil
	{
		private const int m_maxSizeForList = 5;

		private CollectionUtil()
		{
		}

		/// <summary>
		/// Creates an IDictionary object using the optimal implementation for the given maximum size.
		/// </summary>
		/// <param name="maxSize">The maximum size of the dictionary.</param>
		/// <returns>The created IDictionary object.</returns>
		/// <remarks>As described in the documentation for the <see cref="HybridDictionary" /> class, a
		/// ListDictionary is faster for a small number of elements than a Hashtable. Use this method when
		/// the maximum size is known in advance to create either a ListDictionary or a Hashtable. If
		/// the specified maximum size is exceeded the dictionary will still work, but performance may
		/// suffer.
		/// </remarks>
		public static IDictionary CreateDictionary(int maxSize)
		{
			if (maxSize > m_maxSizeForList)
				return new Hashtable(maxSize);
			else
				return new ListDictionary();
		}

		public static IDictionary GetOptimalDictionary(IDictionary input)
		{
			if (input == null)
				return null;

			Hashtable hashtable;
			ListDictionary list;

			if ((hashtable = input as Hashtable) != null)
			{
				if (input.Count > m_maxSizeForList)
					return input;

				// The dictionary is too small for a Hashtable - replace it with a ListDictionary.

				list = new ListDictionary();
				CopyDictionary(hashtable, list);
				return list;
			}
			else if ((list = input as ListDictionary) != null)
			{
				if (input.Count <= m_maxSizeForList)
					return input;

				// The dictionary is too large for a ListDictionary - replace it with a Hashtable.

				return new Hashtable(list);
			}
			else
				return input; // Unknown type of dictionary - can't do anything about it.
		}

		/// <summary>
		/// Copies the contents of one dictionary to another. The destination dictionary is not cleared
		/// before copying.
		/// </summary>
		/// <param name="source">The dictionary from which to copy.</param>
		/// <param name="destination">The dictionary into which to copy.</param>
		public static void CopyDictionary(IDictionary source, IDictionary destination)
		{
			const string method = "CopyDictionary";

			if (source == null)
				throw new NullParameterException(typeof(CollectionsUtil), method, "source");
			if (destination == null)
				throw new NullParameterException(typeof(CollectionsUtil), method, "destination");

			IDictionaryEnumerator enumerator = source.GetEnumerator();
			while (enumerator.MoveNext())
			{
				destination.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>
		/// Creates a new IDictionary object with the same contents as the supplied IDictionary object.
		/// This method is intended to be used only when no more entries will be added to the cloned object.
		/// </summary>
		/// <param name="source">The IDictionary object from which values are to be copied.</param>
		/// <returns>A new IDictionary object with the same contents as the source object, but not necessarily
		/// the same concrete type as the source object.</returns>
		public static IDictionary CloneFixedSizeDictionary(IDictionary source)
		{
			if (source == null)
				return null;

			if (source.Count > m_maxSizeForList)
			{
				Hashtable sourceHashtable = source as Hashtable;
				return (sourceHashtable != null ? (Hashtable)sourceHashtable.Clone() : new Hashtable(source));
			}
			else
			{
				ListDictionary list = new ListDictionary();

				IDictionaryEnumerator enumerator = source.GetEnumerator();
				while (enumerator.MoveNext())
				{
					list.Add(enumerator.Key, enumerator.Value);
				}

				return list;
			}
		}

        public static bool DictionariesEqual(IDictionary a, IDictionary b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null)
                return (b == null);
            if (b == null)
                return false;
            if (a.Count != b.Count)
                return false;

            IDictionaryEnumerator enumerator = a.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object bValue = b[enumerator.Key];

                if (enumerator.Value == null)
                {
                    if (bValue != null || !b.Contains(enumerator.Key))
                        return false;
                }
                else
                {
                    if (!Equals(enumerator.Value, bValue))
                        return false;
                }
            }

            return true;
        }
	}
}
