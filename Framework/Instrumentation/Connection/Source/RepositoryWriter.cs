using System.Collections;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	public abstract class RepositoryWriter
		:	IRepositoryWriter
	{
        private readonly string _initialisationString;
        private SortedList _namespaces;
        private SortedList _sources;
	    private SortedList _events;

	    protected RepositoryWriter(string initialisationString)
		{
            _initialisationString = initialisationString;
            _namespaces = new SortedList();
            _sources = new SortedList();
            _events = new SortedList();
		}

        protected string InitialisationString
        {
            get { return _initialisationString; }
        }

        #region IRepositoryWriter Members

		void IRepositoryWriter.Write(Catalogue catalogue)
		{
			// Iterate.

			foreach ( EventType eventType in catalogue.EventTypes )
				Add(eventType);
			foreach ( Namespace ns in catalogue.Namespaces )
				Add(ns);
		}

		void IRepositoryWriter.Write(Namespace ns, bool iterate)
		{
			// Iterate.

			if ( iterate )
			{
				foreach ( Namespace childNamespace in ns.Namespaces )
					Add(childNamespace);
				foreach ( Source source in ns.Sources )
					Add(source);
			}
		}

		void IRepositoryWriter.Write(Source source)
		{
			Add(source);
		}

		void IRepositoryWriter.Write(EventType eventType)
		{
			Add(eventType);
		}

		void IRepositoryWriter.Close()
		{
			// Create the catalogue writer that will be written to.

			ICatalogueWriter writer = StartWriting();

			// Write out all elements.

			foreach ( Namespace ns in _namespaces.Values )
				writer.Write(ns);
			foreach ( Source source in _sources.Values )
				writer.Write(source);
			foreach ( EventType eventType in _events.Values )
				writer.Write(eventType);

			// Stop the writing so that everything can be written to the repository.

			StopWriting();

            _namespaces = new SortedList();
            _sources = new SortedList();
		    _events = new SortedList();
		}

		#endregion

		protected abstract ICatalogueWriter StartWriting();
		protected abstract void StopWriting();

		private void Add(Namespace ns)
		{
			// Add the namespace.

			_namespaces[ns.FullName] = ns;

			// Iterate.

			foreach ( Namespace childNamespace in ns.Namespaces )
				Add(childNamespace);
			foreach ( Source source in ns.Sources )
				Add(source);
		}

		private void Add(Source source)
		{
            _sources[source.FullyQualifiedReference] = source;
		}

		private void Add(EventType eventType)
		{
            _events[eventType.FullName] = eventType;
		}
	}
}
