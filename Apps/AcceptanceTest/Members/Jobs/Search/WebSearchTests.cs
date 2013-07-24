using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search
{
    public class JsonJobAdModel
    {
        public Guid JobAdId { get; set; }
        public string Title { get; set; }
        public string ContactDetails { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string Content { get; set; }
        public string[] BulletPoints { get; set; }
        public Guid[] Industries { get; set; }
        public bool IsNew { get; set; }
        public bool IsHighlighted { get; set; }
        public string CreatedTime { get; set; }
        public string JobTypes { get; set; }
        public bool HasViewed { get; set; }
        public bool HasApplied { get; set; }
        public bool IsFlagged { get; set; }
    }

    public class JsonSearchResponseModel
        : JsonResponseModel
    {
        public int TotalJobAds { get; set; }
        public IList<JsonJobAdModel> JobAds { get; set; }
        public string Hash { get; set; }
    }

    [TestClass]
    public abstract class WebSearchTests
        : SearchTests
    {
        protected HtmlTextBoxTester _allWordsTextBox;
        protected HtmlButtonTester _searchButton;

        protected HtmlTextAreaTester _keywordsTextBox;
        protected HtmlTextAreaTester _adTitleTextBox;
        protected HtmlTextBoxTester _advertiserTextBox;
        protected HtmlTextBoxTester _salaryLowerBoundTextBox;
        protected HtmlTextBoxTester _salaryUpperBoundTextBox;
        protected HtmlTextBoxTester _recencyTextBox;
        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlDropDownListTester _countryIdDropDownList;
        protected HtmlDropDownListTester _distanceDropDownList;
        protected HtmlTextBoxTester _distanceInFilterTextBox;
        protected HtmlRadioButtonTester _hasAppliedEitherRadioButton;
        protected HtmlRadioButtonTester _hasAppliedYesRadioButton;
        protected HtmlRadioButtonTester _hasAppliedNoRadioButton;
        protected HtmlRadioButtonTester _isFlaggedEitherRadioButton;
        protected HtmlRadioButtonTester _isFlaggedYesRadioButton;
        protected HtmlRadioButtonTester _isFlaggedNoRadioButton;
        protected HtmlRadioButtonTester _hasNotesEitherRadioButton;
        protected HtmlRadioButtonTester _hasNotesYesRadioButton;
        protected HtmlRadioButtonTester _hasNotesNoRadioButton;
        protected HtmlRadioButtonTester _hasViewedEitherRadioButton;
        protected HtmlRadioButtonTester _hasViewedYesRadioButton;
        protected HtmlRadioButtonTester _hasViewedNoRadioButton;
        protected HtmlDropDownListTester _sortOrderDropDownList;
        protected HtmlHiddenTester _includeSynonymsTextBox;

        [TestInitialize]
        public void WebSearchTestsInitialize()
        {
            _allWordsTextBox = new HtmlTextBoxTester(Browser, "KeywordsAdvanced");
            _searchButton = new HtmlButtonTester(Browser, "searchAdvanced");

            _keywordsTextBox = new HtmlTextAreaTester(Browser, "Keywords");
            _adTitleTextBox = new HtmlTextAreaTester(Browser, "AdTitle");
            _advertiserTextBox = new HtmlTextBoxTester(Browser, "Advertiser");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _salaryUpperBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryUpperBound");

            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _countryIdDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _distanceDropDownList = new HtmlDropDownListTester(Browser, "Distance");
            _distanceInFilterTextBox = new HtmlTextBoxTester(Browser, "DistanceInFilter");

            _sortOrderDropDownList = new HtmlDropDownListTester(Browser, "SortOrder");

            _includeSynonymsTextBox = new HtmlHiddenTester(Browser, "IncludeSynonyms");

            _recencyTextBox = new HtmlTextBoxTester(Browser, "Recency");

            _hasAppliedEitherRadioButton = new HtmlRadioButtonTester(Browser, "HasAppliedEither");
            _hasAppliedYesRadioButton = new HtmlRadioButtonTester(Browser, "HasAppliedYes");
            _hasAppliedNoRadioButton = new HtmlRadioButtonTester(Browser, "HasAppliedNo");

            _isFlaggedEitherRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedEither");
            _isFlaggedYesRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedYes");
            _isFlaggedNoRadioButton = new HtmlRadioButtonTester(Browser, "IsFlaggedNo");

            _hasNotesEitherRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesEither");
            _hasNotesYesRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesYes");
            _hasNotesNoRadioButton = new HtmlRadioButtonTester(Browser, "HasNotesNo");

            _hasViewedEitherRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedEither");
            _hasViewedYesRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedYes");
            _hasViewedNoRadioButton = new HtmlRadioButtonTester(Browser, "HasViewedNo");
        }
    }
}
