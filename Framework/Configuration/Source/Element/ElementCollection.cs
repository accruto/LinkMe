using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Collections;
using LinkMe.Framework.Configuration.Exceptions;
using System;

namespace LinkMe.Framework.Configuration
{
	public abstract class ElementCollection
		:	Collection
	{
		#region Constructors

		protected ElementCollection(Element parent)
			: base(parent)
		{
		}

		#endregion

		#region Operations

		protected sealed override IEnumerator GetEnumeratorWithoutLoading()
		{
			return (m_elements == null ? EmptyEnumerator.Value : m_elements.GetEnumerator());
		}

		internal override CommitStatus Commit()
		{
			EnsureLoaded();

			CommitStatus status = CommitStatus.Continue;

			if (m_removedElements != null)
			{
				// Iterate through all removed elements first.

				foreach ( Element element in m_removedElements.Values )
				{
					status = element.CommitImpl();
					if ( status == CommitStatus.Discontinue )
						break;
				}
			}

			if (m_elements != null)
			{
				// Iterate through all real elements now.

				if ( status != CommitStatus.Discontinue )
				{
					foreach ( Element element in m_elements )
					{
						status = element.CommitImpl();
						if ( status == CommitStatus.Discontinue )
							break;
					}
				}
			}

			// Clean out the removed elements now that everything is done.

			m_removedElements = null;

			return status;
		}

		internal override void UpdateCommit()
		{
			EnsureLoaded();

			if (m_elements != null)
			{
				// Iterate through all elements.

				foreach ( Element element in m_elements )
					element.UpdateCommit();
			}
		}

		#endregion

		#region object Members

		public override int GetHashCode()
		{
			return (m_elements == null ? 0 : m_elements.Count.GetHashCode());
		}

		public override bool Equals(object other)
		{
			ElementCollection otherCollection = other as ElementCollection;
			if (otherCollection == null)
				return false;

			// Check the count of elements.

			if (m_elements == null)
				return (otherCollection.m_elements == null);
			else if (otherCollection.m_elements == null)
				return false;

			if ( m_elements.Count != otherCollection.m_elements.Count
				|| State == LoadState.SomeChildrenLoaded || State != otherCollection.State )
				return false;

			// Enumerate over each collection comparing each element.

			IEnumerator enumerator = m_elements.GetEnumerator();
			IEnumerator otherEnumerator = otherCollection.m_elements.GetEnumerator();

			while ( enumerator.MoveNext() && otherEnumerator.MoveNext() )
			{
				if ( !object.Equals(enumerator.Current, otherEnumerator.Current) )
					return false;
			}

			// If it gets to here then everything is ok.

			return true;
		}

		#endregion

		#region Protected Operations

		protected Element this[int index]
		{
			get
			{
				EnsureLoaded();

				if (m_elements == null)
				{
					// Throw the same exception as an ArrayList with 0 elements would.

					throw new System.ArgumentOutOfRangeException("index", "Index was out of range."
						+"  Must be non-negative and less than the size of the collection.");
				}

				return (Element)m_elements[index];
			}
		}

		protected Element this[string key]
		{
			get
			{
				switch (State)
				{
					case LoadState.AllChildrenLoaded:
						return (m_elements == null ? null : GetByKey(key, false));

					case LoadState.SomeChildrenLoaded:
						// Look in the elements loaded so far.

						if (m_elements != null)
						{
							Element element = GetByKey(key, false);
							if (element != null)
								return element;
						}

						// Load the rest and look again.

						EnsureLoaded();
						return (m_elements == null ? null : GetByKey(key, false));

					case LoadState.NoChildrenLoaded:
						EnsureLoaded();
						return (m_elements == null ? null : GetByKey(key, false));

					case LoadState.CurrentlyLoading:
						Debug.Fail("Attempted to retrive element '" + key + "' while loading.");
						return null;

					default:
						Debug.Fail("Unexpected value of State: " + State.ToString());
						return null;
				}
			}
		}

		protected int IndexOf(Element element)
		{
			EnsureLoaded();

			if (m_elements == null)
				return -1;

			return m_elements.IndexOf(element);
		}

		protected override void CheckAddElement(Element element)
		{
			const string method = "Add";

			EnsureLoaded();

			if (m_elements != null)
			{
				string key = ((IElementIdentification) element).ElementKey;
				Element existing = GetByKey(key, true);
				if (existing != null)
					throw NewElementAlreadyExistsException(method, element, existing);
			}
		}

		protected void Add(Element element)
		{
			const string method = "Add";

			EnsureElements();
			EnsureLoaded();

			// Check that this element can in fact be added.

			element.CheckAdd();

			// Add it to the list.

			string key = ((IElementIdentification) element).ElementKey;
			Element existing = GetByKey(key, true);
			if (existing != null)
				throw NewElementAlreadyExistsException(method, element, existing);

			m_elements.Add(element);
			m_keyMap.Add(key, element);

			// Update it now that it is done.

			element.UpdateAdd();
			UpdateAdd();
		}

		/// <summary>
		/// Add the element if it is not in the collection and not already deleted from the collection. Unlike
		/// Add(), this method does not attempt to load the collection and always marks the element as Clean.
		/// </summary>
		/// <param name="element">The element to add.</param>
		/// <returns>If the element already exists in the collection - the existing element, if the element
		/// has already been deleted from the collection - null, otherwise the element being added.</returns>
		protected Element AddIfNotLoaded(Element element)
		{
			EnsureElements();
			// Do not try to load this collection, it should be in the process of loading already.

			// Check that this element can in fact be added.

			element.CheckAdd();

			// Make sure there is not already an element with the given name.

			string key = ((IElementIdentification) element).ElementKey;
			Element existing = GetByKey(key, false);
			if (existing != null)
				return existing; // Already loaded - return the existing element.

			if (m_removedElements != null && m_removedElements.Contains(key))
				return null; // Already loaded and deleted.

			// Add it to the list.

			m_elements.Add(element);
			m_keyMap.Add(key, element);

			// The element has been loaded, so it's clean.

			element.UpdateLoad();
			UpdateAdd();

			return element;
		}

		protected void AddIfNotLoaded(Element[] elements)
		{
			foreach (Element element in elements)
			{
				AddIfNotLoaded(element);
			}
		}

		protected void Remove(Element element)
		{
			const string method = "Remove";

			EnsureLoaded();

			// Check that this element can in fact be removed.

			element.CheckRemove();

			// Make sure that this element is in fact in the collection.

			if (m_elements == null)
				throw NewElementCannotBeRemovedException(method, element);

			string key = ((IElementIdentification) element).ElementKey;
			int index = IndexOfKey(key);
			if (index == -1)
				throw NewElementCannotBeRemovedException(method, element);

			// Move it to the removed elements.

			m_elements.RemoveAt(index);
			m_keyMap.Remove(key);

			EnsureRemovedElements();
			m_removedElements.Add(key, element);

			// Update it now that it is done.

			element.UpdateRemove();
		}

		protected void RemoveAll()
		{
			EnsureLoaded();

			if (m_elements != null)
			{
				// Check that each element can in fact be removed.

				foreach (Element element in m_elements)
					element.CheckRemove();

				// Move all elements to the removed elements.

				EnsureRemovedElements();

				foreach (Element element in m_elements)
				{
					string key = ((IElementIdentification) element).ElementKey;
					m_removedElements[key] = element;
				}

				m_elements = null;
				m_keyMap = null;

				// Update them now that it is done.

				foreach (Element element in m_removedElements.Values)
					element.UpdateRemove();
			}
		}

		protected void Replace(Element oldElement, Element newElement)
		{
			const string method = "Replace";

			EnsureLoaded();

			// Check the elements.

			oldElement.CheckRemove();
			newElement.CheckAdd();

			// Make sure that the old element is in fact in the collection.

			if (m_elements == null)
				throw NewElementCannotBeRemovedException(method, oldElement);

			string oldKey = ((IElementIdentification) oldElement).ElementKey;
			Element currentElement = GetByKey(oldKey, false);
			if ( !object.ReferenceEquals(oldElement, currentElement) )
				throw NewElementCannotBeRemovedException(method, oldElement);

			// Make sure there is not already an element with the given name (or the given name with
			// a different case), but allow the old and the new name to differ by case. This lets the
			// user "rename" an element and change only the case.

			string newKey = ((IElementIdentification) newElement).ElementKey;
			if (string.Compare(newKey, oldKey, true) != 0)
			{
				Element existing = GetByKey(newKey, true);
				if (existing != null)
					throw NewElementAlreadyExistsException(method, newElement, existing);
			}

			int index = IndexOfKey(oldKey);
			Debug.Assert(index >= 0, "index >= 0");

			// Remove the old and add the new.

			m_keyMap.Remove(oldKey);

			EnsureRemovedElements();

			m_removedElements[oldKey] = oldElement;
			m_elements[index] = newElement;
			m_keyMap.Add(newKey, newElement);

			// Update it now that it is done.

			oldElement.UpdateRemove();
			newElement.UpdateAdd();
		}

		protected void DeleteChildren()
		{
			EnsureLoaded();

			// Delete the children in both collections.

			if (m_removedElements != null)
			{
				foreach ( Element element in m_removedElements.Values )
					element.DoDeleteChildren();
			}

			if (m_elements != null)
			{
				foreach ( Element element in m_elements )
					element.DoDeleteChildren();
			}
		}

		#endregion

		internal override int GetElementCount()
		{
			return (m_elements == null ? 0 : m_elements.Count);
		}

		private void EnsureElements()
		{
			if (m_elements == null)
			{
				m_elements = new ArrayList();
				// Use a case-insensitive comparer to be able to detect child element names that differ
				// only by case. All logic that searches for an element by name should check that the
				// name is exactly equal (case-sensitive).
				m_keyMap = new Hashtable(StringComparer.InvariantCulture);
			}
		}

		private void EnsureRemovedElements()
		{
			if (m_removedElements == null)
			{
				// This needs to be case-sensitive.
				m_removedElements = new Hashtable();
			}
		}

		private Element GetByKey(string key, bool ignoreCase)
		{
			Debug.Assert(m_keyMap != null, "m_keyMap != null");

			Element element = (Element)m_keyMap[key];
			if (element == null || ignoreCase)
				return element;

			// Found an element - make sure its name matches the key using a case-sensitive comparison
			// (m_keyMap is case-insensitive).

			string elementKey = ((IElementIdentification)element).ElementKey;
			return (string.Equals(elementKey, key) ? element : null);
		}

		private int IndexOfKey(string key)
		{
			Debug.Assert(m_elements != null, "m_elements != null");

			for (int index = 0; index < m_elements.Count; index++)
			{
				Element element = (Element)m_elements[index];
				string elementKey = ((IElementIdentification)element).ElementKey;
				if (string.Equals(elementKey, key))
					return index;
			}

			return -1;
		}

		#region Members

		// Do not instantiate these members until they are needed to save memory (Component Catalogue may create
		// many instances of this class).

		private ArrayList m_elements = null;
		private Hashtable m_keyMap = null; // Key: element key, value: element.
		private Hashtable m_removedElements = null; // Key: element key, value: element.

		#endregion
	}
}
