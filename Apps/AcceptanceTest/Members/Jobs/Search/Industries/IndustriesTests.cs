using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Industries
{
    [TestClass]
    public class IndustriesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private const string Analyst = "Analyst";

        private ReadOnlyUrl _advancedSearchUrl;
        private ReadOnlyUrl _searchResultsUrl;
        private HtmlTextBoxTester _allWordsTextBox;
        private HtmlListBoxTester _industriesListBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _advancedSearchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            _allWordsTextBox = new HtmlTextBoxTester(Browser, "KeywordsAdvanced");
            _searchButton = new HtmlButtonTester(Browser, "searchAdvanced");
            _industriesListBox = new HtmlListBoxTester(Browser, "IndustriesAdvanced");
        }

        [TestMethod]
        public void TestIndustries()
        {
            var accounting = _industriesQuery.GetIndustry("Accounting");
            var administration = _industriesQuery.GetIndustry("Administration");
            var other = _industriesQuery.GetIndustry("Other");

            // Create some job ads.

            var employer = CreateEmployer(0);
            var jobAd0 = PostJobAd(Analyst, 0, employer, accounting);
            var jobAd1 = PostJobAd(Analyst, 1, employer, administration);
            var jobAd2 = PostJobAd(Analyst, 2, employer, other);
            var jobAd3 = PostJobAd(Analyst, 3, employer, accounting, other);
            var jobAd4 = PostJobAd(Analyst, 4, employer);

            // Search.

            Search(Analyst);
            AssertJobAds(jobAd0, jobAd1, jobAd2, jobAd3, jobAd4);

            Search(Analyst, _industriesQuery.GetIndustries().ToArray());
            AssertJobAds(jobAd0, jobAd1, jobAd2, jobAd3, jobAd4);

            Search(Analyst, accounting);
            AssertJobAds(jobAd0, jobAd3);

            Search(Analyst, administration);
            AssertJobAds(jobAd1);

            // Other should return "not specfified" job ads as well.

            Search(Analyst, other);
            AssertJobAds(jobAd2, jobAd3, jobAd4);

            Search(Analyst, accounting, administration);
            AssertJobAds(jobAd0, jobAd1, jobAd3);

            Search(Analyst, other, administration);
            AssertJobAds(jobAd1, jobAd2, jobAd3, jobAd4);
        }

        [TestMethod]
        public void TestBrowseIndustries()
        {
            var accounting = _industriesQuery.GetIndustry("Accounting");
            var administration = _industriesQuery.GetIndustry("Administration");
            var other = _industriesQuery.GetIndustry("Other");

            // Create some job ads.

            var employer = CreateEmployer(0);
            var jobAd0 = PostJobAd(Analyst, 0, employer, accounting);
            var jobAd1 = PostJobAd(Analyst, 1, employer, administration);
            var jobAd2 = PostJobAd(Analyst, 2, employer, other);
            var jobAd3 = PostJobAd(Analyst, 3, employer, accounting, other);
            var jobAd4 = PostJobAd(Analyst, 4, employer);

            // Browse.

            Browse(accounting);
            AssertJobAds(jobAd0, jobAd3);

            Browse(administration);
            AssertJobAds(jobAd1);

            // Other should return "not specfified" job ads as well.

            Browse(other);
            AssertJobAds(jobAd2, jobAd3, jobAd4);
        }

        private void Browse(Industry industry)
        {
            Get(new ReadOnlyApplicationUrl("~/jobs/victoria-jobs/" + industry.UrlName + "-jobs"));
        }

        private void Search(string jobTitle, params Industry[] industries)
        {
            Get(_advancedSearchUrl);
            _allWordsTextBox.Text = jobTitle;
            _industriesListBox.SelectedValues = (from i in industries select i.Id.ToString()).ToArray();
            _searchButton.Click();
            AssertPath(_searchResultsUrl);
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        private JobAd PostJobAd(string jobTitle, int index, IEmployer employer, params Industry[] industries)
        {
            var jobAd = employer.CreateTestJobAd(GetJobTitle(jobTitle, index), "The content for job #" + index);
            jobAd.Description.Industries = industries;
            jobAd.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Melbourne VIC 3000");
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private static string GetJobTitle(string jobTitle, int index)
        {
            return jobTitle + " job #" + index;
        }

        private void AssertJobAds(params JobAd[] jobAds)
        {
            var jobads = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]/div");
            Assert.IsNotNull(jobads);
            var titles = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[starts-with(@class, 'jobad-list-view')]//div[@class='column title']/a");
            Assert.IsNotNull(titles);
            Assert.AreEqual(jobAds.Length, titles.Count);

            for (var index = 0; index < titles.Count; ++index)
            {
                var title = titles[index].InnerText.Trim();
                Assert.IsTrue((from j in jobAds where j.Title == title select j).Any());
            }
        }
    }
}