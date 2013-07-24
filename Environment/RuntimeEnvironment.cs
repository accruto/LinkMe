using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.Win32;

namespace LinkMe.Environment
{
    /// <summary>
    /// Provides static methods that return information about the LinkMe Framework runtime environment.
    /// </summary>
    public static class RuntimeEnvironment
    {
        /// <summary>
        /// The path of the LinkMe framework's root registry key, relative to HKEY_LOCAL_MACHINE.
        /// </summary>
        public const string RootKeyName = "SOFTWARE\\LinkMe";
        public const string EnvironmentVariable = "LINKME_APP_ENV";
        public const string TestEmailDomain = "test.linkme.net.au";

        private const string InstallDir = "InstallDir";
        private static readonly string[] EnvironmentNames = new [] { "", "dev", "uat", "beta", "prod" };

        private static ApplicationEnvironment _currentEnvironment;
        private static string _sourceFolder;

        static RuntimeEnvironment()
        {
            // Get environment on which app is running. Use the machine environment, not current process
            // environment, so VS doesn't need to be restarted when switching.

            var environmentVariable = System.Environment.GetEnvironmentVariable(EnvironmentVariable, EnvironmentVariableTarget.Machine);
            if (environmentVariable == null)
                throw new ApplicationException(string.Format("The {0} environment variable is not set.", EnvironmentVariable));

            _currentEnvironment = GetApplicationEnvironment(environmentVariable);
            if (_currentEnvironment == ApplicationEnvironment.None)
                throw new ApplicationException(string.Format("The current value of the {0} environment variable, '{1}', is invalid. One of the following is expected: '{2}'.", EnvironmentVariable, environmentVariable, string.Join("', '", EnvironmentNames)));
        }

        public static ApplicationEnvironment Environment
        {
            get { return _currentEnvironment; }
        }

        public static string EnvironmentName
        {
            get { return GetApplicationEnvironmentName(_currentEnvironment); }
        }

        public static string HostName
        {
            get { return Dns.GetHostName().ToLower(); }
        }

        public static string MachineName
        {
            get { return System.Environment.MachineName; }
        }

        /// <summary>
        /// Checks whether the specified version of the framework is installed on this machine.
        /// </summary>
        /// <param name="version">The version to check for.</param>
        /// <returns>True if the specified version of the LinkMe framework is installed on the machine, otherwise false.</returns>
        public static bool IsVersionInstalled(Version version)
        {
            var versionPath = GetRegistryKeyForVersion(version);
            var versionKey = Registry.LocalMachine.OpenSubKey(versionPath, false);
            if (versionKey == null)
                return false;

            using (versionKey)
            {
                return (versionKey.GetValue(InstallDir) != null);
            }
        }

        /// <summary>
        /// Gets the versions of the LinkMe framework installed on this machine.
        /// </summary>
        /// <returns>An array of <see cref="System.Version"/> objects, each of which represents a
        /// version of the LinkMe framework instaleld on this machine.</returns>
        public static Version[] GetInstalledVersions()
        {
            var rootKey = Registry.LocalMachine.OpenSubKey(RootKeyName, false);
            if (rootKey == null)
                return new Version[0];

            string[] subKeyNames;
            using (rootKey)
            {
                subKeyNames = rootKey.GetSubKeyNames();
            }

            var versions = new ArrayList();
            foreach (var keyName in subKeyNames)
            {
                try
                {
                    var version = new Version(keyName);
                    versions.Add(version);
                }
                catch (FormatException)
                {
                }
                catch (ArgumentException)
                {
                }
            }

            return (Version[])versions.ToArray(typeof(Version));
        }

        /// <summary>
        /// Gets the directory where the currently running version of the framework is installed.
        /// </summary>
        /// <returns>The directory where the currently running version of the framework is installed.</returns>
        public static string GetInstallFolder()
        {
            return GetInstallFolder(StaticEnvironment.ProductVersion);
        }

        /// <summary>
        /// Gets the directory where the currently running version of the framework is installed.
        /// </summary>
        /// <param name="throwIfNotFound">True to throw an exception if the running version is
        /// not installed, false to return null in that case.</param>
        /// <returns>The directory where the currently running version of the framework is installed.</returns>
        public static string GetInstallFolder(bool throwIfNotFound)
        {
            return GetInstallFolder(StaticEnvironment.ProductVersion, throwIfNotFound);
        }

        /// <summary>
        /// Gets the directory where the specified version of the framework is installed.
        /// </summary>
        /// <param name="version">The framework version to search for.</param>
        /// <returns>The directory where the specified version of the framework is installed.</returns>
        public static string GetInstallFolder(Version version)
        {
            return GetInstallFolder(version, true);
        }

