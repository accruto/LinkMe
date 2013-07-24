using System.IO;
using Microsoft.Tools.WindowsInstallerXml.Serialize;
using Directory=Microsoft.Tools.WindowsInstallerXml.Serialize.Directory;

namespace LinkMe.Environment.Build.Tasks
{
    public class WixModuleLoader
        : WixDocumentLoader
    {
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public System.Version Version
        {
            get { return m_version == null ? null : (System.Version)m_version.Clone(); }
            set { m_version = value; }
        }

        public System.Guid Guid
        {
            get { return m_guid; }
            set { m_guid = value; }
        }

        public string Manufacturer
        {
            get { return m_manufacturer; }
            set { m_manufacturer = value; }
        }

        public void Load(bool isWin64, Catalogue catalogue)
        {
            // Get the root folder path.

            string rootFolderPath;
            if (string.IsNullOrEmpty(catalogue.RootFolder))
                rootFolderPath = Path.GetDirectoryName(catalogue.FullPath);
            else
                rootFolderPath = FilePath.GetAbsolutePath(catalogue.RootFolder, Path.GetDirectoryName(catalogue.FullPath));

            // Create the module element.

            Module module = CreateModule();
            Wix.AddChild(module);

            // Create the package element.

            Microsoft.Tools.WindowsInstallerXml.Serialize.Package package = CreatePackage(isWin64);
            module.AddChild(package);

            // Create a target directory element.

            Directory directory = CreateDirectory(Constants.Wix.Xml.Directory.Target.Id, Constants.Wix.Xml.Directory.Target.Name);
            module.AddChild(directory);

            // Load the artifacts in the catalogue.

            foreach (Artifact artifact in catalogue.Artifacts)
                Load(isWin64, artifact, rootFolderPath, directory);

            // Add the catalogue file itself.

            Artifact catalogueArtifact = new Artifact(FilePath.GetRelativePath(catalogue.FullPath, rootFolderPath));
            catalogueArtifact.SetMetadata(Constants.Catalogue.Artifact.Guid, catalogue.Guid);
            Load(isWin64, catalogueArtifact, rootFolderPath, directory);

            // Resolve all elements.

            Resolve(directory);
        }

        private Module CreateModule()
        {
            Module module = new Module();
            module.Id = m_name;
            module.Language = Constants.Wix.Xml.LanguageDefault;
            module.Codepage = Constants.Wix.Xml.CodepageDefault;
            module.Version = m_version == null ? Constants.Wix.Xml.VersionDefault : m_version.ToString();
            return module;
        }

        private Microsoft.Tools.WindowsInstallerXml.Serialize.Package CreatePackage(bool isWin64)
        {
            Microsoft.Tools.WindowsInstallerXml.Serialize.Package package = new Microsoft.Tools.WindowsInstallerXml.Serialize.Package();
            package.Id = m_guid.ToString("D");
            package.Manufacturer = m_manufacturer;
            if (isWin64)
                package.Platforms = "x64";
            return package;
        }

        private void Load(bool isWin64, Artifact artifact, string rootFolderPath, Directory rootDirectory)
        {
            // Get the parent directory for the component.

            string artifactFolder = Path.GetDirectoryName(artifact.ProjectRelativePath);
            Directory directory = GetDirectory(rootDirectory, artifactFolder);
            if (string.IsNullOrEmpty(artifactFolder))
                directory.FileSource = rootFolderPath;
            else
                directory.FileSource = FilePath.GetAbsolutePath(artifactFolder, rootFolderPath);

            // Create a component loader for the artifact and load everything.

            WixComponentLoader componentLoader = new WixComponentLoader();
            componentLoader.Load(isWin64, artifact, rootFolderPath, rootDirectory, directory);
        }

        private string m_name;
        private System.Version m_version;
        private System.Guid m_guid;
        private string m_manufacturer;
    }
}