using LinkMe.Framework.Configuration;

namespace LinkMe.Framework.Instrumentation.Management.Connection
{
	internal class DefaultCatalogueConnection
		:	ICatalogueConnection
	{
		public virtual CommitStatus Commit(Catalogue catalogue)
		{
			return CommitStatus.Discontinue;
		}

		// Namespace

		public virtual Namespace[] GetNamespaces(INamespaceParent parent)
		{
			return new Namespace[0];
		}

		public virtual CommitStatus Create(Namespace ns)
		{
			return CommitStatus.Discontinue;
		}

		public virtual CommitStatus Delete(Namespace ns)
		{
			return CommitStatus.Discontinue;
		}

		public virtual CommitStatus Update(Namespace ns)
		{
			return CommitStatus.Discontinue;
		}

		// Sources

		public virtual Source[] GetSources(ISourceParent parent)
		{
			return new Source[0];
		}

		public virtual CommitStatus Create(Source source)
		{
			return CommitStatus.Discontinue;
		}

		public virtual CommitStatus Delete(Source source)
		{
			return CommitStatus.Discontinue;
		}

		public virtual CommitStatus Update(Source source)
		{
			return CommitStatus.Discontinue;
		}

		// EventTypes

		public virtual EventType[] GetEventTypes(Catalogue catalogue)
		{
			return new EventType[0];
		}

        public virtual CommitStatus Create(EventType eventType)
		{
			return CommitStatus.Discontinue;
		}

        public virtual CommitStatus Delete(EventType eventType)
		{
			return CommitStatus.Discontinue;
		}

        public virtual CommitStatus Update(EventType eventType)
		{
			return CommitStatus.Discontinue;
		}
	}
}
