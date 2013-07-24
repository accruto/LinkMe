using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateEmployerCriteriaTests
        : CampaignsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private HtmlCheckBoxTester _employersCheckBox;
        private HtmlCheckBoxTester _recruitersCheckBox;
        private HtmlCheckBoxTester _verifiedOrganisationsCheckBox;
        private HtmlCheckBoxTester _unverifiedOrganisationsCheckBox;
        private HtmlListBoxTester _industriesListBox;
        private HtmlTextBoxTester _minimumLoginsTextBox;
        private HtmlTextBoxTester _maximumLoginsTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _employersCheckBox = new HtmlCheckBoxTester(Browser, "Employers");
            _recruitersCheckBox = new HtmlCheckBoxTester(Browser, "Recruiters");
            _verifiedOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "VerifiedOrganisations");
            _unverifiedOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "UnverifiedOrganisations");
            _industriesListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _minimumLoginsTextBox = new HtmlTextBoxTester(Browser, "MinimumLogins");
            _maximumLoginsTextBox = new HtmlTextBoxTester(Browser, "MaximumLogins");
        }

        [TestMethod]
        public void TestDefaults()
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Employer, administrator);
            LogIn(administrator);
            Get(GetEditCriteriaUrl(campaign.Id));

            var criteria = new OrganisationEmployerSearchCriteria();
            AssertPageCriteria(criteria);
            AssertSavedCriteria(criteria, campaign.Id);
        }

        [TestMethod]
        public void TestUpdateEmployers()
        {
            Test(new OrganisationEmployerSearchCriteria { Employers = false }, () => _employersCheckBox.IsChecked = false);
        }

        [TestMethod]
        public void TestUpdateRecruiters()
        {
            Test(new OrganisationEmployerSearchCriteria { Recruiters = false }, () => _recruitersCheckBox.IsChecked = false);
        }

        [TestMethod]
        public void TestUpdateVerifiedOrganisations()
        {
            Test(new OrganisationEmployerSearchCriteria { VerifiedOrganisations = false }, () => _verifiedOrganisationsCheckBox.IsChecked = false);
        }

        [TestMethod]
        public void TestUpdateUnverifiedOrganisations()
        {
            Test(new OrganisationEmployerSearchCriteria { UnverifiedOrganisations = false }, () => _unverifiedOrganisationsCheckBox.IsChecked = false);
        }

        [TestMethod]
        public void TestUpdateIndustries()
        {
            var industry = _industriesQuery.GetIndustry("Administration");
            var criteria = new OrganisationEmployerSearchCriteria { IndustryIds = new[] { industry.Id } };
            Test(criteria, () => Select(industry));
        }

        [TestMethod]
        public void TestUpdateMultipleIndustries()
        {
            var industry1 = _industriesQuery.GetIndustry("Administration");
            var industry2 = _industriesQuery.GetIndustry("Engineering");
            var criteria = new OrganisationEmployerSearchCriteria { IndustryIds = new[] { industry1.Id, industry2.Id } };
            Test(criteria, () => Select(industry1, industry2));
        }

        [TestMethod]
        public void TestUpdateMinimumLogins()
        {
            const int value = 10;
            Test(new OrganisationEmployerSearchCriteria { MinimumLogins = value }, () => _minimumLoginsTextBox.Text = value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestUpdateMaximumLogins()
        {
            const int value = 20;
            Test(new OrganisationEmployerSearchCriteria { MaximumLogins = value }, () => _maximumLoginsTextBox.Text = value.ToString(CultureInfo.InvariantCulture));
        }

        private void Select(params Industry[] industries)
        {
            _industriesListBox.SelectedValues = (from i in industries select i.Id.ToString()).ToList();
        }

        private void Test(OrganisationEmployerSearchCriteria expectedCriteria, params Action[] updates)
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Employer, administrator);
            LogIn(administrator);
            Get(GetEditCriteriaUrl(campaign.Id));

            // Update the page.

            foreach (var update in updates)
                update();
            _saveButton.Click();

            AssertPageCriteria(expectedCriteria);
            AssertSavedCriteria(expectedCriteria, campaign.Id);
        }

        private void AssertSavedCriteria(OrganisationEmployerSearchCriteria expectedCriteria, Guid campaignId)
        {
            Assert.AreEqual(expectedCriteria, _campaignsQuery.GetCriteria(campaignId));
        }

        private void AssertPageCriteria(OrganisationEmployerSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.Employers, _employersCheckBox.IsChecked);
            Assert.AreEqual(criteria.Recruiters, _recruitersCheckBox.IsChecked);
            Assert.AreEqual(criteria.VerifiedOrganisations, _verifiedOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(criteria.UnverifiedOrganisations, _unverifiedOrganisationsCheckBox.IsChecked);

            AssertSelected(_industriesListBox, criteria.IndustryIds);

            Assert.AreEqual(criteria.MinimumLogins == null ? string.Empty : criteria.MinimumLogins.ToString(), _minimumLoginsTextBox.Text);
            Assert.AreEqual(criteria.MaximumLogins == null ? string.Empty : criteria.MaximumLogins.ToString(), _maximumLoginsTextBox.Text);
        }

        private static void AssertSelected(HtmlListBoxTester tester, ICollection<Guid> ids)
        {
            foreach (var item in tester.Items)
                Assert.AreEqual(ids.Contains(new Guid(item.Value)), item.IsSelected);
        }
    }
}
