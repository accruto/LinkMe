using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LinkMe.Framework.Configuration
{
	#region ElementState

	internal enum ElementState
	{
		Clean,
		Dirty,
		Created,
		Added,
		Removed
	}

	#endregion

	#region Element

	public abstract class Element
		:	System.MarshalByRefObject,
			IElement
	{
		#region Constructors

		protected Element(IElement parent, string name)
		{
			// Assign.

			m_name = name;
			m_parent = parent;
			m_isCurrentlyLoading = false;
			m_state = ElementState.Clean;
			if ( parent != null )
			{
				Element element = parent as Element;
				if ( element != null )
					m_state = element.IsCurrentlyLoading ? ElementState.Clean : ElementState.Created;
			}

			m_collections = new Collections();
		}

		#endregion

		#region Interfaces

		[ConfigurationRegexValidation(Constants.Validation.CompleteNamePattern)]
		public string Name
		{
			get { return m_name; }
		}

		public IElement Parent
		{
			get { return m_parent; }
		}

		#endregion

		#region Properties

		#endregion

		#region Operations

		public void Refresh()
		{
			m_collections.Refresh();
		}

		public void Commit()
		{
			ICommitConnection commitConnection = this as ICommitConnection;
			if ( commitConnection != null )
				commitConnection.Commit();
			else
				CommitImpl();
			UpdateCommit();
		}

		protected internal virtual CommitStatus CommitImpl()
		{
			CommitStatus status = CommitStatus.Continue;

			// Act based on the current state.

			switch ( m_state )
			{
				case ElementState.Added:
					status = Create();
					break;

				case ElementState.Dirty:
					status = Update();
					break;

				case ElementState.Removed:
					DeleteChildren();
					status = Delete();
					break;

				case ElementState.Clean:
				case ElementState.Created:
					break;

				default:
					Debug.Fail("Unexpected value of m_state: " + m_state.ToString());
					break;
			}

			// If needed do the children.

			if ( status == CommitStatus.Continue )
				status = m_collections.Commit();
			return status;
		}

		internal void UpdateCommit()
		{
			// Update the state based on the current state.

			switch ( m_state )
			{
				case ElementState.Added:
					m_state = ElementState.Clean;
					break;

				case ElementState.Dirty:
					m_state = ElementState.Clean;
					break;

				case ElementState.Removed:
				case ElementState.Clean:
				case ElementState.Created:
					break;

				default:
					Debug.Fail("Unexpected value of m_state: " + m_state.ToString());
					break;
			}

			// Do the children.

			m_collections.UpdateCommit();
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			return Name;
		}

		abstract protected CommitStatus Create();
		abstract protected CommitStatus Update();
		abstract protected CommitStatus Delete();

		virtual protected void DeleteChildren()
		{
			// Do nothing by default.
		}

		internal void DoDeleteChildren()
		{
			DeleteChildren();
		}

		#endregion

		#region Helpers

		internal ElementState State
		{
			get { return m_state; }
		}

		internal protected bool IsCurrentlyLoading
		{
			get { return m_isCurrentlyLoading; }
			set { m_isCurrentlyLoading = value; }
		}

		internal void CheckAdd()
		{
			// Note that it's possible for the state to be "Removed" if the element is removed and then
			// re-added.

			switch (m_state)
			{
				case ElementState.Created:
				case ElementState.Clean:
				case ElementState.Removed:
					break;

				default:
					Debug.Fail("Unexpected state in CheckAdd: " + m_state.ToString());
					break;
			}
		}

		internal void UpdateAdd()
		{
			switch (m_state)
			{
				case ElementState.Created:
				case ElementState.Removed:
					m_state = ElementState.Added;
					break;

				case ElementState.Clean:
				case ElementState.Dirty:
					break;

				default:
					Debug.Fail("Unexpected state in UpdateAdd: " + m_state.ToString());
					break;
			}
		}

		internal void UpdateLoad()
		{
			switch (m_state)
			{
				case ElementState.Created:
					m_state = ElementState.Clean;
					break;

				case ElementState.Clean:
					break;

				default:
					Debug.Fail("Unexpected state in UpdateLoad: " + m_state.ToString());
					break;
			}
		}

		internal void CheckRemove()
		{
			Debug.Assert(m_state != ElementState.Removed, "Unexpected state in CheckRemove: " + m_state.ToString());
		}

		internal void UpdateRemove()
		{
			m_state = ElementState.Removed;
		}

		protected void UpdateProperty()
		{
			if ( m_state == ElementState.Clean )
				m_state = ElementState.Dirty;
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Element) )
				return false;
			return Name == ((Element) other).Name;
		}

		#endregion

		#region Collections

		protected internal abstract void PopulateCollection(string name);
		
		protected internal virtual int GetCollectionCount(string name)
		{
			// Return -1 to force the collection to be loaded and then get the count. If a derived collection can
			// get the count faster (ie. without loading all its contents) - override this method.

			return -1;
		}

		protected Collection Collection(string name)
		{
			// Check whether it exists first.

			Collection collection = m_collections[name];

			if (collection == null)
			{
				collection = CreateCollection(name);
				collection.State = (State == ElementState.Created || State == ElementState.Added ?
					LoadState.AllChildrenLoaded : LoadState.NoChildrenLoaded);

				m_collections.Add(name, collection);
			}

			return collection;
		}

		protected void InitialiseCollections(int count)
		{
			m_collections.Initialise(count);
		}

		protected abstract Collection CreateCollection(string name);

		internal void LoadCollectionContents(string name)
		{
			IsCurrentlyLoading = true;
			try
			{
				PopulateCollection(name);
			}
			finally
			{
				IsCurrentlyLoading = false;
			}
		}

		#endregion

		#region Members

		private string m_name;
		private IElement m_parent;
		private ElementState m_state;
		private bool m_isCurrentlyLoading;
		private Collections m_collections;

		private static Regex m_regexName = new Regex(Constants.Validation.CompleteNamePattern);

		#endregion
	}

	#endregion
}
