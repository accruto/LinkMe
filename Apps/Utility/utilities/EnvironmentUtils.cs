using System;
using System.DirectoryServices;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
	public static class EnvironmentUtils
	{
	    public const string TestEmailSuffix = "@test.linkme.net.au";

        public static string GetIisVirtualPathForPhysicalPath(string physicalPath)
        {
            const string metaPath = @"IIS:\\localhost\W3SVC\1\Root";

            if (string.IsNullOrEmpty(physicalPath))
                throw new ArgumentException("The physical path must be specified.", "physicalPath");
            if (!FileSystem.IsAbsolutePath(physicalPath))
                throw new ArgumentException("The specified path must be absolute.");

            var webRootEntry = new DirectoryEntry(metaPath);
            string virtualPath = null;

            foreach (DirectoryEntry child in webRootEntry.Children)
            {
                PropertyValueCollection value = child.Properties["Path"];

                if (string.Equals((string)value.Value, physicalPath, StringComparison.OrdinalIgnoreCase))
                {
                    if (virtualPath != null)
                    {
                        throw new ApplicationException(string.Format("There is more than one IIS entry with" 
                            + " Path = '{0}': '{1}' and '{2}'", physicalPath, virtualPath, child.Path));
                    }

                    virtualPath = child.Path;
                }
            }

            if (virtualPath == null)
                return null;

            return virtualPath.Substring(metaPath.Length).Replace('\\', '/');
        }

        public static string ObfuscateEmailAddress(string emailAddress)
        {
            if (emailAddress == null)
                return null;

            if (emailAddress.EndsWith(TestEmailSuffix, StringComparison.CurrentCultureIgnoreCase))
                return emailAddress;

            return emailAddress.Replace("@", "+") + TestEmailSuffix;
        }
    }
}
