namespace LinkMe.Apps.Agents.Reports.Employers
{
    public class CandidateCareReport
        : EmployerReport
    {
        public CandidateCareReport()
        {
            Type = typeof(CandidateCareReport).Name;
        }

        public override string Name
        {
            get { return "Candidate referrals"; }
        }

        public override string Description
        {
            get { return "Reports on the number of members referred by the organisation, as determined by the promo code that the members joined with. Specify the organisation's assigned promo code below."; }
        }

        public override bool ReportAsFile
        {
            get { return false; }
        }

        public string PromoCode
        {
            get { return GetParameterValue("PromoCode", (string)null); }
            set { SetParameterValue("PromoCode", value); }
        }
    }
}