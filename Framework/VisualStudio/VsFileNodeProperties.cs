using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio
{
	[System.CLSCompliant(false), ComVisible(true)]
	public abstract class VsFileNodeProperties
		: NodeProperties
	{
		public VsFileNodeProperties(HierarchyNode node)
			: base(node)
		{
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.FileName.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.FileName.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.FileName.Description)]
		public string FileName
		{
			get { return Node.Caption; }
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.FullPath.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.FullPath.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.FullPath.Description)]
		public string FullPath
		{
			get
			{
				// Convert the path into an absolute path.

				return FilePath.GetAbsolutePath(Node.ItemNode.Item.Include, Node.ProjectMgr.BaseURI.Directory);
			}
		}

		[Browsable(false)]
		public string Extension
		{
			get { return Path.GetExtension(Node.Caption); }
		}
	}
}
