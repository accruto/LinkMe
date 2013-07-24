using System.Globalization;
using LinkMe.Environment;
using Microsoft.VisualStudio.Shell;

namespace LinkMe.Framework.VisualStudio
{
	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class ProvideProjectAttribute
		: RegistrationAttribute
	{
		private System.Type m_factoryType;
		private string m_displayName;
		private string m_displayProjectFileExtensions;
		private string m_defaultProjectExtension;
		private string m_possibleProjectExtensions;
		private string m_projectTemplatesFolder;
		private string m_languageVsTemplate;
		private string m_templateGroupIDsVsTemplate;
		private string m_templateIDsVsTemplate;
		private string m_displayProjectTypeVsTemplate;
		private string m_projectSubTypeVsTemplate;
		private bool m_newProjectRequireNewFolderVsTemplate = false;
		private bool m_showOnlySpecifiedTemplatesVsTemplate = false;

		public ProvideProjectAttribute(System.Type factoryType, string displayName, string displayProjectFileExtensionsResourceID, string defaultProjectExtension, string possibleProjectExtensions, string projectTemplatesFolder)
		{
			if ( factoryType == null )
				throw new System.ArgumentNullException("factoryType");

			m_factoryType = factoryType;
			m_displayName = displayName;
			m_displayProjectFileExtensions = displayProjectFileExtensionsResourceID;
			m_defaultProjectExtension = defaultProjectExtension;
			m_possibleProjectExtensions = possibleProjectExtensions;
			m_projectTemplatesFolder = projectTemplatesFolder;
		}

		public string DisplayName
		{
			get { return m_displayName; }
		}

		public System.Type FactoryType
		{
			get { return m_factoryType; }
		}

		public string DisplayProjectFileExtensions
		{
			get { return m_displayProjectFileExtensions; }
		}

		public string DefaultProjectExtension
		{
			get { return m_defaultProjectExtension; }
		}

		public string PossibleProjectExtensions
		{
			get { return m_possibleProjectExtensions; }
		}

		public string ProjectTemplatesFolder
		{
			get { return m_projectTemplatesFolder; }
		}

		public string LanguageVsTemplate
		{
			get { return m_languageVsTemplate; }
			set { m_languageVsTemplate = value; }
		}

		public string DisplayProjectTypeVsTemplate
		{
			get { return m_displayProjectTypeVsTemplate; }
			set { m_displayProjectTypeVsTemplate = value; }
		}

		public string ProjectSubTypeVsTemplate
		{
			get { return m_projectSubTypeVsTemplate; }
			set { m_projectSubTypeVsTemplate = value; }
		}

		public bool NewProjectRequireNewFolderVsTemplate
		{
			get { return m_newProjectRequireNewFolderVsTemplate; }
			set { m_newProjectRequireNewFolderVsTemplate = value; }
		}

		public bool ShowOnlySpecifiedTemplatesVsTemplate
		{
			get { return m_showOnlySpecifiedTemplatesVsTemplate; }
			set { m_showOnlySpecifiedTemplatesVsTemplate = value; }
		}

		public string TemplateGroupIDsVsTemplate
		{
			get { return m_templateGroupIDsVsTemplate; }
			set { m_templateGroupIDsVsTemplate = value; }
		}

		public string TemplateIDsVsTemplate
		{
			get { return m_templateIDsVsTemplate; }
			set { m_templateIDsVsTemplate = value; }
		}

		private string GetRegistryKey()
		{
			return string.Format(CultureInfo.InvariantCulture, Constants.Registry.VisualStudio.Projects.KeyPathFormat , FactoryType.GUID.ToString("B"));
		}

		public override void Register(RegistrationContext context)
		{
			// Make the folder absolute.

			if ( !string.IsNullOrEmpty(m_projectTemplatesFolder) )
				m_projectTemplatesFolder = FilePath.GetAbsolutePath(m_projectTemplatesFolder, context.ComponentPath);

			using ( Key key = context.CreateKey(GetRegistryKey()) )
			{
				key.SetValue(string.Empty, FactoryType.Name);
				if ( !string.IsNullOrEmpty(m_displayName) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.DisplayName, m_displayName);
				if ( !string.IsNullOrEmpty(m_displayProjectFileExtensions) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.DisplayProjectFileExtensions, m_displayProjectFileExtensions);
				key.SetValue(Constants.Registry.VisualStudio.Projects.Package, context.ComponentType.GUID.ToString("B"));
				if ( !string.IsNullOrEmpty(m_defaultProjectExtension) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.DefaultProjectExtension, m_defaultProjectExtension);
				if ( !string.IsNullOrEmpty(m_possibleProjectExtensions) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.PossibleProjectExtensions, m_possibleProjectExtensions);
				if ( !string.IsNullOrEmpty(m_projectTemplatesFolder) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.ProjectTemplatesDir, m_projectTemplatesFolder);

				// VsTemplate specific values.

				if ( !string.IsNullOrEmpty(m_languageVsTemplate) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.LanguageVsTemplate, m_languageVsTemplate);
				if ( m_showOnlySpecifiedTemplatesVsTemplate )
					key.SetValue(Constants.Registry.VisualStudio.Projects.ShowOnlySpecifiedTemplatesVsTemplate, (int) 1);
				if ( !string.IsNullOrEmpty(m_templateGroupIDsVsTemplate) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.TemplateGroupIDsVsTemplate, m_templateGroupIDsVsTemplate);
				if ( !string.IsNullOrEmpty(m_templateIDsVsTemplate) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.TemplateIDsVsTemplate, m_templateIDsVsTemplate);
				if ( !string.IsNullOrEmpty(m_displayProjectTypeVsTemplate) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.DisplayProjectTypeVsTemplate, m_displayProjectTypeVsTemplate);
				if ( !string.IsNullOrEmpty(m_projectSubTypeVsTemplate) )
					key.SetValue(Constants.Registry.VisualStudio.Projects.ProjectSubTypeVsTemplate, m_projectSubTypeVsTemplate);
				if ( m_newProjectRequireNewFolderVsTemplate )
					key.SetValue(Constants.Registry.VisualStudio.Projects.NewProjectRequireNewFolderVsTemplate, (int) 1);
			}
		}

		public override void Unregister(RegistrationContext context)
		{
			context.RemoveKey(GetRegistryKey());
		}
	}
}

