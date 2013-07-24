namespace LinkMe.Framework.VisualStudio.Package.Constants
{
	internal static class Guids
	{
		internal const string Package = "C7E9D198-7A59-4393-9344-7957955EE05B";
		internal const string Project = "F0A0FFF1-16BB-4619-BC51-103D2D006E4C";
		internal const string ProjectFactory = "6CE850B2-8257-4C50-91A7-DFA43C437D5E";
		internal const string CmdSet = "9C718C27-22E5-4A91-AA71-E917541DC2DC";
		internal const string ConfigurationSettingsPage = "B5C252C6-6B4C-4f8f-809F-158558B43DC2";
		internal const string ProjectSettingsPage = "6FE061B8-BBA2-4b68-A008-54C173FAE11F";
	};

	internal static class Project
	{
		internal const string Type = "ModuleProject";
		internal const string TargetsFile = "LinkMe.Environment.Build.Tasks.Package.targets";
		internal const string DisplayName = "LinkMe Module Project";
		internal const string DisplayProjectFileExtensions = "LinkMe Module Project Files (*.pmproj);*.pmproj";
		internal const string ProjectFileExtension = "pmproj";

		internal static class Property
		{
			internal static class Category
			{
				internal const string Advanced = LinkMe.Framework.VisualStudio.Constants.Project.Property.Category.Advanced;
				internal const string Module = "ModuleCategory";
			}

			internal static class BuildAction
			{
				internal const string Name = "BuildAction";
				internal const string Category = Constants.Project.Property.Category.Advanced;
				internal const string DisplayName = "BuildActionDisplayName";
				internal const string Description = "BuildActionDescription";
			}

			internal static class Manufacturer
			{
				internal const string Name = "Manufacturer";
				internal const string Category = Constants.Project.Property.Category.Module;
				internal const string DisplayName = "ManufacturerDisplayName";
				internal const string Description = "ManufacturerDescription";
			}

			internal static class MergeModuleFile
			{
				internal const string Name = "MergeModuleFile";
				internal const string Category = Constants.Project.Property.Category.Module;
				internal const string DisplayName = "MergeModuleFileDisplayName";
				internal const string Description = "MergeModuleFileDescription";
			}

			internal static class OutputPath
			{
				internal const string Name = "OutputPath";
				internal const string Category = Constants.Project.Property.Category.Module;
				internal const string DisplayName = "OutputPathDisplayName";
				internal const string Description = "OutputPathDescription";
			}

			internal static class Version
			{
				internal const string Name = "Version";
				internal const string Category = Constants.Project.Property.Category.Advanced;
				internal const string DisplayName = "VersionDisplayName";
				internal const string Description = "VersionDescription";
			}

			internal static class ModuleGuid
			{
				internal const string Name = "ModuleGuid";
			}
		}

		internal static class Item
		{
			internal static class Module
			{
				internal const string ItemType = "Catalogue";
			}
		}
	}

	internal static class Folder
	{
		internal const string ProjectTemplates = LinkMe.Framework.VisualStudio.Constants.Folder.ProjectTemplates;
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
		internal const string Catalogue = "Catalogue";
		internal const string None = "None";
	}
}