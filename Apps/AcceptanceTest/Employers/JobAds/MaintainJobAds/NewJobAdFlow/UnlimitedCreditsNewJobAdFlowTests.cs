using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public class UnlimitedCreditsNewJobAdFlowTests
        : EmployerNewJobAdFlowTests
    {
        protected override int? GetEmployerCredits()
        {
            return null;
        }

        protected override bool ShouldFeaturePacksShow
        {
            get { return false; }
        }
    }
}
