using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Browse
{
    [TestClass]
    public class BrowseCandidatesTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";
        private const string Subdivision = "VIC";
        private ReadOnlyUrl _candidatesUrl;
        private ReadOnlyUrl _browseBaseUrl;
        private ReadOnlyUrl _browseSalaryBaseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _candidatesUrl = new ReadOnlyApplicationUrl("~/candidates");
            _browseBaseUrl = new ReadOnlyApplicationUrl("~/candidates/");
            _browseSalaryBaseUrl = new ReadOnlyApplicationUrl("~/candidates/-/");
        }

        [TestMethod]
        public void TestBrowseCandidates()
        {
            Get(_candidatesUrl);
            AssertUrl(_candidatesUrl);

            AssertSalaries(GetBrowseUrl);
            AssertSubdivisions(GetBrowseUrl);
            AssertRegions(GetBrowseUrl);
        }

        [TestMethod]
        public void TestBrowseSalary()
        {
            var salary = new Salary { LowerBound = 80000, UpperBound = 90000 };

            var url = GetBrowseUrl(salary);
            Get(url);

            AssertUrl(url);
            AssertSubdivisions(s => GetBrowseUrl(s, salary));
            AssertRegions(r => GetBrowseUrl(r, salary));
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
            AssertSalaries(i => GetBrowseUrl(subdivision, i));
        }

        [TestMethod]
        public void TestBrowseRegion()
        {
            var region = _locationQuery.GetRegions(_locationQuery.GetCountry(Country))[0];

            var url = GetBrowseUrl(region);
            Get(url);

            AssertUrl(url);
            AssertSalaries(i => GetBrowseUrl(region, i));
        }

        [TestMethod]
        public void TestBrowseLocationSalary()
        {
            var salary = new Salary { LowerBound = 80000, UpperBound = 90000 };
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);
            var subdivision = _locationQuery.GetCountrySubdivision(_locationQuery.GetCountry(Country), Subdivision);

            // Create the member.

            const string title = "Manager";
            CreateMember(0, title, location, salary);

            // View the page.

            var url = GetBrowseUrl(subdivision, salary);
            Get(url);
            AssertUrl(url);

            AssertPageContains("<span class=\"start\">1</span>");
            AssertPageContains("<span class=\"end\">1</span>");
            AssertPageContains("<span class=\"total\">1</span>");
            AssertPageContains(title);
        }

        [TestMethod]
        public void TestUnknownLocation()
        {
            const int count = 100;
            const int itemsPerPage = 10;
            var salary = new Salary { LowerBound = 80000, UpperBound = 90000 };

            var expectedUrl = new ReadOnlyApplicationUrl("~/candidates/-/" + GetUrlSegment(salary));

            // An unknown industry should be sent to the location url.

            var url = new ReadOnlyApplicationUrl("~/candidates/unknown-candidates/" + GetUrlSegment(salary));
            Get(url);
            AssertUrl(expectedUrl);

            // Using any index should get you the same page.

            const int totalPages = count / itemsPerPage;
            var page = 1;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl(true, "~/candidates/unknown-candidates/" + GetUrlSegment(salary) + "/" + page );
                Get(url);
                AssertUrl(expectedUrl);
            }
        }

        [TestMethod]
        public void TestUnknownSalary()
        {
            const int count = 100;
            const int itemsPerPage = 10;

            var expectedUrl = new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates");

            // An unknown industry should be sent to the location url.

            var url = new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates/none-candidates");
            Get(url);
            AssertUrl(expectedUrl);

            // Using any index should get you the same page.

            const int totalPages = count / itemsPerPage;
            var page = 1;
            for (; page <= totalPages; ++page)
            {
                url = new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates/none-candidates/" + page);
                Get(url);
                AssertUrl(expectedUrl);
            }
        }

        [TestMethod]
        public void TestHttps()
        {
            var salary = new Salary { LowerBound = 80000, UpperBound = 90000 };

            var expectedUrl = new ReadOnlyApplicationUrl(true, "~/candidates/melbourne-candidates/" + GetUrlSegment(salary));

            // An http request should be sent to https.

            var url = new ReadOnlyApplicationUrl("~/candidates/melbourne-candidates/" + GetUrlSegment(salary));
            Get(url);
            AssertUrl(expectedUrl);
        }

        private void CreateMember(int index, string desiredJobTitle, LocationReference location, Salary salary)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.Address.Location = location;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredJobTitle = desiredJobTitle;
            candidate.DesiredSalary = salary;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
        }

        private void AssertSalaries(Func<Salary, ReadOnlyUrl> getUrl)
        {
            // Other comes last.

            var salaries = GetSalaryBands();
            var firstSalaries = salaries.Take(salaries.Count / 2).ToList();
            var secondSalaries = salaries.Skip(firstSalaries.Count).ToList();

            var firstNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='industry_section section']//div[@class='columns']/div[@class='column' and position()=1]//a");
            var secondNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='industry_section section']//div[@class='columns']/div[@class='column' and position()=2]//a");

            if (firstNodes == null)
                Assert.Fail();
            if (secondNodes == null)
                Assert.Fail();

            for (var index = 0; index < firstSalaries.Count; ++index)
                AssertLink(getUrl(firstSalaries[index]), firstSalaries[index], firstNodes[index]);
            for (var index = 0; index < secondSalaries.Count; ++index)
                AssertLink(getUrl(secondSalaries[index]), secondSalaries[index], secondNodes[index]);
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
            Assert.AreEqual(url.ToString().ToLower(), new ReadOnlyApplicationUrl(node.Attributes["href"].Value).ToString().ToLower());
            Assert.AreEqual(subdivision.Name, HttpUtility.HtmlDecode(node.InnerText));
        }

        private static void AssertLink(ReadOnlyUrl url, Salary salary, HtmlNode node)
        {
            Assert.AreEqual(url.ToString().ToLower(), new ReadOnlyApplicationUrl(node.Attributes["href"].Value).ToString().ToLower());
            Assert.AreEqual(GetSalaryBandDisplayText(salary), node.InnerText);
        }

        private ReadOnlyUrl GetBrowseUrl(IUrlNamedLocation location, Salary salary)
        {
            return new ReadOnlyApplicationUrl(true, _browseBaseUrl, location.UrlName + "-candidates/" + GetUrlSegment(salary));
        }

        private ReadOnlyUrl GetBrowseUrl(IUrlNamedLocation location)
        {
            return new ReadOnlyApplicationUrl(_browseBaseUrl, location.UrlName + "-candidates");
        }

        private ReadOnlyUrl GetBrowseUrl(Salary salary)
        {
            return new ReadOnlyApplicationUrl(_browseSalaryBaseUrl, GetUrlSegment(salary));
        }

        private static IList<Salary> GetSalaryBands()
        {
            var bands = new List<Salary> { new Salary { UpperBound = 30000 } };

            for (var i = 30000; i < 200000; i += 10000)
            {
                bands.Add(new Salary { LowerBound = i, UpperBound = i + 10000 });
            }

            bands.Add(new Salary { LowerBound = 200000 });
            return bands;
        }

        private static string GetSalaryBandDisplayText(Salary salary)
        {
            if (!salary.LowerBound.HasValue && !salary.UpperBound.HasValue)
                return string.Empty;

            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up to " + ConvertBandForViewing(salary.UpperBound.Value);

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return ConvertBandForViewing(salary.LowerBound.Value) + " and above";

            return ConvertBandForViewing(salary.LowerBound.Value) + "-" + ConvertBandForViewing(salary.UpperBound.Value);
        }

        private static string ConvertBandForViewing(decimal value)
        {
            return string.Format("{0:C0}", value);
        }

        private static string GetUrlSegment(Salary salary)
        {
            if (salary == null)
                return "-";

            if (!salary.LowerBound.HasValue && !salary.UpperBound.HasValue)
                return "-";

            salary = salary.ToRate(SalaryRate.Year);
            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up-to-" + GetUrlSegment(salary.UpperBound.Value) + "-candidates";

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return GetUrlSegment(salary.LowerBound.Value) + "-and-above" + "-candidates";

            return GetUrlSegment(salary.LowerBound.Value) + "-" + GetUrlSegment(salary.UpperBound.Value) + "-candidates";
        }

        private static string GetUrlSegment(decimal value)
        {
            return (int)(value / 1000) + "k";
        }
    }
}