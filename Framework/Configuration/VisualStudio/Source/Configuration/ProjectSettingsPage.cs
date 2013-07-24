using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	[ComVisible(true), Guid(Constants.Guids.ProjectSettingsPage)]
	public class ProjectSettingsPage
		:	SettingsPage
	{
		private string m_outputName;
		private bool m_generateConfigurationFile;
		private bool m_generateAssembly;
		private string m_keyFile;
		private string m_version;

		public ProjectSettingsPage()
		{
			Name = "General";
		}

		public override string GetClassName()
		{
			return GetType().FullName;
		}

		protected override void BindProperties()
		{
			if ( ProjectMgr == null )
				return;

			m_outputName = ProjectMgr.GetProjectProperty("OutputName", true);
			string generateConfigurationFile = ProjectMgr.GetProjectProperty("GenerateConfigurationFile", true);
			m_generateConfigurationFile = generateConfigurationFile == null || generateConfigurationFile == string.Empty ? false : bool.Parse(generateConfigurationFile);
			string generateAssembly = ProjectMgr.GetProjectProperty("GenerateAssembly", true);
			m_generateAssembly = generateAssembly == null || generateAssembly == string.Empty ? false : bool.Parse(generateAssembly);
			string keyFile = ProjectMgr.GetProjectProperty("KeyFile", true);
			if ( !string.IsNullOrEmpty(keyFile) )
				m_keyFile = FileSystem.GetAbsolutePath(keyFile, ProjectMgr.BaseURI.Directory);
			m_version = ProjectMgr.GetProjectProperty("Version", true);
		}

		protected override int ApplyChanges()
		{
			if ( ProjectMgr == null )
				return VSConstants.E_INVALIDARG;

			ProjectMgr.SetProjectProperty("OutputName", m_outputName);
			ProjectMgr.SetProjectProperty("GenerateConfigurationFile", m_generateConfigurationFile ? "True" : "False");
			ProjectMgr.SetProjectProperty("GenerateAssembly", m_generateAssembly ? "True" : "False");
			string keyFile = null;
			if ( !string.IsNullOrEmpty(m_keyFile) )
				keyFile = FileSystem.GetRelativePath(m_keyFile, ProjectMgr.BaseURI.Directory);
			ProjectMgr.SetProjectProperty("KeyFile", keyFile);
			ProjectMgr.SetProjectProperty("Version", m_version);
			IsDirty = false;
			return VSConstants.S_OK;
		}

		[SRCategoryAttribute(SR.Project)]
		[LocDisplayName(SR.ProjectFile)]
		[SRDescriptionAttribute(SR.ProjectFileDescription)]
		public string ProjectFile
		{
			get { return Path.GetFileName(ProjectMgr.ProjectFile); }
		}

		[SRCategoryAttribute(SR.Project)]
		[LocDisplayName(SR.ProjectFolder)]
		[SRDescriptionAttribute(SR.ProjectFolderDescription)]
		public string ProjectFolder
		{
			get { return Path.GetDirectoryName(ProjectMgr.ProjectFolder); }
		}

		[CategoryAttribute("Configuration")]
		[DisplayName("Output Name")]
		[SRDescriptionAttribute("Output Name")]
		public string OutputName
		{
			get
			{
				return m_outputName;
			}
			set
			{
				m_outputName = value;
				IsDirty = true;
			}
		}

		[CategoryAttribute("Configuration")]
		[DisplayName("Generate Configuration File")]
		[SRDescriptionAttribute("Generate Configuration File")]
		public bool GenerateConfigurationFile
		{
			get
			{
				return m_generateConfigurationFile;
			}
			set
			{
				m_generateConfigurationFile = value;
				IsDirty = true;
			}
		}

		[CategoryAttribute("Configuration")]
		[DisplayName("Generate Assembly")]
		[SRDescriptionAttribute("Generate Assembly")]
		public bool GenerateAssembly
		{
			get
			{
				return m_generateAssembly;
			}
			set
			{
				m_generateAssembly = value;
				IsDirty = true;
			}
		}

		[CategoryAttribute("Configuration")]
		[DisplayName("Key File")]
		[SRDescriptionAttribute("Key File")]
		public string KeyFile
		{
			get
			{
				return m_keyFile;
			}
			set
			{
				m_keyFile = value;
				IsDirty = true;
			}
		}

		[CategoryAttribute("Configuration")]
		[DisplayName("Version")]
		[SRDescriptionAttribute("Version")]
		public string Version
		{
			get
			{
				return m_version;
			}
			set
			{
				m_version = value;
				IsDirty = true;
			}
		}
	}
}
