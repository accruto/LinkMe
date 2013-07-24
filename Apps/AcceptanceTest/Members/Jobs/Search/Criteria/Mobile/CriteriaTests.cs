using System;
using System.Globalization;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Criteria.Mobile
{
    public abstract class CriteriaTests
        : MobileSearchTests
    {
        protected const string BusinessAnalyst = "business analyst";
        private const int MaxSalary = 250000;
        private const int DefaultDistance = 50;
        private const int DefaultCountryId = 1;
        private const int DefaultRecency = 30;

        [TestMethod]
        public void TestCriteriaDisplay()
        {
            TestDisplay();
        }

        protected abstract void TestDisplay();

        protected void TestDisplay(JobAdSearchCriteria criteria)
        {
            // Get the page and check that all fields are set properly.

            Get(GetSearchUrl(criteria));
            AssertCriteria(criteria);
        }

        private void AssertCriteria(JobAdSearchCriteria criteria)
        {
            AssertSortOrder(criteria);
            AssertKeywords(criteria);
            AssertLocation(criteria);
            AssertJobTypes(criteria);
            AssertSalary(criteria);
            Assert.AreEqual(criteria.Recency == null ? DefaultRecency.ToString(CultureInfo.InvariantCulture) : criteria.Recency.Value.Days.ToString(CultureInfo.InvariantCulture), _recencyTextBox.Text);
            AssertIndustryIds(criteria);
        }

        private void AssertKeywords(JobAdSearchCriteria criteria)
        {
            AssertPageContains("criteria[\"Keywords\"] = \"" + criteria.GetKeywords() + "\";");
            AssertPageContains("resetCriteria[\"Keywords\"] = \"" + criteria.GetKeywords() + "\";");
        }

        private void AssertJobTypes(JobAdSearchCriteria criteria)
        {
            AssertJobType(criteria, JobTypes.FullTime);
            AssertJobType(criteria, JobTypes.PartTime);
            AssertJobType(criteria, JobTypes.Contract);
            AssertJobType(criteria, JobTypes.Temp);
            AssertJobType(criteria, JobTypes.JobShare);

            if (criteria.JobTypes != JobTypes.All && criteria.JobTypes != JobTypes.None)
            {
                AssertPageContains("criteria[\"JobTypes\"] = \"" + criteria.JobTypes + "\";");
                AssertPageContains("resetCriteria[\"JobTypes\"] = \"" + criteria.JobTypes + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"JobTypes\"]");
                AssertPageDoesNotContain("resetCriteria[\"JobTypes\"]");
            }
        }

        private void AssertJobType(JobAdSearchCriteria criteria, JobTypes jobType)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='content JobType']/div[@class='wrapper " + jobType + "']");
            var checkedNode = node.SelectSingleNode("./div[@class='jobtype checked']");
            var uncheckedNode = node.SelectSingleNode("./div[@class='jobtype ']");
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

            if (criteria.Salary != null)
            {
                AssertPageContains("criteria[\"SalaryRate\"] = \"" + criteria.Salary.Rate + "\";");
                AssertPageContains("resetCriteria[\"SalaryRate\"] = \"" + criteria.Salary.Rate + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"SalaryRate\"]");
                AssertPageDoesNotContain("resetCriteria[\"SalaryRate\"]");
            }

            if (criteria.Salary != null && criteria.Salary.LowerBound != null)
            {
                AssertPageContains("criteria[\"SalaryLowerBound\"] = \"" + (int)criteria.Salary.LowerBound.Value + "\";");
                AssertPageContains("resetCriteria[\"SalaryLowerBound\"] = \"" + (int)criteria.Salary.LowerBound.Value + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"SalaryLowerBound\"]");
                AssertPageDoesNotContain("resetCriteria[\"SalaryLowerBound\"]");
            }

            if (criteria.Salary != null && criteria.Salary.UpperBound != null)
            {
                AssertPageContains("criteria[\"SalaryUpperBound\"] = \"" + (int)criteria.Salary.UpperBound.Value + "\";");
                AssertPageContains("resetCriteria[\"SalaryUpperBound\"] = \"" + (int)criteria.Salary.UpperBound.Value + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"SalaryUpperBound\"]");
                AssertPageDoesNotContain("resetCriteria[\"SalaryUpperBound\"]");
            }

            if (criteria.ExcludeNoSalary)
            {
                AssertPageContains("criteria[\"IncludeNoSalary\"] = \"" + !criteria.ExcludeNoSalary + "\";");
                AssertPageContains("resetCriteria[\"IncludeNoSalary\"] = \"" + !criteria.ExcludeNoSalary + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"IncludeNoSalary\"]");
                AssertPageDoesNotContain("resetCriteria[\"IncludeNoSalary\"]");
            }
        }

        private void AssertLocation(JobAdSearchCriteria criteria)
        {
            if (criteria.Location == null)
            {
                // Cannot specify a distance unless a location is provided.

                Assert.IsNull(criteria.Distance);

                Assert.AreEqual(string.Empty, _locationTextBox.Text);
                Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), _distanceTextBox.Text);

                var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='distancerange']");
                Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), node.Attributes["data-defaultdistance"].Value);
            }
            else
            {
                Assert.AreEqual(criteria.Location.ToString(), _locationTextBox.Text);

                if (criteria.Distance == null)
                {
                    Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), _distanceTextBox.Text);
                    var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='distancerange']");
                    Assert.AreEqual(DefaultDistance.ToString(CultureInfo.InvariantCulture), node.Attributes["data-defaultdistance"].Value);
                }
                else
                {
                    Assert.AreEqual(criteria.Distance.ToString(), _distanceTextBox.Text);
                    var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='distancerange']");
                    Assert.AreEqual(criteria.Distance.ToString(), node.Attributes["data-defaultdistance"].Value);
                }
            }

            if (criteria.Location != null)
            {
                AssertPageContains("criteria[\"Location\"] = \"" + criteria.Location + "\";");
                AssertPageContains("criteria[\"Distance\"] = \"" + (criteria.Distance == 0 || criteria.Distance == null ? DefaultDistance : criteria.Distance) + "\";");
                AssertPageContains("criteria[\"CountryId\"] = \"" + (criteria.Location == null ? DefaultCountryId : criteria.Location.Country.Id) + "\";");
                AssertPageContains("resetCriteria[\"Location\"] = \"" + criteria.Location + "\";");
                AssertPageContains("resetCriteria[\"Distance\"] = \"" + (criteria.Distance == 0 || criteria.Distance == null ? DefaultDistance : criteria.Distance) + "\";");
                AssertPageContains("resetCriteria[\"CountryId\"] = \"" + (criteria.Location == null ? DefaultCountryId : criteria.Location.Country.Id) + "\";");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"Location\"]");
                AssertPageDoesNotContain("criteria[\"Distance\"]");
                AssertPageDoesNotContain("criteria[\"CountryId\"]");
                AssertPageDoesNotContain("resetCriteria[\"Location\"]");
                AssertPageDoesNotContain("resetCriteria[\"Distance\"]");
                AssertPageDoesNotContain("resetCriteria[\"CountryId\"]");
            }
        }

        private void AssertIndustryIds(JobAdSearchCriteria criteria)
        {
            if (criteria.IndustryIds.IsNullOrEmpty())
            {
                Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox checked']"));
                Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox ']"));

                foreach (var industry in _industriesQuery.GetIndustries())
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox ']"));
                }
            }
            else
            {
                var industries = _industriesQuery.GetIndustries();
                if (criteria.IndustryIds.CollectionContains(industries.Select(i => i.Id)))
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox ']"));
                }
                else
                {
                    Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox checked']"));
                    Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='allindustries']//div[@class='checkbox ']"));
                }

                foreach (var industry in industries)
                {
                    if (criteria.IndustryIds.Contains(industry.Id))
                    {
                        Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox checked']"));
                        Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox ']"));
                    }
                    else
                    {
                        Assert.IsNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox checked']"));
                        Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + industry.Id + "']//div[@class='checkbox ']"));
                    }
                }
            }

            if (!criteria.IndustryIds.IsNullOrEmpty())
            {
                AssertPageContains("criteria[\"IndustryIds\"] = [" + string.Join(",", (from id in criteria.IndustryIds select "\"" + id + "\"").ToArray()) + "];");
                AssertPageContains("resetCriteria[\"IndustryIds\"] = [" + string.Join(",", (from id in criteria.IndustryIds select "\"" + id + "\"").ToArray()) + "];");
            }
            else
            {
                AssertPageDoesNotContain("criteria[\"IndustryIds\"]");
                AssertPageDoesNotContain("resetCriteria[\"IndustryIds\"]");
            }
        }

        private void AssertSortOrder(JobAdSearchCriteria criteria)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='filterbar']//div[@class='button sort']");
            Assert.IsNotNull(node);

            var sortOrders = new[]
            {
                Tuple.Create(JobAdSortOrder.CreatedTime, "Date posted"),
                Tuple.Create(JobAdSortOrder.Distance, "Distance"),
                Tuple.Create(JobAdSortOrder.JobType, "Job type"),
                Tuple.Create(JobAdSortOrder.Relevance, "Relevance"),
                Tuple.Create(JobAdSortOrder.Salary, "Salary")
            };
            Assert.AreEqual("{" + string.Join(",", (from s in sortOrders select "\"" + s.Item1 + "\":\"" + s.Item2 + "\"").ToArray()) + "}", node.Attributes["data-sortorder"].Value);
            Assert.AreEqual(criteria.SortCriteria.SortOrder.ToString(), node.Attributes["data-current"].Value);
        }
    }
}