using System.IO;
using System.Reflection;
using LinkMe.Environment.CommandLines;

namespace LinkMe.Environment.Util.Commands
{
    public abstract class UtilCommand
        : Command
    {
        private static readonly string m_entryAssemblyLocation;
        private static readonly string m_entryAssemblyFileName;

        static UtilCommand()
        {
            m_entryAssemblyLocation = Assembly.GetEntryAssembly().Location;
            m_entryAssemblyFileName = Path.GetFileName(m_entryAssemblyLocation);
        }

        private static string EntryAssemblyLocation
        {
            get { return m_entryAssemblyLocation; }
        }

        protected static string EntryAssemblyFileName
        {
            get { return m_entryAssemblyFileName; }
        }

        protected static string GetAssemblyFolder()
        {
            return Path.GetDirectoryName(EntryAssemblyLocation);
        }

        protected static string GetInstallFolder()
        {
            // Set it to the folder where the assembly is located.

            string installFolder = GetAssemblyFolder();

            // Move up past the Bin folder if needed.

            if (string.Compare(Path.GetFileName(installFolder), Environment.Constants.Folder.Bin, true) == 0)
                installFolder = Path.GetDirectoryName(installFolder);

            // Move up past the Install folder if needed.

            if (string.Compare(Path.GetFileName(installFolder), Environment.Constants.Folder.Install, true) == 0)
            {
                string parentFolder = Path.GetDirectoryName(installFolder);
                if (string.Compare(Path.GetFileName(parentFolder), Environment.Constants.Folder.Build, true) != 0)
                    installFolder = Path.GetDirectoryName(installFolder);
            }

            // Move up past the last parts of the assembly name if needed.

            AssemblyName assemblyName = AssemblyName.GetAssemblyName(EntryAssemblyLocation);
            string[] nameParts = assemblyName.Name.Split('.');

            for (var index = nameParts.Length - 1; index > 0; --index)
            {
                if (string.Compare(Path.GetFileName(installFolder), nameParts[index], true) == 0)
                    installFolder = Path.GetDirectoryName(installFolder);
                else
                    break;
            }

            return installFolder;
        }
    }
}