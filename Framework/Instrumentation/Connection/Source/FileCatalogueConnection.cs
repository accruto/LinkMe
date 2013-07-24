using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection.Config
{
	internal abstract class FileCatalogueConnection
		:	CatalogueConnection
	{
		public override CommitStatus Commit(Catalogue catalogue)
		{
			Save(catalogue);
			return CommitStatus.Discontinue;
		}

		// Namespace

		public override CommitStatus Create(Namespace ns)
		{
			Save(ns.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Delete(Namespace ns)
		{
			Save(ns.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Update(Namespace ns)
		{
			Save(ns.Catalogue);
			return CommitStatus.Discontinue;
		}

		// Sources

		public override CommitStatus Create(Source source)
		{
			Save(source.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Delete(Source source)
		{
			Save(source.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Update(Source source)
		{
			Save(source.Catalogue);
			return CommitStatus.Discontinue;
		}

		// EventTypes

		public override CommitStatus Create(EventType eventType)
		{
			Save(eventType.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Delete(EventType eventType)
		{
			Save(eventType.Catalogue);
			return CommitStatus.Discontinue;
		}

		public override CommitStatus Update(EventType eventType)
		{
			Save(eventType.Catalogue);
			return CommitStatus.Discontinue;
		}

        private void Save(Catalogue catalogue)
        {
            // Create a writer.

            IRepositoryWriter writer = CreateWriter();

            // Write the catalogue and close the file.

            writer.Write(catalogue);
            writer.Close();
        }

        protected abstract IRepositoryWriter CreateWriter();
	}
}
