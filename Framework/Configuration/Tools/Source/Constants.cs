namespace LinkMe.Framework.Configuration.Tools.Constants
{
	internal static class RepositoryType
	{
		internal static class ConfigurationFileConnection
		{
			internal const string FileExtension = LinkMe.Framework.Configuration.Constants.RepositoryType.ConfigurationFileConnection.FileExtension;
		}
	}

	internal static class Icon
	{
		internal const string ResourcePrefix = "LinkMe.Framework.Configuration.Tools.Source.Icons.";
		internal const string ResourceSuffix = ".ico";
	}

	public static class Project
	{
		public const string FileExtension = "acproj";
        public const string OutputFolder = "OutputFolder";
		public const string KeyFile = "KeyFile";
        public const string InstallInGac = "InstallInGac";
        public const string Version = "Version";
        public const string OutputFiles = "OutputFiles";
	}

	internal static class Config
	{
		internal const string RootElement = LinkMe.Framework.Configuration.Constants.Config.RootElement;
		internal const string SectionElement = LinkMe.Framework.Configuration.Constants.Config.SectionElement;
		internal const string ReferencesElement = LinkMe.Framework.Configuration.Constants.Config.ReferencesElement;
		internal const string ReferenceElement = LinkMe.Framework.Configuration.Constants.Config.ReferenceElement;
		internal const string FileElement = LinkMe.Framework.Configuration.Constants.Config.FileElement;
	}

	internal static class MSBuild
	{
		internal const string Namespace = "http://schemas.microsoft.com/developer/msbuild/2003";
		internal const string ProjectElement = "Project";
		internal const string ItemGroupElement = "ItemGroup";
		internal const string ConfigReferenceElement = "ConfigReference";
		internal const string HintPathElement = "HintPath";
	}
}

