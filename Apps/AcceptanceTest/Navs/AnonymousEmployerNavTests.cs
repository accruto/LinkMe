using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class AnonymousEmployerNavTests
        : NavTests
    {
        private ReadOnlyApplicationUrl _membersUrl;
        private ReadOnlyApplicationUrl _homeUrl;

        private ReadOnlyApplicationUrl _searchUrl;
        private const string SearchText = "Candidate search";
        private const string SearchSecondaryText = "New search";

        private static ReadOnlyApplicationUrl _savedSearchesUrl;
        private const string SavedSearchesText = "Saved searches";

        private static ReadOnlyApplicationUrl _suggestedCandidatesUrl;
        private const string SuggestedCandidatesText = "Suggested candidates";

        private static ReadOnlyApplicationUrl _candidateAlertsUrl;
        private const string CandidateAlertsText = "Candidate alerts";

        private static ReadOnlyApplicationUrl _flaggedCandidatesUrl;
        private const string FlaggedCandidatesText = "Flagged candidates";

        private static ReadOnlyApplicationUrl _manageFoldersUrl;
        private const string ManageFoldersText = "Manage folders";

        private static ReadOnlyApplicationUrl _blocklistsUrl;
        private const string BlocklistsText = "Block lists";

        private static ReadOnlyApplicationUrl _openAdsUrl;
        private const string OpenAdsText = "Open ads";

        private static ReadOnlyApplicationUrl _newJobAdUrl;
        private const string PostAdText = "Job ads";
        private const string PostAdSecondaryText = "Post new ad";

        private static ReadOnlyApplicationUrl _draftAdsUrl;
        private const string DraftAdsText = "Draft ads";

        private static ReadOnlyApplicationUrl _closedAdsUrl;
        private const string ClosedAdsText = "Closed ads";

        private static ReadOnlyApplicationUrl _accountUrl;
        private const string AccountText = "Account";

        private static ReadOnlyApplicationUrl _orderUrl;
        private const string OrderText = "Purchase";

        private static ReadOnlyApplicationUrl _resourcesUrl;
        private const string ResourcesText = "Resources";

        private static ReadOnlyApplicationUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _membersUrl = new ReadOnlyApplicationUrl(false, "~/", new ReadOnlyQueryString("ignorePreferred", "true"));
            _homeUrl = new ReadOnlyApplicationUrl(true, "~/employers");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");

            var savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/SavedResumeSearches.aspx");
            _savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", savedSearchesUrl.Path));
            var suggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAdsSuggestions.aspx");
            _suggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", suggestedCandidatesUrl.Path));
            var candidateAlertsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/SavedResumeSearchAlerts.aspx");
            _candidateAlertsUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", candidateAlertsUrl.Path));

            var flaggedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
            _flaggedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", flaggedCandidatesUrl.Path));
            var manageFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders");
            _manageFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", manageFoldersUrl.Path));
            var blocklistsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blocklists/permanent");
            _blocklistsUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", blocklistsUrl.Path));

            _newJobAdUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
            var openAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Open");
            _openAdsUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", openAdsUrl.PathAndQuery));
            var draftAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Draft");
            _draftAdsUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", draftAdsUrl.PathAndQuery));
            var closedAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Closed");
            _closedAdsUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", closedAdsUrl.PathAndQuery));

            var accountUrl = new ReadOnlyApplicationUrl("~/employers/settings");
            _accountUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", accountUrl.Path));

            _orderUrl = new ReadOnlyApplicationUrl("~/employers/products/neworder");
            _resourcesUrl = new ReadOnlyApplicationUrl("~/employers/resources");
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
        }

        [TestMethod]
        public void TestNavs()
        {
            TestNavs(null);
        }

        [TestMethod]
        public void TestVerticalNavs()
        {
            TestNavs(TestCommunity.Scouts);
        }

        private void TestNavs(TestCommunity? testCommunity)
        {
            Set(testCommunity);
            var navs = GetNavs();
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);
        }

        private IList<Nav> GetNavs()
        {
            return new List<Nav>
            {
                new Nav
                {
                    Url = _searchUrl,
                    Text = SearchText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _searchUrl, Text = SearchSecondaryText},
                        new Nav {Url = _savedSearchesUrl, Text = SavedSearchesText},
                        new Nav {Url = _suggestedCandidatesUrl, Text = SuggestedCandidatesText},
                        new Nav {Url = _candidateAlertsUrl, Text = CandidateAlertsText},
                        new Nav {Url = _flaggedCandidatesUrl, Text = FlaggedCandidatesText},
                        new Nav {Url = _manageFoldersUrl, Text = ManageFoldersText},
                        new Nav {Url = _blocklistsUrl, Text = BlocklistsText},
                    }
                },
                new Nav
                {
                    Url = _newJobAdUrl,
                    Text = PostAdText,
                    SubNavs = new List<Nav>
                    {
                        new Nav {Url = _openAdsUrl, Text = OpenAdsText},
                        new Nav {Url = _newJobAdUrl, Text = PostAdSecondaryText},
                        new Nav {Url = _draftAdsUrl, Text = DraftAdsText},
                        new Nav {Url = _closedAdsUrl, Text = ClosedAdsText},
                    }
                },
                new Nav {Url = _accountUrl, Text = AccountText},
                new Nav {Url = _orderUrl, Text = OrderText},
                new Nav {Url = _resourcesUrl, Text = ResourcesText},
            };
        }

        private void Set(TestCommunity? testCommunity)
        {
            if (testCommunity != null)
            {
                var community = testCommunity.Value.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
                Get(new ReadOnlyApplicationUrl("~/" + _verticalsQuery.GetVertical(community.Id).Url));
            }
        }

        protected override void AssertHeaderNavs()
        {
            // Second row.

            AssertMemberSwitchNav(_membersUrl);

            // Third row.

            if (Browser.CurrentUrl.PathAndQuery == _homeUrl.PathAndQuery)
                AssertNoLoginNav();
            else
                AssertLoginNav(_loginUrl);
            AssertNoAccountNav();
            AssertNoLogoutNav();
        }
    }
}