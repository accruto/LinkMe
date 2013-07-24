using System;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.EmailJobSearchAlerts
{
    [TestClass]
    public class AnonymousUsersTests
        : EmailJobSearchAlertsTaskTests
    {
        [TestMethod]
        public void TestAnonymousUser()
        {
            PostJobAd();

            // Create member.

            var contact = CreateAnonymousContact(0);

            // Create a job search.

            var criteria = new JobAdSearchCriteria
            {
                AdTitle = BusinessAnalyst,
            };

            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(contact.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var email = ExecuteTask();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");
        }

        private void PostJobAd()
        {
            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd(BusinessAnalyst);
            _jobAdsCommand.PostJobAd(jobAd);
        }
    }
}