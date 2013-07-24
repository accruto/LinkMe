using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace LinkMe.Framework.VisualStudio.Data
{
	[Guid(Constants.Guids.ProjectFactory)]
	public class DataProjectFactory
		:	ProjectFactory
	{
        public DataProjectFactory(DataProjectPackage package)
            :	base(package)
        {
        }

		protected override ProjectNode CreateProject()
		{
			DataProjectNode project = new DataProjectNode(Package as DataProjectPackage);
			project.SetSite((IOleServiceProvider) ((System.IServiceProvider) Package).GetService(typeof(IOleServiceProvider)));
			return project;
		}
	}
}
