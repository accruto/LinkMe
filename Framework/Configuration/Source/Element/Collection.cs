using System.Collections;
using System.Diagnostics;

using LinkMe.Framework.Configuration.Exceptions;

namespace LinkMe.Framework.Configuration
{
	internal enum LoadState
	{
		NoChildrenLoaded,
		SomeChildrenLoaded,
		AllChildrenLoaded,
		CurrentlyLoading
	}

	public abstract class Collection
		:	System.MarshalByRefObject,
			IEnumerable
	{
		private Element m_parent;
		private LoadState m_loadState = LoadState.NoChildrenLoaded;
		private int m_count = -1;

		protected Collection(Element parent)
		{
			m_parent = parent;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			EnsureLoaded();
			return GetEnumeratorWithoutLoading();
		}

		#endregion

		public int Count
		{
			get
			{
				// If the collection hasn't been loaded yet don't load it all just to get the count - call
				// ElementGetCollectionCount() once and cache the result.

				if (m_loadState == LoadState.AllChildrenLoaded)
					return GetElementCount(); 

				if (m_count == -1)
				{
					m_count = Parent.GetCollectionCount(Name);
					if (m_count == -1)
					{
						// The collection doesn't support retrieving the count without the contents - load it all.

						EnsureLoaded();
						return GetElementCount();
					}
					else if (m_count == 0)
					{
						// The collection is empty, so it doesn't need to be loaded at all.

						m_loadState = LoadState.AllChildrenLoaded;
					}
				}

				return m_count;
			}
		}

		public bool IsLoaded
		{
			get { return (m_loadState == LoadState.AllChildrenLoaded); }
		}

		protected internal Element Parent
		{
			get { return m_parent; }
		}

		protected internal abstract string Name
		{
			get;
		}

		internal LoadState State
		{
			get { return m_loadState; }
			set { m_loadState = value; }
		}

		/// <summary>
		/// Tells the collection that all its children have been added. You do not need to call this method
		/// when the children are loaded from Element.PopulateCollection().
		/// </summary>
		public void UpdateLoaded()
		{
			m_loadState = LoadState.AllChildrenLoaded;
		}

		protected abstract Element CreateElement(Element parent, string key);
		protected abstract void CheckAddElement(Element element);
		protected abstract IEnumerator GetEnumeratorWithoutLoading();

		protected void EnsureLoaded()
		{
			if (m_loadState != LoadState.AllChildrenLoaded && m_loadState != LoadState.CurrentlyLoading)
			{
				LoadState priorLoadState = m_loadState;
				m_loadState = LoadState.CurrentlyLoading;

				try
				{
					m_parent.LoadCollectionContents(Name);
				}
				catch
				{
					m_loadState = priorLoadState;
					throw;
				}

				m_loadState = LoadState.AllChildrenLoaded;
			}
		}

		protected System.Exception NewElementCannotBeRemovedException(string method, Element element)
		{
			string key = ((IElementIdentification) element).ElementKey;
			string type = ((IElementIdentification) element).ElementType;
			throw new ElementCannotBeRemovedException(GetType(), method, key, type, Parent);
		}

		protected System.Exception NewElementAlreadyExistsException(string method, Element element, Element existingElement)
		{
			string key = ((IElementIdentification) element).ElementKey;
			string type = ((IElementIdentification) element).ElementType;
			string existingKey = ((IElementIdentification) existingElement).ElementKey;
			string existingType = ((IElementIdentification) existingElement).ElementType;

			// If the keys happen to differ only by case - throw a more specific exception.

			if (key == existingKey)
				throw new ElementAlreadyExistsException(GetType(), method, key, type, existingType, Parent);
			else
			{
				throw new ElementAlreadyExistsWithDifferentCaseException(GetType(), method, key, type,
					existingKey, existingType, Parent);
			}
		}

		internal abstract CommitStatus Commit();
		internal abstract void UpdateCommit();
		internal abstract int GetElementCount();

		internal void UpdateAdd()
		{
			if (m_loadState == LoadState.NoChildrenLoaded)
			{
				m_loadState = LoadState.SomeChildrenLoaded;
			}
		}
	}

	internal class Collections
	{
		public Collections()
		{
		}

		public void Initialise(int count)
		{
			m_collections = new Collection[count];
		}

		public Collection this[string name]
		{
			get
			{
				Debug.Assert(m_collections != null, "Collections were accessed before Initialise() was called.");

				// Iterate.

				for (int index = 0; index < m_collections.Length; index++)
				{
					Collection col = (Collection)m_collections[index];
					if (col != null && col.Name == name)
						return col;
				}

				return null;
			}
		}

		public void Add(string name, Collection collection)
		{
			Debug.Assert(m_collections != null, "Collections were accessed before Initialise() was called.");

			// Find the first free slot.

			for (int index = 0; index < m_collections.Length; index++)
			{
				if (m_collections[index] == null)
				{
					m_collections[index] = collection;
					return;
				}
			}

			Debug.Fail("No free slots are available to add collection '" + name
				+ "' - ensure that Initialise() is called with the correct value.");
		}

		public void Replace(string name, Collection newCollection)
		{
			Debug.Assert(m_collections != null, "Collections were accessed before Initialise() was called.");

			// Iterate.

			for (int index = 0; index < m_collections.Length; index++)
			{
				Collection col = (Collection)m_collections[index];
				if (col != null && col.Name == name)
				{
					m_collections[index] = newCollection;
					return;
				}
			}

			Debug.Fail("Failed to find the collection to replace, '" + name + "'.");
		}

		public void Refresh()
		{
			if (m_collections != null)
			{
				// Clear out all values for the collections.

				System.Array.Clear(m_collections, 0, m_collections.Length);
			}
		}

		public CommitStatus Commit()
		{
			CommitStatus status = CommitStatus.Continue;

			if (m_collections != null)
			{
				// Iterate through all collections.

				for (int index = 0; index < m_collections.Length; index++)
				{
					Collection col = (Collection)m_collections[index];
					if (col != null)
					{
						status = col.Commit();
						if (status == CommitStatus.Discontinue)
							return status;
					}
				}
			}

			return status;
		}

		public void UpdateCommit()
		{
			if (m_collections != null)
			{
				for (int index = 0; index < m_collections.Length; index++)
				{
					Collection col = (Collection)m_collections[index];
					if (col != null)
						col.UpdateCommit();
				}
			}
		}

		// For performance reasons a Hashtable may be better but this collection of collections is subject to
		// modification during iteration so to ensure enumerators etc are still OK an array and index look up
		// is used.

		private Collection[] m_collections = null;
	}
}
