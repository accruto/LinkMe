using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
    [TestClass]
    public class NewJobAdCreditTests
        : MaintainJobAdTests
    {
        [TestMethod]
        public void TestLimitedCredits()
        {
            TestCredits(1000, true);
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            TestCredits(null, true);
        }

        [TestMethod]
        public void TestNoCredits()
        {
            TestCredits(0, true);
        }

        [TestMethod]
        public void TestLimitedOrganisationCredits()
        {
            TestOrganisationCredits(1000, true);
        }

        [TestMethod]
        public void TestUnlimitedOrganisationCredits()
        {
            TestOrganisationCredits(null, true);
        }

        [TestMethod]
        public void TestNoOrganisationCredits()
        {
            TestOrganisationCredits(0, true);
        }

        [TestMethod]
        public void TestLimitedParentOrganisationCredits()
        {
            TestParentOrganisationCredits(1000, true);
        }

        [TestMethod]
        public void TestUnlimitedParentOrganisationCredits()
        {
            TestParentOrganisationCredits(null, true);
        }

        [TestMethod]
        public void TestNoParentOrganisationCredits()
        {
            TestParentOrganisationCredits(0, true);
        }

        private void TestCredits(int? quantity, bool canPublish)
        {
            // Create employer and allocate credits.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = quantity });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null });

            CreateNewJobAd(employer, canPublish);
        }

        private void TestOrganisationCredits(int? quantity, bool canPublish)
        {
            // Create organisation and allocate credits.

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = quantity });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null });

            // Create employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            CreateNewJobAd(employer, canPublish);
        }

        private void TestParentOrganisationCredits(int? quantity, bool canPublish)
        {
            // Create organisations and allocate credits.

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parent.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = quantity });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parent.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null });

            // Create employer.

            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, Guid.NewGuid());
            var employer = _employerAccountsCommand.CreateTestEmployer(1, child);

            CreateNewJobAd(employer, canPublish);
        }

        private void CreateNewJobAd(IUser employer, bool canPublish)
        {
            // Log in and fill in the details.

            LogIn(employer);
            Get(GetJobAdUrl(null));

            EnterJobDetails();
            _previewButton.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);

            if (canPublish)
                AssertCanPublish(jobAdIds[0]);
            else
                AssertCannotPublish(jobAdIds[0]);
        }

        private void AssertCanPublish(Guid jobAdId)
        {
            // Check on right page.

            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));
            Assert.IsTrue(_publishButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            AssertPageDoesNotContain("Please purchase job ad credits or call us on");

            // Publish.

            _publishButton.Click();
            AssertPageContains("was successfully published");
        }

        private void AssertCannotPublish(Guid jobAdId)
        {
            // Check on right page.

            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));
            Assert.IsFalse(_publishButton.IsVisible);
            Assert.IsTrue(_editButton.IsVisible);
            AssertPageContains("Please purchase job ad credits or call us on");
        }
    }
}