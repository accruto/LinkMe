using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using File=LinkMe.Environment.Build.Tasks.Constants.File;

namespace LinkMe.Environment.Build.Tasks.Package
{
    public class CleanMergeModule
        : Task
    {
        public CleanMergeModule()
        {
            m_options = new ModuleOptions();
        }

        public override bool Execute()
        {
            try
            {
                // Initialise everything.

                Initialise();

                // Simply delete the merge module file.

                if (System.IO.File.Exists(MergeModuleFile))
                    System.IO.File.Delete(MergeModuleFile);

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

        [Required]
        public string ProjectFullPath
        {
            get { return m_options.ProjectFullPath; }
            set { m_options.ProjectFullPath = value; }
        }

        [Required]
        public string OutputPath
        {
            get { return m_options.OutputFolder; }
            set { m_options.OutputFolder = value; }
        }

        public string MergeModuleFile
        {
            get { return m_options.MergeModuleFile; }
            set { m_options.MergeModuleFile = value; }
        }

        private void Initialise()
        {
            // OutputFolder should be absolute.

            m_options.OutputFolder = FilePath.GetAbsolutePath(m_options.OutputFolder, Path.GetDirectoryName(m_options.ProjectFullPath));

            // MergeModuleFile should be an absolute path.

            if (string.IsNullOrEmpty(m_options.MergeModuleFile))
                m_options.MergeModuleFile = Path.GetFileNameWithoutExtension(m_options.ProjectFullPath) + File.Msm.Extension;
            if (!FilePath.IsAbsolutePath(m_options.MergeModuleFile))
                m_options.MergeModuleFile = FilePath.GetAbsolutePath(m_options.MergeModuleFile, m_options.OutputFolder);
            if (!string.Equals(Path.GetExtension(m_options.MergeModuleFile), File.Msm.Extension, System.StringComparison.OrdinalIgnoreCase))
                m_options.MergeModuleFile += File.Msm.Extension;
        }

        private void Finalise()
        {
        }

        private ModuleOptions m_options;
    }
}