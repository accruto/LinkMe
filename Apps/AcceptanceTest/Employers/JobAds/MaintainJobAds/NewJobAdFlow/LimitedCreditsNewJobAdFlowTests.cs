using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public class LimitedCreditsNewJobAdTests
        : EmployerNewJobAdFlowTests
    {
        protected override int? GetEmployerCredits()
        {
            return 10;
        }

        protected override bool ShouldFeaturePacksShow
        {
            get { return false; }
        }
    }
}
