using System;

namespace LinkMe.Environment.Build.Tasks
{
    internal static class VersionUtil
    {
        public static string GetProductVersion(string buildVersion)
        {
            if (string.IsNullOrEmpty(buildVersion))
                return null;

            // Try to parse the build version.

            var parts = buildVersion.Split('.');
            if (parts.Length > 4)
                throw new ApplicationException("The build version '" + buildVersion + "' has an incorrect format.");

            // Check that all parts are numbers.

            for (var index = 0; index < parts.Length; ++index)
            {
                int part;
                if (!int.TryParse(parts[index], out part))
                    throw new ApplicationException("The build version '" + buildVersion + "' has an incorrect format.");
            }

            var version = parts[0];
            switch (parts.Length)
            {
                case 1:
                    version += ".0.0";
                    break;

                case 2:
                    version += "." + parts[1] + ".0";
                    break;

                default:
                    version += "." + parts[1] + "." + parts[2];
                    break;
            }

            return version;
        }
    }
}