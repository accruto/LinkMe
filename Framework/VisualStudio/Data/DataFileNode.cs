using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;
using ProjectElement = Microsoft.VisualStudio.Project.ProjectElement;
using ProjectNode=Microsoft.VisualStudio.Project.ProjectNode;

namespace LinkMe.Framework.VisualStudio.Data
{
	internal class DataFileNode
		:	LinkFileNode
	{
		public DataFileNode(ProjectNode root, ProjectElement e)
			:	base(root, e)
		{
			ExcludeNodeFromScc = true;
		}

		protected override NodeProperties CreatePropertiesObject()
		{
			return new DataFileNodeProperties(this);
		}

		public override string GetMkDocument()
		{
			return ((DataFileNodeProperties) NodeProperties).FullPath;
		}

		#region Status

		protected override int QueryStatusOnNode(System.Guid guidCmdGroup, uint cmd, System.IntPtr pCmdText, ref QueryStatusResult result)
		{
			if ( guidCmdGroup == VsMenus.guidStandardCommandSet97 )
			{
				switch ( (VsCommands) cmd )
				{
					// Cut, Copy and ViewCode are not supported.

					case VsCommands.Cut:
					case VsCommands.Copy:
					case VsCommands.ViewCode:
						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;

					case VsCommands.Rename:
					case VsCommands.RenameBookmark:

						// This never seems to be called but still include it, see GetEditLabel.

						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;
				}
			}
			else if ( guidCmdGroup == VsMenus.guidStandardCommandSet2K )
			{
				switch ( (VsCommands2K) cmd )
				{
					// Run custom tool not supported.

					case VsCommands2K.RUNCUSTOMTOOL:
						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;
				}
			}

			return base.QueryStatusOnNode(guidCmdGroup, cmd, pCmdText, ref result);
		}

		#endregion
	}
}
