using System.IO;

using Microsoft.VisualStudio.Package;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	internal class ConfigAssemblyReferenceNode
		:AssemblyReferenceNode
	{
		public ConfigAssemblyReferenceNode(ProjectNode root, string assemblyPath)
			: base(root, assemblyPath)
		{
		}

		protected override void BindReferenceData()
		{
			if ( ItemNode == null || ItemNode.Item == null )
				ItemNode = new ProjectElement(ProjectMgr, AssemblyName.Name, ProjectFileConstants.Reference);

			// Set the basic information we know about.

			ItemNode.SetMetadata(ProjectFileConstants.Name, AssemblyName.Name);
			ItemNode.SetMetadata(ProjectFileConstants.AssemblyName, Path.GetFileName(Url));
			ItemNode.SetMetadata(ProjectFileConstants.HintPath, FileSystem.GetRelativePath(Url, Path.GetDirectoryName(ProjectMgr.Url)));
		}
	}
}
