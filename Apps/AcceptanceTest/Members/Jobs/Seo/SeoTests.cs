using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public abstract class SeoTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        protected const string Country = "Australia";

        [TestInitialize]
        public void SeoTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected JobAd CreateJobAd(IEmployer employer, string jobTitle, LocationReference location, Industry industry, Guid? integratorUserId)
        {
            var jobAd = employer.CreateTestJobAd(jobTitle, "Blah blah blah", industry, location);
            jobAd.Integration.IntegratorUserId = integratorUserId;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected void CloseJobAd(JobAd jobAd)
        {
            _jobAdsCommand.CloseJobAd(jobAd);
        }

        protected LocationReference GetLocation(string location)
        {
            return _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), location);
        }

        protected void TestUrl(ReadOnlyUrl url)
        {
            AssertNoRedirect(url);
            Get(url);
            AssertUrl(url);
        }

        protected void TestUrls(ReadOnlyUrl expectedUrl, params ReadOnlyUrl[] urls)
        {
            foreach (var url in urls)
                TestUrl(expectedUrl, url);
        }

        protected void TestUrl(ReadOnlyUrl expectedUrl, ReadOnlyUrl url)
        {
            AssertRedirect(url, expectedUrl);
            Get(url);
            AssertUrl(expectedUrl);
        }

        private static void AssertRedirect(ReadOnlyUrl url, ReadOnlyUrl redirectUrl)
        {
            // Do a request and explicitly check the status code headers returned.

            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.AllowAutoRedirect = false;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Assert.AreEqual(HttpStatusCode.MovedPermanently, response.StatusCode);

                var expected = redirectUrl.AbsoluteUri;
                var actual = response.Headers["Location"];
                Assert.AreEqual(expected.ToLower(), actual.ToLower());
            }
        }

        private static void AssertNoRedirect(ReadOnlyUrl url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.AllowAutoRedirect = false;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Assert.AreNotEqual(HttpStatusCode.MovedPermanently, response.StatusCode);
            }
        }
    }
}