using System.Runtime.InteropServices;
using LinkMe.Environment;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;

namespace LinkMe.Framework.VisualStudio.Data
{
	[ComVisible(true), Guid(Constants.Guids.ConfigurationSettingsPage)]
	public class ConfigurationSettingsPage
		:	SettingsPage
	{
		public ConfigurationSettingsPage()
		{
			Name = StringResourceManager.GetString(typeof(Resources.StringResources), Constants.Project.Property.Category.Merge);
		}

		public override string GetClassName()
		{
			return GetType().FullName;
		}

		protected override void BindProperties()
		{
			m_outputPath = GetConfigProperty(Constants.Project.Property.OutputPath.Name);
		}

		protected override int ApplyChanges()
		{
			SetConfigProperty(Constants.Project.Property.OutputPath.Name, m_outputPath);
			IsDirty = false;
			return VSConstants.S_OK;
		}

		[ResourceCategory(typeof(Resources.StringResources), Constants.Project.Property.OutputPath.Category)]
		[ResourceDisplayName(typeof(Resources.StringResources), Constants.Project.Property.OutputPath.DisplayName)]
		[ResourceDescription(typeof(Resources.StringResources), Constants.Project.Property.OutputPath.Description)]
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
