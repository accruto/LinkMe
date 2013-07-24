using System;
using System.Text;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public abstract class JobTests
        : SeoTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string JobTitle = "Manager";
        private const string Location = "Melbourne VIC 3000";
        private const int MaxTitleSegmentLength = 100;

        private ReadOnlyUrl _baseJobUrl;
        private ReadOnlyUrl _baseJobAdUrl;
        private ReadOnlyUrl _jobAspxUrl;
        private ReadOnlyUrl _jobApplicationSignInFormUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();

            _baseJobUrl = new ReadOnlyApplicationUrl(true, "~/jobs/");
            _baseJobAdUrl = new ReadOnlyApplicationUrl(true, "~/jobads/");
            _jobAspxUrl = new ReadOnlyApplicationUrl(true, "~/jobs/Job.aspx");
            _jobApplicationSignInFormUrl = new ReadOnlyApplicationUrl(false, "~/ui/unregistered/common/jobapplicationsigninform.aspx");
        }

        [TestMethod]
        public void TestJob()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry, GetIntegratorUserId());

            // Definitive url.

            // ~/jobs/melbourne-vic-3000/accounting/manager/0f07cfa8-a69f-42da-9067-8ce180336718

            var expectedUrl = GetJobUrl(jobAd.Id, JobTitle, location, industry);
            TestUrl(expectedUrl);
            AssertPageContains(jobAd.Id.ToString());

            // Use the old form.

            // ~/jobs/melbourne-vic-3000/accounting/0f07cfa8a69f42da90678ce180336718-manager

            TestUrl(expectedUrl, GetOldJobUrl(jobAd.Id, JobTitle, location, industry));
            AssertPageContains(jobAd.Id.ToString());

            TestUrl(expectedUrl, GetOldJobUrl(jobAd.Id, JobTitle + " " + JobTitle, location, industry));
            AssertPageContains(jobAd.Id.ToString());

            // If you change anything except the job ad id then you should be redirected.

            // ~/jobs/melbourne-vic-3000/accounting/xyz/0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobUrl(jobAd.Id, "xyz", location, industry));
            AssertPageContains(jobAd.Id.ToString());

            // ~/jobs/xyz/accounting/manager/0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobUrl(jobAd.Id, JobTitle, _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), "xyz"), industry));
            AssertPageContains(jobAd.Id.ToString());

            // ~/jobs/melbourne-vic-3000/advertising-media-entertainment/manager/0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobUrl(jobAd.Id, JobTitle, location, _industriesQuery.GetIndustries()[2]));
            AssertPageContains(jobAd.Id.ToString());

            // Use the direct url.

            // ~/jobs/0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobUrl(jobAd.Id));
            AssertPageContains(jobAd.Id.ToString());

            // ~/jobads/0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobAdUrl(jobAd.Id));
            AssertPageContains(jobAd.Id.ToString());

            // ~/jobs/Job.aspx?jobAdId=0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobAspxUrl(jobAd.Id));
            AssertPageContains(jobAd.Id.ToString());

            // ~/ui/unregistered/common/jobapplicationsigninform.aspx?jobadid=0f07cfa8-a69f-42da-9067-8ce180336718

            TestUrl(expectedUrl, GetJobApplicationSignInFormAspxUrl(jobAd.Id));
            AssertPageContains(jobAd.Id.ToString());
        }

        [TestMethod]
        public void TestIndustryAlias()
        {
            // The use healthcare-medical was causing a server error.

            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");
            Assert.AreEqual("healthcare-medical-pharmaceutical", industry.UrlName);
            Assert.AreEqual("healthcare-medical", industry.UrlAliases[0]);
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry, GetIntegratorUserId());

            var expectedUrl = GetJobUrl(jobAd.Id, JobTitle, location, industry);
            TestUrl(expectedUrl);
            AssertPageContains(jobAd.Id.ToString());

            // Swap industry alias for industry.

            var url = GetJobUrl(jobAd.Id, JobTitle, location, industry.UrlAliases[0]);
            TestUrl(expectedUrl, url);
            AssertPageContains(jobAd.Id.ToString());

            // Update industry in back to search parameters.

            var backToSearchUrl = GetBackToSearchUrl(GetJobUrl(jobAd.Id), industry.UrlName);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());

            backToSearchUrl = GetBackToSearchUrl(GetJobUrl(jobAd.Id), industry.UrlAliases[0]);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());

            backToSearchUrl = GetBackToSearchUrl(GetJobAdUrl(jobAd.Id), industry.UrlName);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());

            backToSearchUrl = GetBackToSearchUrl(GetJobAdUrl(jobAd.Id), industry.UrlAliases[0]);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());

            backToSearchUrl = GetBackToSearchUrl(GetJobAspxUrl(jobAd.Id), industry.UrlName);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());

            backToSearchUrl = GetBackToSearchUrl(GetJobAspxUrl(jobAd.Id), industry.UrlAliases[0]);
            TestUrl(expectedUrl, backToSearchUrl);
            AssertPageContains(jobAd.Id.ToString());
        }

        [TestMethod]
        public void TestJobNoIndexTag()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry, GetIntegratorUserId());

            // Open job.

            var url = GetJobUrl(jobAd.Id, JobTitle, location, industry);
            Get(url);
            AssertUrl(url);
            AssertPageContains(jobAd.Id.ToString());
            AssertPageDoesNotContain("noindex");

            // Close the job
            CloseJobAd(jobAd);
            Get(url);
            AssertPageContains(jobAd.Id.ToString());
            AssertPageContains("name=\"robots\"");
            AssertPageContains("content=\"noindex\"");
        }

        [TestMethod]
        public void TestJobTitle()
        {
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustries()[0];
            var location = GetLocation(Location);
            var jobAd = CreateJobAd(employer, JobTitle, location, industry, GetIntegratorUserId());

            var url = GetJobUrl(jobAd.Id, JobTitle, location, industry);
            Get(url);

            // The first H1 should be the job title.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='title']");
            Assert.AreEqual(JobTitle, node.InnerText);

            // The page title should also be the job title.

            node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("/html/head/title");
            Assert.AreEqual(GetPageTitle(jobAd), node.InnerText.Trim());
        }

        [TestMethod]
        public void TestLongJob()
        {
            const string jobTitle = "AGEING DISABILITY AND HOME CARE (ADHC) / Aboriginal Service Coordinator - ATSI Identified Position / Central West NSW ( Job Ref: 000017LE)";
            var employer = CreateEmployer();
            var industry = _industriesQuery.GetIndustryByUrlName("healthcare-medical-pharmaceutical");
            var location = GetLocation("Regional NSW (Blue Mountains & Central West)");
            var jobAd = CreateJobAd(employer, jobTitle, location, industry, GetIntegratorUserId());

            var url = GetJobUrl(jobAd.Id, jobTitle, location, industry);
            Get(url);
            AssertUrl(url);
        }

        protected abstract Guid? GetIntegratorUserId();

        private static ReadOnlyUrl GetBackToSearchUrl(ReadOnlyUrl url, string industries)
        {
            var backToSearchUrl = url.AsNonReadOnly();
            backToSearchUrl.QueryString.Add("backToSearchType", "advancedJobSearch");
            backToSearchUrl.QueryString.Add("backToResultIndex", "1");
            backToSearchUrl.QueryString.Add("Distance", "");
            backToSearchUrl.QueryString.Add("SortOrder", "2");
            backToSearchUrl.QueryString.Add("Industries", industries);
            return backToSearchUrl;
        }

        private static string GetPageTitle(JobAd jobAd)
        {
            return jobAd.Title
                + " Job | "
                + jobAd.Description.Location
                + " | "
                + jobAd.Description.Industries[0].Name
                + " | "
                + jobAd.Description.Salary.GetDisplayText()
                + " | Jobs | Career | Resumes";
        }

        private ReadOnlyUrl GetJobUrl(Guid jobAdId, string jobAdTitle, LocationReference location, Industry industry)
        {
            return GetJobUrl(jobAdId, jobAdTitle, location, industry == null ? null : industry.UrlName);
        }

        private ReadOnlyUrl GetJobUrl(Guid jobAdId, string jobAdTitle, LocationReference location, string industry)
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
                industrySb.Append(industry);
            sb.Append("/");
            if (industrySb.Length == 0)
                sb.Append("-");
            else
                sb.Append(industrySb);

            // Job title.

            sb.Append("/");
            var segment = TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(jobAdTitle)).ToLower().Replace(' ', '-');
            if (segment.Length > MaxTitleSegmentLength)
                segment = segment.Substring(0, MaxTitleSegmentLength);
            sb.Append(segment);

            // Id

            sb.Append("/");
            sb.Append(jobAdId.ToString());

            return new ReadOnlyApplicationUrl(_baseJobUrl, sb.ToString());
        }

        private ReadOnlyUrl GetOldJobUrl(Guid jobAdId, string jobAdTitle, LocationReference location, Industry industry)
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

            sb.Append("-");
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(jobAdTitle)).ToLower().Replace(' ', '-'));

            return new ReadOnlyApplicationUrl(_baseJobUrl, sb.ToString());
        }

        private ReadOnlyUrl GetJobUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(_baseJobUrl, jobAdId.ToString());
        }

        private ReadOnlyUrl GetJobAdUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(_baseJobAdUrl, jobAdId.ToString());
        }

        private ReadOnlyUrl GetJobAspxUrl(Guid jobAdId)
        {
            var url = _jobAspxUrl.AsNonReadOnly();
            url.QueryString.Add("jobAdId", jobAdId.ToString());
            return url;
        }

        private ReadOnlyUrl GetJobApplicationSignInFormAspxUrl(Guid jobAdId)
        {
            var url = _jobApplicationSignInFormUrl.AsNonReadOnly();
            url.QueryString.Add("jobAdId", jobAdId.ToString());
            return url;
        }
    }
}