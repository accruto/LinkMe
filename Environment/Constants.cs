namespace LinkMe.Environment
{
    public static class Constants
    {
        public static class Product
        {
            public const string Sdk = "SDK";
        }

        public static class Folder
        {
            public const string Bin = "Bin";
            public const string Install = "Install";
            public const string Build = "Build";
        }

        internal static class Path
        {
            internal const int MaxPath = 255;
        }

        internal static class Registry
        {
            internal const string RootKeyPath = @"SOFTWARE\LinkMe";
            internal const string InstallDirValueName = "InstallDir";
            internal const string InstallTypeValueName = "InstallType";
        }

        public static class Xml
        {
            public const string RootNamespace = "http://xmlns.linkme.com.au";
        }
    }
}