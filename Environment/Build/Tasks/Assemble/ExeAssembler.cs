using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class ExeAssembler
        : ArtifactAssembler
    {
        public ExeAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
            : base(artifact, sourceFullPath, destinationFullPath, options, log)
        {
        }

        public override void Assemble(Action action)
        {
            // Copy the file itself.

            Copy(SourceFullPath);

            // Copy associated files.

            SetAssociatedFile();
            CopyAssociatedFile(SourceFullPath, Constants.File.Xml.Extension, ExtensionAction.Replace);
            CopyAssociatedFile(SourceFullPath, Constants.File.Config.Extension, ExtensionAction.Add);
            CopyAssociatedFile(SourceFullPath, Constants.File.Pdb.Extension, ExtensionAction.Replace);
            ClearAssociatedFile();
        }

        public override void Clean(Action action)
        {
            // Clean associated files.

            SetAssociatedFile();
            CleanAssociatedFile(DestinationFullPath, Constants.File.Pdb.Extension, ExtensionAction.Replace);
            CleanAssociatedFile(DestinationFullPath, Constants.File.Config.Extension, ExtensionAction.Add);
            CleanAssociatedFile(DestinationFullPath, Constants.File.Xml.Extension, ExtensionAction.Replace);
            ClearAssociatedFile();

            if ( System.IO.File.Exists(DestinationFullPath) )
            {
                // Delete the file itself.

                Delete(DestinationFullPath);
            }
        }
    }
}