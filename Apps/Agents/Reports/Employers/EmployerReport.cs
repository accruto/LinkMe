namespace LinkMe.Apps.Agents.Reports.Employers
{
    public abstract class EmployerReport
        : Report
    {
        private const bool DefaultIncludeChildOrganisations = true;

        public abstract string Name { get; }
        public abstract string Description { get; }

        public bool IncludeChildOrganisations
        {
            get { return GetParameterValue("IncludeChildOrgUnits", DefaultIncludeChildOrganisations); }
            set { SetParameterValue("IncludeChildOrgUnits", value); }
        }

        public virtual bool ReportAsFile
        {
            get { return true; }
        }

        public virtual bool ReportFileEvenIfNoResults
        {
            get { return false; }
        }
    }
}