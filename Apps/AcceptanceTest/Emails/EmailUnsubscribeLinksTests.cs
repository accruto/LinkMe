using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerUpdates;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Emails
{
    /// <summary>
    /// These tests are primarily in place to check that links in emails do in fact go somewhere.
    /// They are not intended to check all logic around the sending of emails, and may in fact be in
    /// some ways duplicates of other tests that already exist.  They are put here primarily
    /// to ensure some focus is placed on the email links, particularly as site restuctures happen.
    /// </summary> 
    [TestClass]
    public class EmailUnsubscribeLinksTests
        : EmailsTests
    {
        private const string EmailAddress = "testuser@test.linkme.net.au";
        private const string EmailAddress2 = "testuser2@test.linkme.net.au";
        private const string EmployerUserId = "employer";
        private const string JobTitle = "Sea Captain";
        private const string JobContent = "Sail the seven seas";
        private const int MaximumResults = 20;
        private const string StartHighlightTag = "<span style=\"background-color: #ffff99\">";
        private const string EndHighlightTag = "</span>";

        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        private readonly ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        private readonly ICampaignEmailsCommand _campaignEmailsCommand = Resolve<ICampaignEmailsCommand>();
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestInvitationEmailLinks()
        {
            var category = _settingsQuery.GetCategory("MemberToMemberNotification");

            // Create some members.

            var inviter = _memberAccountsCommand.CreateTestMember(EmailAddress);
            var invitee = _memberAccountsCommand.CreateTestMember(EmailAddress2);

            // Create an invitation.

            var invitation = new FriendInvitation {InviterId = inviter.Id, InviteeId = invitee.Id};

            // Check settings.

            AssertSettings(invitee, category, Frequency.Immediately);
            _memberFriendsCommand.SendInvitation(invitation);
            var email = _emailServer.AssertEmailSent();
            AssertUnsubscribeLink(invitee, category, email.GetHtmlView().GetLinks()[3]);

            // Accept the invitation.

            AssertSettings(inviter, category, Frequency.Immediately);
            _memberFriendsCommand.AcceptInvitation(invitee.Id, invitation);
            email = _emailServer.AssertEmailSent();
            AssertUnsubscribeLink(invitee, category, email.GetHtmlView().GetLinks()[3]);
        }

        [TestMethod]
        public void TestJobSearchAlertEmailLinks()
        {
            var category = _settingsQuery.GetCategory("MemberAlert");

            // Create a job ad.

            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = employer.CreateTestJobAd(JobTitle, JobContent);
            _jobAdsCommand.PostJobAd(jobAd);

            // Do a search.

            var member = _memberAccountsCommand.CreateTestMember(EmailAddress);
            var savedJobSearch = CreateTestSavedJobSearchAlert(member.Id, JobTitle, DateTime.Now.AddDays(-1));

            savedJobSearch = _jobAdSearchesQuery.GetJobAdSearch(savedJobSearch.Id);
            var search = PerformJobSearchUntilYouGetResults(member, savedJobSearch.Criteria, 1);
            Assert.AreEqual(1, search.Results.TotalMatches);

            // Send the email.

            AssertSettings(member, category, Frequency.Daily);

            _emailsCommand.TrySend(new JobSearchAlertEmail(member, search.Results.TotalMatches, 
                CreateEmailResults(search.Results, search.Criteria, MaximumResults),
                search.Criteria, savedJobSearch.Id));

            var email = _emailServer.AssertEmailSent();
            AssertUnsubscribeLink(member, category, email.GetHtmlView().GetLinks()[5]);
        }

        [TestMethod]
        public void TestMemberNewsletterEmailLinks()
        {
            var category = _settingsQuery.GetCategory("MemberUpdate");
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress);

            // Send the email.

            AssertSettings(member, category, Frequency.Monthly);
            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            var email = _emailServer.AssertEmailSent();

            AssertUnsubscribeLink(member, category, email.GetHtmlView().GetLinks()[20]);
        }

        [TestMethod]
        public void TestEmployerNewsletterEmailLinks()
        {
            var category = _settingsQuery.GetCategory("EmployerUpdate");
            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            // Send the email.

            AssertSettings(employer, category, Frequency.Immediately);
            _emailsCommand.TrySend(new EmployerNewsletterEmail(employer));
            var email = _emailServer.AssertEmailSent();

            AssertUnsubscribeLink(employer, category, email.GetHtmlView().GetLinks()[7]);
        }

        [TestMethod]
        public void TestIosLaunchNewsletterEmailLinks()
        {
            var category = _settingsQuery.GetCategory("Campaign");
            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            // Send the email.

            AssertSettings(employer, category, Frequency.Immediately);

            var campaign = CreateCampaign();
            _campaignEmailsCommand.Send(new[] { _campaignEmailsCommand.CreateEmail(campaign, employer) }, campaign.Id, false);

            var email = _emailServer.AssertEmailSent();

            AssertUnsubscribeLink(employer, category, email.GetHtmlView().GetLinks()[4]);
        }

        private Campaign CreateCampaign()
        {
            var campaign = new Campaign
            {
                Id = new Guid("{83B9911D-0AE8-4550-B64A-F0D2A97B2380}"),
                Category = CampaignCategory.Employer,
                Name = "iOS Launch",
                Query = null,
                Status = CampaignStatus.Draft,
                CommunicationCategoryId = _settingsQuery.GetCategory("Campaign").Id,
                CommunicationDefinitionId = _settingsQuery.GetDefinition("IosLaunchEmail").Id,
                CreatedBy = new Guid("2E7D03B6-37E2-4D12-89F3-FFB36B939509"),
            };
            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        private IList<JobSearchAlertEmailResult> CreateEmailResults(JobAdSearchResults searchResults, JobAdSearchCriteria criteria, int maximumResults)
        {
            var emailResults = new List<JobSearchAlertEmailResult>();
            if (searchResults.JobAdIds.Count > 0)
            {
                var highlighter = new JobSearchHighlighter(criteria, StartHighlightTag, EndHighlightTag);
                AppendResults(emailResults, searchResults, maximumResults, highlighter, criteria.KeywordsExpression != null);
            }
            return emailResults;
        }

        private void AppendResults(ICollection<JobSearchAlertEmailResult> emailResults, JobAdSearchResults searchResults, int maximumResults, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            foreach (var jobAdId in searchResults.JobAdIds)
            {
                // Get the job ad for the result.

                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd != null)
                {
                    AppendResult(emailResults, jobAd, highlighter, haveKeywords);
                    if (emailResults.Count == maximumResults)
                        return;
                }
            }
        }

        private static void AppendResult(ICollection<JobSearchAlertEmailResult> emailResults, JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            var emailResult = new JobSearchAlertEmailResult
            {
                JobAdId = jobAd.Id.ToString("n"),
                Title = highlighter.HighlightTitle(jobAd.Title),
                Location = jobAd.GetLocationDisplayText()
            };

            if (jobAd.Description.Salary != null)
                emailResult.Salary = jobAd.Description.Salary.GetDisplayText();

            emailResult.PostedAge = jobAd.GetPostedDisplayText();
            emailResult.PostedDate = jobAd.CreatedTime.ToShortDateString();
            emailResult.JobType = jobAd.Description.JobTypes.GetDisplayText(", ", false, false);

            if (jobAd.Description.Industries != null)
                emailResult.Industry = jobAd.Description.Industries.GetCriteriaIndustriesDisplayText();

            Summarize(jobAd, highlighter, haveKeywords, out emailResult.Digest, out emailResult.Fragments);

            emailResults.Add(emailResult);
        }

        private static void Summarize(JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords, out string digestHtml, out string fragmentsHtml)
        {
            if (haveKeywords)
            {
                // Show highlighted short summary + best highlighted content fragments.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, false, out digestText, out bodyText);

                digestHtml = highlighter.HighlightContent(digestText, false);
                fragmentsHtml = highlighter.GetBestContent(bodyText);
            }
            else
            {
                // Show long summary without highlighting.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, true, out digestText, out bodyText);

                digestHtml = HttpUtility.HtmlEncode(digestText).Replace("\x2022", "&#x2022;");
                fragmentsHtml = string.Empty;
            }
        }

        private JobAdSearch CreateTestSavedJobSearchAlert(Guid jobSearcherId, string jobTitle, DateTime whenSaved)
        {
            var criteria = new JobAdSearchCriteria
            {
                AdTitle = jobTitle,
            };

            var execution = new JobAdSearchExecution { SearcherId = jobSearcherId, Criteria = criteria, Context = "Search" };
            execution.Results = _executeJobAdSearchCommand.Search(null, execution.Criteria, null).Results;

            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            var search = new JobAdSearch { OwnerId = jobSearcherId, Name = "Test Search Alert", Criteria = execution.Criteria.Clone() };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(jobSearcherId, search, whenSaved);

            return search;
        }

        private JobAdSearchExecution PerformJobSearchUntilYouGetResults(IHasId<Guid> searcher, JobAdSearchCriteria criteria, int expectedResultCount)
        {
            const int maxAttempts = 100;
            const int delayMs = 500;

            // If at first you don't succeed... wait for the full-text catalog to get updated and try again!

            for (var i = 0; i < maxAttempts; i++)
            {
                var execution = new JobAdSearchExecution { SearcherId = searcher == null ? (Guid?)null : searcher.Id, Criteria = criteria, Context = "Search" };
                execution.Results = _executeJobAdSearchCommand.Search(null, execution.Criteria, null).Results;
                if (execution.Results.TotalMatches == expectedResultCount)
                    return execution;

                Thread.Sleep(delayMs);
            }

            throw new ApplicationException("The job search has not returned " + expectedResultCount + " results as expected, even after "
                + maxAttempts + " attempts.");
        }
    }
}
