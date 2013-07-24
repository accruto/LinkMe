using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class PostDraftJobAdsTests
        : PostJobAdsTests
    {
        [TestMethod]
        public void TestDraftOpen()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as a draft.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Draft);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Draft));

            // Post it again to open it.

            jobAd.Status = JobAdStatus.Open;
            PostJobAds(integratorUser, employer, true, new[] {jobAd}, 0, 1, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestOpenDraft()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as open.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Open);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));

            // Post it again with draft which results in a new job ad.

            jobAd.Status = JobAdStatus.Draft;
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 1, 0);

            // 2 ads with the same details, one draft and one closed.

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, employer.Id, jobAd.ExternalReferenceId));
            AssertJobAd((from j in jobAds where j.Status == JobAdStatus.Draft select j).Single(), integratorUser, jobAd.ExternalReferenceId, JobAdStatus.Draft);
            AssertJobAd((from j in jobAds where j.Status == JobAdStatus.Closed select j).Single(), integratorUser, jobAd.ExternalReferenceId, JobAdStatus.Closed);
        }

        [TestMethod]
        public void TestOpenClosed()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as open.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Open);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));

            // Close it.

            CloseJobAds(integratorUser, employer, new[] { jobAd }, 1, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Closed));
        }

        [TestMethod]
        public void TestOpenClosedOpen()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as open.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Open);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));

            // Close it.

            CloseJobAds(integratorUser, employer, new[] { jobAd }, 1, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Closed));

            // Open it again.

            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 0, 1, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }

        [TestMethod]
        public void TestDraftClosed()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as draft.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Draft);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Draft));

            // Close it.

            CloseJobAds(integratorUser, employer, new[] { jobAd }, 1, 0);

            // Draft ads aren't actually closed, ie they remain in the draft state.

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Draft));
        }

        [TestMethod]
        public void TestDraftClosedOpen()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });

            // Post it as draft.

            var jobAd = CreateJobAd(1, j => j.Status = JobAdStatus.Draft);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 1, 0, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Draft));

            // Close it.

            CloseJobAds(integratorUser, employer, new[] { jobAd }, 1, 0);

            // Draft ads aren't actually closed, ie they remain in the draft state.

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Draft));

            // Open it.

            jobAd.Status = JobAdStatus.Open;
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 0, 1, 0, 0);

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, JobAdStatus.Open));
        }
    }
}
