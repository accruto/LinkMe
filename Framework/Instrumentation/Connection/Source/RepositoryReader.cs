using LinkMe.Framework.Configuration.Exceptions;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	public abstract class RepositoryReader
		:	IRepositoryReader
	{
        private readonly string _initialisationString;

        protected RepositoryReader(string initialisationString)
        {
            _initialisationString = initialisationString;
        }

        protected string InitialisationString
        {
            get { return _initialisationString; }
        }

        protected abstract ICatalogueReader StartReading();

		protected virtual void StopReading()
		{
		}

        #region IRepositoryReader Members

		public Catalogue Read()
		{
			const string method = "Read";

			try
			{
                var catalogue = new Catalogue();
				ICatalogueReader reader = StartReading();

				try
				{
					reader.Read(catalogue);
				}
				finally
				{
					StopReading();
				}

				return catalogue;
			}
			catch ( System.Exception e )
			{
				throw new CannotConnectToRepositoryException(GetType(), method, Constants.Module.DisplayName, "XXX", _initialisationString, e);
			}
		}

		#endregion
	}
}
