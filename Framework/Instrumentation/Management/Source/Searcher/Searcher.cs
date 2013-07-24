using System.Collections;
using System.Diagnostics;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class Searcher
	{
		internal Searcher(ICatalogueElement searchRoot)
		{
			_searchRoot = searchRoot;
			_filter = new SearchFilter();
			_options = new SearchOptions();
		}

		public SearchFilter Filter
		{
			get { return _filter; }
		}

		public SearchOptions Options
		{
			get { return _options; }
		}

		public SearchResults Get()
		{
			// This is what will be returned.

			var searchResults = new ArrayList();
			_filter.Prepare(Options.IgnoreCase);

			// Start at the root.

			Get(_searchRoot, searchResults);

			return new SearchResults(searchResults);
		}

		private void Get(ICatalogueElement element, IList searchResults)
		{
			// Check the element itself.

			if ( _filter.IsMatch(element) )
				searchResults.Add(element);

			if ( _options.Recursive )
			{
				// Check children.

				if ( (_filter.Elements & (CatalogueElements.Namespace | CatalogueElements.Source)) != CatalogueElements.None )
				{
					var namespaceParent = element as INamespaceParent;
					if ( namespaceParent != null )
					{
						foreach ( Namespace ns in namespaceParent.Namespaces )
							Get(ns, searchResults);
					}
				}

				if ( (_filter.Elements & CatalogueElements.Source) != CatalogueElements.None )
				{
					var sourceParent = element as ISourceParent;
					if ( sourceParent != null )
					{
						foreach ( Source source in sourceParent.Sources )
							Get(source, searchResults);
					}
				}

				var catalogue = element as Catalogue;
				if (catalogue != null)
				{
					if ( (_filter.Elements & CatalogueElements.Event) != CatalogueElements.None )
					{
						foreach (EventType eventType in catalogue.EventTypes)
							Get(eventType, searchResults);
					}
				}
			}
		}

		public Namespace GetNamespace(string relativeName)
		{
			const string method = "GetNamespace";

			if (relativeName == null)
				throw new NullParameterException(GetType(), method, "relativeName");

			ICatalogueSearch search = GetCatalogueSearch();
			if (search == null)
			{
				// Repository search not available, iterate.

				return GetNamespaceMandatory(relativeName);
			}
		    
            // Search in memory first, without loading collections.

		    INamespaceParent found;
		    string remainingPath;

		    if (GetNamespaceOptional(relativeName, out found, out remainingPath))
		        return (Namespace)found;

		    // If the search failed, but some collections were not loaded then search the repository.

		    if (remainingPath != null)
		        return search.GetNamespace(found, remainingPath, Options.IgnoreCase);

		    return null; // All collections were already loaded, but the namespace was still not found.
		}

		public Source GetSource(string relativeReference)
		{
			const string method = "GetSource";

			if (relativeReference == null)
				throw new NullParameterException(GetType(), method, "relativeReference");

			var catalogueName = new CatalogueName(relativeReference);

			Namespace ns = (catalogueName.Namespace.Length == 0 ? _searchRoot as Namespace :
				GetNamespace(catalogueName.Namespace));
			if (ns == null)
				return null;

			ICatalogueSearch search = GetCatalogueSearch();
			if (search == null)
			{
				// Repository search not available, so load the Sources collection if necessary.

				return ns.Sources.GetItem(catalogueName.RelativeQualifiedReference, Options.IgnoreCase, true);
			}
		    
            // Search in memory first, without loading the Sources collection.

		    Source result = ns.Sources.GetItem(catalogueName.RelativeQualifiedReference, Options.IgnoreCase,
		                                       false);

		    // If the search failed, but the collection was not loaded then search the repository.

		    if (result == null && !ns.Sources.IsLoaded)
		        return search.GetSource(ns, catalogueName.Name, Options.IgnoreCase);
		    return result;
		}

		private ICatalogueSearch GetCatalogueSearch()
		{
			return _searchRoot.Catalogue.CatalogueConnection as ICatalogueSearch;
		}

	    private Namespace GetNamespaceMandatory(string nsName)
		{
			Debug.Assert(nsName != null, "nsName != null");

			var nsParent = _searchRoot as INamespaceParent;
			if (nsParent == null)
				return null;

			string[] nsParts = nsName.Split('.');
			Debug.Assert(nsParts.Length > 0, "nsParts.Length > 0");

			bool ignoreCase = Options.IgnoreCase;
			int index = 0;

			while (index < nsParts.Length)
			{
				Namespace child = nsParent.Namespaces.GetItem(nsParts[index++], ignoreCase, true);
				if (child == null)
					return null;

				nsParent = child;
			}
			Debug.Assert(nsParent != null, "nsParent != null");

			return (Namespace)nsParent;
		}

		/// <summary>
		/// Searches for the namespace without loading collections.
		/// </summary>
		/// <param name="nsName">The namespace name to find.</param>
		/// <param name="bestFound">The namespace being searched for, if found, otherwise the closest parent
		/// that was found (which may be the Catalogue).</param>
		/// <param name="remainingPath">The path of the namespace searched for relative to bestFound.</param>
		/// <returns>True if the namespace was found, otherwise false.</returns>
		private bool GetNamespaceOptional(string nsName, out INamespaceParent bestFound, out string remainingPath)
		{
			Debug.Assert(nsName != null, "nsName != null");

			bestFound = _searchRoot as INamespaceParent;
			if (bestFound == null)
			{
				remainingPath = null;
				return false;
			}

			string[] nsParts = nsName.Split('.');
			Debug.Assert(nsParts.Length > 0, "nsParts.Length > 0");

			int index = 0;
			bool ignoreCase = Options.IgnoreCase;

			while (index < nsParts.Length)
			{
				Namespace child = bestFound.Namespaces.GetItem(nsParts[index], ignoreCase, false);
				if (child == null)
				{
				    remainingPath = bestFound.Namespaces.IsLoaded ? null : string.Join(".", nsParts, index, nsParts.Length - index);
				    return false;
				}

			    bestFound = child;
				index++;
			}
			Debug.Assert(bestFound != null, "bestFound != null");

			remainingPath = null;
			return true;
		}

		#region Members

		private readonly ICatalogueElement _searchRoot;
		private readonly SearchFilter _filter;
		private readonly SearchOptions _options;

		#endregion
	}
}
