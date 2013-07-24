namespace LinkMe.Framework.Host.Constants
{
    internal static class Application
	{
		internal static class WindowsService
		{
			internal const string Name = "WindowsService";
			internal const string DisplayName = "Windows Service";
			internal const string Description = "";
			internal const string ServiceFileName = "LinkMe.Framework.Host.Service.exe";
		}
	}

	internal static class Exceptions
	{
		internal const string ContainerName = "ContainerName";
		internal const string ThreadPoolName = "ThreadPoolName";
	}

	internal static class Validation
	{
		internal const string CompleteNamePattern = Configuration.Constants.Validation.CompleteNamePattern;
	}
}