using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateMemberCriteriaTests
        : CampaignsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private HtmlTextBoxTester _jobTitleTextBox;
        private HtmlTextBoxTester _companyKeywordsTextBox;
        private HtmlTextBoxTester _desiredJobTitleTextBox;
        private HtmlTextBoxTester _anyKeywordsTextBox;
        private HtmlTextBoxTester _allKeywordsTextBox;
        private HtmlTextBoxTester _exactPhraseTextBox;
        private HtmlTextBoxTester _withoutKeywordsTextBox;
        private HtmlDropDownListTester _countryDropDownList;
        private HtmlTextBoxTester _distanceTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlCheckBoxTester _includeRelocatingCheckBox;
        private HtmlListBoxTester _industriesListBox;
        private HtmlTextBoxTester _lowerBoundTextBox;
        private HtmlTextBoxTester _upperBoundTextBox;
        private HtmlDropDownListTester _communityDropDownList;
        private HtmlCheckBoxTester _availableNowCheckBox;
        private HtmlCheckBoxTester _activelyLookingCheckBox;
        private HtmlCheckBoxTester _openToOffersCheckBox;
        private HtmlCheckBoxTester _notLookingCheckBox;
        private HtmlCheckBoxTester _unspecifiedCheckBox;
        private HtmlCheckBoxTester _aboriginalCheckBox;
        private HtmlCheckBoxTester _torresIslanderCheckBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _jobTitleTextBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _companyKeywordsTextBox = new HtmlTextBoxTester(Browser, "CompanyKeywords");
            _desiredJobTitleTextBox = new HtmlTextBoxTester(Browser, "DesiredJobTitle");
            _anyKeywordsTextBox = new HtmlTextBoxTester(Browser, "AnyKeywords");
            _allKeywordsTextBox = new HtmlTextBoxTester(Browser, "AllKeywords");
            _exactPhraseTextBox = new HtmlTextBoxTester(Browser, "ExactPhrase");
            _withoutKeywordsTextBox = new HtmlTextBoxTester(Browser, "WithoutKeywords");
            _countryDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _distanceTextBox = new HtmlTextBoxTester(Browser, "Distance");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _includeRelocatingCheckBox = new HtmlCheckBoxTester(Browser, "IncludeRelocating");
            _industriesListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _lowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _upperBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryUpperBound");
            _availableNowCheckBox = new HtmlCheckBoxTester(Browser, "AvailableNow");
            _activelyLookingCheckBox = new HtmlCheckBoxTester(Browser, "ActivelyLooking");
            _openToOffersCheckBox = new HtmlCheckBoxTester(Browser, "OpenToOffers");
            _notLookingCheckBox = new HtmlCheckBoxTester(Browser, "NotLooking");
            _unspecifiedCheckBox = new HtmlCheckBoxTester(Browser, "Unspecified");
            _aboriginalCheckBox = new HtmlCheckBoxTester(Browser, "Aboriginal");
            _torresIslanderCheckBox = new HtmlCheckBoxTester(Browser, "TorresIslander");
            _communityDropDownList = new HtmlDropDownListTester(Browser, "CommunityId");
        }

        [TestMethod]
        public void TestDefaults()
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);
            LogIn(administrator);
            Get(GetEditCriteriaUrl(campaign.Id));

            var criteria = new MemberSearchCriteria();
            AssertPageCriteria(criteria);
            AssertSavedCriteria(criteria, campaign.Id);
        }

        [TestMethod]
        public void TestUpdateJobTitle()
        {
            const string value = "New job title";
            Test(new MemberSearchCriteria { JobTitle = value }, () => _jobTitleTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateCompanyKeywords()
        {
            const string value = "New company";
            Test(new MemberSearchCriteria { CompanyKeywords = value }, () => _companyKeywordsTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateDesiredJobTitle()
        {
            const string value = "New desired job title";
            Test(new MemberSearchCriteria { DesiredJobTitle = value }, () => _desiredJobTitleTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateAnyKeywords()
        {
            const string value = "Any keywords";
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, value, null);
            Test(criteria, () => _anyKeywordsTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateAllKeywords()
        {
            const string value = "All keywords";
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(value, null, null, null);
            Test(criteria, () => _allKeywordsTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateExactPhrase()
        {
            const string value = "Exact phrase";
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, value, null, null);
            Test(criteria, () => _exactPhraseTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateWithoutKeywords()
        {
            const string anyKeywords = "Any keywords";
            const string withoutKeywords = "Without keywords";
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(null, null, anyKeywords, withoutKeywords);
            Test(criteria, () => _anyKeywordsTextBox.Text = anyKeywords, () => _withoutKeywordsTextBox.Text = withoutKeywords);
        }

        [TestMethod]
        public void TestUpdateCountry()
        {
            var country = _locationQuery.GetCountry("New Zealand");
            var criteria = new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, null) };
            Test(criteria, () => Select(country));
        }

        [TestMethod]
        public void TestUpdateLocationNoCountry()
        {
            var country = _locationQuery.GetCountry("Australia");
            const string value = "Melbourne VIC 3000";
            var criteria = new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, value) };
            Test(criteria, () => _locationTextBox.Text = value);
        }

        [TestMethod]
        public void TestUpdateLocationCountry()
        {
            var country = _locationQuery.GetCountry("Australia");
            const string value = "Melbourne VIC 3000";
            var criteria = new MemberSearchCriteria { Location = _locationQuery.ResolveLocation(country, value) };
            Test(criteria, () => _locationTextBox.Text = value, () => Select(country));
        }

        [TestMethod]
        public void TestUpdateDistance()
        {
            const int value = 20;
            var criteria = new MemberSearchCriteria { Distance = value };
            Test(criteria, () => _distanceTextBox.Text = value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestUpdateRelocating()
        {
            const bool value = true;
            var criteria = new MemberSearchCriteria { IncludeRelocating = value };
            Test(criteria, () => _includeRelocatingCheckBox.IsChecked = value);
        }

        [TestMethod]
        public void TestUpdateLowerBound()
        {
            const decimal value = 10000;
            var criteria = new MemberSearchCriteria {Salary = new Salary {LowerBound = value}};
            Test(criteria, () => _lowerBoundTextBox.Text = value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestUpdateUpperBound()
        {
            const decimal value = 10000;
            var criteria = new MemberSearchCriteria { Salary = new Salary { UpperBound = value } };
            Test(criteria, () => _upperBoundTextBox.Text = value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestUpdateBothBounds()
        {
            const decimal lowerValue = 10000;
            const decimal upperValue = 20000;
            var criteria = new MemberSearchCriteria { Salary = new Salary { LowerBound = lowerValue, UpperBound = upperValue } };
            Test(criteria, () => _lowerBoundTextBox.Text = lowerValue.ToString(CultureInfo.InvariantCulture), () => _upperBoundTextBox.Text = upperValue.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestUpdateCandidateStatus()
        {
            var criteria = new MemberSearchCriteria { CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.NotLooking };
            Test(criteria, () => _activelyLookingCheckBox.IsChecked = true, () => _notLookingCheckBox.IsChecked = true);
        }

        [TestMethod]
        public void TestUpdateEthnicStatus()
        {
            var criteria = new MemberSearchCriteria { EthnicStatus = EthnicStatus.Aboriginal };
            Test(criteria, () => _aboriginalCheckBox.IsChecked = true);
        }

        [TestMethod]
        public void TestUpdateIndustries()
        {
            var industry = _industriesQuery.GetIndustry("Administration");
            var criteria = new MemberSearchCriteria { IndustryIds = new[]{industry.Id} };
            Test(criteria, () => Select(industry));
        }

        [TestMethod]
        public void TestUpdateMultipleIndustries()
        {
            var industry1 = _industriesQuery.GetIndustry("Administration");
            var industry2 = _industriesQuery.GetIndustry("Engineering");
            var criteria = new MemberSearchCriteria { IndustryIds = new[] { industry1.Id, industry2.Id } };
            Test(criteria, () => Select(industry1, industry2));
        }

        private void Select(Country country)
        {
            _countryDropDownList.SelectedValue = country.Id.ToString(CultureInfo.InvariantCulture);
        }

        private void Select(params Industry[] industries)
        {
            _industriesListBox.SelectedValues = (from i in industries select i.Id.ToString()).ToList();
        }

        private void Test(MemberSearchCriteria expectedCriteria, params Action[] updates)
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);
            LogIn(administrator);
            Get(GetEditCriteriaUrl(campaign.Id));

            // Update the page.

            foreach (var update in updates)
                update();
            _saveButton.Click();

            AssertPageCriteria(expectedCriteria);
            AssertSavedCriteria(expectedCriteria, campaign.Id);
        }

        private void AssertSavedCriteria(MemberSearchCriteria expectedCriteria, Guid campaignId)
        {
            Assert.AreEqual(expectedCriteria, _campaignsQuery.GetCriteria(campaignId));
        }

        private void AssertPageCriteria(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.JobTitle ?? string.Empty, _jobTitleTextBox.Text);
            Assert.AreEqual(criteria.CompanyKeywords ?? string.Empty, _companyKeywordsTextBox.Text);
            Assert.AreEqual(criteria.DesiredJobTitle ?? string.Empty, _desiredJobTitleTextBox.Text);
            Assert.AreEqual(criteria.AnyKeywords ?? string.Empty, _anyKeywordsTextBox.Text);
            Assert.AreEqual(criteria.AllKeywords ?? string.Empty, _allKeywordsTextBox.Text);
            Assert.AreEqual(criteria.ExactPhrase ?? string.Empty, _exactPhraseTextBox.Text);
            Assert.AreEqual(criteria.WithoutKeywords ?? string.Empty, _withoutKeywordsTextBox.Text);

            Assert.AreEqual(criteria.Location == null ? "" : criteria.Location.Country.Name, _countryDropDownList.SelectedItem.Text);
            Assert.AreEqual(criteria.Distance == null ? string.Empty : criteria.Distance.ToString(), _distanceTextBox.Text);
            Assert.AreEqual(criteria.Location == null ? string.Empty : criteria.Location.ToString(), _locationTextBox.Text);
            Assert.AreEqual(criteria.IncludeRelocating, _includeRelocatingCheckBox.IsChecked);

            AssertSelected(_industriesListBox, criteria.IndustryIds);

            Assert.AreEqual(criteria.Salary == null ? string.Empty : criteria.Salary.LowerBound.ToString(), _lowerBoundTextBox.Text);
            Assert.AreEqual(criteria.Salary == null ? string.Empty : criteria.Salary.UpperBound.ToString(), _upperBoundTextBox.Text);

            Assert.AreEqual(criteria.CandidateStatusFlags != null && criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.AvailableNow), _availableNowCheckBox.IsChecked);
            Assert.AreEqual(criteria.CandidateStatusFlags != null && criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.ActivelyLooking), _activelyLookingCheckBox.IsChecked);
            Assert.AreEqual(criteria.CandidateStatusFlags != null && criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.OpenToOffers), _openToOffersCheckBox.IsChecked);
            Assert.AreEqual(criteria.CandidateStatusFlags != null && criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.NotLooking), _notLookingCheckBox.IsChecked);
            Assert.AreEqual(criteria.CandidateStatusFlags != null && criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.Unspecified), _unspecifiedCheckBox.IsChecked);

            Assert.AreEqual(criteria.EthnicStatus != null && criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.Aboriginal), _aboriginalCheckBox.IsChecked);
            Assert.AreEqual(criteria.EthnicStatus != null && criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.TorresIslander), _torresIslanderCheckBox.IsChecked);

            Assert.AreEqual(string.Empty, _communityDropDownList.SelectedItem.Text);
        }

        private static void AssertSelected(HtmlListBoxTester tester, ICollection<Guid> ids)
        {
            foreach (var item in tester.Items)
                Assert.AreEqual(ids.Contains(new Guid(item.Value)), item.IsSelected);
        }
    }
}
