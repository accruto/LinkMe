using System.Runtime.InteropServices;
using Microsoft.VisualStudio;

namespace LinkMe.Framework.VisualStudio.Data
{
	[ComVisible(true), Guid(Constants.Guids.ProjectSettingsPage)]
	public class ProjectSettingsPage
		:	VsProjectSettingsPage
	{
		public ProjectSettingsPage()
		{
		}

		protected override void BindProperties()
		{
			if ( ProjectMgr == null )
				return;
		}

		protected override int ApplyChanges()
		{
			if ( ProjectMgr == null )
				return VSConstants.E_INVALIDARG;

			IsDirty = false;
			return VSConstants.S_OK;
		}
	}
}
