using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public class AssembleClean
        : AssembleTask
    {
        #region Constructor

        public AssembleClean()
        {
        }

        #endregion

        #region Task

        public override bool Execute()
        {
            try
            {
                // Initialise everything.

                Initialise();

                // Clean the catalogue file.

                CleanCatalogueFile();

                using (Assembler assembler = new Assembler(Options, Log))
                {
                    // Clean all artifacts.

                    CleanArtifacts(assembler, CreateAssembleArtifacts(false), Action.Assemble);
                    CleanArtifacts(assembler, CreateCopyOnBuildArtifacts(false), Action.CopyOnBuild);
                }

                // Clean up the output folder.

                CleanOutputFolder();

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

        private void CleanArtifacts(Assembler assembler, LinkedList<KeyValuePair<string, Artifact>> artifacts, Action action)
        {
            // Iterate over each artifact.

            foreach ( KeyValuePair<string, Artifact> pair in artifacts )
            {
                string sourcePath = pair.Key;
                Artifact artifact = pair.Value;

                // Determine the paths for the artifact.

                string sourceFullPath = GetSourceFullPath(sourcePath);
                string destinationFullPath = GetDestinationFullPath(artifact);

                // Clean it.

                assembler.CleanArtifact(artifact, action, sourceFullPath, destinationFullPath);
            }
        }

        private void CleanOutputFolder()
        {
            // Iterate.

            CleanFolder(Options.OutputFolder);
        }

        private void CleanFolder(string folder)
        {
            if ( Directory.Exists(folder) )
            {
                // Iterate over child folders.

                foreach ( string childFolder in Directory.GetDirectories(folder) )
                    CleanFolder(childFolder);

                // Only delete the folder if there is nothing in it.

                if ( Directory.GetDirectories(folder).Length == 0 && Directory.GetFiles(folder).Length == 0 )
                    Directory.Delete(folder);
            }
        }

        private void CleanCatalogueFile()
        {
            // Delete the file.

            if ( System.IO.File.Exists(Options.CatalogueFile) )
                System.IO.File.Delete(Options.CatalogueFile);
        }

        #endregion
    }
}