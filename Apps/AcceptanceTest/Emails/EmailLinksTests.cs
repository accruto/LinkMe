using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerUpdates;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Search.Members;
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
    public class EmailLinksTests
        : EmailsTests
    {
        private const string EmailAddress = "testuser3@test.linkme.net.au";
        private const string JobTitle = "Sea Captain";
        private const string CoverLetter = @"Can I have a job?";

        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();

        private ReadOnlyUrl _memberSettingsUrl;
        private ReadOnlyUrl _profileUrl;
        private ReadOnlyUrl _contactUsUrl;
        private ReadOnlyUrl _candidateSearchUrl;
        private ReadOnlyUrl _changePasswordUrl;
        private ReadOnlyUrl _invitationsUrl;
        private ReadOnlyUrl _savedResumeSearchAlertsUrl;
        private ReadOnlyUrl _employerNewJobAdUrl;
        private ReadOnlyUrl _employerSettingsUrl;
        private ReadOnlyUrl _resourcesUrl;
        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _baseCandidateUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _memberSettingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/setting-up-your-profile/09d11385-0213-4157-a5a9-1b2a74e6887e");
            _candidateSearchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _changePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changepassword");
            _invitationsUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/Invitations.aspx");
            _savedResumeSearchAlertsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/SavedResumeSearchAlerts.aspx");
            _employerNewJobAdUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
            _employerSettingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");
            _resourcesUrl = new ReadOnlyApplicationUrl("~/members/resources");
            _candidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates");
            _baseCandidateUrl = new ReadOnlyApplicationUrl(true, "~/candidates/");
        }

        [TestMethod]
        public void TestInvitationEmailLinks()
        {
            // Create some members.

            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // Create an invitation.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);

            // Check the default variation.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(4, links.Count);

            var definition = typeof(FriendInvitationEmail).Name;

            AssertLink(definition, invitee, _invitationsUrl, links[0]);
            AssertLink(definition, _contactUsUrl, links[1]);
            AssertLink(definition, invitee, _memberSettingsUrl, links[2]);
            AssertLink(definition, GetUnsubscribeUrl(invitee.Id.ToString("n"), "MemberToMemberNotification"), links[3]);

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);

            // Accept the invitation.

            _memberFriendsCommand.AcceptInvitation(invitee.Id, invitation);

            // Check the email.

            email = _emailServer.AssertEmailSent();
            links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(4, links.Count);

            definition = typeof(FriendInvitationConfirmationEmail).Name;

            // Not logged in.

            AssertLink(definition, _contactUsUrl, links[1]);
            AssertLink(definition, inviter, _memberSettingsUrl, links[2]);
            AssertLink(definition, GetUnsubscribeUrl(inviter.Id.ToString("n"), "MemberToMemberNotification"), links[3]);

            // Check the tracking pixel.

            link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);

            // Invite non-member.

            invitation = new FriendInvitation {InviterId = inviter.Id, InviteeEmailAddress = EmailAddress};
            _memberFriendsCommand.SendInvitation(invitation);

            // Check the email.

            email = _emailServer.AssertEmailSent();
            links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(2, links.Count);

            definition = typeof(ContactInvitationEmail).Name;

            AssertLink(definition, new ReadOnlyApplicationUrl(true, "~/join", new ReadOnlyQueryString("inviteId", invitation.Id.ToString("n"))), links[0]);
            AssertLink(definition, _contactUsUrl, links[1]);

            // Check the tracking pixel.

            link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);
        }

        [TestMethod]
        public void TestCandidateEmailLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send an email.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            _emailsCommand.TrySend(new EmployerContactCandidateConfirmationEmail(null, employer, member.Id, view.GetDisplayText(true), "subject", "content"));

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(2, links.Count);

            var definition = typeof(EmployerContactCandidateConfirmationEmail).Name;

            AssertLink(definition, GetCandidateUrl(member), links[0]);
            AssertLink(definition, _contactUsUrl, links[1]);

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);

            // Check the MyCandidatesResumeEmail

            view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            _emailsCommand.TrySend(new CandidateResumeEmail(employer, view));

            // Check the email.

            email = _emailServer.AssertEmailSent();
            links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(3, links.Count);

            definition = typeof(CandidateResumeEmail).Name;

            AssertLink(definition, GetCandidateUrl(member), links[0]);
            AssertLink(definition, _candidateSearchUrl, links[1]);
            AssertLink(definition, _contactUsUrl, links[2]);

            // Check the tracking pixel.

            link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);
        }

        [TestMethod]
        public void TestResumeSearchAlertEmail()
        {
            // Create an employer and search.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var criteria = new MemberSearchCriteria { JobTitle = JobTitle };
            var search = new MemberSearch { OwnerId = employer.Id, Criteria = criteria };

            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            var results = new MemberSearchResults { MemberIds = new[] { member.Id }, TotalMatches = 1 };

            // Send.

            _emailsCommand.TrySend(new ResumeSearchAlertEmail(employer, search.Criteria, null, results, _employerMemberViewsQuery.GetEmployerMemberViews(employer, results.MemberIds), search.Id));

            // Check email.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(3, links.Count);

            var definition = typeof(ResumeSearchAlertEmail).Name;

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);

            // Not logged in.

            var url = _candidatesUrl.AsNonReadOnly();
            url.QueryString.Add("candidateId", member.Id.ToString());
            AssertLink(definition, employer, url, GetCandidateUrl(member), links[0]);
            AssertLink(definition, employer, _savedResumeSearchAlertsUrl, links[1]);
            AssertLink(definition, _contactUsUrl, links[2]);

            // Logged in.

            LogIn(employer);

            AssertLink(definition, GetCandidateUrl(member), links[0]);
            AssertLink(definition, _savedResumeSearchAlertsUrl, links[1]);
        }

        [TestMethod]
        public void TestNewEmployerWelcomeEmailLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create the email.

            var candidates = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now);
            _emailsCommand.TrySend(new NewEmployerWelcomeEmail(employer, employer.GetLoginId(), employer.GetPassword(), candidates));

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(3, links.Count);

            var definition = typeof(NewEmployerWelcomeEmail).Name;

            AssertLink(definition, employer, _changePasswordUrl, links[0]);
            AssertLink(definition, new ReadOnlyApplicationUrl("~/employers/resources"), links[1]);
            AssertLink(definition, _contactUsUrl, links[2]);

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);
        }

        [TestMethod]
        public void TestJobApplicationEmailLinks()
        {
            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = employer.CreateTestJobAd();
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();
            Assert.AreEqual(3, links.Count);

            var definition = typeof(JobApplicationEmail).Name;

            AssertLink(definition, EmployerHomeUrl, links[0]);
            AssertLink(definition, GetCandidateUrl(member), links[1]);
            AssertLink(definition, _contactUsUrl, links[2]);

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);
        }

        [TestMethod]
        public void TestJobSearchAlertEmailLinks()
        {
/*            // Create a job ad.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = employer.CreateTestJobAd(JobTitle, JobContent);
            _jobAdsCommand.PostJobAd(jobAd);

            // Do a search.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var savedJobSearch = TestObjectMother.CreateTestSavedJobSearchAlert(member, JobTitle, DateTime.Now.AddDays(-1));

            savedJobSearch = _jobAdSearchesQuery.GetJobAdSearch(savedJobSearch.Id);
            var search = TestObjectMother.PerformJobSearchUntilYouGetResults(member, savedJobSearch.Criteria, 1);
            Assert.AreEqual(1, search.Results.TotalMatches);

            // Send the email.

            _emailsCommand.TrySend(new JobSearchAlertEmail(member, search.Results.TotalMatches, 
                CreateEmailResults(search.Results, search.Criteria, MaximumResults),
                search.Criteria, savedJobSearch.Id));

            // Find the links.

            var email = _emailServer.AssertEmailSent();
            var links = email.GetHtmlView().GetLinks();

            Assert.IsTrue(links.Count == 5);

            AssertLink<Web.Search.Jobs.AdvancedSearch>(links[0], true, JobTitle);
            AssertLink<JobSearchEmailAlerts>(member, links[1], true, false);
            AssertLink<JobAdPage>(links[2], true, JobTitle, JobContent);
            AssertLink(_settingsUrl, member, links[3], true, false);
            AssertLink<Unsubscribe>(links[4], true, "Please confirm that you would like to unsubscribe");

            // Check the tracking pixel.

            var link = email.GetHtmlView().GetImageLinks().Last();
            AssertTrackingLink(link);
*/
        }

        [TestMethod]
        public void TestMemberNewsletterEmailLinks()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Send the email.

            _emailsCommand.TrySend(new MemberNewsletterEmail(member));
            _settingsCommand.SetLastSentTime(member.Id, _settingsQuery.GetDefinition("MemberNewsletterEmail").Id, null);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            var links = GetUpdateLinks(email.GetHtmlView().Body);
            var definition = typeof(MemberNewsletterEmail).Name;

            Assert.AreEqual(21, links.Count);

            // First link is in the intro.

            var currentLink = 0;

            // Resource link that is not working?
            currentLink++;
            //AssertLink(definition, new ReadOnlyUrl("www.redstarresume.com"), links[currentLink++]);

            // Talent 2 link
            currentLink++;

            AssertLink(definition, member, _profileUrl, links[currentLink++]);

            // Resource link that is not working?

            currentLink++;
            AssertLink(definition, _resourcesUrl, links[currentLink++]);

            // Balance recruitment and resource links.
            currentLink++;
            currentLink++;

            AssertLink(definition, HomeUrl, links[currentLink++]);
            AssertLink(definition, member, _profileUrl, links[currentLink++]);

            // Resource links.
            currentLink++;
            currentLink++;
            currentLink++;
            currentLink++;
            currentLink++;
            currentLink++;

            // Facebook and twitter.

            currentLink++;
            currentLink++;
            currentLink++;

            AssertLink(definition, HomeUrl, links[currentLink++]);
            AssertLink(definition, member, _memberSettingsUrl, links[currentLink++]);
            AssertLink(definition, GetUnsubscribeUrl(member.Id.ToString(), "MemberUpdate"), links[currentLink]);

            // Check the tracking pixel.

