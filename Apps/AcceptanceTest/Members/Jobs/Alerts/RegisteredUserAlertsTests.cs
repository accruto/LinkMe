using System;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Alerts
{
    [TestClass]
    public class RegisteredUserAlertsTests
        : AlertsTests
    {
        [TestMethod]
        public void TestEmail()
        {
            // Create member.

            var member = CreateMember(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");

            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(6, links.Count);

            var definition = typeof(JobSearchAlertEmail).Name;

            AssertLink(definition, GetResultsUrl(), links[0]);
            AssertPageContains(BusinessAnalyst);
            AssertLink(definition, member, _savedUrl, links[1]);
            AssertLink(definition, GetJobAdUrl(jobAd), links[2]);
            AssertPageContains(BusinessAnalyst);
            AssertLink(definition, _contactUsUrl, links[3]);
            AssertLink(definition, member, _settingsUrl, links[4]);
            AssertLink(definition, GetUnsubscribeUrl(member.Id), links[5]);
            AssertPageContains("Please confirm that you would like to unsubscribe");
            AssertTrackingLink(email.GetHtmlView().GetImageLinks().Last());
        }

        [TestMethod]
        public void TestMultipleRuns()
        {
            // Create member.

            var member = CreateMember(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            _settingsCommand.SetLastSentTime(member.Id, _settingsQuery.GetDefinition("JobSearchAlertEmail").Id, DateTime.Now.AddDays(-1));

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");

            // Go again.

            ExecuteTask();
            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");
        }

        [TestMethod]
        public void TestUnsubscribe()
        {
            // Create member.

            var member = CreateMember(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            _settingsCommand.SetLastSentTime(member.Id, _settingsQuery.GetDefinition("JobSearchAlertEmail").Id, DateTime.Now.AddDays(-1));

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");

            // Unsubscribe.

            var links = email.GetHtmlView().GetLinks();
            Get(links[5]);
            _unsubscribeButton.Click();

            // Go again.

            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private ReadOnlyUrl GetUnsubscribeUrl(Guid userId)
        {
            var unsubscribeUrl = _unsubscribeUrl.AsNonReadOnly();
            unsubscribeUrl.QueryString["userId"] = userId.ToString("n");
            unsubscribeUrl.QueryString["category"] = "MemberAlert";
            return unsubscribeUrl;
        }
    }
}
