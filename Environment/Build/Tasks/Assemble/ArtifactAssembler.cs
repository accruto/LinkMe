using System.IO;
using LinkMe.Environment.Build.Tasks.Resources;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal enum ExtensionAction
    {
        Replace,
        Add
    }

    internal abstract class ArtifactAssembler
    {
        protected ArtifactAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
        {
            m_artifact = artifact;
            m_sourceFullPath = GetAdjustedSourceFullPath(sourceFullPath, options.Configuration);
            m_destinationFullPath = destinationFullPath;
            m_options = options;
            m_log = log;
        }

        public abstract void Assemble(Action action);
        public abstract void Clean(Action action);

        protected AssembleOptions Options
        {
            get { return m_options; }
        }

        protected Artifact Artifact
        {
            get { return m_artifact; }
        }

        protected string SourceFullPath
        {
            get { return m_sourceFullPath; }
        }

        protected string DestinationFullPath
        {
            get { return m_destinationFullPath; }
        }

        public string DestinationFolder
        {
            get { return Path.GetDirectoryName(m_destinationFullPath); }
        }

        protected TaskLoggingHelper Log
        {
            get { return m_log; }
        }

        private static string GetAdjustedSourceFullPath(string sourceFullPath, string configuration)
        {
            string adjustedSourceFullPath = AssembleFile.ConvertToAdjustedPath(sourceFullPath, configuration);
            return adjustedSourceFullPath;
        }

        protected string Copy(string fullPath)
        {
            return Copy(fullPath, DestinationFolder);
        }

        protected string Copy(string fullPath, string destinationFolder)
        {
            // Make sure the destination folder exists.

            if ( !Directory.Exists(destinationFolder) )
                Directory.CreateDirectory(destinationFolder);

            // Copy the file.

            string destinationFullPath = Path.Combine(destinationFolder, Path.GetFileName(fullPath));
            System.IO.File.Copy(fullPath, destinationFullPath, true);

            // Ensure that the file can be copied over in the future.

            ClearReadOnly(destinationFullPath);

            LogMessage(true, Messages.FileCopied, fullPath, destinationFullPath);
            return destinationFullPath;
        }

        protected void Delete(string fullPath)
        {
            if ( System.IO.File.Exists(fullPath) )
            {
                System.IO.File.Delete(fullPath);
                LogMessage(true, Messages.FileDeleted, fullPath);
            }
        }

        protected void CopyAssociatedFile(string sourceFullPath, string extension, ExtensionAction action)
        {
            // Look for a file with the same name but different extension.

            string fileFullPath = action == ExtensionAction.Replace ? Path.ChangeExtension(sourceFullPath, extension) : sourceFullPath + extension;
            if ( System.IO.File.Exists(fileFullPath) )
            {
                Copy(fileFullPath);
                Artifact.AddAssociatedArtifact(Path.Combine(Path.GetDirectoryName(Artifact.ProjectRelativePath), Path.GetFileName(fileFullPath)));
            }
        }

        protected void CleanAssociatedFile(string destinationFullPath, string extension, ExtensionAction action)
        {
            // Look for a file with the same name but different extension.

            string fileFullPath = action == ExtensionAction.Replace ? Path.ChangeExtension(destinationFullPath, extension) : destinationFullPath + extension;
            Delete(fileFullPath);
        }

        private static void ClearReadOnly(string fullPath)
        {
            // Clear the ReadOnly flag.

            FileAttributes attributes = System.IO.File.GetAttributes(fullPath);
            if ( (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly )
            {
                attributes &= ~FileAttributes.ReadOnly;
                System.IO.File.SetAttributes(fullPath, attributes);
            }
        }

        protected void LogMessage(string message)
        {
            LogMessage(false, message, null);
        }

        protected void LogMessage(string message, params object[] args)
        {
            LogMessage(false, message, args);
        }

        private void LogMessage(bool isMainLog, string message, params object[] args)
        {
            if ( isMainLog )
                m_log.LogMessage(m_fileIndentation != string.Empty ? MessageImportance.Low : MessageImportance.High, m_fileIndentation + message, args);
            else
                m_log.LogMessage(MessageImportance.Low, m_fileIndentation + m_indentation + message, args);
        }

        protected void SetAssociatedFile()
        {
            m_fileIndentation = m_indentation;
        }

        protected void ClearAssociatedFile()
        {
            m_fileIndentation = string.Empty;
        }

        protected bool IsAssociatedFile()
        {
            return m_fileIndentation != string.Empty;
        }

        private Artifact m_artifact;
        private string m_sourceFullPath;
        private string m_destinationFullPath;
        private AssembleOptions m_options;
        private TaskLoggingHelper m_log;
        private string m_indentation = new string('\t', 1);
        private string m_fileIndentation = string.Empty;
    }
}