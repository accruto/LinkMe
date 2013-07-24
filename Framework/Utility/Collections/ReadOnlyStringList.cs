using System.Collections;

namespace LinkMe.Framework.Utility.Collections
{
	/// <summary>
	/// A list of strings implemented as an array. The list itself is read-only (ie. this class is immutable),
	/// but it provides Add, Remove and Insert methods that return new instances.
	/// </summary>
	[System.Serializable]
	public class ReadOnlyStringList : IList
	{
		/// <summary>
		/// A singleton instance of an empty string list.
		/// </summary>
		public static ReadOnlyStringList Empty = new ReadOnlyStringList(new string[0]);

		private string[] m_array;

		public ReadOnlyStringList(string[] array)
		{
			if (array == null)
				throw new System.ArgumentNullException("array");

			m_array = array;
		}

		#region IList Members

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool IsFixedSize
		{
			get { return true; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { throw new System.NotSupportedException("The collection is read-only."); }
		}

		bool IList.Contains(object value)
		{
			return (System.Array.IndexOf(m_array, value) >= 0);
		}

		int IList.IndexOf(object value)
		{
			return System.Array.IndexOf(m_array, value);
		}

		int IList.Add(object value)
		{
			throw new System.NotSupportedException("The collection is read-only.");
		}

		void IList.Insert(int index, object value)
		{
			throw new System.NotSupportedException("The collection is read-only.");
		}

		void IList.Clear()
		{
			throw new System.NotSupportedException("The collection is read-only.");
		}

		void IList.Remove(object value)
		{
			throw new System.NotSupportedException("The collection is read-only.");
		}

		void IList.RemoveAt(int index)
		{
			throw new System.NotSupportedException("The collection is read-only.");
		}

		#endregion

		#region ICollection Members

		public int Count
		{
			get { return m_array.Length; }
		}

		public bool IsSynchronized
		{
			get { return false; }
		}

		public object SyncRoot
		{
			get { return this; }
		}

		void ICollection.CopyTo(System.Array array, int index)
		{
			m_array.CopyTo(array, index);
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return (m_array.Length == 0 ? EmptyEnumerator.Value : m_array.GetEnumerator());
		}

		#endregion

		public string this[int index]
		{
			// If m_array is null throw the same exception as an ArrayList with 0 elements would.

			get
			{
				if (index < 0 || index >= m_array.Length)
				{
					throw new System.ArgumentOutOfRangeException("index", "Index was out of range."
						+"  Must be non-negative and less than the size of the collection.");
				}

				return m_array[index];
			}
		}

		#region Static methods

		public static bool Equals(ReadOnlyStringList a, ReadOnlyStringList b)
		{
			if (object.ReferenceEquals(a, b))
				return true;
			if (a == null || b == null)
				return false;
			if (a.Count != b.Count)
				return false;

			for (int index = 0; index < a.Count; index++)
			{
				if (!string.Equals(a[index], b[index]))
					return false;
			}

			return true;
		}

		#endregion

		public bool Contains(string value)
		{
			return (System.Array.IndexOf(m_array, value) >= 0);
		}

		public int IndexOf(string value)
		{
			return System.Array.IndexOf(m_array, value);
		}

		public void CopyTo(string[] array, int index)
		{
			m_array.CopyTo(array, index);
		}

		public ReadOnlyStringList Add(string value)
		{
			return Insert(Count, value);
		}

		public ReadOnlyStringList AddRange(string[] values)
		{
			return InsertRange(Count, values);
		}

		public ReadOnlyStringList Insert(int index, string value)
		{
			if (index < 0 || index > m_array.Length)
			{
				throw new System.ArgumentOutOfRangeException("index", "Insertion index was out of range."
					+ " Must be non-negative and less than or equal to size.");
			}

			string[] newArray = new string[m_array.Length + 1];

			if (index > 0)
			{
				System.Array.Copy(m_array, 0, newArray, 0, index);
			}

			newArray[index] = value;

			if (index < m_array.Length)
			{
				System.Array.Copy(m_array, index, newArray, index + 1, m_array.Length - index);
			}

			return new ReadOnlyStringList(newArray);
		}

		public ReadOnlyStringList InsertRange(int index, string[] values)
		{
			if (values == null)
				throw new System.ArgumentNullException("values");

			if (index < 0 || index > m_array.Length)
			{
				throw new System.ArgumentOutOfRangeException("index", "Insertion index was out of range."
					+ " Must be non-negative and less than or equal to size.");
			}

			string[] newArray = new string[m_array.Length + values.Length];

			if (index > 0)
			{
				System.Array.Copy(m_array, 0, newArray, 0, index);
			}

			System.Array.Copy(values, 0, newArray, index, values.Length);

			if (index < m_array.Length)
			{
				System.Array.Copy(m_array, index, newArray, index + values.Length, m_array.Length - index);
			}

			return new ReadOnlyStringList(newArray);
		}

		public ReadOnlyStringList Remove(string value)
		{
			// If the value is not in the collection return this instance.

			int index = IndexOf(value);
			return (index >= 0 ? RemoveAt(index) : this);
		}

		public ReadOnlyStringList RemoveAt(int index)
		{
			if (index < 0 || index >= m_array.Length)
			{
				throw new System.ArgumentOutOfRangeException("index", "Index was out of range."
					+"  Must be non-negative and less than the size of the collection.");
			}

			string[] newArray = new string[m_array.Length - 1];

			if (index > 0)
			{
				System.Array.Copy(m_array, 0, newArray, 0, index);
			}
			if (index < m_array.Length - 1)
			{
				System.Array.Copy(m_array, index + 1, newArray, index, m_array.Length - index - 1);
			}

			return new ReadOnlyStringList(newArray);
		}

		public ReadOnlyStringList RemoveRange(string[] values)
		{
			if (values == null)
				throw new System.ArgumentNullException("values");

			string[] tempArray = new string[m_array.Length];
			int newIndex = 0;

			// Iterate over the existing array and copy each element that is not removed to a new array.

			for (int index = 0; index < m_array.Length; index++)
			{
				string current = m_array[index];
				if (System.Array.IndexOf(values, current) == -1)
				{
					tempArray[newIndex++] = current;
				}
			}

			if (newIndex == m_array.Length)
				return this; // Nothing removed, return this instance.

			// Create a new array of the correct length.

			string[] newArray = new string[newIndex];
			System.Array.Copy(tempArray, 0, newArray, 0, newIndex);

			return new ReadOnlyStringList(newArray);
		}

		public string[] ToArray()
		{
			return (string[])m_array.Clone();
		}

		public string Join(string separator)
		{
			return string.Join(separator, m_array);
		}
	}
}
