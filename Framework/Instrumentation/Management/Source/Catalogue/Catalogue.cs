using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class Catalogue
		:	CatalogueQualifiedElement,
			INamespaceParent,
			ICommitConnection
	{
		#region Constructors

		public Catalogue(ICatalogueConnection catalogueConnection)
			:	base(null, string.Empty)
		{
			_catalogueConnection = catalogueConnection;

			InitialiseCollections(8);
		}

		public Catalogue()
			:	this(new DefaultCatalogueConnection())
		{
		}

		#endregion

		#region Relationships

		Catalogue ICatalogueElement.Catalogue
		{
			get { return this; }
		}

		CatalogueElements ICatalogueElement.Element
		{
			get { return CatalogueElements.Catalogue; }
		}

		internal ICatalogueConnection CatalogueConnection
		{
			get { return _catalogueConnection; }
		}

        public Searcher GetSearcher()
        {
            return new Searcher(this);
        }

        void ICommitConnection.Commit()
		{
			if ( CatalogueConnection.Commit(this) == CommitStatus.Continue )
				CommitImpl();
		}

		#endregion

		#region Collections

		public EventTypes EventTypes
		{
			get { return (EventTypes) Collection(Constants.Collections.EventTypes); }
		}

		public void Add(EventType eventType)
		{
			EventTypes.Add(eventType);
		}

		public void Remove(EventType eventType)
		{
            EventTypes.Remove(eventType);
		}

		public void Replace(EventType oldEventType, EventType newEventType)
		{
			EventTypes.Replace(oldEventType, newEventType);
		}

		public Namespaces Namespaces
		{
			get { return (Namespaces) Collection(Constants.Collections.Namespaces); }
		}

		public void Add(Namespace ns)
		{
			Namespaces.Add(ns);
		}

		public void AddIfNotLoaded(Namespace ns)
		{
			Namespaces.AddIfNotLoaded(ns);
		}

		public void Remove(Namespace ns)
		{
			Namespaces.Remove(ns);
		}

		void INamespaceParent.Add(Namespace ns, bool updateParentState)
		{
			Namespaces.Add(ns);
		}

		void INamespaceParent.Remove(Namespace ns, bool updateParentState)
		{
			Namespaces.Remove(ns);
		}

		#endregion

		#region Equality

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Catalogue) )
				return false;
			return Equals((Catalogue) other);
		}

		public bool Equals(Catalogue other)
		{
			if ( other == null )
				return false;

			// Check base.

			if ( !base.Equals(other) )
				return false;

			// Check details.

			if ( !EqualDetails(other) )
				return false;

			// Check children.

			return EqualChildren(other);
		}

		public static bool Equals(Catalogue c1, Catalogue c2)
		{
			return object.Equals(c1, c2);
		}

		public static bool operator==(Catalogue c1, Catalogue c2)
		{
			return Equals(c1, c2);
		}

		public static bool operator!=(Catalogue c1, Catalogue c2)
		{
			return !Equals(c1, c2);
		}

		private static bool EqualDetails(Catalogue other)
		{
			return true;
		}

		private bool EqualChildren(Catalogue other)
		{
			return EventTypes.Equals(other.EventTypes)
				&& Namespaces.Equals(other.Namespaces);
		}

		#endregion

		#region Overrides

		protected override CommitStatus Create()
		{
			return CommitStatus.Continue;
		}

		protected override CommitStatus Update()
		{
			return CommitStatus.Continue;
		}

		protected override CommitStatus Delete()
		{
			return CommitStatus.Continue;
		}

		#endregion

		#region Ensure

		public Namespace EnsureNamespace(string fullName)
		{
			return (Namespace) EnsureNamespaceParent(fullName);
		}

		public INamespaceParent EnsureNamespaceParent(string fullName)
		{
			// Check for the top.

			if ( fullName.Length == 0 )
				return this;

			// Extract the top name and look for it.

			string namespaceName;
			string relativeName;
			CatalogueName.SplitRootName(fullName, out namespaceName, out relativeName);

			// Ensure the top category exists.

			Namespace ns = Namespaces[namespaceName];
			if ( ns == null )
			{
				// Not there so create one.

				ns = CreateNamespace(this, namespaceName);
				ns.IsEnsured = true;
				Namespaces.Add(ns);
			}

			// Keep going.

			return ns.EnsureNamespaceParent(relativeName);
		}

		public ISourceParent EnsureSourceParent(string fullName)
		{
			// Check for the top.

			if ( fullName.Length == 0 )
				return null;

			// Extract the top name and look for it.

			string parentName;
			string relativeName;
			CatalogueName.SplitRootName(fullName, out parentName, out relativeName);

			// Ensure the top namespace exists.

			Namespace ns = Namespaces[parentName];
			if ( ns == null )
			{
				// Not there so create one.

				ns = CreateNamespace(this, parentName);
				Namespaces.Add(ns);
			}

			// Keep going.

			return ns.EnsureSourceParent(relativeName);
		}

		#endregion

		#region Create

		// EventType

		public EventType CreateEventType(Catalogue catalogue, string name, bool isEnabled)
		{
			return new EventType(catalogue, _interner.Intern(name), isEnabled);
		}

		public EventType CreateEventType(Catalogue catalogue, string name)
		{
			return new EventType(catalogue, _interner.Intern(name));
		}

		// Namespace

		public Namespace CreateNamespace(INamespaceParent parent, string name, string[] enabledEvents, string[] mixedEvents)
		{
			return new Namespace(parent, _interner.Intern(name), enabledEvents, mixedEvents, _interner);
		}

		public Namespace CreateNamespace(INamespaceParent parent, string name)
		{
			return new Namespace(parent, _interner.Intern(name));
		}

		// Source

		public Source CreateSource(ISourceParent parent, string name, string[] enabledEvents)
		{
			return new Source(parent, _interner.Intern(name), enabledEvents, _interner);
		}

		public Source CreateSource(ISourceParent parent, string name)
		{
			return new Source(parent, _interner.Intern(name));
		}

		#endregion

		private readonly ICatalogueConnection _catalogueConnection;
		private readonly Interner _interner = new Interner();
	}
}
