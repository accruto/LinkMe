using System;

namespace LinkMe.Environment.Build.Tasks
{
    [Serializable]
    public class Options
    {
        public string ProjectFullPath
        {
            get { return m_projectFullPath; }
            set { m_projectFullPath = value; }
        }

        private string m_projectFullPath;
    }
}