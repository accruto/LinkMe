using System.Collections;

namespace LinkMe.Framework.Utility.Collections
{
	/// <summary>
	/// A singleton implementation of IEnumerator that simulates enumerating over an empty collection.
	/// </summary>
	[System.Serializable]
	public class EmptyEnumerator : IEnumerator
	{
		public static readonly EmptyEnumerator Value = new EmptyEnumerator();

		private EmptyEnumerator()
		{
		}

		#region IEnumerator Members

		public object Current
		{
			get { throw new System.InvalidOperationException("The collection is empty."); }
		}

		public void Reset()
		{
		}

		public bool MoveNext()
		{
			return false;
		}

		#endregion
	}
}
