using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Web
{
    public abstract class CriteriaTests
        : WebSearchTests
    {
        protected const string BusinessAnalyst = "business analyst";
        private const int MaxSalary = 250000;
        private const int DefaultDistance = 50;
        private const int DefaultRecency = 30;

        [TestMethod]
        public void TestCriteriaDisplay()
        {
            TestDisplay();
        }

        protected abstract void TestDisplay();

        protected void TestDisplay(bool isLoggedIn, JobAdSearchCriteria criteria)
        {
            // Get the page and check that all fields are set properly.

            Get(GetSearchUrl(criteria));
            AssertCriteria(criteria);

            // Look for the hash in the page.

            var hash = AssertHash(criteria);
            Get(GetLeftSideUrl());
            AssertLeftSideCriteria(isLoggedIn, criteria);

            // Resubmit the hash.

            var url = GetSearchUrl().AsNonReadOnly();
            url.QueryString.Add(new QueryString(hash));
            Get(url);
            AssertCriteria(criteria);
            Get(GetLeftSideUrl());

            if (criteria.IndustryIds != null && criteria.IndustryIds.CollectionContains(_industriesQuery.GetIndustries().Select(i => i.Id)))
                criteria.IndustryIds = null;

            AssertLeftSideCriteria(isLoggedIn, criteria);

            // Look for the hash in the api results and resubmit it.

            var model = ApiSearch(criteria);
            hash = AssertHash(model, criteria);

            url = GetSearchUrl().AsNonReadOnly();
            url.QueryString.Add(new QueryString(hash));
            Get(url);
            AssertCriteria(criteria);
            Get(GetLeftSideUrl());
            AssertLeftSideCriteria(isLoggedIn, criteria);
        }

        protected void AssertCriteria(JobAdSearchCriteria criteria)
        {
            AssertSortOrder(criteria);
            AssertSynonyms(criteria);
        }

        private void AssertLeftSideCriteria(bool isLoggedIn, JobAdSearchCriteria criteria)
        {
            AssertKeywords(criteria);
            AssertLocation(criteria);
            AssertJobTypes(criteria);
            AssertSalary(criteria);
            Assert.AreEqual(criteria.Recency == null ? DefaultRecency.ToString(CultureInfo.InvariantCulture) : criteria.Recency.Value.Days.ToString(CultureInfo.InvariantCulture), _recencyTextBox.Text);
            AssertIndustryIds(criteria);
            AssertActivity(isLoggedIn, criteria);
        }

        private void AssertKeywords(JobAdSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.GetKeywords(), _keywordsTextBox.Text.Trim());
            Assert.AreEqual(criteria.AdTitle ?? string.Empty, _adTitleTextBox.Text.Trim());
            Assert.AreEqual(criteria.AdvertiserName ?? string.Empty, _advertiserTextBox.Text);
        }

        private void AssertJobTypes(JobAdSearchCriteria criteria)
        {
            AssertJobType(criteria, JobTypes.FullTime);
            AssertJobType(criteria, JobTypes.PartTime);
            AssertJobType(criteria, JobTypes.Contract);
            AssertJobType(criteria, JobTypes.Temp);
            AssertJobType(criteria, JobTypes.JobShare);
        }

        private void AssertJobType(JobAdSearchCriteria criteria, JobTypes jobType)
        {
            var checkedNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='jobtype " + jobType + "']/div[@class='icon checked']");
            var uncheckedNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='jobtype " + jobType + "']/div[@class='icon ']");
            if (criteria.JobTypes == JobTypes.None || criteria.JobTypes.IsFlagSet(jobType))
            {
                Assert.IsNotNull(checkedNode);
                Assert.IsNull(uncheckedNode);
            }
            else
            {
                Assert.IsNull(checkedNode);
                Assert.IsNotNull(uncheckedNode);
            }
        }

        private void AssertSalary(JobAdSearchCriteria criteria)
        {
            Assert.AreEqual((criteria.Salary == null || criteria.Salary.LowerBound == null ? 0 : criteria.Salary.LowerBound.Value).ToString(CultureInfo.InvariantCulture), _salaryLowerBoundTextBox.Text);
            Assert.AreEqual((criteria.Salary == null || criteria.Salary.UpperBound == null ? MaxSalary : criteria.Salary.UpperBound.Value).ToString(CultureInfo.InvariantCulture), _salaryUpperBoundTextBox.Text);
        }

        private void AssertActivity(bool isLoggedIn, JobAdSearchCriteria criteria)
        {
            if (isLoggedIn)
            {
                Assert.AreEqual(criteria.HasApplied == null, _hasAppliedEitherRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasApplied == true, _hasAppliedYesRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasApplied == false, _hasAppliedNoRadioButton.IsChecked);

                Assert.AreEqual(criteria.IsFlagged == null, _isFlaggedEitherRadioButton.IsChecked);
                Assert.AreEqual(criteria.IsFlagged == true, _isFlaggedYesRadioButton.IsChecked);
                Assert.AreEqual(criteria.IsFlagged == false, _isFlaggedNoRadioButton.IsChecked);

                Assert.AreEqual(criteria.HasNotes == null, _hasNotesEitherRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasNotes == true, _hasNotesYesRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasNotes == false, _hasNotesNoRadioButton.IsChecked);

                Assert.AreEqual(criteria.HasViewed == null, _hasViewedEitherRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasViewed == true, _hasViewedYesRadioButton.IsChecked);
                Assert.AreEqual(criteria.HasViewed == false, _hasViewedNoRadioButton.IsChecked);
            }
            else
            {
                Assert.IsFalse(_hasAppliedEitherRadioButton.IsVisible);
                Assert.IsFalse(_hasAppliedYesRadioButton.IsVisible);
                Assert.IsFalse(_hasAppliedNoRadioButton.IsVisible);

                Assert.IsFalse(_isFlaggedEitherRadioButton.IsVisible);
                Assert.IsFalse(_isFlaggedYesRadioButton.IsVisible);
                Assert.IsFalse(_isFlaggedNoRadioButton.IsVisible);

                Assert.IsFalse(_hasNotesEitherRadioButton.IsVisible);
                Assert.IsFalse(_hasNotesYesRadioButton.IsVisible);
                Assert.IsFalse(_hasNotesNoRadioButton.IsVisible);

                Assert.IsFalse(_hasViewedEitherRadioButton.IsVisible);
                Assert.IsFalse(_hasViewedYesRadioButton.IsVisible);
                Assert.IsFalse(_hasViewedNoRadioButton.IsVisible);
            }
        }

        private void AssertLocation(JobAdSearchCriteria criteria)
        {
            if (criteria.Location == null)
            {
                // Cannot specify a distance unless a location is provided.

                Assert.IsNull(criteria.Distance);

                Assert.AreEqual(string.Empty, _locationTextBox.Text);
                Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), _distanceInFilterTextBox.Text);
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
                    Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), _distanceInFilterTextBox.Text);
                    foreach (var item in _distanceDropDownList.Items)
                        Assert.AreEqual(item.Value == DefaultDistance.ToString(CultureInfo.InvariantCulture), item.IsSelected);
                }
                else
                {
                    Assert.AreEqual(criteria.Distance.ToString(), _distanceInFilterTextBox.Text);
                    Assert.AreEqual(criteria.Distance.ToString(), _distanceDropDownList.SelectedItem.Value);
                }
            }
        }

        private void AssertIndustryIds(JobAdSearchCriteria criteria)
        {
            if (criteria.IndustryIds.IsNullOrEmpty())
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox checked']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox ']"));

                foreach (var industry in _industriesQuery.GetIndustries())
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox ']"));
                }
            }
            else
            {
                var industries = _industriesQuery.GetIndustries();
                if (criteria.IndustryIds.CollectionContains(industries.Select(i => i.Id)))
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox ']"));
                }
                else
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustry']/div[@class='checkbox ']"));
                }

                foreach (var industry in industries)
                {
                    if (criteria.IndustryIds.Contains(industry.Id))
                    {
                        Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox checked']"));
                        Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox ']"));
                    }
                    else
                    {
                        Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox checked']"));
                        Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']/div[@class='checkbox ']"));
                    }
                }
            }
        }

        private void AssertSortOrder(JobAdSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.SortCriteria.SortOrder.ToString(), _sortOrderDropDownList.SelectedItem.Value);
            if (criteria.SortCriteria.ReverseSortOrder)
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending ']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending ']"));
            }
            else
            {
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending active']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='ascending ']"));
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending active']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='descending ']"));
            }
        }

        private void AssertSynonyms(JobAdSearchCriteria criteria)
        {
            Assert.AreEqual(criteria.IncludeSynonyms ? "with synonyms" : "without synonyms", Browser.CurrentHtml.DocumentNode.SelectSingleNode("//span[@class='synonyms-filter-text']").InnerText);
        }

        private string AssertHash(JsonSearchResponseModel model, JobAdSearchCriteria criteria)
        {
            var hash = model.Hash;
            Assert.AreEqual(GetHash(criteria), hash);
            return hash;
        }

        private string AssertHash(JobAdSearchCriteria criteria)
        {
            var hash = GetPageHash();
            Assert.AreEqual(GetHash(criteria), hash);
            return hash;
        }

        private string GetHash(JobAdSearchCriteria criteria)
        {
            // This simulates the workings of new QueryStringGenerator(new JobAdSearchCriteriaConverter()).GenerateQueryString(criteria);
            // Don't use that method here though so it gets tested independently.

            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(criteria.ExactPhrase) || !string.IsNullOrEmpty(criteria.AnyKeywords) || !string.IsNullOrEmpty(criteria.WithoutKeywords))
            {
                if (!string.IsNullOrEmpty(criteria.ExactPhrase))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("ExactPhrase=").Append(HttpUtility.UrlEncode(criteria.ExactPhrase));
                }

                if (!string.IsNullOrEmpty(criteria.AllKeywords))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("AllKeywords=").Append(HttpUtility.UrlEncode(criteria.AllKeywords));
                }

                if (!string.IsNullOrEmpty(criteria.WithoutKeywords))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("WithoutKeywords=").Append(HttpUtility.UrlEncode(criteria.WithoutKeywords));
                }

                if (!string.IsNullOrEmpty(criteria.AnyKeywords))
                {
                    foreach (var anyKeywords in criteria.AnyKeywords.Split(new[] { ' ' }))
                    {
                        if (sb.Length != 0)
                            sb.Append("&");
                        sb.Append("AnyKeywords=").Append(HttpUtility.UrlEncode(anyKeywords));
                    }
                }
            }
            else
            {
                var keywords = criteria.GetKeywords();
                if (!string.IsNullOrEmpty(keywords))
                    sb.Append("Keywords=").Append(HttpUtility.UrlEncode(keywords));
            }

            if (!string.IsNullOrEmpty(criteria.AdTitle))
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("AdTitle=").Append(HttpUtility.UrlEncode(criteria.AdTitle));
            }

            if (!string.IsNullOrEmpty(criteria.AdvertiserName))
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("Advertiser=").Append(HttpUtility.UrlEncode(criteria.AdvertiserName));
            }

            if (!criteria.IndustryIds.IsNullOrEmpty())
            {
                // Industry's only included if they are a proper subset.

                if (!criteria.IndustryIds.CollectionContains(_industriesQuery.GetIndustries().Select(i => i.Id)))
                {
                    foreach (var industryId in criteria.IndustryIds)
                    {
                        if (sb.Length != 0)
                            sb.Append("&");
                        sb.Append("IndustryIds=").Append(industryId);
                    }
                }
            }

            if (criteria.Location != null)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("CountryId=").Append(criteria.Location.Country.Id);

                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                {
                    sb.Append("&");
                    sb.Append("Location=").Append(HttpUtility.UrlEncode(location));
                }

                if (criteria.Distance != null && criteria.Distance.Value != 50)
                {
                    sb.Append("&");
                    sb.Append("Distance=").Append(criteria.Distance.Value);
                }
            }

            if (criteria.Recency != null && criteria.Recency.Value != new TimeSpan(JobAdSearchCriteria.DefaultRecency, 0, 0, 0))
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("Recency=").Append(criteria.Recency.Value.Days);
            }

            if (criteria.Salary != null)
            {
                if (criteria.Salary.LowerBound != null)
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("SalaryLowerBound=").Append(criteria.Salary.LowerBound.Value);
                }
                if (criteria.Salary.UpperBound != null)
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("SalaryUpperBound=").Append(criteria.Salary.UpperBound.Value);
                }
            }

            if (criteria.JobTypes != JobTypes.All && criteria.JobTypes != JobTypes.None)
            {
                if (criteria.JobTypes.IsFlagSet(JobTypes.FullTime))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("FullTime=").Append(true.ToString().ToLower());
                }
                if (criteria.JobTypes.IsFlagSet(JobTypes.PartTime))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("PartTime=").Append(true.ToString().ToLower());
                }
                if (criteria.JobTypes.IsFlagSet(JobTypes.Contract))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("Contract=").Append(true.ToString().ToLower());
                }
                if (criteria.JobTypes.IsFlagSet(JobTypes.Temp))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("Temp=").Append(true.ToString().ToLower());
                }
                if (criteria.JobTypes.IsFlagSet(JobTypes.JobShare))
                {
                    if (sb.Length != 0)
                        sb.Append("&");
                    sb.Append("JobShare=").Append(true.ToString().ToLower());
                }
            }

            if (criteria.HasApplied != null)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("HasApplied=").Append(criteria.HasApplied.Value.ToString().ToLower());
            }

            if (criteria.HasViewed != null)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("HasViewed=").Append(criteria.HasViewed.Value.ToString().ToLower());
            }

            if (criteria.HasNotes != null)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("HasNotes=").Append(criteria.HasNotes.Value.ToString().ToLower());
            }

            if (criteria.IsFlagged != null)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("IsFlagged=").Append(criteria.IsFlagged.Value.ToString().ToLower());
            }

            if (!criteria.IncludeSynonyms)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("IncludeSynonyms=").Append(criteria.IncludeSynonyms.ToString().ToLower());
            }

            if (criteria.SortCriteria.SortOrder != JobAdSearchCriteria.DefaultSortOrder || criteria.SortCriteria.ReverseSortOrder)
            {
                if (sb.Length != 0)
                    sb.Append("&");
                sb.Append("SortOrder=").Append(criteria.SortCriteria.SortOrder);

                sb.Append(criteria.SortCriteria.ReverseSortOrder ? "&SortOrderDirection=SortOrderIsAscending" : "&SortOrderDirection=SortOrderIsDescending");
            }

            return sb.Length == 0 ? null : sb.ToString();
        }

        private string GetPageHash()
        {
            // Looking for: window.location.hash = "#<%= hash %>";

            var startIndex = Browser.CurrentPageText.IndexOf("window.location.hash = \"#");
            if (startIndex == -1)
                return null;

            startIndex += "window.location.hash = \"#".Length;
            var endIndex = Browser.CurrentPageText.IndexOf("\";", startIndex, StringComparison.Ordinal);
            return endIndex == -1
                ? Browser.CurrentPageText.Substring(startIndex)
                : Browser.CurrentPageText.Substring(startIndex, endIndex - startIndex);
        }
    }
}