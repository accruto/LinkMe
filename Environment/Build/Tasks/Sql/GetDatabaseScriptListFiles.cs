using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    /// <summary>
    /// Works out which scripts should be run for to build the database for the specified release.
    /// </summary>
    public class GetDatabaseScriptListFiles
        : Task
    {
        private const string ScriptListFileName = "install.db.txt";

        private string _scriptRootDirectory;
        private string _productVersion;
        private string _baselineVersion;
        private ITaskItem[] _scriptListFiles;

        /// <summary>
        /// The top-level directory under which all the scripts are located, ie. the parent directory of "Release2", "Release3", etc.
        /// </summary>
        [Required]
        public string ScriptRootDirectory
        {
            get { return _scriptRootDirectory; }
            set { _scriptRootDirectory = value; }
        }

        /// <summary>
        /// The current release number extracted from the builder number, eg. "3.2.4".
        /// </summary>
        [Required]
        public string ProductVersion
        {
            get { return _productVersion; }
            set { _productVersion = value; }
        }

        /// <summary>
        /// The release number of the baseline to restore (the last major release).
        /// </summary>
        [Required]
        public string BaselineVersion
        {
            get { return _baselineVersion; }
            set { _baselineVersion = value; }
        }

        /// <summary>
        /// The list of script list files (install.db.txt files) to run, in order, to build the current database release
        /// from the baseline.
        /// </summary>
        [Output]
        public ITaskItem[] ScriptListFiles
        {
            get { return _scriptListFiles; }
        }

        public override bool Execute()
        {
            var productVersion = new Version(_productVersion);
            var scriptDirectories = new SortedDictionary<Version, string>();

            if (productVersion.Build <= 0 && productVersion.Revision <= 0)
            {
                // The current release is a major release, so the baseline is the LAST major release and we need to run
                // all the hotfixes for the last release before the scripts for this one. Eg. if the current release is
                // 3.2 then we need to restore the 3.1 baseline, then run scripts for 3.1.1, 3.1.2, etc. and finally 3.2.

                var baselineVersion = new Version(_baselineVersion);

                var baselineReleaseDirectory = GetReleaseDirectory(baselineVersion);
                if (!Directory.Exists(baselineReleaseDirectory))
                {
                    Log.LogError("The baseline release directory, '{0}', does not exist.", baselineReleaseDirectory);
                    return false;
                }

                AddHotfixDirectories(scriptDirectories, baselineReleaseDirectory, baselineVersion, null);

                // Only add the release directory if it exists - there may not be any DB scripts for this major release yet.

                var releaseDirectory = GetReleaseDirectory(productVersion);
                if (Directory.Exists(releaseDirectory))
                {
                    scriptDirectories.Add(productVersion, releaseDirectory);
                }
                else
                {
                    Log.LogMessage("Major release directory {0} does not exist.", releaseDirectory);
                }
            }
            else
            {
                // The current release is a minor release, so the baseline is the CURRENT major release and we need to run
                // only the hotfixes for this release up to, and including, the current release. Eg. if the current release is
                // 3.1.3 then we need to restore the 3.1 baseline, then run scripts for 3.1.1, 3.1.2 and 3.1.3, but not for
                // 3.1.4 (which might also exist).

                _baselineVersion = productVersion.ToString(2);

                var baselineReleaseDirectory = GetReleaseDirectory(productVersion);
                if (!Directory.Exists(baselineReleaseDirectory))
                {
                    Log.LogError("The baseline release directory, '{0}', does not exist.", baselineReleaseDirectory);
                    return false;
                }

                AddHotfixDirectories(scriptDirectories, baselineReleaseDirectory, productVersion, productVersion);
            }

            if (scriptDirectories.Count == 0)
            {
                Log.LogMessage("No scripts were found for this release.");
            }
            else
            {
                Log.LogMessage("Looking for scripts in these {0} directories (in order):", scriptDirectories.Count);
                foreach (string scriptDirectory in scriptDirectories.Values)
                {
                    Log.LogMessage("\t{0}", scriptDirectory);
                }
            }

            var scriptListFileList = new List<ITaskItem>(scriptDirectories.Count);

            foreach (var scriptDirectory in scriptDirectories.Values)
            {
                var scriptFilePath = Path.Combine(scriptDirectory, ScriptListFileName);
                if (File.Exists(scriptFilePath))
                {
                    scriptListFileList.Add(new TaskItem(scriptFilePath));
                }
                else
                {
                    // There directory exists, but there is no list file in it. That's OK, unless it
                    // has some .sql files in it.

                    var sqlFiles = Directory.GetFiles(scriptDirectory, "*.sql", SearchOption.TopDirectoryOnly);
                    if (sqlFiles.Length > 0)
                    {
                        Log.LogError("Directory '{0}' contains {1} .sql files, but no {2} file.", scriptDirectory, sqlFiles.Length, ScriptListFileName);
                        return false;
                    }
                }
            }

            _scriptListFiles = scriptListFileList.ToArray();
            return true;
        }

        private static void AddHotfixDirectories(IDictionary<Version, string> scriptDirectories,
                                                 string parentReleaseDirectory, Version majorVersion, Version maxVersion)
        {
            foreach (string hotfixDir in GetHotfixDirectories(parentReleaseDirectory, majorVersion))
            {
                string hotfixDirName = Path.GetFileName(hotfixDir);
                Version hotfixVersion;
                try
                {
                    hotfixVersion = new Version(hotfixDirName);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("Failed to parse directory name '{0}' (under '{1}')"
                                                                 + " as a version number.", hotfixDirName, parentReleaseDirectory), ex);
                }

                if (maxVersion == null || hotfixVersion <= maxVersion)
                {
                    scriptDirectories.Add(hotfixVersion, hotfixDir);
                }
            }
        }

        private static string[] GetHotfixDirectories(string majorReleaseDirectory, Version majorVersion)
        {
            return Directory.GetDirectories(
                majorReleaseDirectory, majorVersion.ToString(2) + ".*", SearchOption.TopDirectoryOnly);
        }

        private string GetReleaseDirectory(Version release)
        {
            return Path.Combine(ScriptRootDirectory, "Release" + release.Major + Path.DirectorySeparatorChar + release.ToString(2));
        }
    }
}