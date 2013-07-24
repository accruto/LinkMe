namespace LinkMe.Framework.VisualStudio
{
    public static class Constants
    {
        internal static class Guids
        {
            internal const string TemplateFolders = "3C0B97DA-1905-45d4-90C7-793540DC6038";
        }

        internal static class Product
        {
            internal const string Sdk = LinkMe.Environment.Constants.Product.Sdk;
        }

        public static class Project
        {
            internal const string ProjectTemplatesName = "LinkMe";

            public static class Property
            {
                public static class Category
                {
                    public const string General = "GeneralCategory";
                    public const string Advanced = "AdvancedCategory";
                    internal const string Misc = "MiscCategory";
                    internal const string Project = "ProjectCategory";
                }

                internal static class ProjectFile
                {
                    internal const string Category = Constants.Project.Property.Category.Project;
                    internal const string DisplayName = "ProjectFileDisplayName";
                    internal const string Description = "ProjectFileDescription";
                }

                internal static class ProjectFolder
                {
                    internal const string Category = Constants.Project.Property.Category.Project;
                    internal const string DisplayName = "ProjectFolderDisplayName";
                    internal const string Description = "ProjectFolderDescription";
                }

                internal static class FileName
                {
                    internal const string Category = Constants.Project.Property.Category.Misc;
                    internal const string DisplayName = "FileNameDisplayName";
                    internal const string Description = "FileNameDescription";
                }

                internal static class FullPath
                {
                    internal const string Category = Constants.Project.Property.Category.Misc;
                    internal const string DisplayName = "FullPathDisplayName";
                    internal const string Description = "FullPathDescription";
                }
            }

            internal static class Item
            {
                internal const string SubType = "SubType";
                internal const string Link = LinkMe.Environment.Build.Tasks.Constants.Project.Item.Link;
            }
        }

        public static class Folder
        {
            internal const string Bin = LinkMe.Environment.Constants.Folder.Bin;
            public const string Targets = "Targets";
            public const string ProjectTemplates = "Templates\\Projects";
        }

        public static class Registry
        {
            public static class VisualStudio
            {
                public const string RootKeyPath = @"Software\Microsoft\VisualStudio\9.0";
                internal const string GeneralKeyPath = "General";
                internal const string Verbosity = "MSBuildLoggerVerbosity";

                internal static class TemplateDirs
                {
                    internal const string KeyPathFormat = "NewProjectTemplates\\TemplateDirs\\{0}\\/1";
                    internal const string SortPriority = "SortPriority";
                    internal const string TemplatesDir = "TemplatesDir";
                }

                internal static class Projects
                {
                    internal const string KeyPathFormat = "Projects\\{0}";
                    internal const string DisplayName = "DisplayName";
                    internal const string DisplayProjectFileExtensions = "DisplayProjectFileExtensions";
                    internal const string Package = "Package";
                    internal const string DefaultProjectExtension = "DefaultProjectExtension";
                    internal const string PossibleProjectExtensions = "PossibleProjectExtensions";
                    internal const string ProjectTemplatesDir = "ProjectTemplatesDir";
                    internal const string LanguageVsTemplate = "Language(VsTemplate)";
                    internal const string ShowOnlySpecifiedTemplatesVsTemplate = "ShowOnlySpecifiedTemplates(VsTemplate)";
                    internal const string TemplateGroupIDsVsTemplate = "TemplateGroupIDs(VsTemplate)";
                    internal const string TemplateIDsVsTemplate = "TemplateIDs(VsTemplate)";
                    internal const string DisplayProjectTypeVsTemplate = "DisplayProjectType(VsTemplate)";
                    internal const string ProjectSubTypeVsTemplate = "ProjectSubType(VsTemplate)";
                    internal const string NewProjectRequireNewFolderVsTemplate = "NewProjectRequireNewFolder(VsTemplate)";
                }
            }
        }
    }
}

