using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Credits
{
    [TestClass]
    public abstract class CreditsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();
        private readonly IEmployerJobAdsQuery _employerJobAdsQuery = Resolve<IEmployerJobAdsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        private Allocation _allocation;

        [TestInitialize]
        public void CreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod, ExpectedException(typeof(InsufficientCreditsException))]
        public void TestOpenJobAdNoAllocations()
        {
            TestOpenJobAd(false, null, null);
        }

        [TestMethod, ExpectedException(typeof(InsufficientCreditsException))]
        public void TestOpenJobAdZeroCredits()
        {
            TestOpenJobAd(true, 0, null);
        }

        [TestMethod]
        public void TestOpenJobAdSomeCredits()
        {
            TestOpenJobAd(true, 10, null);
        }

        [TestMethod]
        public void TestOpenJobAdUnlimitedCredits()
        {
            TestOpenJobAd(true, null, null);
        }

        [TestMethod, ExpectedException(typeof(InsufficientCreditsException))]
        public void TestOpenJobAdSomeCreditsNoApplicantCredits()
        {
            TestOpenJobAd(true, 10, 0);
        }

        [TestMethod, ExpectedException(typeof(InsufficientCreditsException))]
        public void TestOpenJobAdUnlimitedCreditsNoApplicantCredits()
        {
            TestOpenJobAd(true, null, 0);
        }

        [TestMethod]
        public void TestUpdateJobAdZeroCredits()
        {
            TestUpdateJobAd(true, 1, null);
        }

        [TestMethod]
        public void TestUpdateJobAdSomeCredits()
        {
            TestUpdateJobAd(true, 10, null);
        }

        [TestMethod]
        public void TestUpdateJobAdUnlimitedCredits()
        {
            TestUpdateJobAd(true, null, null);
        }

        private void TestOpenJobAd(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var employer = CreateEmployer(allocate, jobAdCredits, applicantCredits);
            var jobAd = CreateJobAd(employer.Id, 0);

            // Create and open the job ad.

            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);

            // Check.

            AssertJobAd(jobAd, _employerJobAdsQuery.GetJobAd<JobAd>(employer, jobAd.Id));
            AssertAllocation(jobAdCredits);
        }

        private void TestUpdateJobAd(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var employer = CreateEmployer(allocate, jobAdCredits, applicantCredits);
            var jobAd = CreateJobAd(employer.Id, 0);

            // Create and open the job ad.

            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);

            // Check.

            AssertJobAd(jobAd, _employerJobAdsQuery.GetJobAd<JobAd>(employer, jobAd.Id));
            AssertAllocation(jobAdCredits);

            // Update.

            jobAd.Title = string.Format(TitleFormat, 1);
            _employerJobAdsCommand.UpdateJobAd(employer, jobAd);

            // Make sure it is open.

            _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);

            // Check - no extra credit should have been used for the same job ad.

            AssertJobAd(jobAd, _employerJobAdsQuery.GetJobAd<JobAd>(employer, jobAd.Id));
            AssertAllocation(jobAdCredits);
        }

        protected abstract Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits);

        protected Employer CreateEmployer(IOrganisation organisation)
        {
            var employer = _employersCommand.CreateTestEmployer(0, organisation);
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected void CreateAllocation(Guid ownerId, bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            if (allocate)
            {
                _allocation = new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = jobAdCredits, OwnerId = ownerId };
                _allocationsCommand.CreateAllocation(_allocation);
                _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = applicantCredits, OwnerId = ownerId });
            }
            else
            {
                _allocation = null;
            }
        }

        private void AssertAllocation(int? credits)
        {
            // Check the allocation.

            var allocation = _allocationsQuery.GetAllocation(_allocation.Id);
            if (credits == null)
            {
                Assert.IsNull(allocation.InitialQuantity);
                Assert.IsNull(allocation.RemainingQuantity);
            }
            else
            {
                Assert.AreEqual(credits.Value, allocation.InitialQuantity);
                Assert.AreEqual(credits.Value - 1, allocation.RemainingQuantity);
            }
        }

        private static JobAd CreateJobAd(Guid posterId, int index)
        {
            return new JobAd
            {
                PosterId = posterId,
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
            };
        }

        private static void AssertJobAd(JobAd expectedJobAd, JobAd jobAd)
        {
            Assert.AreEqual(expectedJobAd.Id, jobAd.Id);
            Assert.AreEqual(expectedJobAd.PosterId, jobAd.PosterId);
            Assert.AreEqual(expectedJobAd.Title, jobAd.Title);
            Assert.AreEqual(expectedJobAd.Description.Content, jobAd.Description.Content);
        }
    }
}
