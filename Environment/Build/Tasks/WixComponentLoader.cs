using System.IO;
using Microsoft.Tools.WindowsInstallerXml.Serialize;

using Directory = Microsoft.Tools.WindowsInstallerXml.Serialize.Directory;
using File = Microsoft.Tools.WindowsInstallerXml.Serialize.File;
using Wix = LinkMe.Environment.Build.Tasks.Constants.Wix;

namespace LinkMe.Environment.Build.Tasks
{
    public class WixComponentLoader
        : WixFragmentLoader
    {
        public void Load(bool isWin64, Artifact artifact, string rootFolderPath, Directory rootDirectory, Directory directory)
        {
            // Create a component for this artifact.

            string guid = artifact.GetMetadata(Constants.Catalogue.Artifact.Guid);
            if ( string.IsNullOrEmpty(guid) )
                guid = System.Guid.Empty.ToString();
            Component component = CreateComponent(isWin64, artifact.ProjectRelativePath, new System.Guid(guid));
            directory.AddChild(component);

            // Load all details into the component.

            Load(isWin64, artifact, rootFolderPath, rootDirectory, directory, component);
        }

        private void Load(bool isWin64, Artifact artifact, string rootFolderPath, Directory rootDirectory, Directory directory, Component component)
        {
            // Load details based on the type of the artifact.

            switch ( Path.GetExtension(artifact.ProjectRelativePath) )
            {
                case Constants.File.Dll.Extension:
                    if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.Assembly) )
                        LoadAssembly(isWin64, artifact, rootFolderPath, rootDirectory, component);
                    else
                        LoadDll(artifact, rootFolderPath, component);
                    break;

                case Constants.File.Tlb.Extension:
                    LoadTlb(artifact, rootFolderPath, component);
                    break;

                case Constants.File.Reg.Extension:
                    LoadReg(artifact, rootFolderPath, component);
                    break;

                default:
                    LoadDefault(artifact, rootFolderPath, directory, component);
                    break;
            }

            // Iterate over the associated files.

