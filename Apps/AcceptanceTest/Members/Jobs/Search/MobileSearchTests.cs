using LinkMe.Apps.Asp.Test.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search
{
    [TestClass]
    public abstract class MobileSearchTests
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
        protected HtmlDropDownListTester _distanceDropDownList;
        protected HtmlTextBoxTester _distanceTextBox;
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
        public void MobileSearchTestsInitialize()
        {
            Browser.UseMobileUserAgent = true;

            _allWordsTextBox = new HtmlTextBoxTester(Browser, "KeywordsAdvanced");
            _searchButton = new HtmlButtonTester(Browser, "searchAdvanced");

            _keywordsTextBox = new HtmlTextAreaTester(Browser, "Keywords");
            _adTitleTextBox = new HtmlTextAreaTester(Browser, "AdTitle");
            _advertiserTextBox = new HtmlTextBoxTester(Browser, "Advertiser");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _salaryUpperBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryUpperBound");

            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _distanceDropDownList = new HtmlDropDownListTester(Browser, "Distance");
            _distanceTextBox = new HtmlTextBoxTester(Browser, "Distance");

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
