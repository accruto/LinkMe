using System.IO;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public sealed class AssembleFile
    {
        private AssembleFile()
        {
        }

        public static string ConvertToAdjustedPath(string path, string configuration)
        {
            // Check whether the files exists in the current path.

            if ( System.IO.File.Exists(path) )
                return path;

            string originalPath = path;
            if (Path.GetExtension(path) == Constants.File.Dll.Extension || Path.GetExtension(path) == Constants.File.Exe.Extension)
            {
                string baseFolder = Path.GetDirectoryName(path);
                string fileName = Path.GetFileName(path);

                // Look for it in ".\bin\".

                string outputFolder = Constants.Folder.Bin;
                path = Path.Combine(Path.Combine(baseFolder, outputFolder), fileName);
                if ( System.IO.File.Exists(path) )
                    return path;

                // Look for it in ".\bin\<config>".

                outputFolder = Path.Combine(Constants.Folder.Bin, configuration);
                path = Path.Combine(Path.Combine(baseFolder, outputFolder), fileName);
                if ( System.IO.File.Exists(path) )
                    return path;

                // Look for it in .\<config>.

                outputFolder = configuration;
                path = Path.Combine(Path.Combine(baseFolder, outputFolder), fileName);
                if ( System.IO.File.Exists(path) )
                    return path;
            }

            // Not found, just return anyway.

            return originalPath;
        }

        public static string ConvertFromAdjustedPath(string path, string configuration)
        {
            // Remove ".\bin\" or ".\bin\<config>" or ".\<config>" for .dll & .exe files.

            if (Path.GetExtension(path) == Constants.File.Dll.Extension || Path.GetExtension(path) == Constants.File.Exe.Extension)
            {
                // Split the folder.

                string fileName = Path.GetFileName(path);
                string folder = Path.GetDirectoryName(path);

                // Remove configuration.

                string pattern = Path.DirectorySeparatorChar + configuration.ToLower();
                if ( folder.ToLower().EndsWith(pattern) )
                    folder = folder.Substring(0, folder.Length - pattern.Length);

                // Remove bin.

                pattern = Path.DirectorySeparatorChar + Constants.Folder.Bin.ToLower();
                if ( folder.ToLower().EndsWith(pattern) )
                    folder = folder.Substring(0, folder.Length - pattern.Length);

                return Path.Combine(folder, fileName);
            }

            return path;
        }
    }
}