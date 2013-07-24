using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;

namespace LinkMe.Framework.VisualStudio
{
	public abstract class LinkFolderNode
		: FolderNode
	{
		public LinkFolderNode(ProjectNode root, string directoryPath, ProjectElement e)
			: base(root, directoryPath, e)
		{
			ExcludeNodeFromScc = true;
		}

		#region Status

		protected override bool CanDeleteItem(__VSDELETEITEMOPERATION deleteOperation)
		{
			// The folder can be removed from the project.

			return deleteOperation == __VSDELETEITEMOPERATION.DELITEMOP_RemoveFromProject;
		}

		#endregion

		public override void CreateDirectory()
		{
			// Do not create the folder itself in the file system.
		}

		public override void DeleteFolder(string url)
		{
			// Nothing to delete.
		}
	}
}
