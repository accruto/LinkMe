using System.IO;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class Assembler
        : Worker
    {
        public Assembler(AssembleOptions options, TaskLoggingHelper log)
        {
            m_options = options;
            m_log = log;
        }

        public void AssembleArtifact(Artifact artifact, Action action, string sourceFullPath, string destinationFullPath)
        {
            ArtifactAssembler assembler = CreateArtifactAssembler(artifact, sourceFullPath, destinationFullPath);
            assembler.Assemble(action);
        }

        public void CopyArtifact(Artifact artifact, Action action, string sourceFullPath, string destinationFullPath)
        {
            ArtifactAssembler assembler = CreateArtifactCopier(artifact, sourceFullPath, destinationFullPath);
            assembler.Assemble(action);
        }

        public void CleanArtifact(Artifact artifact, Action action, string sourceFullPath, string destinationFullPath)
        {
            ArtifactAssembler assembler = CreateArtifactAssembler(artifact, sourceFullPath, destinationFullPath);
            assembler.Clean(action);
        }

        public void DeleteArtifact(Artifact artifact, Action action, string sourceFullPath, string destinationFullPath)
        {
            ArtifactAssembler assembler = CreateArtifactCopier(artifact, sourceFullPath, destinationFullPath);
            assembler.Clean(action);
        }

        private ArtifactAssembler CreateArtifactAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath)
        {
            // Assembler is determined by the artifact file extension.

            switch ( Path.GetExtension(sourceFullPath).ToLower() )
            {
                case Constants.File.Dll.Extension:
                    return new DllAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log, GetGac());

                case Constants.File.Exe.Extension:
                    return new ExeAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);

//				case Constants.File.Tlb.Extension:
//					return new TlbAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);

                case Constants.File.Catalogue.Extension:
                    return new CatalogueAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);

                case Constants.File.Reg.Extension:
                    return new RegAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);

                default:
                    return new DefaultAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);
            }
        }

        private ArtifactAssembler CreateArtifactCopier(Artifact artifact, string sourceFullPath, string destinationFullPath)
        {
            // Assembler is determined by the artifact file extension.

            switch (Path.GetExtension(sourceFullPath).ToLower())
            {
                case Constants.File.Catalogue.Extension:
                    return new CatalogueAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);

                default:
                    return new DefaultAssembler(artifact, sourceFullPath, destinationFullPath, m_options, m_log);
            }
        }

        private readonly AssembleOptions m_options;
        private readonly TaskLoggingHelper m_log;
    }
}