using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class MemberNavTests
        : NavTests
    {
        private TestCommunity? _testCommunity;

        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();

        private static ReadOnlyApplicationUrl _homeUrl;

        private static ReadOnlyApplicationUrl _settingsUrl;
        private static ReadOnlyApplicationUrl _profileUrl;
        private const string ProfileText = "Profile";
        private const string ProfileSecondaryText = "Resume";
        private static ReadOnlyApplicationUrl _diaryUrl;
        private const string DiaryText = "CPD Diary";

        private static ReadOnlyApplicationUrl _friendsUrl;
        private const string FriendsText = "Friends";
        private const string FriendsSecondaryText = "Friends list";
        private static ReadOnlyApplicationUrl _findFriendsUrl;
        private const string FindFriendsText = "Find friends";
        private static ReadOnlyApplicationUrl _inviteFriendsUrl;
        private const string InviteFriendsText = "Invite friends";
        private static ReadOnlyApplicationUrl _invitationsUrl;
        private const string InvitationsText = "Invitations";
        private static ReadOnlyApplicationUrl _representativeUrl;
        private const string RepresentativeText = "Representative";

        private static ReadOnlyApplicationUrl _jobsUrl;
        private const string JobsText = "Jobs";
        private static ReadOnlyApplicationUrl _browseJobsUrl;
        private const string BrowseJobsText = "Browse all jobs";
        private static ReadOnlyApplicationUrl _applicationsUrl;
        private const string ApplicationsText = "Applications";
        private static ReadOnlyApplicationUrl _previousSearchesUrl;
        private const string PreviousSearchesText = "Recent and favourite searches";
        private static ReadOnlyApplicationUrl _suggestedJobsUrl;
        private const string SuggestedJobsText = "Suggested jobs";

        private static ReadOnlyApplicationUrl _resourcesUrl;
        private const string ResourcesText = "Resources";

        [TestInitialize]
        public void TestInitialize()
        {
            _testCommunity = null;

            _homeUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            _diaryUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/Diary.aspx");
            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
            _friendsUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/ViewFriends.aspx");
            _findFriendsUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/FindFriends.aspx");
            _inviteFriendsUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/InviteFriends.aspx");
            _invitationsUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/Invitations.aspx");
            _representativeUrl = new ReadOnlyApplicationUrl(true, "~/members/friends/ViewRepresentative.aspx");
            _jobsUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _browseJobsUrl = new ReadOnlyApplicationUrl("~/jobs");
            _applicationsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/PreviousApplications.aspx");
            _previousSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/recent");
            _suggestedJobsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/suggested");
            _resourcesUrl = new ReadOnlyApplicationUrl("~/members/resources");
        }

        [TestMethod]
        public void TestNavs()
        {
            Browser.UseMobileUserAgent = false;
            TestNavs(null);
        }

        [TestMethod]
        public void TestOverrideMobileNavs()
        {
            // Override the browser.

            Browser.UseMobileUserAgent = true;
            SwitchBrowser(false);

            TestNavs(null);
        }

        [TestMethod]
        public void TestVerticalNavs()
        {
            Browser.UseMobileUserAgent = false;
            TestNavs(TestCommunity.Scouts);
        }

        [TestMethod]
        public void TestOtdNavs()
        {
            Browser.UseMobileUserAgent = false;
            TestNavs(TestCommunity.Otd);
        }

        private void TestNavs(TestCommunity? testCommunity)
        {
            LogIn(testCommunity);
            var navs = GetNavs();
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);
        }

        private static IList<Nav> GetNavs()
        {
            return new List<Nav>
            {
                new Nav
                {
                    Url = _profileUrl,
                    Text = ProfileText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _profileUrl, Text = ProfileSecondaryText},
                        new Nav {Url = _diaryUrl, Text = DiaryText},
                    }
                },
                new Nav
                {
                    Url = _jobsUrl,
                    Text = JobsText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _jobsUrl, Text = JobsText},
                        new Nav {Url = _applicationsUrl, Text = ApplicationsText},
                        new Nav {Url = _previousSearchesUrl, Text = PreviousSearchesText},
                        new Nav {Url = _suggestedJobsUrl, Text = SuggestedJobsText},
                        new Nav {Url = _browseJobsUrl, Text = BrowseJobsText},
                    }
                },
                new Nav
                {
                    Url = _friendsUrl,
                    Text = FriendsText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _friendsUrl, Text = FriendsSecondaryText},
                        new Nav {Url = _findFriendsUrl, Text = FindFriendsText},
                        new Nav {Url = _inviteFriendsUrl, Text = InviteFriendsText},
                        new Nav {Url = _representativeUrl, Text = RepresentativeText},
                        new Nav {Url = _invitationsUrl, Text = InvitationsText},
                    }
                },
                new Nav
                {
                    Url = _resourcesUrl,
                    Text = ResourcesText,
                },
            };
        }

        private void LogIn(TestCommunity? testCommunity)
        {
            var member = _memberAccountsCommand.CreateTestMember(1);

            _testCommunity = testCommunity;
            if (testCommunity != null)
            {
                var community = testCommunity.Value.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
                _memberAffiliationsCommand.SetAffiliation(member.Id, community.Id);
            }

            LogIn(member);
        }

        protected override void AssertHeaderNavs()
        {
            // Second row.

            AssertNoEmployerSwitchNav();

            // Third row.

            AssertNoLoginNav();

            if (Browser.CurrentUrl.PathAndQuery == _settingsUrl.PathAndQuery)
                AssertNoSettingsNav();
            else
                AssertSettingsNav(_settingsUrl);

            if (_testCommunity != null && (_testCommunity.Value == TestCommunity.Otd || _testCommunity.Value == TestCommunity.UniMelbArts))
                AssertNoLogoutNav();
            else
                AssertLogoutNav();
        }

        protected override void AssertFooterNavs()
        {
            if (_testCommunity != null && (_testCommunity.Value == TestCommunity.Otd || _testCommunity.Value == TestCommunity.UniMelbArts))
            {
                AssertNoMainFooterNav(AboutText);
                AssertNoMainFooterNav(ContactText);
                AssertNoSubFooterNav(TermsText);
                AssertNoSubFooterNav(PrivacyText);
            }
            else
            {
                base.AssertFooterNavs();
            }
        }
    }
}