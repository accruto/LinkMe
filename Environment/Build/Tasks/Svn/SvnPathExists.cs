using Microsoft.Build.Framework;
using MSBuild.Community.Tasks.Subversion;

namespace LinkMe.Environment.Build.Tasks.Svn
{
    public class SvnPathExists
        : SvnClient
    {
        private bool _pathExists;

        public SvnPathExists()
        {
            Arguments = "--non-interactive";
            Command = "info";
        }

        [Output]
        public bool PathExists
        {
            get { return _pathExists; }
        }

        protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
        {
            _pathExists = singleLine.IndexOf("Not a valid URL") == -1;
        }
    }
}