using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;

namespace LinkMe.Framework.VisualStudio.Package
{
	[Guid(Constants.Guids.Project)]
	internal class PackageProjectNode
		: LinkProjectNode
	{
		private PackageProjectPackage m_package;

		public PackageProjectNode(PackageProjectPackage package)
		{
			m_package = package;
			CanProjectDeleteItems = true;
		}

		public override System.Guid ProjectGuid
		{
			get { return typeof(PackageProjectFactory).GUID; }
		}

		public override string ProjectType
		{
			get { return Constants.Project.Type; }
		}

		#region Status

		protected override QueryStatusResult QueryStatusCommandFromOleCommandTarget(System.Guid guidCmdGroup, uint cmd, out bool handled)
		{
			if ( guidCmdGroup == VsMenus.guidStandardCommandSet2K )
			{
				switch ( (VsCommands2K) cmd )
				{
					case VsCommands2K.ADDREFERENCE:

						// Project does not support references.

						handled = true;
						return QueryStatusResult.NOTSUPPORTED;
				}
			}

			return base.QueryStatusCommandFromOleCommandTarget(guidCmdGroup, cmd, out handled);
		}

		protected override int QueryStatusOnNode(System.Guid guidCmdGroup, uint cmd, System.IntPtr pCmdText, ref QueryStatusResult result)
		{
			if ( guidCmdGroup == VsMenus.guidStandardCommandSet97 )
			{
				switch ( (VsCommands) cmd )
				{
					case VsCommands.AddNewItem:
					case VsCommands.NewFolder:
					case VsCommands.SetStartupProject:

						// Project does not support new items/folders or setting as the start up project.

						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;

				}
			}
			else if ( guidCmdGroup == VsMenus.guidStandardCommandSet2K )
			{
				switch ( (VsCommands2K) cmd )
				{
					case VsCommands2K.ADDREFERENCE:

						// Project does not support references.

						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;
				}
			}

			return base.QueryStatusOnNode(guidCmdGroup, cmd, pCmdText, ref result);
		}

		#endregion

		#region Nodes

		protected override ReferenceContainerNode CreateReferenceContainerNode()
		{
			// This project does not support references.

			return null;
		}

		protected override FolderNode CreateFolderNode(string path, ProjectElement item)
		{
			// This project does not support folder nodes.

			return null;
		}

		public override FileNode CreateFileNode(ProjectElement item)
		{
			return new PackageFileNode(this, item);
		}

		protected override bool IsItemTypeFileType(string type)
		{
			return string.Compare(type, BuildAction.Catalogue.ToString(), System.StringComparison.OrdinalIgnoreCase) == 0
				|| string.Compare(type, BuildAction.None.ToString(), System.StringComparison.OrdinalIgnoreCase) == 0;
		}

		#endregion

		#region Files

		protected override void UpdateNewFileNode(HierarchyNode node, string projectPath)
		{
			base.UpdateNewFileNode(node, projectPath);

			// Update the properties of the build element.

			ProjectElement element = node.ItemNode;
			element.ItemName = Constants.Project.Item.Module.ItemType;
		}

		#endregion

		protected override System.Guid[] GetConfigurationIndependentPropertyPages()
		{
			return new System.Guid[] { typeof(ProjectSettingsPage).GUID };
		}

		protected override System.Guid[] GetConfigurationDependentPropertyPages()
		{
			if ( SupportsProjectDesigner )
				return GetConfigurationIndependentPropertyPages();

			return new System.Guid[] { typeof(ConfigurationSettingsPage).GUID };
		}

		protected override string[] GetProjectTargetsFiles()
		{
			return new string[] { Constants.Project.TargetsFile };
		}

		protected override void InitializeProjectProperties()
		{
			base.InitializeProjectProperties();

			// Add a new module guid.

			if ( string.IsNullOrEmpty(GetProjectProperty(Constants.Project.Property.ModuleGuid.Name)) )
				SetProjectProperty(Constants.Project.Property.ModuleGuid.Name, System.Guid.NewGuid().ToString("B"));
		}
	}
}
