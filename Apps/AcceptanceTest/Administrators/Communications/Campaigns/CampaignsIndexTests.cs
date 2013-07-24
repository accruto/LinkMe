using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class CampaignsIndexTests
        : CampaignsTests
    {
        [TestMethod]
        public void NavigateMinimalCampaignsTest()
        {
            IList<Campaign> campaigns;
            IList<Template> templates;
            CreateCampaigns(0, MaxCampaignsPerPage - 1, CampaignCategory.Employer, out campaigns, out templates);

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate.

            Get(GetCampaignsUrl());

            // Check.

            AssertCampaigns(campaigns, templates);
        }

        [TestMethod]
        public void NavigateLotsOfCampaignsTest()
        {
            IList<Campaign> campaigns;
            IList<Template> templates;
            CreateCampaigns(0, 10 * MaxCampaignsPerPage + 1, CampaignCategory.Employer, out campaigns, out templates);

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            NavigateTest(campaigns, templates, null);
        }

        [TestMethod]
        public void NavigateLotsOfCategoryCampaignsTest()
        {
            const int memberPages = 2;
            const int employerPages = 5;

            // Create some test campaigns.

            IList<Campaign> memberCampaigns;
            IList<Template> memberTemplates;
            CreateCampaigns(0, memberPages * MaxCampaignsPerPage, CampaignCategory.Member, out memberCampaigns, out memberTemplates);

            IList<Campaign> employerCampaigns;
            IList<Template> employerTemplates;
            CreateCampaigns(memberPages * MaxCampaignsPerPage, employerPages * MaxCampaignsPerPage + 1, CampaignCategory.Employer, out employerCampaigns, out employerTemplates);

            var allCampaigns = employerCampaigns.Concat(memberCampaigns).ToList();
            var allTemplates = employerTemplates.Concat(memberTemplates).ToList();

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Test all the categories.

            NavigateTest(allCampaigns, allTemplates, null);
            NavigateTest(memberCampaigns, memberTemplates, CampaignCategory.Member);
            NavigateTest(employerCampaigns, employerTemplates, CampaignCategory.Employer);
        }

        private void NavigateTest(ICollection<Campaign> campaigns, IEnumerable<Template> templates, CampaignCategory? category)
        {
            var totalPages = GetTotalPages(campaigns.Count, MaxCampaignsPerPage);

            // First page should contain first campaigns.

            Get(GetCampaignsUrl(category));
            AssertCampaigns(campaigns.Take(MaxCampaignsPerPage), templates.Take(MaxCampaignsPerPage), campaigns.Skip(MaxCampaignsPerPage), templates.Skip(MaxCampaignsPerPage));
            AssertPager(category, totalPages, null);

            // Work through all pages.

            for (var page = 1; page <= totalPages; ++page)
            {
                Get(GetCampaignsUrl(category, page));
                var expectedCampaigns = campaigns.Skip(MaxCampaignsPerPage * (page - 1)).Take(MaxCampaignsPerPage);
                var expectedTemplates = templates.Skip(MaxCampaignsPerPage * (page - 1)).Take(MaxCampaignsPerPage);

                // Should not be campaigns from other pages.

                var notExpectedCampaigns = campaigns.Take(MaxCampaignsPerPage * (page - 1)).Concat(campaigns.Skip(MaxCampaignsPerPage * page));
                var notExpectedTemplates = templates.Take(MaxCampaignsPerPage * (page - 1)).Concat(templates.Skip(MaxCampaignsPerPage * page));

                AssertCampaigns(expectedCampaigns, expectedTemplates, notExpectedCampaigns, notExpectedTemplates);
                AssertPager(category, totalPages, page);
            }
        }

        private void AssertPager(CampaignCategory? category, int totalPages, int? page)
        {
            // Check 'Prev'.

            var currentPage = page ?? 1;
            if (currentPage == 1)
                AssertPageDoesNotContain(GetPagerLink(category, 1, "Prev"));
            else
                AssertPageContains(GetPagerLink(category, currentPage - 1, "Prev"));

            // Check 'Next'

            if (currentPage == totalPages)
                AssertPageDoesNotContain(GetPagerLink(category, currentPage + 1, "Next"));
            else
                AssertPageContains(GetPagerLink(category, currentPage + 1, "Next"));

            // Test each page in pager.

            for (var testPage = 1; testPage <= totalPages; ++testPage)
            {
                if (testPage == currentPage)
                {
                    AssertPageContains(GetCurrentPagerSpan(testPage));
                    AssertPageDoesNotContain(GetPagerLink(category, testPage));
                }
                else
                {
                    AssertPageDoesNotContain(GetCurrentPagerSpan(testPage));
                    AssertPageContains(GetPagerLink(category, testPage));
                }
            }
        }

        private static string GetCurrentPagerSpan(int page)
        {
            return "<span class=\"Curr\">" + page + "</span>";
        }

        private string GetPagerLink(CampaignCategory? category, int page)
        {
            var url = GetCampaignsUrl(category, page);
            return "<a href=\"" + url.Path + "\">" + page + "</a>";
        }

        private string GetPagerLink(CampaignCategory? category, int page, string text)
        {
            var url = GetCampaignsUrl(category, page);
            return "<a href=\"" + url.Path + "\">" + text + "</a>";
        }

        private void AssertCampaigns(IEnumerable<Campaign> expectedCampaigns, IEnumerable<Template> expectedTemplates)
        {
            AssertCampaigns(expectedCampaigns, expectedTemplates, null, null);
        }

        private void AssertCampaigns(IEnumerable<Campaign> expectedCampaigns, IEnumerable<Template> expectedTemplates, IEnumerable<Campaign> notExpectedCampaigns, IEnumerable<Template> notExpectedTemplates)
        {
            foreach (var campaign in expectedCampaigns)
                AssertPageContains(campaign.Name);
            foreach (var template in expectedTemplates)
                AssertPageContains(template.Subject);

            if (notExpectedCampaigns != null)
            {
                foreach (var campaign in notExpectedCampaigns)
                    AssertPageDoesNotContain(campaign.Name);
                foreach (var template in notExpectedTemplates)
                    AssertPageDoesNotContain(template.Subject);
            }
        }
    }
}