//            var link = GetUpdateImageLinks(email.GetHtmlView().Body).Last();
//            AssertTrackingLink(link);
        }

        [TestMethod]
        public void TestEmployerNewsletterEmailLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Send the email.

            _emailsCommand.TrySend(new EmployerNewsletterEmail(employer));
            _settingsCommand.SetLastSentTime(employer.Id, _settingsQuery.GetDefinition("EmployerNewsletterEmail").Id, null);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            var links = GetUpdateLinks(email.GetHtmlView().Body);
            Assert.AreEqual(8, links.Count);

            var definition = typeof(EmployerNewsletterEmail).Name;

            var currentLink = 0;
            var url = EmployerLogInUrl.AsNonReadOnly();
            url.QueryString["userId"] = employer.GetLoginId();
            AssertLink(definition, url, links[currentLink++]);

            // These 3 links are search, create alert, create a saved search.

            AssertLink(definition, _candidateSearchUrl, links[currentLink++]);
            AssertLink(definition, _candidateSearchUrl, links[currentLink++]);
            AssertLink(definition, _candidateSearchUrl, links[currentLink++]);
            AssertLink(definition, _employerNewJobAdUrl, links[currentLink++]);

            AssertLink(definition, HomeUrl, links[currentLink++]);
            AssertLink(definition, employer, _employerSettingsUrl, links[currentLink++]);
            AssertLink(definition, GetUnsubscribeUrl(employer.Id.ToString(), "EmployerUpdate"), links[currentLink]);

            // Check the tracking pixel.

            var link = GetUpdateImageLinks(email.GetHtmlView().Body).Last();
            AssertTrackingLink(link);
        }

        private static IList<ReadOnlyUrl> GetUpdateLinks(string body)
        {
            var links = new List<ReadOnlyUrl>();

            // Need to do it by hand for this email.

            var pos = body.IndexOf(" href=\"");
            while (pos != -1)
            {
                var start = pos + " href=\"".Length;
                var end = body.IndexOf("\"", start);
                if (end == -1)
                    break;

                var link = body.Substring(start, end - start);
                links.Add(new ReadOnlyUrl(link));
                
                pos = body.IndexOf(" href=\"", end);
            }

            return links;
        }

        private static IEnumerable<ReadOnlyUrl> GetUpdateImageLinks(string body)
        {
            var links = new List<ReadOnlyUrl>();

            // Need to do it by hand for this email.

            var pos = body.IndexOf("<img src=\"");
            while (pos != -1)
            {
                var start = pos + "<img src=\"".Length;
                var end = body.IndexOf("\"", start + "img src=\"".Length);
                if (end == -1)
                    break;

                var link = body.Substring(start, end - start);
                links.Add(new ReadOnlyUrl(link));

                pos = body.IndexOf("<img src=\"", end);
            }

            return links;
        }

        private ReadOnlyUrl GetCandidateUrl(IMember member)
        {
            // Assume that the user has no salary and no title but does have a location.

            var sb = new StringBuilder();
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(member.Address.Location.ToString())).ToLower().Replace(' ', '-'));
            sb.Append("/");
            sb.Append("-");
            sb.Append("/");
            sb.Append("-");
            sb.Append("/");
            sb.Append(member.Id.ToString());
            return new ReadOnlyApplicationUrl(_baseCandidateUrl, sb.ToString());
        }

        private ReadOnlyUrl GetUnsubscribeUrl(string userId, string category)
        {
            var url = _unsubscribeUrl.AsNonReadOnly();
            url.QueryString["userId"] = userId;
            url.QueryString["category"] = category;
            return url;
        }
    }
}