using System.Runtime.InteropServices;
using System.IO;
using LinkMe.Environment;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio
{
	[ComVisible(true)]
	public abstract class VsProjectSettingsPage
		: SettingsPage
	{
		public VsProjectSettingsPage()
		{
			Name = StringResourceManager.GetString(typeof(Resources.StringResources), Constants.Project.Property.Category.General);
		}

		public override string GetClassName()
		{
			return GetType().FullName;
		}

		protected override void BindProperties()
		{
		}

		protected override int ApplyChanges()
		{
			return VSConstants.S_OK;
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.ProjectFile.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.ProjectFile.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.ProjectFile.Description)]
		public string ProjectFile
		{
			get { return Path.GetFileName(ProjectMgr.ProjectFile); }
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.ProjectFolder.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.ProjectFolder.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.ProjectFolder.Description)]
		public string ProjectFolder
		{
			get { return Path.GetDirectoryName(ProjectMgr.ProjectFolder); }
		}
	}
}
