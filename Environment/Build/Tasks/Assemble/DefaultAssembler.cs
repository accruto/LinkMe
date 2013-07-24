using System.IO;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class DefaultAssembler
        : ArtifactAssembler
    {
        public DefaultAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
            : base(artifact, sourceFullPath, destinationFullPath, options, log)
        {
        }

        public override void Assemble(Action action)
        {
            // Copy the file.

            Copy(SourceFullPath);
        }

        public override void Clean(Action action)
        {
            // Delete the file.

            Delete(DestinationFullPath);
        }
    }
}