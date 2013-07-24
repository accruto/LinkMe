using System;

namespace LinkMe.Environment.Build.Tasks.Data
{
    [Serializable]
    public class MergeDataOptions
        : Options
    {
        public string OutputFolder
        {
            get { return m_outputFolder; }
            set { m_outputFolder = value; }
        }

        public string Configuration
        {
            get { return m_configuration; }
            set { m_configuration = value; }
        }

        private string m_outputFolder;
        private string m_configuration;
    }
}