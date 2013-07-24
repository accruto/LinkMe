using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    public class AnyKeywordsTester
        : HtmlTester
    {
        private readonly HtmlTextBoxTester _anyKeywords1TextBox;
        private readonly HtmlTextBoxTester _anyKeywords2TextBox;
        private readonly HtmlTextBoxTester _anyKeywords3TextBox;

        public AnyKeywordsTester(HttpClient browser, string aspId1, string aspId2, string aspId3)
            : base(browser, aspId1)
        {
            _anyKeywords1TextBox = new HtmlTextBoxTester(browser, aspId1);
            _anyKeywords2TextBox = new HtmlTextBoxTester(browser, aspId2);
            _anyKeywords3TextBox = new HtmlTextBoxTester(browser, aspId3);
        }

        public string[] Texts
        {
            get
            {
                return new []
                {
                    _anyKeywords1TextBox.Text,
                    _anyKeywords2TextBox.Text,
                    _anyKeywords3TextBox.Text
                };
            }
            set
            {
                var name = GetAttributeValue("name");
                SetValue(name, value[0]);
                AddValue(name, value[1]);
                AddValue(name, value[2]);
            }
        }

        protected override void AssertNode(HtmlNode node)
        {
        }
    }

    [TestClass]
    public abstract class SearchTests
        : CandidateListsTests
    {
        protected const string BusinessAnalyst = "business analyst";

        private const int MaxSalary = 250000;
        private const int DefaultDistance = 50;
        private const int DefaultRegionalDistance = 0;

        protected IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICommunitiesQuery _communitiesQuery = Resolve<ICommunitiesQuery>();

        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _resultsUrl;
        private ReadOnlyUrl _partialSearchUrl;
        private ReadOnlyUrl _baseSavedSearchUrl;

        protected HtmlTextBoxTester _keywordsTextBox;
        protected HtmlTextBoxTester _allKeywordsTextBox;
        protected HtmlTextBoxTester _exactPhraseTextBox;
        protected AnyKeywordsTester _anyKeywordsTester;
        protected HtmlTextBoxTester _withoutKeywordsTextBox;

        protected HtmlCheckBoxTester _availableNowCheckBox;
        protected HtmlCheckBoxTester _activelyLookingCheckBox;
        protected HtmlCheckBoxTester _openToOffersCheckBox;
        protected HtmlCheckBoxTester _unspecifiedCheckBox;

        protected HtmlCheckBoxTester _fullTimeCheckBox;
        protected HtmlCheckBoxTester _partTimeCheckBox;
        protected HtmlCheckBoxTester _contractCheckBox;
        protected HtmlCheckBoxTester _tempCheckBox;
        protected HtmlCheckBoxTester _jobShareCheckBox;

        protected HtmlTextBoxTester _educationKeywordsTextBox;

        protected HtmlCheckBoxTester _allIndustriesCheckBox;
        protected IList<HtmlCheckBoxTester> _industryIdCheckBoxes;

        protected HtmlTextBoxTester _jobTitleTextBox;
        protected HtmlTextBoxTester _desiredJobTitleTextBox;
        protected HtmlRadioButtonTester _recentJobsRadioButton;
        protected HtmlRadioButtonTester _allJobsRadioButton;

        protected HtmlTextBoxTester _companyKeywordsTextBox;
        protected HtmlRadioButtonTester _recentCompaniesRadioButton;
        protected HtmlRadioButtonTester _allCompaniesRadioButton;
        protected HtmlRadioButtonTester _lastCompanyRadioButton;

        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlDropDownListTester _countryIdDropDownList;
        protected HtmlDropDownListTester _distanceDropDownList;
        protected HtmlCheckBoxTester _includeRelocatingCheckBox;
        protected HtmlCheckBoxTester _includeInternationalCheckBox;
        protected HtmlCheckBoxTester _includeRelocatingFilterCheckBox;
        protected HtmlCheckBoxTester _includeInternationalFilterCheckBox;

        protected HtmlDropDownListTester _sortOrderDropDownList;
        protected HtmlRadioButtonTester _sortOrderIsAscendingRadioButton;
        protected HtmlRadioButtonTester _sortOrderIsDescendingRadioButton;

        protected HtmlHiddenTester _includeSynonymsTextBox;

        protected HtmlTextBoxTester _recencyTextBox;

        protected HtmlTextBoxTester _salaryLowerBoundTextBox;
        protected HtmlTextBoxTester _salaryUpperBoundTextBox;

        protected HtmlDropDownListTester _communityIdDropDownList;

        protected HtmlRadioButtonTester _inFolderEitherRadioButton;
        protected HtmlRadioButtonTester _inFolderYesRadioButton;
        protected HtmlRadioButtonTester _inFolderNoRadioButton;
        protected HtmlRadioButtonTester _isFlaggedEitherRadioButton;
        protected HtmlRadioButtonTester _isFlaggedYesRadioButton;
        protected HtmlRadioButtonTester _isFlaggedNoRadioButton;
        protected HtmlRadioButtonTester _hasNotesEitherRadioButton;
        protected HtmlRadioButtonTester _hasNotesYesRadioButton;
        protected HtmlRadioButtonTester _hasNotesNoRadioButton;
        protected HtmlRadioButtonTester _hasViewedEitherRadioButton;
        protected HtmlRadioButtonTester _hasViewedYesRadioButton;
        protected HtmlRadioButtonTester _hasViewedNoRadioButton;
        protected HtmlRadioButtonTester _isUnlockedEitherRadioButton;
        protected HtmlRadioButtonTester _isUnlockedYesRadioButton;
        protected HtmlRadioButtonTester _isUnlockedNoRadioButton;

        protected HtmlCheckBoxTester _ethnicStatusCheckBox;
        protected HtmlCheckBoxTester _aboriginalCheckBox;
        protected HtmlCheckBoxTester _torresIslanderCheckBox;

        protected HtmlCheckBoxTester _visaStatusCheckBox;
        protected HtmlCheckBoxTester _citizenCheckBox;
        protected HtmlCheckBoxTester _unrestrictedWorkVisaCheckBox;
        protected HtmlCheckBoxTester _restrictedWorkVisaCheckBox;
        protected HtmlCheckBoxTester _noWorkVisaCheckBox;

        protected HtmlTextBoxTester _searchByNameTextBox;
        protected HtmlRadioButtonTester _exactMatchNameRadioButton;
        protected HtmlRadioButtonTester _includeSimilarNamesRadioButton;

        protected HtmlButtonTester _searchButton;

        [TestInitialize]
        public void SearchTestsInitialize()
        {
            ClearSearchIndexes();

            _candidatesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _resultsUrl = new ReadOnlyApplicationUrl("~/search/candidates/results");
            _partialSearchUrl = new ReadOnlyApplicationUrl("~/search/candidates/partial");
            _baseSavedSearchUrl = new ReadOnlyApplicationUrl("~/search/candidates/saved/");

            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _allKeywordsTextBox = new HtmlTextBoxTester(Browser, "AllKeywords");
            _exactPhraseTextBox = new HtmlTextBoxTester(Browser, "ExactPhrase");
            _anyKeywordsTester = new AnyKeywordsTester(Browser, "AnyKeywords1", "AnyKeywords2", "AnyKeywords3");
            _withoutKeywordsTextBox = new HtmlTextBoxTester(Browser, "WithoutKeywords");

            _availableNowCheckBox = new HtmlCheckBoxTester(Browser, "AvailableNow");
            _activelyLookingCheckBox = new HtmlCheckBoxTester(Browser, "ActivelyLooking");
            _openToOffersCheckBox = new HtmlCheckBoxTester(Browser, "OpenToOffers");
            _unspecifiedCheckBox = new HtmlCheckBoxTester(Browser, "Unspecified");

            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _partTimeCheckBox = new HtmlCheckBoxTester(Browser, "PartTime");
            _contractCheckBox = new HtmlCheckBoxTester(Browser, "Contract");
            _tempCheckBox = new HtmlCheckBoxTester(Browser, "Temp");
            _jobShareCheckBox = new HtmlCheckBoxTester(Browser, "JobShare");

            _educationKeywordsTextBox = new HtmlTextBoxTester(Browser, "EducationKeywords");

            _allIndustriesCheckBox = new HtmlCheckBoxTester(Browser, "all-industries");
            _industryIdCheckBoxes = new List<HtmlCheckBoxTester>();
            for (var index = 0; index < _industriesQuery.GetIndustries().Count; ++index)
                _industryIdCheckBoxes.Add(new HtmlCheckBoxTester(Browser, "industry" + index));

            _searchByNameTextBox = new HtmlTextBoxTester(Browser, "CandidateName");
            _exactMatchNameRadioButton = new HtmlRadioButtonTester(Browser, "ExactMatch");
            _includeSimilarNamesRadioButton = new HtmlRadioButtonTester(Browser, "CloseMatch");

            _jobTitleTextBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _desiredJobTitleTextBox = new HtmlTextBoxTester(Browser, "DesiredJobTitle");
            _recentJobsRadioButton = new HtmlRadioButtonTester(Browser, "RecentJobs");
            _allJobsRadioButton = new HtmlRadioButtonTester(Browser, "AllJobs");

            _companyKeywordsTextBox = new HtmlTextBoxTester(Browser, "CompanyKeywords");
            _recentCompaniesRadioButton = new HtmlRadioButtonTester(Browser, "RecentCompanies");
            _allCompaniesRadioButton = new HtmlRadioButtonTester(Browser, "AllCompanies");
            _lastCompanyRadioButton = new HtmlRadioButtonTester(Browser, "LastCompany");

            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _countryIdDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _distanceDropDownList = new HtmlDropDownListTester(Browser, "Distance");
            _includeRelocatingCheckBox = new HtmlCheckBoxTester(Browser, "IncludeRelocating");
            _includeInternationalCheckBox = new HtmlCheckBoxTester(Browser, "IncludeInternational");
            _includeRelocatingFilterCheckBox = new HtmlCheckBoxTester(Browser, "IncludeRelocatingFilter");
            _includeInternationalFilterCheckBox = new HtmlCheckBoxTester(Browser, "IncludeInternationalFilter");

            _sortOrderDropDownList = new HtmlDropDownListTester(Browser, "SortOrder");
            _sortOrderIsAscendingRadioButton = new HtmlRadioButtonTester(Browser, "SortOrderIsAscending");
            _sortOrderIsDescendingRadioButton = new HtmlRadioButtonTester(Browser, "SortOrderIsDescending");

            _includeSynonymsTextBox = new HtmlHiddenTester(Browser, "IncludeSynonyms");

            _recencyTextBox = new HtmlTextBoxTester(Browser, "Recency");

            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _salaryUpperBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryUpperBound");

            _inFolderEitherRadioButton = new HtmlRadioButtonTester(Browser, "InFolderEither");
            _inFolderYesRadioButton = new HtmlRadioButtonTester(Browser, "InFolderYes");
            _inFolderNoRadioButton = new HtmlRadioButtonTester(Browser, "InFolderNo");

            _isFlaggedEitherRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedEither");
            _isFlaggedYesRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedYes");
            _isFlaggedNoRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedNo");

            _hasNotesEitherRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesEither");
            _hasNotesYesRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesYes");
            _hasNotesNoRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesNo");

            _hasViewedEitherRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedEither");
            _hasViewedYesRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedYes");
            _hasViewedNoRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedNo");

            _isUnlockedEitherRadioButton = new HtmlRadioButtonTester(Browser, "IsUnlockedEither");
            _isUnlockedYesRadioButton = new HtmlRadioButtonTester(Browser, "IsUnlockedYes");
            _isUnlockedNoRadioButton = new HtmlRadioButtonTester(Browser, "IsUnlockedNo");

            _ethnicStatusCheckBox = new HtmlCheckBoxTester(Browser, "EthnicStatus");
            _aboriginalCheckBox = new HtmlCheckBoxTester(Browser, "Aboriginal");
            _torresIslanderCheckBox = new HtmlCheckBoxTester(Browser, "TorresIslander");

            _visaStatusCheckBox = new HtmlCheckBoxTester(Browser, "VisaStatus");
            _citizenCheckBox = new HtmlCheckBoxTester(Browser, "Citizen");
            _unrestrictedWorkVisaCheckBox = new HtmlCheckBoxTester(Browser, "UnrestrictedWorkVisa");
            _restrictedWorkVisaCheckBox = new HtmlCheckBoxTester(Browser, "RestrictedWorkVisa");
            _noWorkVisaCheckBox = new HtmlCheckBoxTester(Browser, "NoWorkVisa");

            _communityIdDropDownList = new HtmlDropDownListTester(Browser, "CommunityId");

            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        protected ReadOnlyUrl GetCandidatesUrl(params Guid[] candidateIds)
        {
            var url = _candidatesUrl.AsNonReadOnly();
            foreach (var candidateId in candidateIds)
                url.QueryString.Add("candidateId", candidateId.ToString());
            return url;
        }

        protected ReadOnlyUrl GetSearchUrl()
        {
            return _searchUrl;
        }

        protected ReadOnlyUrl GetResultsUrl()
        {
            return _resultsUrl;
        }

        protected ReadOnlyUrl GetPartialSearchUrl()
        {
            return _partialSearchUrl;
        }

        protected ReadOnlyUrl GetSearchUrl(int? page, int? items)
        {
            if (page == null && items == null)
                return GetSearchUrl();
            return Get(GetSearchUrl(), page, items);
        }

        protected ReadOnlyUrl GetSearchUrl(string keywords)
        {
            return Get(GetSearchUrl(), keywords);
        }

        protected ReadOnlyUrl GetSearchUrl(MemberSearchCriteria criteria)
        {
            return Get(GetSearchUrl(), criteria);
        }

        protected ReadOnlyUrl GetSearchUrl(string keywords, MemberSortOrder sortOrder, bool isAscending)
        {
            return Get(Get(GetSearchUrl(), keywords), sortOrder, isAscending);
        }

        protected ReadOnlyUrl GetSearchUrl(MemberSearchCriteria criteria, MemberSortOrder sortOrder, bool isAscending)
        {
            return Get(Get(GetSearchUrl(), criteria), sortOrder, isAscending);
        }

        protected ReadOnlyUrl GetPartialSearchUrl(string keywords)
        {
            return Get(GetPartialSearchUrl(), keywords);
        }

        protected ReadOnlyUrl GetPartialSearchUrl(MemberSearchCriteria criteria)
        {
            return Get(GetPartialSearchUrl(), criteria);
        }

        protected ReadOnlyUrl GetPartialSearchUrl(MemberSearchCriteria criteria, DetailLevel detailLevel)
        {
            return Get(Get(GetPartialSearchUrl(), criteria), detailLevel);
        }

        protected ReadOnlyUrl GetSavedSearchUrl(Guid searchId)
        {
            return new ReadOnlyApplicationUrl(_baseSavedSearchUrl, searchId.ToString());
        }

        private ReadOnlyUrl Get(ReadOnlyUrl baseUrl, string keywords)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(keywords);
            return Get(baseUrl, criteria);
        }

        private ReadOnlyUrl Get(ReadOnlyUrl baseUrl, MemberSearchCriteria criteria)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add(new QueryStringGenerator(new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery)).GenerateQueryString(criteria));
            return url;
        }

        protected void AssertCriteria(MemberSearchCriteria criteria)
        {
            AssertKeywords(criteria);
            AssertCandidateStatus(criteria);
            AssertJobTypes(criteria);
            Assert.AreEqual(criteria.EducationKeywords ?? string.Empty, _educationKeywordsTextBox.Text);
            AssertIndustryIds(criteria);
            Assert.AreEqual(criteria.JobTitle ?? string.Empty, _jobTitleTextBox.Text);
            Assert.AreEqual(criteria.DesiredJobTitle ?? string.Empty, _desiredJobTitleTextBox.Text);
            AssertJobsToSearch(criteria);
            Assert.AreEqual(criteria.Name ?? string.Empty, _searchByNameTextBox.Text);
            AssertIncludeSimilarName(criteria);
            Assert.AreEqual(criteria.CompanyKeywords ?? string.Empty, _companyKeywordsTextBox.Text);
            AssertCompaniesToSearch(criteria);
            AssertLocation(criteria);
            AssertSortOrder(criteria);
            AssertActivity(criteria);
            AssertEthnicStatus(criteria);
            AssertVisaStatus(criteria);
            AssertCommunityId(criteria);
            Assert.AreEqual(criteria.IncludeSynonyms ? "true" : "false", _includeSynonymsTextBox.Text);
            Assert.AreEqual(criteria.Recency == null ? "" : criteria.Recency.Value.Days.ToString(CultureInfo.InvariantCulture), _recencyTextBox.Text);
            AssertSalary(criteria);
        }

        private void AssertKeywords(MemberSearchCriteria criteria)
        {
            var keywords = string.Empty;
            var allKeywords = string.Empty;
            var exactPhrase = string.Empty;
            var anyKeywords1 = string.Empty;
            var anyKeywords2 = string.Empty;
            var anyKeywords3 = string.Empty;
            var withoutKeywords = string.Empty;

            if (criteria.GetKeywords() != criteria.AllKeywords)
            {
                // It should be split.

                allKeywords = criteria.AllKeywords ?? string.Empty;
                exactPhrase = criteria.ExactPhrase ?? string.Empty;
                withoutKeywords = criteria.WithoutKeywords ?? string.Empty;

                if (!string.IsNullOrEmpty(criteria.AnyKeywords))
                {
                    var anyKeywords = criteria.AnyKeywords.Split(new[] { ' ' });
                    if (anyKeywords.Length > 0)
                        anyKeywords1 = anyKeywords[0];
                    if (anyKeywords.Length > 1)
                        anyKeywords2 = anyKeywords[1];
                    if (anyKeywords.Length > 2)
                        anyKeywords3 = string.Join(" ", anyKeywords.Skip(2).ToArray());
                }
            }
            else
            {
                keywords = criteria.AllKeywords ?? string.Empty;
            }

            Assert.AreEqual(keywords, _keywordsTextBox.Text);
            Assert.AreEqual(allKeywords, _allKeywordsTextBox.Text);
            Assert.AreEqual(exactPhrase, _exactPhraseTextBox.Text);
            var texts = _anyKeywordsTester.Texts;
            Assert.AreEqual(anyKeywords1, texts[0]);
            Assert.AreEqual(anyKeywords2, texts[1]);
            Assert.AreEqual(anyKeywords3, texts[2]);
            Assert.AreEqual(withoutKeywords, _withoutKeywordsTextBox.Text);
        }

        private void AssertCandidateStatus(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(_availableNowCheckBox.IsChecked, criteria.CandidateStatusFlags == null || criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.AvailableNow));
            Assert.AreEqual(_activelyLookingCheckBox.IsChecked, criteria.CandidateStatusFlags == null || criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.ActivelyLooking));
            Assert.AreEqual(_openToOffersCheckBox.IsChecked, criteria.CandidateStatusFlags == null || criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.OpenToOffers));
            Assert.AreEqual(_unspecifiedCheckBox.IsChecked, criteria.CandidateStatusFlags == null || criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.Unspecified));
        }

        private void AssertJobTypes(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(_fullTimeCheckBox.IsChecked, criteria.JobTypes.IsFlagSet(JobTypes.FullTime));
            Assert.AreEqual(_partTimeCheckBox.IsChecked, criteria.JobTypes.IsFlagSet(JobTypes.PartTime));
            Assert.AreEqual(_contractCheckBox.IsChecked, criteria.JobTypes.IsFlagSet(JobTypes.Contract));
            Assert.AreEqual(_tempCheckBox.IsChecked, criteria.JobTypes.IsFlagSet(JobTypes.Temp));
            Assert.AreEqual(_jobShareCheckBox.IsChecked, criteria.JobTypes.IsFlagSet(JobTypes.JobShare));
        }

        private void AssertSalary(MemberSearchCriteria criteria)
        {
            Assert.AreEqual((criteria.Salary == null || criteria.Salary.LowerBound == null ? 0 : criteria.Salary.LowerBound.Value).ToString(CultureInfo.InvariantCulture), _salaryLowerBoundTextBox.Text);
            Assert.AreEqual((criteria.Salary == null || criteria.Salary.UpperBound == null ? MaxSalary : criteria.Salary.UpperBound.Value).ToString(CultureInfo.InvariantCulture), _salaryUpperBoundTextBox.Text);
        }

        private void AssertSortOrder(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.SortCriteria.SortOrder.ToString(), _sortOrderDropDownList.SelectedItem.Value);
            Assert.AreEqual(criteria.SortCriteria.ReverseSortOrder, _sortOrderIsAscendingRadioButton.IsChecked);
            Assert.AreEqual(!criteria.SortCriteria.ReverseSortOrder, _sortOrderIsDescendingRadioButton.IsChecked);
        }

        private void AssertActivity(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.InFolder == null, _inFolderEitherRadioButton.IsChecked);
            Assert.AreEqual(criteria.InFolder == true, _inFolderYesRadioButton.IsChecked);
            Assert.AreEqual(criteria.InFolder == false, _inFolderNoRadioButton.IsChecked);

            Assert.AreEqual(criteria.IsFlagged == null, _isFlaggedEitherRadioButton.IsChecked);
            Assert.AreEqual(criteria.IsFlagged == true, _isFlaggedYesRadioButton.IsChecked);
            Assert.AreEqual(criteria.IsFlagged == false, _isFlaggedNoRadioButton.IsChecked);

            Assert.AreEqual(criteria.HasNotes == null, _hasNotesEitherRadioButton.IsChecked);
            Assert.AreEqual(criteria.HasNotes == true, _hasNotesYesRadioButton.IsChecked);
            Assert.AreEqual(criteria.HasNotes == false, _hasNotesNoRadioButton.IsChecked);

            Assert.AreEqual(criteria.HasViewed == null, _hasViewedEitherRadioButton.IsChecked);
            Assert.AreEqual(criteria.HasViewed == true, _hasViewedYesRadioButton.IsChecked);
            Assert.AreEqual(criteria.HasViewed == false, _hasViewedNoRadioButton.IsChecked);

            Assert.AreEqual(criteria.IsUnlocked == null, _isUnlockedEitherRadioButton.IsChecked);
            Assert.AreEqual(criteria.IsUnlocked == true, _isUnlockedYesRadioButton.IsChecked);
            Assert.AreEqual(criteria.IsUnlocked == false, _isUnlockedNoRadioButton.IsChecked);
        }

        private void AssertEthnicStatus(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.EthnicStatus != null, _ethnicStatusCheckBox.IsChecked);
            Assert.AreEqual(criteria.EthnicStatus == null || criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.Aboriginal), _aboriginalCheckBox.IsChecked);
            Assert.AreEqual(criteria.EthnicStatus == null || criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.TorresIslander), _torresIslanderCheckBox.IsChecked);
        }

        private void AssertVisaStatus(MemberSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.VisaStatusFlags != null, _visaStatusCheckBox.IsChecked);
            Assert.AreEqual(criteria.VisaStatusFlags == null || criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.Citizen), _citizenCheckBox.IsChecked);
            Assert.AreEqual(criteria.VisaStatusFlags == null || criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.UnrestrictedWorkVisa), _unrestrictedWorkVisaCheckBox.IsChecked);
            Assert.AreEqual(criteria.VisaStatusFlags == null || criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.RestrictedWorkVisa), _restrictedWorkVisaCheckBox.IsChecked);
            Assert.AreEqual(criteria.VisaStatusFlags == null || criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.NoWorkVisa), _noWorkVisaCheckBox.IsChecked);
        }

        private void AssertCommunityId(MemberSearchCriteria criteria)
        {
            AssertCommunityId(true, _communitiesQuery.GetCommunities(), criteria.CommunityId);
        }

        protected void AssertCommunityId(bool visible, IList<Community> communities, Guid? communityId)
        {
            Assert.AreEqual(_communityIdDropDownList.IsVisible, visible);

            if (visible)
            {
                Assert.AreEqual(communities.Count + 1, _communityIdDropDownList.Items.Count);

                Assert.AreEqual(communityId == null ? string.Empty : communityId.Value.ToString(), _communityIdDropDownList.SelectedItem.Value);
            }
        }

        private void AssertLocation(MemberSearchCriteria criteria)
        {
            if (criteria.Location == null)
            {
                // Cannot specify a distance unless a location is provided.

                Assert.IsNull(criteria.Distance);

                Assert.AreEqual(string.Empty, _locationTextBox.Text);
                Assert.AreEqual(0, _countryIdDropDownList.SelectedIndex);
                foreach (var item in _distanceDropDownList.Items)
                {
                    // Default distance is selected.

                    Assert.AreEqual(item.Value == DefaultDistance.ToString(CultureInfo.InvariantCulture), item.IsSelected);
                }
            }
            else
            {
                Assert.AreEqual(criteria.Location.ToString(), _locationTextBox.Text);
                Assert.AreEqual(criteria.Location.Country.Id.ToString(CultureInfo.InvariantCulture), _countryIdDropDownList.SelectedItem.Value);

                if (criteria.Distance == null)
                {
                    // Default distance varies depending on location type.

                    var defaultDistance = criteria.Location.NamedLocation is CountrySubdivision
                        || criteria.Location.NamedLocation is Region
                        ? DefaultRegionalDistance.ToString(CultureInfo.InvariantCulture)
                        : DefaultDistance.ToString(CultureInfo.InvariantCulture);

                    foreach (var item in _distanceDropDownList.Items)
                        Assert.AreEqual(item.Value == defaultDistance, item.IsSelected);
                }
                else
                {
                    Assert.AreEqual(criteria.Distance.ToString(), _distanceDropDownList.SelectedItem.Value);
                }
            }

            Assert.AreEqual(criteria.IncludeRelocating, _includeRelocatingCheckBox.IsChecked);
            Assert.AreEqual(criteria.IncludeInternational, _includeInternationalCheckBox.IsChecked);
            Assert.AreEqual(criteria.IncludeRelocating, _includeRelocatingFilterCheckBox.IsChecked);
            Assert.AreEqual(criteria.IncludeInternational, _includeInternationalFilterCheckBox.IsChecked);
        }

        private void AssertCompaniesToSearch(MemberSearchCriteria criteria)
        {
            switch (criteria.CompaniesToSearch)
            {
                case JobsToSearch.AllJobs:
                    Assert.AreEqual(true, _allCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(false, _recentCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(false, _lastCompanyRadioButton.IsChecked);
                    break;

                case JobsToSearch.RecentJobs:
                    Assert.AreEqual(false, _allCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(true, _recentCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(false, _lastCompanyRadioButton.IsChecked);
                    break;

                default:
                    Assert.AreEqual(false, _allCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(false, _recentCompaniesRadioButton.IsChecked);
                    Assert.AreEqual(true, _lastCompanyRadioButton.IsChecked);
                    break;
            }
        }

        private void AssertJobsToSearch(MemberSearchCriteria criteria)
        {
            switch (criteria.JobTitlesToSearch)
            {
                case JobsToSearch.AllJobs:
                    Assert.AreEqual(true, _allJobsRadioButton.IsChecked);
                    Assert.AreEqual(false, _recentJobsRadioButton.IsChecked);
                    break;

                default:
                    Assert.AreEqual(false, _allJobsRadioButton.IsChecked);
                    Assert.AreEqual(true, _recentJobsRadioButton.IsChecked);
                    break;
            }
        }

        private void AssertIncludeSimilarName(MemberSearchCriteria criteria)
        {
            if (criteria.IncludeSimilarNames)
            {
                Assert.AreEqual(false, _exactMatchNameRadioButton.IsChecked);
                Assert.AreEqual(true, _includeSimilarNamesRadioButton.IsChecked);
            }
            else
            {
                Assert.AreEqual(true, _exactMatchNameRadioButton.IsChecked);
                Assert.AreEqual(false, _includeSimilarNamesRadioButton.IsChecked);
            }
        }

        private void AssertIndustryIds(MemberSearchCriteria criteria)
        {
            // Gather all checked industries.

            var industryIds = new List<Guid>();
            foreach (var industryIdCheckBox in _industryIdCheckBoxes)
            {
                if (industryIdCheckBox.IsChecked)
                    industryIds.Add(new Guid(industryIdCheckBox.Value));
            }

            // Check now.

            if (criteria.IndustryIds == null || criteria.IndustryIds.Count == 0)
            {
                Assert.AreEqual(0, industryIds.Count);
                Assert.AreEqual(true, _allIndustriesCheckBox.IsChecked);
            }
            else
            {
                Assert.AreEqual(criteria.IndustryIds.Count, industryIds.Count);
                foreach (var industryId in industryIds)
                    Assert.AreEqual(true, criteria.IndustryIds.Contains(industryId));
                Assert.AreEqual(false, _allIndustriesCheckBox.IsChecked);
            }
        }

        protected void AssertResultCounts(int start, int end, int total)
        {
            AssertPageContains("Results <span class=\"start\">" + start + "</span> - <span class=\"end\">" + end + "</span> of <span class=\"total\">" + total + "</span>");
        }

        protected void AssertNoResults()
        {
            AssertPageDoesNotContain("Results <span class=\"start\">");
            AssertPageDoesNotContain("</span> - <span class=\"end\">");
            AssertPageDoesNotContain("</span> of <span class=\"total\">");
        }
    }
}