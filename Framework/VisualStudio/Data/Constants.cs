namespace LinkMe.Framework.VisualStudio.Data
{
    internal static class Constants
    {
        internal static class Guids
        {
            internal const string Package = "B30C5DA5-FD7C-445c-AF5F-09D6F649878D";
            internal const string Project = "76CE0BCD-6FC9-46ac-9B83-586DC2D7504D";
            internal const string ProjectFactory = "399BE3CE-48CC-48d7-AFC5-D613D8348D63";
            internal const string CmdSet = "5E1CC26F-3228-4c10-AE70-D963CE9D4289";
            internal const string ConfigurationSettingsPage = "7EF44D1C-027C-4437-B024-DDC66E56DDA0";
            internal const string ProjectSettingsPage = "72EE06DA-B75C-4c65-B548-4A55CA0C74BE";
        };

        internal static class Project
        {
            internal const string Type = "DataProject";
            internal const string Configuration = "Configuration";
            internal const string TargetsFile = "LinkMe.Environment.Build.Tasks.Data.targets";
            internal const string DisplayName = "LinkMe Data Project";
            internal const string DisplayProjectFileExtensions = "LinkMe Data Project Files (*.pdproj);*.pdproj";
            internal const string ProjectFileExtension = "pdproj";

            internal static class Property
            {
                internal static class Category
                {
                    internal const string General = LinkMe.Framework.VisualStudio.Constants.Project.Property.Category.General;
                    internal const string Advanced = LinkMe.Framework.VisualStudio.Constants.Project.Property.Category.Advanced;
                    internal const string Merge = "MergeCategory";
                }

                internal static class OutputPath
                {
                    internal const string Name = "OutputPath";
                    internal const string Category = Constants.Project.Property.Category.Merge;
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
                internal static class Merge
                {
                    internal const string ItemType = "Merge";
                }
            }
        }

        internal static class Folder
        {
            internal const string Bin = LinkMe.Environment.Constants.Folder.Bin;
            internal const string Targets = LinkMe.Framework.VisualStudio.Constants.Folder.Targets;
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
            internal const string Merge = "Merge";
            internal const string None = "None";
        }
    }
}