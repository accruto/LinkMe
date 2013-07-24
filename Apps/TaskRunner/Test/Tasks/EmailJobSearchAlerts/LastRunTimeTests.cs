using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.EmailJobSearchAlerts
{
    [TestClass]
    public class LastRunTimeTests
        : EmailJobSearchAlertsTaskTests
    {
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        [TestMethod]
        public void TestLastRunTime()
        {
            var definition = _settingsQuery.GetDefinition("JobSearchAlertEmail");
            var employer = CreateEmployer();
            var jobAd1 = PostJobAd(employer, DateTime.Now.AddDays(-5));
            var jobAd2 = PostJobAd(employer, DateTime.Now.AddDays(-3));
            var jobAd3 = PostJobAd(employer, DateTime.Now.AddDays(-1));

            // Create member.

            var member = CreateMember(0);

            // Create a job search, last run 2 days ago.

            var criteria = new JobAdSearchCriteria
            {
                AdTitle = BusinessAnalyst,
            };
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-6));

            // Get the email.

            var email = ExecuteTask();
            email.AssertHtmlViewContains("<strong>3 new jobs</strong>");
            AssertJobAds(email, jobAd1, jobAd2, jobAd3);

            _jobAdSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now.AddDays(-4));
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            email = ExecuteTask();
            email.AssertHtmlViewContains("<strong>2 new jobs</strong>");
            AssertJobAds(email, jobAd2, jobAd3);

            _jobAdSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now.AddDays(-2));
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            email = ExecuteTask();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");
            AssertJobAds(email, jobAd3);

            _jobAdSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now);
            _settingsCommand.SetLastSentTime(member.Id, definition.Id, null);

            email = ExecuteTask();
            Assert.IsNull(email);
        }

        private JobAd PostJobAd(IEmployer employer, DateTime createdTime)
        {
            var jobAd = employer.CreateTestJobAd(BusinessAnalyst);
            jobAd.CreatedTime = createdTime;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }
    }
}
