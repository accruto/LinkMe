using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    /// <summary>
    /// Parses a build number to figure out the the baseline version.
    /// </summary>
    public class GetBaselineVersion
        : Task
    {
        private string _buildVersion;
        private string _baselineVersion;

        /// <summary>
        /// e.g. 9.7.1.23
        /// </summary>
        public string BuildVersion
        {
            get { return _buildVersion ?? string.Empty; }
            set { _buildVersion = value; }
        }

        /// <summary>
        /// The release number of the baseline to restore (the last major release).
        /// </summary>
        [Output]
        public string BaselineVersion
        {
            get { return _baselineVersion; }
        }

        public override bool Execute()
        {
            var productVersion = VersionUtil.GetProductVersion(_buildVersion);

            // The System.Version class conveniently parses and compares version numbers in just the format we need.

            var currentVersion = new Version(productVersion);
            if (currentVersion.Build <= 0 && currentVersion.Revision <= 0)
            {
                // The current release is a major release, so the baseline is the LAST major release and we need to run
                // all the hotfixes for the last release before the scripts for this one. Eg. if the current release is
                // 3.2 then we need to restore the 3.1 baseline, then run scripts for 3.1.1, 3.1.2, etc. and finally 3.2.

                _baselineVersion = GetLastMajorVersion(currentVersion).ToString(2);
            }
            else
            {
                // The current release is a minor release, so the baseline is the CURRENT major release and we need to run
                // only the hotfixes for this release up to, and including, the current release. Eg. if the current release is
                // 3.1.3 then we need to restore the 3.1 baseline, then run scripts for 3.1.1, 3.1.2 and 3.1.3, but not for
                // 3.1.4 (which might also exist).

                _baselineVersion = currentVersion.ToString(2);
            }

            return true;
        }

        private static Version GetLastMajorVersion(Version version)
        {
            // Decrement the "Minor" version part, which for our purposes is a "major" release.

            if (version.Minor == 0)
                return new Version(version.Major - 1, 9);
            else
                return new Version(version.Major, version.Minor - 1);
        }
    }
}