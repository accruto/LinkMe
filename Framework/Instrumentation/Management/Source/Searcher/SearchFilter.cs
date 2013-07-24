using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

using LinkMe.Framework.Configuration;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class SearchFilter
	{
		internal SearchFilter()
		{
			m_elementFilter = CatalogueElements.All;
		}

		public CatalogueElements Elements
		{
			get { return m_elementFilter; }
			set { m_elementFilter = value; }
		}

		public string ElementReferencePattern
		{
			get { return m_elementReferencePattern; }
			set { m_elementReferencePattern = value; }
		}

		/// <summary>
		/// Call before starting a search.
		/// </summary>
		internal void Prepare(bool ignoreCase)
		{
			if (m_elementReferencePattern != null)
			{
				RegexOptions options = (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
				m_elementReferenceRegex = new Regex(m_elementReferencePattern, options);
			}
			else
			{
				m_elementReferenceRegex = null;
			}
		}

		internal bool IsMatch(ICatalogueElement element)
		{
			// Check the element.

			if ( (m_elementFilter & element.Element) == CatalogueElements.None  )
				return false;

			// Check the names.

			if ( !IsNamesMatch(element) )
				return false;

			return true;
		}

		private bool IsNamesMatch(ICatalogueElement element)
		{
			// Only have to check if there is a filter.

			if ( m_elementReferencePattern != null )
			{
				Debug.Assert(m_elementReferenceRegex != null, "m_elementReferenceRegex is null - Prepare()"
					+ " not called?");

				IElementReference referencedElement = element as IElementReference;
				if ( referencedElement != null && !m_elementReferenceRegex.IsMatch(referencedElement.FullyQualifiedReference) )
					return false;
			}

			return true;
		}

		private CatalogueElements m_elementFilter;
		private string m_elementReferencePattern;
		private Regex m_elementReferenceRegex;
	}
}
