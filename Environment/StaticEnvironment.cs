using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace LinkMe.Environment
{
    public enum EnvironmentType
    {
        Development,
        Production
    }

    public static class StaticEnvironment
    {
        private static System.Version _productVersion;

        public static System.Version ProductVersion
        {
            get
            {
                if ( _productVersion == null )
                {
                    // Use the major and minor numbers of this assembly.

                    System.Version assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    _productVersion = new System.Version(assemblyVersion.Major, assemblyVersion.Minor);
                }

                return _productVersion;
            }
        }

        public static System.Version GetFileVersion(Assembly assembly)
        {
            var location = assembly.Location;
            return new System.Version(FileVersionInfo.GetVersionInfo(location).FileVersion);
        }

        public static bool IsVersionInstalled(string product, System.Version version)
        {
            // Look for an InstallDir value in the registry.

            string versionKeyPath = GetRegistryKeyPath(product, version);
            RegistryKey versionKey = Registry.LocalMachine.OpenSubKey(versionKeyPath, false);
            if ( versionKey == null )
                return false;

            using ( versionKey )
            {
                return !string.IsNullOrEmpty(versionKey.GetValue(Constants.Registry.InstallDirValueName) as string);
            }
        }

        public static string GetInstallFolder()
        {
            return GetInstallFolder(null);
        }

        public static string GetInstallFolder(string product)
        {
            return GetInstallFolder(product, ProductVersion);
        }

        public static string GetInstallFolder(string product, System.Version version)
        {
            // Look for the InstallDir value in the registry.

            string versionKeyPath = GetRegistryKeyPath(product, version);
            RegistryKey versionKey = Registry.LocalMachine.OpenSubKey(versionKeyPath, false);
            if ( versionKey == null )
                return null;

            using ( versionKey )
            {
                return versionKey.GetValue(Constants.Registry.InstallDirValueName) as string;
            }
        }

        public static EnvironmentType GetInstallType()
        {
            return GetInstallType(null);
        }

        public static EnvironmentType GetInstallType(string product)
        {
            return GetInstallType(product, ProductVersion);
        }

        public static EnvironmentType GetInstallType(string product, System.Version version)
        {
            // Look for the InstallType value in the registry.

            string versionKeyPath = GetRegistryKeyPath(product, version);
            RegistryKey versionKey = Registry.LocalMachine.OpenSubKey(versionKeyPath, false);
            if ( versionKey == null )
                return EnvironmentType.Production;

            using ( versionKey )
            {
                // Get the value and try to convert into the type.

                var value = versionKey.GetValue(Constants.Registry.InstallTypeValueName) as string;
                if ( string.IsNullOrEmpty(value) )
                    return EnvironmentType.Production;

                try
                {
                    return (EnvironmentType) System.Enum.Parse(typeof(EnvironmentType), value);
                }
                catch ( System.Exception )
                {
                    return EnvironmentType.Production;
                }
            }
        }

        public static string GetFilePath(string product, string assembleFolder, string fileName)
        {
            return GetFilePath(product, assembleFolder, fileName, null);
        }

        public static string GetFilePath(string product, string assembleFolder, string fileName, string subsystem)
        {
            // Get the install values.

            string installFolder = GetInstallFolder(product);
            if (installFolder == null)
                return null;
            EnvironmentType type = GetInstallType(product);

            switch ( type )
            {
                case EnvironmentType.Development:
                    return GetDevelopmentFilePath(installFolder, assembleFolder, fileName, subsystem);

                default:

                    // Combine all the parts together.

                    return Path.Combine(Path.Combine(installFolder, assembleFolder), fileName);
            }
        }

        public static string GetDevelopmentFilePath(string rootFolder, string assembleFolder, string fileName, string subsystem)
        {
            // The relative location of the file will mirror the file's name or come from the subsystem.

            string filePath = string.IsNullOrEmpty(subsystem)
                ? Path.GetFileNameWithoutExtension(fileName).Replace('.', Path.DirectorySeparatorChar)
                : subsystem.Replace('.', Path.DirectorySeparatorChar);

            // Remove the top level.

            int pos = filePath.IndexOf(Path.DirectorySeparatorChar);
            if ( pos != -1 )
                filePath = filePath.Substring(pos + 1);

            return GetSubsystemFilePath(rootFolder, assembleFolder, fileName, filePath);
        }

        private static string GetSubsystemFilePath(string rootFolder, string assembleFolder, string fileName, string filePath)
        {
            string installFolderPath = Path.Combine(Path.Combine(rootFolder, filePath), Constants.Folder.Install);
            if ( Directory.Exists(installFolderPath) )
                return Path.Combine(Path.Combine(installFolderPath, assembleFolder), fileName);

            // Remove the last part and iterate.

            int pos = filePath.LastIndexOf(Path.DirectorySeparatorChar);
            if ( pos == -1 )
                return null;

            filePath = filePath.Substring(0, pos);
            return GetSubsystemFilePath(rootFolder, assembleFolder, fileName, filePath);
        }

        public static void SetInstallFolder(string folder)
        {
            SetInstallFolder(null, folder);
        }

        public static void SetInstallFolder(string product, string folder)
        {
            SetInstallFolder(product, ProductVersion, folder);
        }

        public static void SetInstallFolder(string product, System.Version version, string folder)
        {
            // Check that the folder path is absolute.

            if ( folder == null )
                throw new System.ArgumentNullException("folder");
            if ( !FilePath.IsAbsolutePath(folder) )
                throw new System.ArgumentException("'" + folder + "' is not an absolute path.", folder);

            // Write the folder into the registry.

            using ( RegistryKey key = GetRegistryKey(product, version) )
            {
                key.SetValue(Constants.Registry.InstallDirValueName, folder);
            }
        }

        public static void SetInstallType(EnvironmentType type)
        {
            SetInstallType(null, type);
        }

        public static void SetInstallType(string product, EnvironmentType type)
        {
            SetInstallType(product, ProductVersion, type);
        }

        public static void SetInstallType(string product, System.Version version, EnvironmentType type)
        {
            // Write the type into the registry.

            using ( RegistryKey key = GetRegistryKey(product, version) )
            {
                key.SetValue(Constants.Registry.InstallTypeValueName, type.ToString());
            }
        }

        private static string GetRegistryKeyPath(string product, System.Version version)
        {
            if ( version == null )
                throw new System.ArgumentNullException("version");
            if ( product == null )
                return Constants.Registry.RootKeyPath + "\\" + version;
            return Constants.Registry.RootKeyPath + "\\" + product + "\\" + version;
        }

        private static RegistryKey GetRegistryKey(string product, System.Version version)
        {
            string keyPath = GetRegistryKeyPath(product, version);

            // Try to open the key.

            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true);
            if ( key == null )
            {
                // Create the registry key.

                key = Registry.LocalMachine.CreateSubKey(keyPath);
                if ( key == null )
                    throw new System.ApplicationException(string.Format("Failed to create registry key" + " '{0}\\{1}'.", Registry.LocalMachine.Name, keyPath));
            }

            return key;
        }
    }
}