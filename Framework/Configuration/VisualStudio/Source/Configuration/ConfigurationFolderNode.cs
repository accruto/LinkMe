using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	class ConfigurationFolderNode
		:	FolderNode
	{
		public ConfigurationFolderNode(ProjectNode root, string directoryPath, ProjectElement e)
			:	base(root, directoryPath, e)
		{
			ExcludeNodeFromScc = true;
		}
	}
}
