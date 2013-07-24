using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection
{
	/// <summary>
	/// Summary description for CatalogueWriter.
	/// </summary>
	public abstract class CatalogueWriter
		:	ICatalogueWriter
	{
		#region ICatalogueWriter Members

		public virtual void Write(Namespace ns)
		{
		}

		public virtual void Write(Source source)
		{
		}

		public virtual void Write(EventType eventType)
		{
		}

		#endregion
	}
}
