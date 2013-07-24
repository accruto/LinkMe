using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	[Guid(Constants.Guids.ProjectFactory)]
	public class AssembleProjectFactory
		:	ProjectFactory
	{
        public AssembleProjectFactory(AssembleProjectPackage package)
            :	base(package)
        {
        }

		protected override ProjectNode CreateProject()
		{
			AssembleProjectNode project = new AssembleProjectNode(Package as AssembleProjectPackage);
			project.SetSite((IOleServiceProvider) ((System.IServiceProvider) Package).GetService(typeof(IOleServiceProvider)));
			return project;
		}
	}
}