        /// <summary>
        /// Gets the directory where the specified version of the framework is installed.
        /// </summary>
        /// <param name="version">The framework version to search for.</param>
        /// <param name="throwIfNotFound">True to throw an exception if the specified version is
        /// not installed, false to return null in that case.</param>
        /// <returns>The directory where the specified version of the framework is installed.</returns>
        public static string GetInstallFolder(Version version, bool throwIfNotFound)
        {
            var versionPath = GetRegistryKeyForVersion(version);
            var versionKey = Registry.LocalMachine.OpenSubKey(versionPath, false);
            if (versionKey == null)
            {
                if (throwIfNotFound)
                    throw new ApplicationException(string.Format("Registry key '{0}\\{1}' cannot be opened for reading.", Registry.LocalMachine.Name, versionPath));
                return null;
            }

            object value;
            using (versionKey)
            {
                value = versionKey.GetValue(InstallDir);
            }

            if (value == null)
            {
                if (throwIfNotFound)
                    throw new ApplicationException(string.Format("Registry value '{0}' on key '{1}\\{2}' cannot be read.", InstallDir, Registry.LocalMachine.Name, versionPath));
                return null;
            }

            try
            {
                return (string)value;
            }
            catch (InvalidCastException)
            {
                throw new ApplicationException(string.Format("Registry value '{0}' on key '{1}\\{2}' is of type '{3}', but a string was expected.", InstallDir, Registry.LocalMachine.Name, versionPath, value.GetType().FullName));
            }
        }

        /// <summary>
        /// Gets the directory where the specified version of the framework is installed.
        /// </summary>
        /// <param name="version">The framework version to search for.</param>
        /// <param name="installFolder"></param>
        /// <returns>The directory where the specified version of the framework is installed.</returns>
        public static void SetInstallFolder(Version version, string installFolder)
        {
            if (installFolder != null && !FilePath.IsAbsolutePath(installFolder))
                throw new ArgumentException("'" + installFolder + "' is not an absolute path.", installFolder);

            var versionPath = GetRegistryKeyForVersion(version);

            // Try to open the key, in case it already exists.

            var versionKey = Registry.LocalMachine.OpenSubKey(versionPath, true);
            if (versionKey == null)
            {
                if (installFolder == null)
                    return; // Good, nothing to delete.

                // Create the registry key.

                versionKey = Registry.LocalMachine.CreateSubKey(versionPath);
                if (versionKey == null)
                    throw new ApplicationException(string.Format("Failed to create registry key '{0}\\{1}'.", Registry.LocalMachine.Name, versionPath));
            }

            using (versionKey)
            {
                if (installFolder == null)
                    versionKey.DeleteValue(InstallDir, false);
                else
                    versionKey.SetValue(InstallDir, installFolder);
            }
        }

        public static string GetSourceFolder()
        {
            if (_sourceFolder != null)
                return _sourceFolder;

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LinkMe.Environment.sourcepath.txt");
            if (stream == null)
                throw new ApplicationException("Cannot load the source path stream.");

            string path;
            using (var reader = new StreamReader(stream))
            {
                path = reader.ReadLine();
            }

            if (string.IsNullOrEmpty(path))
                throw new ApplicationException("No folder was specified in the embedded 'sourcepath.txt' file.");

            var info = new DirectoryInfo(path);
            if (!info.Exists)
                return null; // Probably running on a different machine or the path has simply changed since the build.

            // The "source root" directory is one level up.

            var parent = info.Parent;
            if (parent == null || parent.Parent == null)
                throw new ApplicationException("The specified source root path, '" + path + "', does not have a parent directory.");

            _sourceFolder = parent.FullName;
            return _sourceFolder;
        }

        /// <summary>
        /// Gets the folder path where application data for the current user should be saved. The folder
        /// may or may not exist.
        /// </summary>
        /// <returns>The folder path where application data for the current user should be saved.</returns>
        public static string GetApplicationDataFolder()
        {
            var rootFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            if (string.IsNullOrEmpty(rootFolder))
                throw new ApplicationException(string.Format("The system did not return an 'ApplicationData' folder for the current user, '{0}\\{1}'.", System.Environment.UserDomainName, System.Environment.UserName));

            return Path.Combine(rootFolder, "LinkMe");
        }

        private static string GetRegistryKeyForVersion(Version version)
        {
            if (version == null)
                throw new ArgumentNullException("version");
            if (version.Major < 0 || version.Minor < 0)
                throw new ArgumentException(string.Format("The version number, '{0}.{1}', is invalid.", version.Major, version.Minor), "version");

            return RootKeyName + "\\" + version;
        }

        public static ApplicationEnvironment GetApplicationEnvironment(string name)
        {
            if (string.IsNullOrEmpty(name))
                return ApplicationEnvironment.None;

            for (var index = 0; index < EnvironmentNames.Length; index++)
            {
                if (string.Compare(name, EnvironmentNames[index], true) == 0)
                    return (ApplicationEnvironment)index;
            }

            return ApplicationEnvironment.None;
        }

        public static string GetApplicationEnvironmentName(ApplicationEnvironment environment)
        {
            if (!Enum.IsDefined(typeof(ApplicationEnvironment), environment))
                throw new ArgumentException("The specified environment is not a valid ApplicationEnvironment value.", "environment");
            return EnvironmentNames[(int)environment];
        }

        public static void ChangeEnvironment(ApplicationEnvironment newEnvironment)
        {
            if (_currentEnvironment != newEnvironment)
                _currentEnvironment = newEnvironment;
        }
    }
}