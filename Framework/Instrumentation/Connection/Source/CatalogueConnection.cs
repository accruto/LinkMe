using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	public abstract class CatalogueConnection
		:	ICatalogueConnection
	{
		public virtual CommitStatus Commit(Catalogue catalogue)
		{
			return CommitStatus.Continue;
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

		// Events

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
