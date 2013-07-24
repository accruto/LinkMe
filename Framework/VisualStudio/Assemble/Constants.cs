namespace LinkMe.Framework.VisualStudio.Assemble.Constants
{
    internal static class Guids
    {
        internal const string Package = "3E36D6BC-B13A-496E-BBD5-F98958C4AC96";
		internal const string Project = "C3C42895-658A-406E-B79A-81F05857FD88";
		internal const string ProjectFactory = "B1AC1FD2-5FDD-4604-8997-8BD033AD39E1";
		internal const string CmdSet = "20DDED8B-9D84-46F0-97CF-3E7B5B5C3DB3";
		internal const string ConfigurationSettingsPage = "9EDA37CC-2321-4136-92E7-3D8CCAF28548";
		internal const string ProjectSettingsPage = "D73A9774-BC4E-45d5-9513-A61083126F8B";
    };

	internal static class Project
	{
		internal const string Type = "AssembleProject";
		internal const string Configuration = "Configuration";
		internal const string TargetsFile = "LinkMe.Environment.Build.Tasks.Assemble.targets";
		internal const string DisplayName = "LinkMe Assemble Project";
		internal const string DisplayProjectFileExtensions = "LinkMe Assemble Project Files (*.paproj);*.paproj";
		internal const string ProjectFileExtension = "paproj";

		internal static class Property
		{
			internal static class Category
			{
				internal const string General = LinkMe.Framework.VisualStudio.Constants.Project.Property.Category.General;
				internal const string Advanced = LinkMe.Framework.VisualStudio.Constants.Project.Property.Category.Advanced;
				internal const string Assemble = "AssembleCategory";
			}

			internal static class CatalogueFile
			{
				internal const string Name = "CatalogueFile";
				internal const string Category = Constants.Project.Property.Category.Assemble;
				internal const string DisplayName = "CatalogueFileDisplayName";
				internal const string Description = "CatalogueFileDescription";
			}

			internal static class OutputPath
			{
				internal const string Name = "OutputPath";
				internal const string Category = Constants.Project.Property.Category.Assemble;
				internal const string DisplayName = "OutputPathDisplayName";
				internal const string Description = "OutputPathDescription";
			}

			internal static class BuildAction
			{
				internal const string Name = "BuildAction";
				internal const string Category = Constants.Project.Property.Category.Advanced;
				internal const string DisplayName = "BuildActionDisplayName";
				internal const string Description = "BuildActionDescription";
			}
		}

		internal static class Item
		{
			internal static class Assemble
			{
				internal const string ItemType = "Assemble";

				internal static class InstallInGac
				{
					internal const string Name = LinkMe.Environment.Build.Tasks.Constants.Project.Item.Assemble.InstallInGac.Name;
					internal const string Category = Constants.Project.Property.Category.Assemble;
					internal const string DisplayName = "InstallInGacDisplayName";
					internal const string Description = "InstallInGacDescription";
				}

				internal static class ShortcutName
				{
					internal const string Name = LinkMe.Environment.Build.Tasks.Constants.Project.Item.Assemble.ShortcutName.Name;
					internal const string Category = Constants.Project.Property.Category.Assemble;
					internal const string DisplayName = "ShortcutNameDisplayName";
					internal const string Description = "ShortcutNameDescription";
				}

				internal static class ShortcutPath
				{
                    internal const string Name = LinkMe.Environment.Build.Tasks.Constants.Project.Item.Assemble.ShortcutPath.Name;
					internal const string Category = Constants.Project.Property.Category.Assemble;
					internal const string DisplayName = "ShortcutPathDisplayName";
					internal const string Description = "ShortcutPathDescription";
				}

				internal static class Guid
				{
                    internal const string Name = LinkMe.Environment.Build.Tasks.Constants.Project.Item.Assemble.Guid.Name;
				}
			}
		}
	}

	internal static class Folder
	{
		internal const string Bin = LinkMe.Environment.Constants.Folder.Bin;
		internal const string Targets = LinkMe.Framework.VisualStudio.Constants.Folder.Targets;
		internal const string ProjectTemplates = LinkMe.Framework.VisualStudio.Constants.Folder.ProjectTemplates;
	}

	internal static class File
	{
		internal static class Catalogue
		{
            internal const string Extension = LinkMe.Environment.Build.Tasks.Constants.File.Catalogue.Extension;
		}
	}

	internal static class Registry
	{
		internal static class VisualStudio
		{
			internal const string RootKeyPath = LinkMe.Framework.VisualStudio.Constants.Registry.VisualStudio.RootKeyPath;
		}
	}

	internal static class BuildAction
	{
		internal const string Assemble = "Assemble";
		internal const string CopyOnBuild = "CopyOnBuild";
		internal const string None = "None";
	}
}