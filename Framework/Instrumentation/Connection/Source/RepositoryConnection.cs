using LinkMe.Framework.Configuration.Exceptions;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	public abstract class RepositoryConnection
		:	IRepositoryConnection
	{
        private readonly string _initialisationString;
        private Catalogue _catalogue;

	    protected RepositoryConnection(string initialisationString)
        {
            _initialisationString = initialisationString;
        }

        protected string InitialisationString
        {
            get { return _initialisationString; }
        }

        protected abstract ICatalogueConnection CreateCatalogueConnection();

        #region IRepositoryConnection Members

		public Catalogue Connect()
		{
			const string method = "Connect";

			try
			{
				return GetCatalogue();
			}
			catch ( System.Exception e )
			{
				throw new CannotConnectToRepositoryException(GetType(), method, Constants.Module.DisplayName, "XXX", _initialisationString, e);
			}
		}

		#endregion

		#region IRepositoryWriter Members

		void IRepositoryWriter.Write(Catalogue catalogue)
		{
			CatalogueCopier.Copy(GetCatalogue(), catalogue, ReadOnlyOption.Clear, false);
		}

		void IRepositoryWriter.Write(Namespace ns, bool iterate)
		{
			CatalogueCopier.Copy(GetCatalogue(), ns, ReadOnlyOption.Clear, false, iterate);
		}

		void IRepositoryWriter.Write(Source source)
		{
			CatalogueCopier.Copy(GetCatalogue(), source, ReadOnlyOption.Clear, false);
		}

		void IRepositoryWriter.Write(EventType eventType)
		{
			CatalogueCopier.Copy(GetCatalogue(), eventType, ReadOnlyOption.Clear, false);
		}

		void IRepositoryWriter.Close()
		{
			// Commit all changes that have been made.

			GetCatalogue().Commit();
		}

		#endregion

		#region IRepositoryReader Members

		public Catalogue Read()
		{
			const string method = "Read";

			try
			{
				return GetCatalogue();
			}
			catch ( System.Exception e )
			{
				throw new CannotConnectToRepositoryException(GetType(), method, Constants.Module.DisplayName, "XXX", _initialisationString, e);
			}
		}

		#endregion

		private Catalogue GetCatalogue()
		{
			if ( _catalogue == null )
			{
                _catalogue = new Catalogue(CreateCatalogueConnection());
                PrepareCatalogue();
			}

			return _catalogue;
		}

        protected virtual void PrepareCatalogue(Catalogue catalogue)
        {
        }

        private void PrepareCatalogue()
        {
            const string method = "PrepareCatalogue";

            try
            {
                PrepareCatalogue(_catalogue);
            }
            catch (System.Exception e)
            {
                throw new CannotConnectToRepositoryException(GetType(), method, Constants.Module.DisplayName, "XXX", _initialisationString, e);
            }
        }
	}
}
