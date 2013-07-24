using System;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class BrowseJobsTests
        : JobsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";
        private ReadOnlyUrl _browseJobsUrl;
        private ReadOnlyUrl _browseBaseUrl;
        private ReadOnlyUrl _browseIndustryBaseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _browseJobsUrl = new ReadOnlyApplicationUrl("~/jobs");
            _browseBaseUrl = new ReadOnlyApplicationUrl("~/jobs/");
            _browseIndustryBaseUrl = new ReadOnlyApplicationUrl("~/jobs/-/");
        }

        [TestMethod]
        public void TestBrowseJobs()
        {
            Get(_browseJobsUrl);
            AssertUrl(_browseJobsUrl);

            AssertIndustries(GetBrowseUrl);
            AssertSubdivisions(GetBrowseUrl);
            AssertRegions(GetBrowseUrl);
        }

        [TestMethod]
        public void TestBrowseIndustry()
        {
            var industry = _industriesQuery.GetIndustries()[0];

            var url = GetBrowseUrl(industry);
            Get(url);

            AssertUrl(url);
            AssertSubdivisions(s => GetBrowseUrl(s, industry));
            AssertRegions(r => GetBrowseUrl(r, industry));
        }

        [TestMethod]
        public void TestBrowseSubdivision()
        {
            var subdivision = (from s in _locationQuery.GetCountrySubdivisions(_locationQuery.GetCountry(Country))
            where !s.IsCountry
            select s).First();

            var url = GetBrowseUrl(subdivision);
            Get(url);

            AssertUrl(url);
            AssertIndustries(i => GetBrowseUrl(subdivision, i));
        }

        [TestMethod]
        public void TestBrowseRegion()
        {
            var region = _locationQuery.GetRegions(_locationQuery.GetCountry(Country))[0];

            var url = GetBrowseUrl(region);
            Get(url);

            AssertUrl(url);
            AssertIndustries(i => GetBrowseUrl(region, i));
        }

        [TestMethod]
        public void TestBrowseLocationIndustry()
        {
            var employer = CreateEmployer();

            var industry = _industriesQuery.GetIndustries()[0];
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            var subdivision = (from s in _locationQuery.GetCountrySubdivisions(_locationQuery.GetCountry(Country))
            where _locationQuery.Contains(s, location)
            select s).First();

            // Create the job ad.

            var jobAd = CreateJobAd(employer, "Manager", location, industry);

            // View the page.

            var url = GetBrowseUrl(subdivision, industry);
            Get(url);
            AssertUrl(url);

            AssertPageContains("Jobs 1 - 1");
            AssertPageContains(" of 1");
            AssertPageContains(HttpUtility.HtmlEncode(jobAd.Title));
        }

        [TestMethod]
        public void TestUnknownLocation()
        {
            const int count = 100;
            const int itemsPerPage = 10;

            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/-/accounting-jobs");

            // An unknown industry should be sent to the location url.

            var url = new ReadOnlyApplicationUrl("~/jobs/unknown-jobs/accounting-jobs");
            Get(url);
            AssertUrl(expectedUrl);

            // Using any index should get you the same page.

            const int totalPages = count / itemsPerPage;
            var page = 1;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl("~/jobs/unknown-jobs/accounting-jobs/" + page );
                Get(url);
                AssertUrl(expectedUrl);
            }
        }

        [TestMethod]
        public void TestUnknownIndustry()
        {
            const int count = 100;
            const int itemsPerPage = 10;

            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs");

            // An unknown industry should be sent to the location url.

            var url = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/none-jobs");
            Get(url);
            AssertUrl(expectedUrl);

            // Using any index should get you the same page.

            const int totalPages = count/itemsPerPage;
            var page = 1;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/none-jobs/" + page );
                Get(url);
                AssertUrl(expectedUrl);
            }
        }

        private JobAd CreateJobAd(IEmployer employer, string jobTitle, LocationReference location, Industry industry)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle, "Blah blah blah", industry, location);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private void AssertIndustries(Func<Industry, ReadOnlyUrl> getUrl)
        {
            // Other comes last.

            var industries = _industriesQuery.GetIndustries().ToList();
            industries = (from i in industries
            where i.Name != "Other"
            orderby i.Name
            select i)
                .Concat(from i in industries
                where i.Name == "Other"
                select i).ToList();

            var firstIndustries = industries.Take(industries.Count / 2).ToList();
            var secondIndustries = industries.Skip(firstIndustries.Count).ToList();

            var firstNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='industry_section section']//div[@class='columns']/div[@class='column' and position()=1]//a");
            var secondNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='industry_section section']//div[@class='columns']/div[@class='column' and position()=2]//a");

            if (firstNodes == null)
                Assert.Fail();
            if (secondNodes == null)
                Assert.Fail();

            for (var index = 0; index < firstIndustries.Count; ++index)
                AssertLink(getUrl(firstIndustries[index]), firstIndustries[index], firstNodes[index]);
            for (var index = 0; index < secondIndustries.Count; ++index)
                AssertLink(getUrl(secondIndustries[index]), secondIndustries[index], secondNodes[index]);
        }

        private void AssertSubdivisions(Func<CountrySubdivision, ReadOnlyUrl> getUrl)
        {
            var subdivisions = (from c in _locationQuery.GetCountrySubdivisions(_locationQuery.GetCountry(Country))
            where !c.IsCountry
            orderby c.Name
            select c).ToList();

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='location_section section']//div[@class='columns']/div[@class='column' and position()=1]//a");
            if (nodes == null)
                Assert.Fail();

            for (var index = 0; index < subdivisions.Count; ++index)
                AssertLink(getUrl(subdivisions[index]), subdivisions[index], nodes[index]);
        }

        private void AssertRegions(Func<Region, ReadOnlyUrl> getUrl)
        {
            var regions = (from r in _locationQuery.GetRegions(_locationQuery.GetCountry(Country))
            orderby r.Name
            select r).ToList();

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='location_section section']//div[@class='columns']/div[@class='column' and position()=2]//a");
            if (nodes == null)
                Assert.Fail();

            for (var index = 0; index < regions.Count; ++index)
                AssertLink(getUrl(regions[index]), regions[index], nodes[index]);
        }

        private static void AssertLink(ReadOnlyUrl url, NamedLocation subdivision, HtmlNode node)
        {
            Assert.AreEqual(url.Path.ToLower(), node.Attributes["href"].Value.ToLower());
            Assert.AreEqual(subdivision.Name + " jobs", HttpUtility.HtmlDecode(node.InnerText));
        }

        private static void AssertLink(ReadOnlyUrl url, Industry industry, HtmlNode node)
        {
            Assert.AreEqual(url.Path.ToLower(), node.Attributes["href"].Value.ToLower());
            Assert.AreEqual(industry.Name + " jobs", HttpUtility.HtmlDecode(node.InnerText));
        }

        private ReadOnlyUrl GetBrowseUrl(IUrlNamedLocation location, Industry industry)
        {
            return new ReadOnlyApplicationUrl(_browseBaseUrl, location.UrlName + "-jobs/" + industry.UrlName + "-jobs");
        }

        private ReadOnlyUrl GetBrowseUrl(IUrlNamedLocation location)
        {
            return new ReadOnlyApplicationUrl(_browseBaseUrl, location.UrlName + "-jobs");
        }

        private ReadOnlyUrl GetBrowseUrl(Industry industry)
        {
            return new ReadOnlyApplicationUrl(_browseIndustryBaseUrl, industry.UrlName + "-jobs");
        }
    }
}