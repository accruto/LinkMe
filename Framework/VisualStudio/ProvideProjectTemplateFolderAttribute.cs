using System.Globalization;
using LinkMe.Environment;
using Microsoft.VisualStudio.Shell;

namespace LinkMe.Framework.VisualStudio
{
	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class ProvideProjectTemplateFolderAttribute
		: RegistrationAttribute
	{
		private string m_name;
		private string m_folder;
		private int m_sortPriority = 100;

		public ProvideProjectTemplateFolderAttribute(string name, string folder)
		{
			m_name = name;
			m_folder = folder;
		}

		public string Name
		{
			get { return m_name; }
		}

		public int SortPriority
		{
			get { return m_sortPriority; }
			set { m_sortPriority = value; }
		}

		public string Folder
		{
			get { return m_folder; }
		}

		private string GetRegistryKey(RegistrationContext context)
		{
			return string.Format(CultureInfo.InvariantCulture, Constants.Registry.VisualStudio.TemplateDirs.KeyPathFormat, context.ComponentType.GUID.ToString("B"));
		}

		public override void Register(RegistrationContext context)
		{
			// Make the folder absolute.

			if ( !string.IsNullOrEmpty(m_folder) )
				m_folder = FilePath.GetAbsolutePath(m_folder, context.ComponentPath);

			using ( Key key = context.CreateKey(GetRegistryKey(context)) )
			{
				if ( !string.IsNullOrEmpty(m_name) )
					key.SetValue(string.Empty, m_name);
				key.SetValue(Constants.Registry.VisualStudio.TemplateDirs.SortPriority, m_sortPriority);
				if ( !string.IsNullOrEmpty(m_folder) )
					key.SetValue(Constants.Registry.VisualStudio.TemplateDirs.TemplatesDir, m_folder);
			}
		}

		public override void Unregister(RegistrationContext context)
		{
			context.RemoveKey(GetRegistryKey(context));
		}
	}
}

