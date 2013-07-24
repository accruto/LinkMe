using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using File=LinkMe.Environment.Build.Tasks.Constants.File;

namespace LinkMe.Environment.Build.Tasks.Package
{
    public class CreateMergeModule
        : Task
    {
        public CreateMergeModule()
        {
            m_options = new ModuleOptions();
        }

        public override bool Execute()
        {
            try
            {
                // Initialise everything.

                Initialise();

                // Create a merge module for both x86 and x64.

                Create(true);
                Create(false);

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

        [Required]
        public string ModuleGuid
        {
            get
            {
                return m_options.Guid.ToString("B");
            }
            set
            {
                try
                {
                    m_options.Guid = new System.Guid(value);
                }
                catch ( System.Exception )
                {
                    m_options.Guid = System.Guid.Empty;
                }
            }
        }

        public string Manufacturer
        {
            get { return m_options.Manufacturer; }
            set { m_options.Manufacturer = value; }
        }

        public string MergeModuleFile
        {
            get { return m_options.MergeModuleFile; }
            set { m_options.MergeModuleFile = value; }
        }

        public string Version
        {
            get
            {
                return m_options.Version == null ? null : m_options.Version.ToString();
            }
            set
            {
                if ( value == null )
                {
                    m_options.Version = null;
                }
                else
                {
                    try
                    {
                        m_options.Version = new System.Version(value);
                    }
                    catch ( System.Exception )
                    {
                        m_options.Version = null;
                    }
                }
            }
        }

        [Required]
        public ITaskItem[] Catalogues
        {
            get { return m_catalogues; }
            set { m_catalogues = value; }
        }

        private void Initialise()
        {
            // OutputFolder should be absolute.

            m_options.OutputFolder = FilePath.GetAbsolutePath(m_options.OutputFolder, Path.GetDirectoryName(m_options.ProjectFullPath));

            // MergeModuleFile should be an absolute path.

            if ( string.IsNullOrEmpty(m_options.MergeModuleFile) )
                m_options.MergeModuleFile = Path.GetFileNameWithoutExtension(m_options.ProjectFullPath) + File.Msm.Extension;
            if ( !FilePath.IsAbsolutePath(m_options.MergeModuleFile) )
                m_options.MergeModuleFile = FilePath.GetAbsolutePath(m_options.MergeModuleFile, m_options.OutputFolder);
            if ( !string.Equals(Path.GetExtension(m_options.MergeModuleFile), File.Msm.Extension, System.StringComparison.OrdinalIgnoreCase) )
                m_options.MergeModuleFile += File.Msm.Extension;
        }

        private void Finalise()
        {
        }

        private string GetCatalogueFullPath(ITaskItem catalogue)
        {
            return FilePath.GetAbsolutePath(catalogue.ItemSpec, Path.GetDirectoryName(m_options.ProjectFullPath));
        }

        private void Create(bool isWin64)
        {
            try
            {
                using (ModuleMerger merger = new ModuleMerger(isWin64, m_options, Log))
                {
                    // Iterate over each item.

                    foreach (ITaskItem catalogue in m_catalogues)
                    {
                        // Load the catalogue.

                        string catalogueFullPath = GetCatalogueFullPath(catalogue);
                        merger.Load(catalogueFullPath);
                    }

                    merger.CreateMergeModule();
                }
            }
            catch (Exception ex)
            {
                Log.LogError("Exception caught whilst trying to create the merge module: " + ex);
            }

            Log.LogMessage(MessageImportance.High, "Merge module '{0}' created.", m_options.MergeModuleFile);
        }

        private ModuleOptions m_options;
        private ITaskItem[] m_catalogues;
    }
}