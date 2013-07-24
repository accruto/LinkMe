using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio;

namespace LinkMe.Framework.VisualStudio.Package
{
	[ComVisible(true), Guid(Constants.Guids.ProjectSettingsPage)]
	public class ProjectSettingsPage
		: VsProjectSettingsPage
	{
		private System.Version m_version;
		private string m_manufacturer;
		private string m_mergeModuleFile;

		public ProjectSettingsPage()
		{
		}

		protected override void BindProperties()
		{
			if ( ProjectMgr == null )
				return;

			m_manufacturer = ProjectMgr.GetProjectProperty(Constants.Project.Property.Manufacturer.Name, false);
			m_mergeModuleFile = ProjectMgr.GetProjectProperty(Constants.Project.Property.MergeModuleFile.Name, false);
			try
			{
				m_version = new System.Version(ProjectMgr.GetProjectProperty(Constants.Project.Property.Version.Name, false));
			}
			catch ( System.Exception )
			{
				m_version = null;
			}
		}

		protected override int ApplyChanges()
		{
			if ( ProjectMgr == null )
				return VSConstants.E_INVALIDARG;

			ProjectMgr.SetProjectProperty(Constants.Project.Property.Manufacturer.Name, m_manufacturer);
			ProjectMgr.SetProjectProperty(Constants.Project.Property.MergeModuleFile.Name, m_mergeModuleFile);
			ProjectMgr.SetProjectProperty(Constants.Project.Property.Version.Name, m_version == null ? null : m_version.ToString());
			IsDirty = false;
			return VSConstants.S_OK;
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.Manufacturer.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.Manufacturer.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.Manufacturer.Description)]
		public string Manufacturer
		{
			get
			{
				return m_manufacturer;
			}
			set
			{
				m_manufacturer = value;
				IsDirty = true;
			}
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.MergeModuleFile.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.MergeModuleFile.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.MergeModuleFile.Description)]
		public string MergeModuleFile
		{
			get
			{
				return m_mergeModuleFile;
			}
			set
			{
				m_mergeModuleFile = value;
				IsDirty = true;
			}
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.Version.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.Version.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.Version.Description)]
		public string Version
		{
			get
			{
				return m_version == null ? null : m_version.ToString();
			}
			set
			{
				try
				{
					m_version = new System.Version(value);
				}
				catch ( System.Exception )
				{
					m_version = null;
				}

				IsDirty = true;
			}
		}
	}
}
