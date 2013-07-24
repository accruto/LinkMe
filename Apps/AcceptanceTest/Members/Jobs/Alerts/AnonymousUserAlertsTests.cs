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
    public class AnonymousUserAlertsTests
        : AlertsTests
    {
        [TestMethod]
        public void TestEmail()
        {
            // Create contact.

            var contact = CreateAnonymousContact(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(contact.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");

            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(4, links.Count);

            var definition = typeof(JobSearchAlertEmail).Name;

            AssertLink(definition, GetResultsUrl(), links[0]);
            AssertPageContains(BusinessAnalyst);
            AssertLink(definition, GetJobAdUrl(jobAd), links[1]);
            AssertPageContains(BusinessAnalyst);
            AssertLink(definition, GetUnsubscribeUrl(search.Id), links[2]);
            AssertPageContains("Please confirm that you would like to unsubscribe");
            AssertLink(definition, _contactUsUrl, links[3]);
            AssertTrackingLink(email.GetHtmlView().GetImageLinks().Last());
        }

        [TestMethod]
        public void TestMultipleRuns()
        {
            // Create contact.

            var contact = CreateAnonymousContact(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(contact.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            _settingsCommand.SetLastSentTime(contact.Id, _settingsQuery.GetDefinition("JobSearchAlertEmail").Id, DateTime.Now.AddDays(-1));

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

            var contact = CreateAnonymousContact(0);

            // Create a job search.

            var search = new JobAdSearch { Criteria = new JobAdSearchCriteria { AdTitle = BusinessAnalyst } };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(contact.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var employer = CreateEmployer();
            PostJobAd(employer, BusinessAnalyst);

            ExecuteTask();
            _settingsCommand.SetLastSentTime(contact.Id, _settingsQuery.GetDefinition("JobSearchAlertEmail").Id, DateTime.Now.AddDays(-1));

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains("<strong>1 new job</strong>");

            // Unsubscribe.

            var links = email.GetHtmlView().GetLinks();
            Get(links[2]);

            var definition = typeof(JobSearchAlertEmail).Name;
            AssertUrl(GetEmailUrl(definition, GetUnsubscribeUrl(search.Id)));
            
            _unsubscribeButton.Click();

            // Go again.

            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private static ReadOnlyUrl GetUnsubscribeUrl(Guid searchId)
        {
            return new ReadOnlyApplicationUrl("~/members/searches/" + searchId + "/delete");
        }
    }
}
