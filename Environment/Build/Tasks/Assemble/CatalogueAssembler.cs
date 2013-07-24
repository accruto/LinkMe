using System.IO;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class CatalogueAssembler
        : ArtifactAssembler
    {
        public CatalogueAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
            : base(artifact, sourceFullPath, destinationFullPath, options, log)
        {
        }

        public override void Assemble(Action action)
        {
            // Copy the source itself.

            Copy(SourceFullPath);

            // Load the catalogue.

            Tasks.Catalogue catalogue = LoadCatalogue();

            if (catalogue.Artifacts.Count > 0)
            {
                // Determine the root folder for the artifacts within the catalogue.

                string rootFolder = FilePath.GetAbsolutePath(catalogue.RootFolder, Path.GetDirectoryName(SourceFullPath));

                using (Assembler assembler = new Assembler(Options, Log))
                {
                    // Iterate over each artifact.

                    foreach (Artifact artifact in catalogue.Artifacts)
                    {
                        // Assemble the file from where it was assembled as part of the catalogue.

                        string artifactSourceFullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolder);
                        string artifactDestinationFullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, Options.OutputFolder);

                        // For artifacts in catalogues do not do the full assemble, just copy.

                        assembler.CopyArtifact(artifact, action, artifactSourceFullPath, artifactDestinationFullPath);

                        // Copy associated artifacts as well.

                        foreach (Artifact associatedArtifact in artifact.AssociatedArtifacts)
                        {
                            artifactSourceFullPath = FilePath.GetAbsolutePath(associatedArtifact.ProjectRelativePath, rootFolder);
                            artifactDestinationFullPath = FilePath.GetAbsolutePath(associatedArtifact.ProjectRelativePath, Options.OutputFolder);
                            assembler.CopyArtifact(associatedArtifact, action, artifactSourceFullPath, artifactDestinationFullPath);
                        }
                    }
                }
            }
        }

        public override void Clean(Action action)
        {
            // Load the catalogue.

            Tasks.Catalogue catalogue = LoadCatalogue();

            // Determine the root folder for the artifacts within the catalogue.

            if (catalogue.RootFolder != string.Empty)
            {
                string rootFolder = FilePath.GetAbsolutePath(catalogue.RootFolder, Path.GetDirectoryName(SourceFullPath));

                using (Assembler assembler = new Assembler(Options, Log))
                {
                    // Iterate over each artifact.

                    foreach (Artifact artifact in catalogue.Artifacts)
                    {
                        // Assemble the file from where it was assembled as part of the catalogue.

                        string artifactSourceFullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, rootFolder);
                        string artifactDestinationFullPath = FilePath.GetAbsolutePath(artifact.ProjectRelativePath, Options.OutputFolder);

                        assembler.DeleteArtifact(artifact, action, artifactSourceFullPath, artifactDestinationFullPath);
                    }
                }
            }

            // Delete the file itself.

            Delete(DestinationFullPath);
        }

        private Tasks.Catalogue LoadCatalogue()
        {
            Tasks.Catalogue catalogue = new Tasks.Catalogue();
            if (System.IO.File.Exists(SourceFullPath))
                catalogue.Load(SourceFullPath);
            return catalogue;
        }
    }
}