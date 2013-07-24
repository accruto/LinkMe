using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class PostNoLimitsTests
        : PostJobAdsTests
    {
        private const int DailyLimit = 50;

        [TestMethod]
        public void TestLimits()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post past the limit.

            for (var index = 0; index < DailyLimit * 2; ++index)
            {
                var jobAd = CreateJobAd(index, j => j.Status = JobAdStatus.Open);
                PostJobAds(integratorUser, employer, false, new[] { jobAd }, 1, 0, 0, 0);

                AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
            }
        }
    }
}
