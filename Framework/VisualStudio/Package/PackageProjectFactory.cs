using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace LinkMe.Framework.VisualStudio.Package
{
	[Guid(Constants.Guids.ProjectFactory)]
	public class PackageProjectFactory
		: ProjectFactory
	{
		public PackageProjectFactory(PackageProjectPackage package)
			: base(package)
		{
		}

		protected override ProjectNode CreateProject()
		{
			PackageProjectNode project = new PackageProjectNode(Package as PackageProjectPackage);
			project.SetSite((IOleServiceProvider) ((System.IServiceProvider) Package).GetService(typeof(IOleServiceProvider)));
			return project;
		}
	}
}
