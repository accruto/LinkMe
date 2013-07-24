using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public class GacAssembly
        : Task
    {
        public override bool Execute()
        {
            using ( GacUtil util = new GacUtil() )
            {
                util.InstallAssemblyFile(m_path);
            }

            return true;
        }

        [Required]
        public string Path
        {
            get { return m_path; }
            set { m_path = value; }
        }

        private string m_path;
    }
}