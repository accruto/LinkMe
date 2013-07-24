using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace LinkMe.Framework.VisualStudio
{
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[DefaultRegistryRoot(Constants.Registry.VisualStudio.RootKeyPath)]
	// This attribute is used to register the informations needed to show the this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration(false, "#110", "#112", "1.0", IconResourceID = 400)]
	[ProvideLoadKey("Standard", "1.0", "LinkMe Project", "LinkMe", 1)]
	[Guid(Constants.Guids.TemplateFolders)]
	[ProvideProjectTemplateFolder(Constants.Project.ProjectTemplatesName, Constants.Folder.ProjectTemplates)]
	public sealed class ProjectTemplateFolderPackage
		: Package
	{
		public ProjectTemplateFolderPackage()
		{
		}
	}
}