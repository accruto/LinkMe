using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	[System.CLSCompliant(false), ComVisible(true)]
	public class AssembleFileNodeProperties
		:	VsFileNodeProperties
	{
		public AssembleFileNodeProperties(HierarchyNode node)
			:	base(node)
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

	[System.CLSCompliant(false), ComVisible(true)]
	public class DllFileNodeProperties
		:	AssembleFileNodeProperties
	{
		public DllFileNodeProperties(HierarchyNode node)
			:	base(node)
		{
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Item.Assemble.InstallInGac.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Item.Assemble.InstallInGac.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Item.Assemble.InstallInGac.Description)]
		public bool InstallInGac
		{
			get
			{
				string installInGac = GetProperty(Constants.Project.Item.Assemble.InstallInGac.Name, true.ToString());
				if ( installInGac == null || installInGac.Length == 0 )
					return false;
				return bool.Parse(installInGac);
			}
			set
			{
				SetProperty(Constants.Project.Item.Assemble.InstallInGac.Name, value.ToString());
			}
		}
	}

	[System.CLSCompliant(false), ComVisible(true)]
	public class ExeFileNodeProperties
		:	AssembleFileNodeProperties
	{
		public ExeFileNodeProperties(HierarchyNode node)
			: base(node)
		{
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutName.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutName.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutName.Description)]
		public string ShortcutName
		{
			get { return GetProperty(Constants.Project.Item.Assemble.ShortcutName.Name, string.Empty); }
			set { SetProperty(Constants.Project.Item.Assemble.ShortcutName.Name, value); }
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutPath.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutPath.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Item.Assemble.ShortcutPath.Description)]
		public string ShortcutPath
		{
			get { return GetProperty(Constants.Project.Item.Assemble.ShortcutPath.Name, string.Empty); }
			set { SetProperty(Constants.Project.Item.Assemble.ShortcutPath.Name, value); }
		}
	}
}
