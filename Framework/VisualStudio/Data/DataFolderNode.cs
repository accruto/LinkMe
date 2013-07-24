using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;

namespace LinkMe.Framework.VisualStudio.Data
{
	internal class DataFolderNode
		: LinkFolderNode
	{
		public DataFolderNode(ProjectNode root, string directoryPath, ProjectElement e)
			: base(root, directoryPath, e)
		{
		}

		#region Status

		protected override int QueryStatusOnNode(System.Guid guidCmdGroup, uint cmd, System.IntPtr pCmdText, ref QueryStatusResult result)
		{
			if ( guidCmdGroup == VsMenus.guidStandardCommandSet97 )
			{
				switch ( (VsCommands) cmd )
				{
					case VsCommands.AddNewItem:

						// Project does not support new items.

						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;
				}
			}

			return base.QueryStatusOnNode(guidCmdGroup, cmd, pCmdText, ref result);
		}

		#endregion
	}
}
