using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;

namespace LinkMe.Framework.VisualStudio.Package
{
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[DefaultRegistryRoot(Constants.Registry.VisualStudio.RootKeyPath)]
	[InstalledProductRegistration(false, "#110", "#112", "1.0", IconResourceID = 400)]
	[ProvideLoadKey("Standard", "1.0", "LinkMe Module Project", "LinkMe", 1)]
	[Guid(Constants.Guids.Package)]
	[ProvideProject(typeof(PackageProjectFactory), Constants.Project.DisplayName, Constants.Project.DisplayProjectFileExtensions, Constants.Project.ProjectFileExtension, Constants.Project.ProjectFileExtension, Constants.Folder.ProjectTemplates)]
	[ProvideObject(typeof(ProjectSettingsPage))]
	[ProvideObject(typeof(ConfigurationSettingsPage))]
	public sealed class PackageProjectPackage
		: ProjectPackage
	{

		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public PackageProjectPackage()
		{
		}



		/////////////////////////////////////////////////////////////////////////////
		// Overriden Package Implementation
		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initilaization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

			// Register the project factory.

			RegisterProjectFactory(new PackageProjectFactory(this));
		}
		#endregion

	}
}