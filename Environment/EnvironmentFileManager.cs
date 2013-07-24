using System.IO;

namespace LinkMe.Environment
{
    public class EnvironmentFileManager
    {
        public static string GetBinFile(string fileName, string relativeDevFolder)
        {
            return GetFile("Bin", relativeDevFolder, fileName);
        }

        public static string GetConfigFile(string fileName, string relativeDevFolder)
        {
            return GetFile("Config", relativeDevFolder, fileName);
        }

        public static string GetDataFile(string fileName, string relativeDevFolder)
        {
            return GetFile("Data", relativeDevFolder, fileName);
        }

        public static string[] GetDataFiles(string[] fileNames, string relativeDevFolder)
        {
            return GetFiles("Data", relativeDevFolder, fileNames);
        }

        private static string GetFile(string folderName, string relativeDevFolder, string fileName)
        {
            return GetFiles(folderName, relativeDevFolder, new[] { fileName })[0];
        }

        private static string[] GetFiles(string folderName, string relativeDevFolder, string[] fileNames)
        {
            var files = GetInstallFiles(folderName, fileNames);
            if (files != null)
                return files;

            files = GetDevInstallFiles(folderName, relativeDevFolder, fileNames);
            if (files == null)
                throw new FileNotFoundException(folderName);

            return files;
        }

        private static string[] GetInstallFiles(string folderName, string[] fileNames)
        {
            var installFolder = RuntimeEnvironment.GetInstallFolder();

            // First look for the folder right under the install directory.

            var installConfigFolder = Path.Combine(installFolder, folderName);
            return Directory.Exists(installConfigFolder)
                ? GetFiles(installConfigFolder, fileNames)
                : null;
        }

        private static string[] GetDevInstallFiles(string folderName, string relativeDevFolder, string[] fileNames)
        {
            var installFolder = RuntimeEnvironment.GetInstallFolder();

            // The install folder in dev points to the top of the source folder.
            // Look in installFolder\relativeDevFolder\bin and installFolder\<top relativeDevFolder>\folderName

            // Construct the path.

            string devInstallFolder = null;
            try
            {
                devInstallFolder = Path.Combine(Path.Combine(installFolder, relativeDevFolder), "Bin");
            }
            catch
            {
                // This may fail if the install folder is directly under the root, eg. C:\Framework - that's fine.
            }

            if (devInstallFolder != null)
            {
                string[] files = null;
                if (Directory.Exists(devInstallFolder))
                    files = GetFiles(devInstallFolder, fileNames);
                if (files != null)
                    return files;
            }

            var pos = relativeDevFolder.IndexOf(Path.DirectorySeparatorChar);
            var topRelativeDevFolder = pos == -1 ? relativeDevFolder : relativeDevFolder.Substring(0, pos);

            devInstallFolder = Path.Combine(installFolder, topRelativeDevFolder);
            devInstallFolder = Path.Combine(Path.Combine(devInstallFolder, "Install"), folderName);

            return Directory.Exists(devInstallFolder)
                ? GetFiles(devInstallFolder, fileNames)
                : null;
        }

        private static string[] GetFiles(string folder, string[] fileNames)
        {
            // Check that each file exists.

            var files = new string[fileNames.Length];
            for (var index = 0; index < fileNames.Length; ++index)
            {
                var file = Path.Combine(folder, fileNames[index]);
                if (!File.Exists(file))
                    return null;
                files[index] = file;
            }

            return files;
        }
    }
}