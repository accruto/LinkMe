using System.IO;
using LinkMe.Environment.Build.Tasks.Assemble;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;
using ProjectElement = Microsoft.VisualStudio.Project.ProjectElement;
using ProjectNode=Microsoft.VisualStudio.Project.ProjectNode;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	internal class AssembleFileNode
		:	LinkFileNode
	{
		public AssembleFileNode(ProjectNode root, ProjectElement e)
			:	base(root, e)
		{
			ExcludeNodeFromScc = true;

			// Give it a guid if it does not already have one.

			if ( string.IsNullOrEmpty(ItemNode.GetMetadata(Constants.Project.Item.Assemble.Guid.Name)) )
				ItemNode.SetMetadata(Constants.Project.Item.Assemble.Guid.Name, System.Guid.NewGuid().ToString("B"));
		}

		protected override NodeProperties CreatePropertiesObject()
		{
			string fileName = FileName;

			// Create node based on the type of the file.

			if ( !string.IsNullOrEmpty(fileName) )
			{
				switch ( Path.GetExtension(fileName) )
				{
					case ".dll":
						return new DllFileNodeProperties(this);

					case ".exe":
					case ".msc":
						return new ExeFileNodeProperties(this);

					default:
						return new AssembleFileNodeProperties(this);
				}
			}

			return new AssembleFileNodeProperties(this);
		}

		public override string GetMkDocument()
		{
			string configuration = ProjectMgr.GetProjectProperty(Constants.Project.Configuration);
			return AssembleFile.ConvertToAdjustedPath(((AssembleFileNodeProperties) NodeProperties).FullPath, configuration);
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
