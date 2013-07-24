using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class GetJobTests
        : JobsTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string JobTitle = "Development Manager";
        private const string Country = "Australia";
        private const string Location = "Melbourne VIC 3000";

        [TestMethod]
        public void TestJob()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry);

            // Definitive url.

            var url = GetJobAdUrl(jobAd.Id, JobTitle, location, industry);
            Get(url);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            // Use the old form.

            var otherUrl = GetOldJobUrl(jobAd.Id, JobTitle, location, industry, '-');
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            otherUrl = GetOldJobUrl(jobAd.Id, JobTitle, location, industry, '+');
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            // If you change anything except the job ad id then you should be redirected.

            otherUrl = GetJobAdUrl(jobAd.Id, "xyz", location, industry);
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            otherUrl = GetJobAdUrl(jobAd.Id, JobTitle, _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), "xyz"), industry);
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            otherUrl = GetJobAdUrl(jobAd.Id, JobTitle, location, _industriesQuery.GetIndustries()[2]);
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());

            // Use the direct url.

            otherUrl = GetJobAdUrl(jobAd.Id);
            Get(otherUrl);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());
        }

        [TestMethod]
        public void TestIndustryAlias()
        {
            // The use healthcare-medical was causing a server error.

            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry);

            Assert.AreEqual("healthcare-medical", industry.UrlAliases[0]);

            var url = GetJobAdUrl(jobAd.Id).AsNonReadOnly();
            url.QueryString.Add("backToSearchType", "advancedJobSearch");
            url.QueryString.Add("backToResultIndex", "1");
            url.QueryString.Add("Distance", "");
            url.QueryString.Add("SortOrder", "2");
            url.QueryString.Add("Industries", industry.UrlAliases[0]);

            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace(" ", "-").ToLower() + "/" + jobAd.Id));
            AssertPageContains(jobAd.Id.ToString());
        }

        [TestMethod]
        public void TestJobLocation()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];

            // Post code.

            var location = GetLocation("Melbourne VIC 3000");
            var jobAd = CreateJobAd(employer, "Manager", location, industry);
            var url = new ReadOnlyApplicationUrl("~/jobs/melbourne-jobs/" + industry.UrlName + "-jobs/" + jobAd.Id.ToString("n") + "-Manager");
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace(" ", "-").ToLower() + "/" + jobAd.Id));
            AssertPageContains("Melbourne VIC 3000");

            // Includes dodgy characters.

            location = GetLocation("East Melbourne, VIC 3002");
            jobAd = CreateJobAd(employer, "Manager", location, industry);
            url = new ReadOnlyApplicationUrl("~/jobs/east-perth-wa-jobs/" + industry.UrlName + "-jobs/" + jobAd.Id.ToString("n") + "-Manager");
            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace(" ", "-").ToLower() + "/" + jobAd.Id));
            AssertPageContains("East Melbourne VIC 3002");

            // Browse.

            url = new ReadOnlyApplicationUrl(true, "~/jobs/victoria-jobs/" + industry.UrlName + "-jobs");
            Get(url);
            AssertUrl(url);

            var jobs = GetJobs();
            Assert.IsTrue(jobs.Count > 0);
            foreach (var job in jobs)
            {
                url = new ReadOnlyApplicationUrl(true, job);

                // When browsing there should be no query string.

                Assert.IsTrue(url.QueryString.Count == 0);
                Get(url);
                AssertUrl(url);
            }
        }

        [TestMethod]
        public void TestJobTitleWithExtraCharacters()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation("Dandenong VIC");

            // Create a job with a title with a '\n' plus extra characters.

            const string jobTitle = "CREDIT SUPERVISOR/ASSISTANT CREDIT MANAGER - sth east - great\n        $$ - industry guru";
            var jobAd = CreateJobAd(employer, jobTitle, location, industry);

            // Browse to a page that has a link to the job.

            var url = new ApplicationUrl("~/jobs/victoria/" + industry.UrlName + "-jobs/");
            Get(url);

            // Follow the link.

            var jobUrl = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='column title']/a").Attributes["href"].Value;
            Assert.IsTrue(!jobUrl.Contains("%"));
            Get(new ApplicationUrl(jobUrl));
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace(" ", "-").Replace("/", "").Replace("$", "").Replace("\n", "").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").ToLower() + "/" + jobAd.Id));
        }

        [TestMethod]
        public void TestJobTitleWithLineFeed()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation("Dandenong VIC");

            // Create a job with a title with a '\n'.

            const string jobTitle = "CREDIT SUPERVISOR ASSISTANT CREDIT MANAGER sth east great\n  industry guru";
            var jobAd = CreateJobAd(employer, jobTitle, location, industry);

            // Follow the link.

            var url = new ApplicationUrl("~/jobs/victoria-jobs/" + industry.UrlName + "-jobs/");
            Get(url);
            var jobUrl = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='column title']/a").Attributes["href"].Value;
            Assert.IsTrue(!jobUrl.Contains("%"));
            Get(new ApplicationUrl(jobUrl));
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace("\n", "").Replace(" ", "-").Replace("--", "-").ToLower() + "/" + jobAd.Id));
        }

        [TestMethod]
        public void TestJobWithOnlyExtraCharacters()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation("Dandenong VIC");

            // Create a job with a title with a '\n'.

            const string jobTitle = ">>> ";
            var jobAd = CreateJobAd(employer, jobTitle, location, industry);

            // Follow the link.

            var url = new ApplicationUrl("~/jobs/victoria-jobs/" + industry.UrlName + "-jobs/");
            Get(url);
            var jobUrl = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='column title']/a").Attributes["href"].Value;
            Assert.IsTrue(!jobUrl.Contains("%"));
            Get(new ApplicationUrl(jobUrl));
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/jobs/" + location.ToString().Replace(" ", "-").ToLower() + "/" + industry.UrlName + "/" + jobAd.Title.Replace(" ", "-").Replace(">", "-").Replace("--", "-").Replace("--", "-").ToLower() + "/" + jobAd.Id));
        }

        private ReadOnlyUrl GetOldJobUrl(Guid jobAdId, string jobAdTitle, LocationReference location, Industry industry, char jobTitleSeparator)
        {
            var sb = new StringBuilder();

            // Location.

            sb.Append(!string.IsNullOrEmpty(location.ToString())
                ? TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(location.ToString())).ToLower().Replace(' ', '-')
                : "-");

            // Industry. If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industrySb = new StringBuilder();
            if (industry != null)
                industrySb.Append(industry.UrlName);
            sb.Append("/");
            if (industrySb.Length == 0)
                sb.Append("-");
            else
                sb.Append(industrySb);

            // Id

            sb.Append("/");
            sb.Append(jobAdId.ToString("n"));

            // Job title.

            sb.Append(jobTitleSeparator);
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(jobAdTitle)).ToLower().Replace(' ', jobTitleSeparator));

            return new ReadOnlyApplicationUrl(_baseJobUrl, sb.ToString());
        }

        private JobAd CreateJobAd(IEmployer employer, string jobTitle, LocationReference location, Industry industry)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle, "Blah blah blah", industry, location);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private LocationReference GetLocation(string location)
        {
            return _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), location);
        }

        private IList<string> GetJobs()
        {
            var jobs = new List<string>();

            var document = Browser.CurrentHtml.DocumentNode;
            var nodes = document.SelectNodes("//div[@class='column title']/a");
            if (nodes != null)
                jobs.AddRange(nodes.Select(node => node.Attributes["href"].Value));
            return jobs;
        }
    }
}