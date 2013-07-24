using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
    [TestClass]
    public class NewJobAdLimitTests
        : MaintainJobAdTests
    {
        private const int DailyLimit = 50;

        [TestMethod]
        public void TestLimits()
        {
            // Create employer and allocate credits.

            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null });

            // Log in and post job ads.

            LogIn(employer);

            IList<Guid> jobAdIds;

            for (var index = 0; index < DailyLimit; ++index)
            {
                Get(GetJobAdUrl(null));

                EnterJobDetails();
                _previewButton.Click();
                _publishButton.Click();

                jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open);
                Assert.AreEqual(index + 1, jobAdIds.Count);
            }

            // Try to post one more.

            Get(GetJobAdUrl(null));

            EnterJobDetails();
            _previewButton.Click();
            _publishButton.Click();

            AssertErrorMessage("Please call LinkMe on 1800 546-563 to post more job ads.");

            jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open);
            Assert.AreEqual(DailyLimit, jobAdIds.Count);
        }
    }
}