using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Package;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	[Guid(Constants.Guids.ProjectFactory)]
	public class ConfigurationProjectFactory
		:ProjectFactory
	{
		public ConfigurationProjectFactory(ConfigurationProjectPackage package)
			: base(package)
		{
		}

		protected override ProjectNode CreateProject()
		{
			ConfigurationProjectNode project = new ConfigurationProjectNode(Package as ConfigurationProjectPackage);
			project.SetSite((IOleServiceProvider) ((System.IServiceProvider) Package).GetService(typeof(IOleServiceProvider)));
			return project;
		}
	}
}
