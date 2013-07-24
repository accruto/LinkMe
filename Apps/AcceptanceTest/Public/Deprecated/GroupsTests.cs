using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Deprecated
{
    [TestClass]
    public class GroupsTests
        : WebTestClass
    {
        private ReadOnlyUrl _groupsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _groupsUrl = new ReadOnlyApplicationUrl("~/groups");
        }

        [TestMethod]
        public void TestDeprecation()
        {
            TestDeprecation("~/groups/public");
            TestDeprecation("~/groups/PublicHome.aspx");
            TestDeprecation("~/groups/browse");
            TestDeprecation("~/groups/ViewGroups.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/public");
            TestDeprecation("~/groups/ViewPublicGroup.aspx");
            TestDeprecation("~/groups/create");
            TestDeprecation("~/groups/Create.aspx");
            TestDeprecation("~/groups");
            TestDeprecation("~/groups/Home.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A");
            TestDeprecation("~/groups/ViewGroup.aspx");
            TestDeprecation("~/groups/mine");
            TestDeprecation("~/groups/MyGroups.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/create3");
            TestDeprecation("~/groups/Create3.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/edit");
            TestDeprecation("~/groups/Edit.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/members");
            TestDeprecation("~/groups/ViewMembers.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/invite");
            TestDeprecation("~/groups/InviteFriends.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/discussions/public");
            TestDeprecation("~/groups/discussions/ViewPublicDiscussions.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/discussions/discussion/public");
            TestDeprecation("~/groups/discussions/ViewPublicDiscussion.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/discussions");
            TestDeprecation("~/groups/discussions/ViewDiscussions.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/discussions/discussion");
            TestDeprecation("~/groups/discussions/ViewDiscussion.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events");
            TestDeprecation("~/groups/events/ViewEvents.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events/create");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events/edit");
            TestDeprecation("~/groups/events/Create.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events/event");
            TestDeprecation("~/groups/events/ViewEvent.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events/invite");
            TestDeprecation("~/groups/events/InviteAttendees.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/events/attendees");
            TestDeprecation("~/groups/events/ViewAttendees.aspx");
            TestDeprecation("~/groups/9366102F-E3AC-4E52-B01E-9461A2C3B89A/files");
            TestDeprecation("~/groups/files/ViewFiles.aspx");

            // Old-old groups urls.

            TestDeprecation("~/ui/unregistered/groups/GroupsHome.aspx");
            TestDeprecation("~/ui/unregistered/groups/AllGroups.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/GroupList.aspx");
            TestDeprecation("~/ui/unregistered/groups/ViewGroup.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/Create.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/GroupsHome.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/ViewGroup.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/MyGroups.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/Create3.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/Edit.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/ViewGroupMembers.aspx");
            TestDeprecation("~/ui/registered/networkers/groups/InviteFriendsToGroup.aspx");
        }

        private void TestDeprecation(string url)
        {
            Get(new ReadOnlyApplicationUrl(url));
            AssertUrl(_groupsUrl);
            AssertPageContains("Unfortunately, this means that Groups will no longer be available on our site.");
        }
    }
}
