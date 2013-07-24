using System.IO;
using System.Runtime.InteropServices;
using LinkMe.Environment.Build.Tasks.Assemble;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;

namespace LinkMe.Framework.VisualStudio.Assemble
{
	[Guid(Constants.Guids.Project)]
	internal class AssembleProjectNode
		:	LinkProjectNode
	{
		private AssembleProjectPackage m_package;

		public AssembleProjectNode(AssembleProjectPackage package)
		{
			m_package = package;
			CanProjectDeleteItems = true;
		}

		public override System.Guid ProjectGuid
		{
			get { return typeof(AssembleProjectFactory).GUID; }
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
					case VsCommands.SetStartupProject:

						// Project does not support new items or setting as the start up project.

						result |= QueryStatusResult.NOTSUPPORTED;
						return VSConstants.S_OK;

				}
			}
			else if (guidCmdGroup == VsMenus.guidStandardCommandSet2K)
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
			return new AssembleFolderNode(this, path, item);
		}

		public override FileNode CreateFileNode(ProjectElement item)
		{
			return new AssembleFileNode(this, item);
		}

		protected override bool IsItemTypeFileType(string type)
		{
			return string.Compare(type, BuildAction.Assemble.ToString(), System.StringComparison.OrdinalIgnoreCase) == 0
				|| string.Compare(type, BuildAction.CopyOnBuild.ToString(), System.StringComparison.OrdinalIgnoreCase) == 0
				|| string.Compare(type, BuildAction.None.ToString(), System.StringComparison.OrdinalIgnoreCase) == 0;
		}

		#endregion

		#region Files

		protected override string GetMsBuildItemPath(string file)
		{
			string itemPath = base.GetMsBuildItemPath(file);

			// Adjust for the current configuration.

			string configuration = GetProjectProperty("Configuration");
			return AssembleFile.ConvertFromAdjustedPath(itemPath, configuration);
		}

		protected override void UpdateNewFileNode(HierarchyNode node, string projectPath)
		{
			base.UpdateNewFileNode(node, projectPath);

			// Update the properties of the build element.

			ProjectElement element = node.ItemNode;
			element.ItemName = Constants.Project.Item.Assemble.ItemType;
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

		protected override void InitializeProjectProperties()
		{
			base.InitializeProjectProperties();

			// Add a default catalogue file.

			if ( string.IsNullOrEmpty(GetProjectProperty(Constants.Project.Property.CatalogueFile.Name)) )
			{
				string projectName = Path.GetFileNameWithoutExtension(FileName);
				if ( !string.IsNullOrEmpty(projectName) )
					SetProjectProperty(Constants.Project.Property.CatalogueFile.Name, projectName + Constants.File.Catalogue.Extension);
			}
		}

		protected override string[] GetProjectTargetsFiles()
		{
			return new string[] { Constants.Project.TargetsFile };
		}
	}
}
