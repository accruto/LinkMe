namespace LinkMe.Environment.Build.Tasks.Package
{
    public class ModuleOptions
        : Options
    {
        public string OutputFolder { get; set; }
        public string MergeModuleFile { get; set; }
        public string Manufacturer { get; set; }
        public System.Guid Guid { get; set; }

        public System.Version Version
        {
            get { return m_version == null ? null : (System.Version) m_version.Clone(); }
            set { m_version = value; }
        }

        private System.Version m_version;
    }
}