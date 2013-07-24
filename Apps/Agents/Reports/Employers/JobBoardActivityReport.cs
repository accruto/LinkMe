namespace LinkMe.Apps.Agents.Reports.Employers
{
    public class JobBoardActivityReport
        : EmployerReport
    {
        public JobBoardActivityReport()
        {
            Type = typeof(JobBoardActivityReport).Name;
        }

        public override string Name
        {
            get { return "Job board activity"; }
        }

        public override string Description
        {
            get { return "Reports on the job ads posted by the organisation and the applicants for those job ads."; }
        }
    }
}