using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Web.Areas.Administrators.Models.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Administrators.Communications.Campaigns
{
    [TestClass]
    public class CampaignsIndexTest
        : CampaignsControllerTests
    {
        private const int MaxCampaignsPerPage = 20;

        [TestMethod]
        public void IndexTest()
        {
            // Create some test campaigns.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaigns = CreateCampaigns(0, MaxCampaignsPerPage - 1, null, administrator.Id);

            // Execute.

            var controller = CreateController(administrator);
            var result = controller.Index() as ViewResult;

            // Check.

            AssertResult(result, campaigns, null);
        }

        [TestMethod]
        public void IndexLotsTest()
        {
            // Create some test campaigns.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var campaigns = CreateCampaigns(0, 10 * MaxCampaignsPerPage + 1, null, administrator.Id);

            IndexTest(administrator, campaigns, 11, null);
        }

        [TestMethod]
        public void IndexLotsCategoryTest()
        {
            const int memberPages = 2;
            const int employerPages = 5;

            // Create some test campaigns.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var memberCampaigns = CreateCampaigns(0, memberPages * MaxCampaignsPerPage, CampaignCategory.Member, administrator.Id);
            var employerCampaigns = CreateCampaigns(memberPages * MaxCampaignsPerPage, employerPages * MaxCampaignsPerPage + 1, CampaignCategory.Employer, administrator.Id);
            var allCampaigns = employerCampaigns.Concat(memberCampaigns);

            // Test all the categories.

            IndexTest(administrator, allCampaigns, memberPages + employerPages + 1, null);
            IndexTest(administrator, memberCampaigns, memberPages, CampaignCategory.Member);
            IndexTest(administrator, employerCampaigns, employerPages, CampaignCategory.Employer);
        }

        private void IndexTest(IRegisteredUser administrator, IEnumerable<Campaign> campaigns, int pages, CampaignCategory? category)
        {
            // First page.

            var controller = CreateController(administrator);
            var result = (category == null ? controller.Index() : controller.CategoryIndex(category.Value)) as ViewResult;
            AssertResult(result, campaigns, null);

            // Check all pages.

            for (var page = 1; page <= pages; ++page)
            {
                result = (category == null ? controller.PagedIndex(page) : controller.PagedCategoryIndex(category.Value, page)) as ViewResult;
                AssertResult(result, campaigns, page);
            }
        }

        protected IList<Campaign> CreateCampaigns(int start, int count, CampaignCategory? category, Guid createdById)
        {
            var campaigns = new List<Campaign>();
            var now = DateTime.Now;

            for (var index = start; index < start + count; ++index)
            {
                var campaign = new Campaign
                {
                    Id = Guid.NewGuid(),
                    CreatedTime = now.AddSeconds(index),
                    Name = Format(CampaignNameFormat, index),
                    Category = category == null ? CampaignCategory.Employer : category.Value,
                    CreatedBy = createdById,
                };
                _campaignsCommand.CreateCampaign(campaign);

                // Newest campaigns at start.

                campaigns.Insert(0, campaign);
            }

            return campaigns;
        }

        private static string Format(string format, int index)
        {
            return string.Format(format, index < 10 ? "00" + index : (index < 100 ? "0" + index : index.ToString()));
        }

        private static void AssertResult(ViewResultBase result, IEnumerable<Campaign> campaigns, int? page)
        {
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            var model = result.ViewData.Model as PaginatedList<CampaignRecord>;
            Assert.IsNotNull(model);

            // Only the campaigns for this page are needed.

            page = page ?? 1;
            var expectedCampaigns = campaigns.Skip((page.Value - 1) * MaxCampaignsPerPage).Take(MaxCampaignsPerPage);

            Assert.AreEqual(campaigns.Count(), model.TotalItems);
            Assert.AreEqual(expectedCampaigns.Count(), model.CurrentItems.Count());
            for (var index = 0; index < expectedCampaigns.Count(); ++index)
                Assert.AreEqual(expectedCampaigns.ElementAt(index).Name, model.CurrentItems.ElementAt(index).Name);

            // Make sure the others are not there.

            var notExpectedCampaigns = campaigns.Take((page.Value - 1) * MaxCampaignsPerPage).Concat(campaigns.Skip(page.Value * MaxCampaignsPerPage));
            foreach (var notExpectedCampaign in notExpectedCampaigns)
            {
                var notExpectedCampaignName = notExpectedCampaign.Name;
                Assert.IsFalse(model.CurrentItems.Any(c => c.Name == notExpectedCampaignName));
            }
        }
    }
}
