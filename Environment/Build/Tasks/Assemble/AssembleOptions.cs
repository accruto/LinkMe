using System;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    [Serializable]
    public class AssembleOptions
        : Options
    {
        public string OutputFolder
        {
            get { return m_outputFolder; }
            set { m_outputFolder = value; }
        }

        public string CatalogueFile
        {
            get { return m_catalogueFile; }
            set { m_catalogueFile = value; }
        }

        public string CatalogueFileGuid
        {
            get { return m_catalogueFileGuid; }
            set { m_catalogueFileGuid = value; }
        }

        public string Configuration
        {
            get { return m_configuration; }
            set { m_configuration = value; }
        }

        private string m_outputFolder;
        private string m_catalogueFile;
        private string m_catalogueFileGuid;
        private string m_configuration;
    }
}