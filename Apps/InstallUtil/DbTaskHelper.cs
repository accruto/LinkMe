namespace LinkMe.InstallUtil
{
    internal static class DbTaskHelper
    {
        public const string DEFAULT_SERVER = "(LOCAL)";
        public const string DEFAULT_DATABASE = "LinkMe";

        public static void DefaultServerAndDbNames(ref string serverName, ref string databaseName)
        {
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = DEFAULT_SERVER;
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = DEFAULT_DATABASE;
            }
        }
    }
}
