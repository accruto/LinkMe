using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using ProjectElement=Microsoft.VisualStudio.Project.ProjectElement;
using ProjectNode=Microsoft.VisualStudio.Project.ProjectNode;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;

namespace LinkMe.Framework.VisualStudio.Package
{
	internal class PackageFileNode
		: LinkFileNode
	{
		public PackageFileNode(ProjectNode root, ProjectElement e)
			: base(root, e)
		{
			ExcludeNodeFromScc = true;
		}

		protected override NodeProperties CreatePropertiesObject()
		{
			return new PackageFileNodeProperties(this);
		}

		public override string GetMkDocument()
		{
			return ((PackageFileNodeProperties) NodeProperties).FullPath;
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
