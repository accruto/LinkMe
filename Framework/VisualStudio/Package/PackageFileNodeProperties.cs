using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio.Package
{
	[System.CLSCompliant(false), ComVisible(true)]
	public class PackageFileNodeProperties
		: VsFileNodeProperties
	{
		public PackageFileNodeProperties(HierarchyNode node)
			: base(node)
		{
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.BuildAction.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.BuildAction.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.BuildAction.Description)]
		public BuildAction BuildAction
		{
			get
			{
				string value = Node.ItemNode.ItemName;
				if ( value == null || value.Length == 0 )
					return BuildAction.None;
				return (BuildAction) System.Enum.Parse(typeof(BuildAction), value);
			}
			set
			{
				Node.ItemNode.ItemName = value.ToString();
			}
		}
	}
}
