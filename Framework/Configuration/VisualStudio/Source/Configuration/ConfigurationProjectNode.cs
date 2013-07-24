using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

using VsMenus = Microsoft.VisualStudio.Package.VsMenus;
using OleConstants = Microsoft.VisualStudio.OLE.Interop.Constants;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	[Guid(Constants.Guids.Project)]
	public class ConfigurationProjectNode
		:	ProjectNode
	{
		private ConfigurationProjectPackage m_package;

		public ConfigurationProjectNode(ConfigurationProjectPackage package)
		{
			m_package = package;
		}

		public override System.Guid ProjectGuid
		{
			get { return typeof(ConfigurationProjectFactory).GUID; }
		}

		public override string ProjectType
		{
			get { return "ConfigurationProject"; }
		}

		public override bool IsCodeFile(string fileName)
		{
			return string.Compare(Path.GetExtension(fileName), ".acs", System.StringComparison.OrdinalIgnoreCase) == 0;
		}

		protected override ReferenceContainerNode CreateReferenceContainerNode()
		{
			return new ConfigurationReferenceContainerNode(this);
		}

		internal protected override FolderNode CreateFolderNode(string path, ProjectElement item)
		{
			return new ConfigurationFolderNode(this, path, item);
		}

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
	}
}
