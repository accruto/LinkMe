using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Affiliations.Partners;
using LinkMe.Domain.Roles.Affiliations.Partners.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Web.UI.Unregistered.Autopeople;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class EmployerNavTests
        : NavTests
    {
        private readonly IPartnersCommand _partnersCommand = Resolve<IPartnersCommand>();

        private TestCommunity? _testCommunity;

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

        private static ReadOnlyApplicationUrl _purchaseUrl;
        private const string PurchaseText = "Purchase";

        private static ReadOnlyApplicationUrl _resourcesUrl;
        private const string ResourcesText = "Resources";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private ReadOnlyUrl _resetUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _testCommunity = null;

            _homeUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");

            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/SavedResumeSearches.aspx");
            _suggestedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAdsSuggestions.aspx");
            _candidateAlertsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/SavedResumeSearchAlerts.aspx");

            _flaggedCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
            _manageFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders");
            _blocklistsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blocklists/permanent");

            _newJobAdUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
            _openAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Open");
            _draftAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Draft");
            _closedAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Closed");

            _accountUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");

            _purchaseUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/neworder");

            _resourcesUrl = new ReadOnlyApplicationUrl(true, "~/employers/resources");

            _resetUrl = new ReadOnlyApplicationUrl("~/verticals/reset");
        }

        [TestMethod]
        public void TestNavs()
        {
            TestNavs(null, GetNavs());
        }

        [TestMethod]
        public void TestVerticalNavs()
        {
            TestNavs(TestCommunity.Scouts, GetNavs());
        }

        [TestMethod]
        public void TestOtdNavs()
        {
            TestNavs(TestCommunity.Otd, GetNavs());
        }

        [TestMethod]
        public void TestUniMelbArtsNavs()
        {
            // This community has organisationational units that cannot search all members, hence should not have purchase nav.

            TestNavs(TestCommunity.UniMelbArts, GetUniMelbArtsNavs());
        }

        /// <summary>
        /// Autopeople is a custom implementation of a community before communities existed.
        /// There was some custom code for navs and this test is to make sure that this is maintained
        /// as Autopeople is migrated from its custom implementation to a general community implementation.
        /// Differences from general employer (see previous test):
        /// - No pricing primary nav.
        /// </summary>
        [TestMethod]
        public void TestAutopeopleNavs()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Associate them with the Autopeople service partner.

            var data = TestCommunity.Autopeople.GetCommunityTestData();
            var partner = new Partner { Name = data.Name };
            _partnersCommand.CreatePartner(partner);
            _partnersCommand.SetPartner(employer.Id, partner.Id);

            // Create the community.

            data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Log in via the special login page.

            AutopeopleLogIn(employer.GetLoginId(), "password");
            _testCommunity = TestCommunity.Autopeople;
            var navs = GetAutopeopleNavs();
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);

            // Log in via the standard process.

            LogOut();
            Get(_resetUrl);
            LogIn(employer);
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
                new Nav {Url = _purchaseUrl, Text = PurchaseText},
                new Nav {Url = _resourcesUrl, Text = ResourcesText},
            };
        }

        private IList<Nav> GetUniMelbArtsNavs()
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
                new Nav {Url = _resourcesUrl, Text = ResourcesText},
            };
        }

        private IList<Nav> GetAutopeopleNavs()
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
                new Nav {Url = _resourcesUrl, Text = ResourcesText},
            };
        }

        private void TestNavs(TestCommunity? testCommunity, IList<Nav> navs)
        {
            LogIn(testCommunity);
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);
        }

        private void LogIn(TestCommunity? testCommunity)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            _testCommunity = testCommunity;
            if (testCommunity != null)
            {
                var community = testCommunity.Value.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
                Get(new ReadOnlyApplicationUrl("~/" + _verticalsQuery.GetVertical(community.Id).Url));
            }

            LogIn(employer);
        }

        private void AutopeopleLogIn(string loginId, string password)
        {
            // Navigate to the autopeople page.

            var url = NavigationManager.GetUrlForPage<APAutoLogin>("username", loginId, "pwd", password);
            Get(url);
        }

        protected override void AssertHeaderNavs()
        {
            if (_testCommunity != null && _testCommunity.Value == TestCommunity.Autopeople)
            {
                // Second row.

                AssertNoMemberSwitchNav();

                // Third row.

                AssertNoLoginNav();
                AssertNoAccountNav();
                AssertNoLogoutNav();
            }
            else
            {
                // Second row.

                AssertNoMemberSwitchNav();

                // Third row.

                AssertNoLoginNav();
                AssertAccountNav(_accountUrl);

                if (_testCommunity != null && _testCommunity.Value == TestCommunity.Otd)
                    AssertNoLogoutNav();
                else
                    AssertLogoutNav();
            }
        }

        protected override void AssertFooterNavs()
        {
            if (_testCommunity != null && (_testCommunity.Value == TestCommunity.Otd || _testCommunity.Value == TestCommunity.Autopeople))
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