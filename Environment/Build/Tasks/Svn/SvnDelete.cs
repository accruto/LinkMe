using MSBuild.Community.Tasks.Subversion;

namespace LinkMe.Environment.Build.Tasks.Svn
{
    public class SvnDelete
        : SvnClient
    {
        private bool _ignoreError;

        public SvnDelete()
        {
            Arguments = "--non-interactive";
            Command = "delete";
        }

        public bool IgnoreError
        {
            get { return _ignoreError; }
            set { _ignoreError = value; }
        }

        protected override bool HandleTaskExecutionErrors()
        {
            if (_ignoreError)
            {
                Log.LogMessage("Ignoring SVN delete error (exit code {0})", ExitCode);
                return true;
            }

            return base.HandleTaskExecutionErrors();
        }
    }
}