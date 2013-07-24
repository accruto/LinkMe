using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public sealed class AssembleFiles
        : AssembleTask
    {
        #region Task

        public override bool Execute()
        {
            try
            {
                // Initialise everything.

                Initialise();

                var catalogue = new Catalogue();

                using (var assembler = new Assembler(Options, Log))
                {
                    // Assemble all artifacts.

                    AssembleArtifacts(catalogue, assembler, CreateAssembleArtifacts(true), Action.Assemble);
                    AssembleArtifacts(catalogue, assembler, CreateCopyOnBuildArtifacts(true), Action.CopyOnBuild);
                }

                // Save the catalogue file.

                catalogue.Save(Options.OutputFolder, Options.CatalogueFile, Options.CatalogueFileGuid);

                // Finalise everything.

                Finalise();
                return true;
            }
            catch ( System.Exception e )
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

        public ITaskItem[] Assembles
        {
            get { return AssembleItems; }
            set { AssembleItems = value; }
        }

        public ITaskItem[] CopyOnBuilds
        {
            get { return CopyOnBuildItems; }
            set { CopyOnBuildItems = value; }
        }

        #endregion

        #region Private Members

        private void AssembleArtifacts(Catalogue catalogue, Assembler assembler, IEnumerable<KeyValuePair<string, Artifact>> artifacts, Action action)
        {
            // Iterate over each artifact.

            foreach ( KeyValuePair<string, Artifact> pair in artifacts )
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