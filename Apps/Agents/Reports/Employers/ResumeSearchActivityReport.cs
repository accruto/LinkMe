namespace LinkMe.Apps.Agents.Reports.Employers
{
    public class ResumeSearchActivityReport
        : EmployerReport
    {
        private const bool DefaultIncludeDisabledUsers = false;

        public ResumeSearchActivityReport()
        {
            Type = typeof(ResumeSearchActivityReport).Name;
            IncludeDisabledUsers = DefaultIncludeDisabledUsers;
        }

        public override string Name
        {
            get { return "Resume search activity"; }
        }

        public override string Description
        {
            get { return "Reports on the searches, resume views and candidate contacts by the organisation."; }
        }

        public override bool ReportFileEvenIfNoResults
        {
            get { return true; }
        }

        public bool IncludeDisabledUsers
        {
            get { return GetParameterValue("IncludeDisabledUsers", DefaultIncludeDisabledUsers); }
            set { SetParameterValue("IncludeDisabledUsers", value); }
        }
    }
}