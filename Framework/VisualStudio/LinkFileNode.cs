using System.Globalization;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace LinkMe.Framework.VisualStudio
{
	public abstract class LinkFileNode
		: FileNode
	{
		public LinkFileNode(ProjectNode root, ProjectElement e)
			: base(root, e)
		{
		}

		public override string GetEditLabel()
		{
			// Instead of responding to a query for the node status this seems to be the way to indicate that renaming is not supported.

			return null;
		}

		protected override bool CanDeleteItem(__VSDELETEITEMOPERATION deleteOperation)
		{
			// The file can be removed from the project.

			return deleteOperation == __VSDELETEITEMOPERATION.DELITEMOP_RemoveFromProject;
		}

		protected internal override bool IsFileOnDisk(bool showMessage)
		{
			bool fileExist = IsFileOnDisk(GetMkDocument());

			if ( !fileExist && showMessage && !Utilities.IsInAutomationFunction(ProjectMgr.Site) )
			{
				string message = string.Format(CultureInfo.CurrentCulture, SR.GetString(SR.ItemDoesNotExistInProjectDirectory), this.Caption);
				string title = string.Empty;
				OLEMSGICON icon = OLEMSGICON.OLEMSGICON_CRITICAL;
				OLEMSGBUTTON buttons = OLEMSGBUTTON.OLEMSGBUTTON_OK;
				OLEMSGDEFBUTTON defaultButton = OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST;
				VsShellUtilities.ShowMessageBox(ProjectMgr.Site, title, message, icon, buttons, defaultButton);
			}

			return fileExist;
		}
	}
}
