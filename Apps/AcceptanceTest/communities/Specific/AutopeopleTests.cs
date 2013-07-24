using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Partners;
using LinkMe.Domain.Roles.Affiliations.Partners.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI.Unregistered.Autopeople;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.Specific
{
    [TestClass]
    public class AutopeopleTests
        : SpecificCommunityTests
    {
        private readonly IPartnersCommand _partnersCommand = Resolve<IPartnersCommand>();

        private const string Subject = "Do you want a job!";
        private const string Body = "This job is tip-top.";

        private ReadOnlyUrl _manageCandidatesUrl;
        private ReadOnlyUrl _sendMessagesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _manageCandidatesUrl = new ReadOnlyApplicationUrl("~/employers/candidates/manage/");
            _sendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendmessages");
        }

        protected override TestCommunity GetTestCommunity()
        {
            return TestCommunity.Autopeople;
        }

        [TestMethod]
        public void AutopeopleEmailCandidateTest()
        {
            TestCommunity.Autopeople.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create the employer and a job.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Apply.

            var member = CreateMember(null);
            ApplyForJob(member, jobAd);
            LogOut();

            // Login as employer.

            AutopeopleLogIn(employer);

            // Navigate to the job ad.

            Get(new ReadOnlyApplicationUrl(_manageCandidatesUrl, jobAd.Id.ToString()));

            // Email.

            _emailServer.ClearEmails();
            SendMessage(Subject, Body, member);

            // Check emails.

            var emails = _emailServer.AssertEmailsSent(2);

            // First one should be email to member.

            var email = emails[0];
            email.AssertAddresses(employer, Return, member);
            email.AssertSubject(Subject);
            email.AssertViewCount(2);
            email.AssertViewContains(MediaTypeNames.Text.Html, Body);
            email.AssertViewContains(MediaTypeNames.Text.Plain, Body);

            // Second one should be confirmation to employer.

            email = emails[1];
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject("Copy: " + Subject);
            email.AssertHtmlViewContains("Please find a copy of the email you sent to");
            email.AssertHtmlViewContains(Body);
            email.AssertHtmlViewDoesNotContain("Your LinkMe team");
            email.AssertHtmlViewContains("Autopeople");
        }

        [TestMethod]
        public void TestAutopeopleQuickLinks()
        {
            var employer = CreateEmployer();

            // Associate them with the Autopeople service partner.

            var data = TestCommunity.Autopeople.GetCommunityTestData();
            var partner = new Partner { Name = data.Name };
            _partnersCommand.CreatePartner(partner);
            _partnersCommand.SetPartner(employer.Id, partner.Id);

            // Create the community.

            data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Log in via the special login page.

            AutopeopleLogIn(employer);

            // Check that the link is there.

            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='post-job-tool']");
            Assert.IsNull(xmlNode);

            // Log in via the standard process.

            LogOut();
            Get(_resetUrl);
            LogIn(employer);

            // Check that the link is there.

            xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='post-job-tool']");
            Assert.IsNull(xmlNode);
        }

        [TestMethod]
        public void TestEmployerSearch()
        {
            // Create community and members.

            var data = TestCommunity.Autopeople.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            var index = 0;
            var communityMembers = CreateMembers(ref index, community, 2);
            var membersNone = CreateMembers(ref index, null, 3);
            var membersAll = MiscUtils.ConcatArrays(new[] { communityMembers, membersNone });

            // Create employer.

            var employer = CreateEmployer();

            // Associate them with the Autopeople service partner.

            var partner = new Partner { Name = data.Name };
            _partnersCommand.CreatePartner(partner);
            _partnersCommand.SetPartner(employer.Id, partner.Id);

            // Log in via the special login page.

            AutopeopleLogIn(employer);

            // In the vertical site should get all members.

            Search(membersAll);
        }

        private void AutopeopleLogIn(IUser reguser)
        {
            // Navigate to the autopeople page.

            var url = NavigationManager.GetUrlForPage<APAutoLogin>("username", reguser.GetLoginId(), "pwd", Password);
            Get(url);
        }

        protected Member[] CreateMembers(ref int memberIndex, Community community, int count)
        {
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(ref memberIndex, community);
            return members;
        }

        private Member CreateMember(ref int memberIndex, Community community)
        {
            var joinTime = new DateTime(2007, 1, 1).AddDays(memberIndex);
            var member = _memberAccountsCommand.CreateTestMember(memberIndex, community != null ? community.Id : (Guid?)null);

            _locationQuery.ResolveLocation(member.Address.Location, Australia, "Melbourne VIC 3000");
            member.CreatedTime = joinTime;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate, joinTime);

            _memberSearchService.UpdateMember(member.Id);

            memberIndex++;
            return member;
        }

        private void Search(ICollection<Member> members)
        {
            // Search.

            var searchUrl = _searchUrl.AsNonReadOnly();
            searchUrl.QueryString["JobTitle"] = BusinessAnalyst;
            Get(searchUrl);

            // Assert that the members are returned.

            AssertPageContains("Results <span class=\"start\">1</span> - <span class=\"end\">" + members.Count + "</span> of <span class=\"total\">" + members.Count + "</span>");
            foreach (var member in members)
                AssertPageContains(member.Address.Location.ToString());
        }

        protected void SendMessage(string subject, string body, Member member)
        {
            var variables = new NameValueCollection
            {
                {"candidateId", member.Id.ToString()},
                {"subject", subject},
                {"body", body},
                {"sendCopy", true.ToString()}
            };
            Post(_sendMessagesUrl, variables);
        }
    }
}