            foreach ( Artifact associatedArtifact in artifact.AssociatedArtifacts )
                Load(isWin64, associatedArtifact, rootFolderPath, rootDirectory, directory, component);
        }

        private void LoadAssembly(bool isWin64, Artifact artifact, string rootFolderPath, Directory rootDirectory, Component component)
        {
            // Create a file.

            File file = CreateFile(artifact.ProjectRelativePath, FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath));
            component.AddChild(file);
            file.KeyPath = YesNoType.yes;
            file.Assembly = File.AssemblyType.net;

            // Set the assembly application which will copy the assembly to the install location, event if it is subsequently installed into the GAC.

            file.AssemblyApplication = file.Id;

            // Check whether to register the file.

            if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.Register) )
                RegisterAssembly(artifact, rootFolderPath, component);
            if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.RegisterPackages) )
                RegisterPackages(artifact, rootFolderPath, component);
            
            // Check whether the assembly is to be loaded into the gac as well.

            if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.InstallInGac) )
                LoadGacAssembly(isWin64, artifact, rootFolderPath, rootDirectory);
        }

        private void LoadGacAssembly(bool isWin64, Artifact artifact, string rootFolderPath, Directory rootDirectory)
        {
            // Create a new component for the gac assembly using the gac guid.

            string guid = artifact.GetMetadata(Constants.Catalogue.Artifact.GacGuid);
            if (string.IsNullOrEmpty(guid))
                guid = System.Guid.Empty.ToString();
            Component component = CreateComponent(isWin64, artifact.ProjectRelativePath, new System.Guid(guid), Wix.Xml.Id.GacPrefix);

            // Put the component into a special directory.

            Directory gacDirectory = GetDirectory(rootDirectory, "Gac");
            gacDirectory.AddChild(component);

            // Create a file, not adding the assembly application property.

            File file = CreateFile(artifact.ProjectRelativePath, FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath), Wix.Xml.Id.GacPrefix);
            component.AddChild(file);
            file.KeyPath = YesNoType.yes;
            file.Assembly = File.AssemblyType.net;
        }

        private void LoadDll(Artifact artifact, string rootFolderPath, Component component)
        {
            // Create a file.

            File file = CreateFile(artifact.ProjectRelativePath, FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath));
            component.AddChild(file);
            file.KeyPath = YesNoType.yes;

            // Check for extra registration.

            if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.Register) )
                RegisterDll(artifact, rootFolderPath, component);
        }

        private void LoadTlb(Artifact artifact, string rootFolderPath, Component component)
        {
            // Create a file.

            File file = CreateFile(artifact.ProjectRelativePath, FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath));
            component.AddChild(file);
            file.KeyPath = YesNoType.yes;

            // Check for extra registration.

            if ( artifact.GetBooleanMetadata(Constants.Catalogue.Artifact.Register) )
                RegisterTypeLib(artifact, rootFolderPath, component);
        }

        private void LoadReg(Artifact artifact, string rootFolderPath, Component component)
        {
            // Load the file.

            RegistryFile registryFile = new RegistryFile();
            registryFile.Load(FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath));

            // Iterate through the keys.

            foreach ( RegistryCaptureKey key in registryFile.Keys )
                Load(key, component);
        }

        private void LoadDefault(Artifact artifact, string rootFolderPath, Directory directory, Component component)
        {
            // Create a file.

            File file = CreateFile(artifact.ProjectRelativePath, FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath));
            component.AddChild(file);

            // Check for short cuts.

            string shortcutName = artifact.GetMetadata(Constants.Catalogue.Artifact.ShortcutName);
            string shortcutPath = artifact.GetMetadata(Constants.Catalogue.Artifact.ShortcutPath);
            if ( !string.IsNullOrEmpty(shortcutName) || !string.IsNullOrEmpty(shortcutPath) )
                CreateShortcut(artifact, directory, shortcutName, shortcutPath, file);
        }

        private void RegisterAssembly(Artifact artifact, string rootFolderPath, Component component)
        {
/*			// Capture the registration.

			string fullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath);
			RegistryCaptureKey captureKey = AssemblyCapture.RegisterAssembly(fullPath);

			// Load the key.

			Load(captureKey, component);
*/
        }

        private void RegisterPackages(Artifact artifact, string rootFolderPath, Component component)
        {
/*			// Capture the registration.

			string fullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath);
			RegistryCaptureKey captureKey = AssemblyCapture.RegisterPackages(fullPath);

			// Load the key.

			Load(captureKey, component);
*/		}

        private void RegisterDll(Artifact artifact, string rootFolderPath, Component component)
        {
/*			// Capture the registration.

			string fullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath);
			RegistryCaptureKey captureKey = ComCapture.RegisterDll(fullPath);

			// Load the key.

			Load(captureKey, component);
*/
        }

        private void RegisterTypeLib(Artifact artifact, string rootFolderPath, Component component)
        {
/*			// Capture the registration.

			string fullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolderPath);
			RegistryCaptureKey captureKey = ComCapture.RegisterTypeLib(fullPath);

			// Load the key.

			Load(captureKey, component);
*/
        }

        private void CreateShortcut(Artifact artifact, Directory directory, string shortcutName, string shortcutPath, File file)
        {
            if ( string.IsNullOrEmpty(shortcutName) )
                shortcutName = Path.GetFileNameWithoutExtension(file.Name);

            // Create the shortcut.

            Shortcut shortcut = CreateShortcut(artifact.ProjectRelativePath, shortcutName);
            if (directory != null)
                shortcut.WorkingDirectory = directory.Id;
            file.AddChild(shortcut);

            // Need to add the shortcut directory.

            Directory targetDirectory = GetTargetDirectory(file);
            Directory programMenuDirectory = GetChildDirectory(targetDirectory, Wix.Xml.Directory.ProgramMenu.Id, Wix.Xml.Directory.ProgramMenu.Name);

            Directory shortcutDirectory;
            if (string.IsNullOrEmpty(shortcutPath))
            {
                shortcutDirectory = programMenuDirectory;
            }
            else
            {
                // Create an application directory with a fixed id (not sure why this has to be like this but tried a few variations which did not seem to work).

                string name;
                int pos = shortcutPath.IndexOf('\\');
                if (pos == -1)
                {
                    name = shortcutPath;
                    shortcutPath = string.Empty;
                }
                else
                {
                    name = shortcutPath.Substring(pos);
                    shortcutPath = shortcutPath.Substring(pos + 1);
                }

                Directory appProgramMenuDirectory = GetChildDirectory(programMenuDirectory, Wix.Xml.Directory.AppProgramMenu.Id, name);
                shortcutDirectory = appProgramMenuDirectory;
                if ( !string.IsNullOrEmpty(shortcutPath) )
                    shortcutDirectory = GetDirectory(shortcutDirectory, shortcutPath);
            }

            shortcut.Directory = shortcutDirectory.Id;
        }
    }
}