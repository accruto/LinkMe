using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;

namespace LinkMe.Framework.Configuration.VisualStudio
{
	[ComVisible(true), Guid(Constants.Guids.ConfigurationSettingsPage)]
	public class ConfigurationSettingsPage
		: SettingsPage
	{
		public ConfigurationSettingsPage()
		{
			Name = "Configuration";
		}

		public override string GetClassName()
		{
			return GetType().FullName;
		}

		protected override void BindProperties()
		{
			m_outputPath = GetConfigProperty("OutputPath");
		}

		protected override int ApplyChanges()
		{
			SetConfigProperty("OutputPath", m_outputPath);
			IsDirty = false;
			return VSConstants.S_OK;
		}

		[SRCategoryAttribute(SR.Project)]
		[DisplayName("Output Path")]
		[SRDescriptionAttribute("Output Path")]
		public string OutputPath
		{
			get
			{
				return m_outputPath;
			}
			set
			{
				m_outputPath = value;
				IsDirty = true;
			}
		}

		private string m_outputPath;
	}
}
