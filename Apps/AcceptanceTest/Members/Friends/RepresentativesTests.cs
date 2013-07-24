using System;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public abstract class RepresentativesTests
        : WebFormDataTestCase
    {
        protected const string Message = "Please be my representative";

        protected readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        protected readonly IRepresentativesQuery _representativesQuery = Resolve<IRepresentativesQuery>();
        protected readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        protected readonly IMemberFriendsQuery _memberFriendsQuery = Resolve<IMemberFriendsQuery>();
        protected readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();

        protected HtmlTextBoxTester _txtName;
        protected HtmlTextBoxTester _txtEmailAddress;
        protected HtmlButtonTester _btnSearch;
        protected HtmlLinkTester _lnkRepresentative;

        [TestInitialize]
        public void RepresentativesTestsInitialize()
        {
            _txtName = new HtmlTextBoxTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_txtName");
            _txtEmailAddress = new HtmlTextBoxTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_txtEmailAddress");
            _btnSearch = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_btnSearch");
            _lnkRepresentative = new HtmlLinkTester(Browser, AddBodyPrefix("FormContent_TopContent") + "_ucRepresentativeContactsList_rptContacts_ctl00_ucContactsListDetails_lnkFullName");
        }

        protected void AssertRepresentative(Guid memberId)
        {
            Assert.IsTrue(_lnkRepresentative.IsVisible);
            Assert.AreEqual(NavigationManager.GetUrlForPage<ViewFriend>("friendId", memberId.ToString("n")).PathAndQuery.ToLower(), _lnkRepresentative.HRef.ToLower());
        }

        protected void AssertNoRepresentative()
        {
            Assert.IsFalse(_lnkRepresentative.IsVisible);
        }

        protected void AssertInvitation(Guid inviterId, Guid inviteeId, string message)
        {
            AssertInvitation(inviterId, inviteeId, message, _memberFriendsQuery.GetRepresentativeInvitation(inviterId, inviteeId));
            AssertInvitation(inviterId, inviteeId, message, _memberFriendsQuery.GetRepresentativeInvitationByInviter(inviterId));
        }

        private static void AssertInvitation(Guid inviterId, Guid inviteeId, string message, Invitation invitation)
        {
            Assert.IsNotNull(invitation);
            Assert.AreEqual(inviteeId, invitation.InviteeId);
            Assert.IsNull(invitation.InviteeEmailAddress);
            Assert.AreEqual(inviterId, invitation.InviterId);
            Assert.AreEqual(RequestStatus.Pending, invitation.Status);
            Assert.AreEqual(message, invitation.Text);
        }
    }
}
