using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    public class GetSolutionPaths
        : Task
    {
        public GetSolutionPaths()
        {
            m_options = new Options();
        }

        public override bool Execute()
        {
            try
            {
                m_solutionPaths = new ITaskItem[m_solutions.Length];

                // Iterate through the solutions extracting the full path for each solution found.

                for ( int index = 0; index < m_solutions.Length; ++index )
                {
                    ITaskItem solution = m_solutions[index];
                    string solutionPath = FilePath.GetAbsolutePath(solution.ItemSpec, Path.GetDirectoryName(m_options.ProjectFullPath));
                    m_solutionPaths[index] = new TaskItem(solutionPath);
                }

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
            get { return Options.ProjectFullPath; }
            set { Options.ProjectFullPath = value; }
        }

        [Required]
        public ITaskItem[] Solutions
        {
            get { return m_solutions; }
            set { m_solutions = value; }
        }

        [Output]
        public ITaskItem[] SolutionPaths
        {
            get { return m_solutionPaths; }
            set { m_solutionPaths = value; }
        }

        protected Options Options
        {
            get { return m_options; }
        }

        private ITaskItem[] m_solutions;
        private ITaskItem[] m_solutionPaths;
        private Options m_options;
    }
}