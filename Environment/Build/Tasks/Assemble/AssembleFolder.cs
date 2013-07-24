using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public sealed class AssembleFolder
        : AssembleTask
    {
        private string _subDirectory;
        private string _extensionFilter;

        #region Task

        public override bool Execute()
        {
            try
            {
                // Initialise everything.

                Initialise();

                Tasks.Catalogue catalogue = new Tasks.Catalogue();

                using (Assembler assembler = new Assembler(Options, Log))
                {
                    // Assemble all artifacts.

                    AssembleArtifacts(catalogue, assembler, CreateArtifacts(), Action.Assemble);
                }

                // Save the catalogue file.

                catalogue.Save(Options.OutputFolder, Options.CatalogueFile, Options.CatalogueFileGuid);

                // Finalise everything.

                Finalise();
                return true;
            }
            catch (System.Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        #endregion

        #region Properties

        [Required]
        public string ProjectFullPath
        {
            get { return Options.ProjectFullPath; }
            set { Options.ProjectFullPath = value; }
        }

        [Required]
        public string OutputPath
        {
            get { return Options.OutputFolder; }
            set { Options.OutputFolder = value; }
        }

        public string SubDirectory
        {
            get { return _subDirectory; }
            set { _subDirectory = value; }
        }

        public string ExtensionFilter
        {
            get { return _extensionFilter; }
            set { _extensionFilter = value; }
        }

        public string CatalogueFile
        {
            get { return Options.CatalogueFile; }
            set { Options.CatalogueFile = value; }
        }

        public string CatalogueFileGuid
        {
            get { return Options.CatalogueFileGuid; }
            set { Options.CatalogueFileGuid = value; }
        }

        public string Configuration
        {
            get { return Options.Configuration; }
            set { Options.Configuration = value; }
        }

        public ITaskItem[] AssembleFolders
        {
            get { return AssembleItems; }
            set { AssembleItems = value; }
        }

        #endregion

        #region Private Members

        private LinkedList<KeyValuePair<string, Artifact>> CreateArtifacts()
        {
            // Create all artifacts for all sources.

            var artifacts = new Dictionary<string, Artifact>();
            ITaskItem[] items = AssembleItems;
            if (items != null)
            {
                foreach (ITaskItem item in items)
                {
                    // Each item points to a folder.

                    CreateArtifacts(item, artifacts);
                }
            }

            return CreateArtifactsAscending(artifacts);
        }

        private void CreateArtifacts(ITaskItem item, IDictionary<string, Artifact> artifacts)
        {
            var path = GetSourceFullPath(item.ItemSpec);
            if (path.EndsWith("\\"))
                path = path.Substring(0, path.Length - 1);
            CreateArtifacts(path, path, artifacts);
        }

        private void CreateArtifacts(string folderPath, string path, IDictionary<string, Artifact> artifacts)
        {
            // Grab all files.

            foreach (var filePath in Directory.GetFiles(path))
            {
                // Don't add it if it will be added as an associated artifact.

                if (!IsAssociatedFile(filePath) && IsFiltered(filePath))
                {
                    var relativePath = FilePath.GetRelativePath(filePath, folderPath);
                    if (!string.IsNullOrEmpty(_subDirectory))
                        relativePath = Path.Combine(_subDirectory, relativePath);
                    var artifact = CreateArtifact(filePath, relativePath, new Dictionary<string, string> { { Constants.Project.Item.Assemble.Guid.Name, Guid.NewGuid().ToString() } });
                    artifacts.Add(filePath, artifact);
                }
            }

            // Iterate.

            foreach (var directoryPath in Directory.GetDirectories(path))
                CreateArtifacts(folderPath, directoryPath, artifacts);
        }

        private bool IsFiltered(string path)
        {
            if (string.IsNullOrEmpty(_extensionFilter))
                return true;

            return Path.GetExtension(path) == _extensionFilter;
        }

        private static bool IsAssociatedFile(string path)
        {
            if (Path.GetExtension(path) == ".pdb" && System.IO.File.Exists(Path.ChangeExtension(path, ".dll")))
                return true;

            return false;
        }

        private void AssembleArtifacts(Tasks.Catalogue catalogue, Assembler assembler, IEnumerable<KeyValuePair<string, Artifact>> artifacts, Action action)
        {
            // Iterate over each artifact.

            foreach (KeyValuePair<string, Artifact> pair in artifacts)
            {
                string sourcePath = pair.Key;
                Artifact artifact = pair.Value;

                // Determine the paths for the artifact.

                string sourceFullPath = GetSourceFullPath(sourcePath);
                string destinationFullPath = GetDestinationFullPath(artifact);

                // Assemble it.

                assembler.AssembleArtifact(artifact, action, sourceFullPath, destinationFullPath);
                catalogue.Add(artifact);
            }
        }

        #endregion
    }
}