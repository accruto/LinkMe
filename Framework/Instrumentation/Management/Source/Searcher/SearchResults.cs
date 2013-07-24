using System;
using System.Collections;
using System.Diagnostics;

namespace LinkMe.Framework.Instrumentation.Management
{
	/// <summary>
	/// A collection of results returned by the searcher.
	/// </summary>
	public class SearchResults : IEnumerable
	{
		private ArrayList m_list;

		internal SearchResults(ArrayList list)
		{
			Debug.Assert(list != null, "list != null");
			m_list = list;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		#endregion

		public ICatalogueElement this[int i]
		{
			get { return (ICatalogueElement)m_list[i]; }
		}

		public int Count
		{
			get { return m_list.Count; }
		}

		public bool Contains(ICatalogueElement element)
		{
			return m_list.Contains(element);
		}
	}
}
