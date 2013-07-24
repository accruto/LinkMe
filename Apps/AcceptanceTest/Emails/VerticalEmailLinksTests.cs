using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Emails
{
    [TestClass]
    public class VerticalEmailLinksTests
        : EmailsTests
    {
        private const string NonMemberInviteeEmailAddress = "carl@test.linkme.net.au";
        private const string StandardSnippet = "Your LinkMe team";
        private readonly CommunityTestData _vertical1Data = TestCommunity.Finsia.GetCommunityTestData();
        private const string Vertical1Snippet = "The Finsia Career Network team";
        private readonly CommunityTestData _vertical2Data = TestCommunity.Rcsa.GetCommunityTestData();
        private const string Vertical2Snippet = "RCSA Career &amp; Networking";
        private readonly string[] _snippets = new [] { StandardSnippet, Vertical1Snippet, Vertical2Snippet };

        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();

        [TestMethod]
        public void TestNoVerticalToMember()
        {
            TestToMember(null, false, null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestNoVerticalToVertical1Member()
        {
            TestToMember(null, false, _vertical1Data, false, Vertical1Snippet, _vertical1Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestNoVerticalToVertical2Member()
        {
            TestToMember(null, false, _vertical2Data, false, Vertical2Snippet, _vertical2Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestVertical1ToMember()
        {
            TestToMember(_vertical1Data, false, null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestVertical2ToMember()
        {
            TestToMember(_vertical2Data, false, null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestVertical1ToVertical2Member()
        {
            TestToMember(_vertical1Data, false, _vertical2Data, false, Vertical2Snippet, _vertical2Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestVertical2ToVertical1Member()
        {
            TestToMember(_vertical2Data, false, _vertical1Data, false, Vertical1Snippet, _vertical1Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestNoVerticalToNonMember()
        {
            TestToNonMember(null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestVertical1ToNonMember()
        {
            TestToNonMember(_vertical1Data, false, Vertical1Snippet, _vertical1Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestVertical2ToNonMember()
        {
            TestToNonMember(_vertical2Data, false, Vertical2Snippet, _vertical2Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestNoVerticalToDeletedVertical1Member()
        {
            TestToMember(null, false, _vertical1Data, true, StandardSnippet, null);
        }

        [TestMethod]
        public void TestNoVerticalToDeletedVertical2Member()
        {
            TestToMember(null, false, _vertical2Data, true, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical1ToMember()
        {
            TestToMember(_vertical1Data, true, null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical2ToMember()
        {
            TestToMember(_vertical2Data, true, null, false, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical1ToVertical2Member()
        {
            TestToMember(_vertical1Data, true, _vertical2Data, false, Vertical2Snippet, _vertical2Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestVertical1ToDeletedVertical2Member()
        {
            TestToMember(_vertical1Data, false, _vertical2Data, true, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical2ToVertical1Member()
        {
            TestToMember(_vertical2Data, true, _vertical1Data, false, Vertical1Snippet, _vertical1Data.HeaderSnippet);
        }

        [TestMethod]
        public void TestVertical2ToDeletedVertical1Member()
        {
            TestToMember(_vertical2Data, false, _vertical1Data, true, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical1ToNonMember()
        {
            TestToNonMember(_vertical1Data, true, StandardSnippet, null);
        }

        [TestMethod]
        public void TestDeletedVertical2ToNonMember()
        {
            TestToNonMember(_vertical2Data, true, StandardSnippet, null);
        }

        private void TestToMember(CommunityTestData fromData, bool isFromDeleted, CommunityTestData toData, bool isToDeleted, string expectedSnippet, string expectedHeaderSnippet)
        {
            var inviter = CreateInviter(fromData, isFromDeleted);

            // Invite member.

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            if (toData != null)
            {
                var community = toData.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

                if (isToDeleted)
                {
                    var vertical = _verticalsCommand.GetVertical(community);
                    vertical.IsDeleted = true;
                    _verticalsCommand.UpdateVertical(vertical);
                }

                _memberAffiliationsCommand.SetAffiliation(invitee.Id, community.Id);
            }

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitee);
            AssertEmail(email, expectedSnippet, expectedHeaderSnippet);
        }
        
        private void TestToNonMember(CommunityTestData fromData, bool isFromDeleted, string expectedSnippet, string expectedHeaderSnippet)
        {
            var inviter = CreateInviter(fromData, isFromDeleted);

            // Invite non-member.

            var invitation = new FriendInvitation { InviterId = inviter.Id, InviteeEmailAddress = NonMemberInviteeEmailAddress };
            _memberFriendsCommand.SendInvitation(invitation);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, invitation.InviteeEmailAddress);
            AssertEmail(email, expectedSnippet, expectedHeaderSnippet);
        }
        
        private Member CreateInviter(CommunityTestData fromData, bool isFromDeleted)
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            if (fromData != null)
            {
                var community = fromData.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

                if (isFromDeleted)
                {
                    var vertical = _verticalsCommand.GetVertical(community);
                    vertical.IsDeleted = true;
                    _verticalsCommand.UpdateVertical(vertical);
                }

                _memberAffiliationsCommand.SetAffiliation(inviter.Id, community.Id);
                inviter.AffiliateId = community.Id;
            }
            
            return inviter;
        }

        private void AssertEmail(MockEmail email, string expectedSnippet, string expectedHeaderSnippet)
        {
            foreach (var snippet in _snippets)
            {
                if (snippet == expectedSnippet)
                    email.AssertHtmlViewContains(snippet);
                else
                    email.AssertHtmlViewDoesNotContain(snippet);
            }

            var links = email.GetHtmlView().GetLinks();
            AssertLink(links[0], expectedHeaderSnippet);
        }
        
        private void AssertLink(ReadOnlyUrl url, string expectedHeaderSnippet)
        {
            Get(url);
            if (expectedHeaderSnippet != null)
                AssertHeader(expectedHeaderSnippet);
            else
                AssertNoHeader(null);
        }

        protected void AssertHeader(string headerSnippet)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='community-header']");
            Assert.IsTrue((xmlNode != null) == (headerSnippet != null));
            if (headerSnippet != null)
                AssertPageContains(headerSnippet);
        }

        protected void AssertNoHeader(string headerSnippet)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='community-header']");
            Assert.IsTrue(xmlNode == null);
            if (headerSnippet != null)
                AssertPageDoesNotContain(headerSnippet);
        }
    }
}
