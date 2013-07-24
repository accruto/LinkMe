using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	[ComVisible(true), Guid(Constants.Guids.ProjectSettingsPage)]
	public class ProjectSettingsPage
		:	VsProjectSettingsPage
	{
		private string m_catalogueFile;

		public ProjectSettingsPage()
		{
		}

		protected override void BindProperties()
		{
			if ( ProjectMgr == null )
				return;

			m_catalogueFile = ProjectMgr.GetProjectProperty(Constants.Project.Property.CatalogueFile.Name, false);
		}

		protected override int ApplyChanges()
		{
			if ( ProjectMgr == null )
				return VSConstants.E_INVALIDARG;

			ProjectMgr.SetProjectProperty(Constants.Project.Property.CatalogueFile.Name, m_catalogueFile);
			IsDirty = false;
			return VSConstants.S_OK;
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.CatalogueFile.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.CatalogueFile.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.CatalogueFile.Description)]
		public string CatalogueFile
		{
			get
			{
				return m_catalogueFile;
			}
			set
			{
				m_catalogueFile = value;
				IsDirty = true;
			}
		}
	}
}
