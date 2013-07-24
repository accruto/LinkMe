using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	public abstract class CatalogueReader
		:	ICatalogueReader
	{
	    #region ICatalogueReader Members

		public virtual void Read(Catalogue catalogue)
		{
		}

		#endregion
	}
}
