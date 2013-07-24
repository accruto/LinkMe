using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    [TestClass]
    public class RoutingRedirectTests
        : RedirectTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();
        }

        [TestMethod]
        public void TestRedirect()
        {
            // Old ASP.NET to ASP.NET MVC using routing.

            var url = new ReadOnlyApplicationUrl(false, "~/guests/profile");
            AssertNoRedirect(url);
            var profileUrl = new ReadOnlyApplicationUrl("~/guests/profile.aspx");
            AssertRedirect(profileUrl, url, url);
        }

        [TestMethod]
        public void TestAuthorizedRedirect()
        {
            var url = new ReadOnlyApplicationUrl(false, "~/guests/profile");
            AssertNoRedirect(url);

            // Access it when logged in.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/members/profile"));
        }

        [TestMethod]
        public void TestAuthorizedRedirectAspx()
        {
            var url = new ReadOnlyApplicationUrl(false, "~/guests/Friends.aspx");
            AssertNoRedirect(url);

            // Access it when logged in.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(url);
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/members/friends/ViewFriends.aspx"));
        }

        [TestMethod]
        public void TestRef()
        {
            // yellowpages.com.au: http://www.linkme.com.au/?ref=ypnav

            var queryString = "?ref=ypnav";
            var url = new ReadOnlyApplicationUrl("~/" + queryString);
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // whitepages.com.au: http://www.linkme.com.au/?ref=wpnav

            queryString = "?ref=wpnav";
            url = new ReadOnlyApplicationUrl("~/" + queryString);
            redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestBigpondLinks()
        {
            // bigpond.com

            const string queryString = "?Keywords=Blah&performSearch=True";
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString);
            var redirectUrl1 = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);
            var redirectUrl2 = new ReadOnlyApplicationUrl("~/search/jobs/results");
            AssertRedirect(url, redirectUrl1, redirectUrl2);
            AssertPageContains("Blah");
        }

        [TestMethod]
        public void TestTradingPostLinks()
        {
            // AdvancedSearch

            var queryString = "?performSearch=True&Industries=fa9b69c7-4a3f-498c-a2c4-42addfb08f7d";
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchAdvancedForm.aspx" + queryString + "&pcode=TPLNCH1");

            var redirectUrl1 = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchAdvancedForm.aspx" + queryString);
            var redirectUrl2 = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);
            var redirectUrl3 = new ReadOnlyApplicationUrl("~/search/jobs/results");

            AssertRedirect(url, redirectUrl1, redirectUrl3);
            AssertRedirect(redirectUrl1, redirectUrl2, redirectUrl3);

            // Search

            queryString = "?gglsrc=NavJobs";
            url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString + "&pcode=TPLNCH1");

            redirectUrl1 = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString);
            redirectUrl2 = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);

            AssertRedirect(url, redirectUrl1, redirectUrl2);
            AssertRedirect(redirectUrl1, redirectUrl2, redirectUrl2);
        }

        [TestMethod]
        public void TestVideo()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/resources");

            // Google has a hold of this old url so redirect it to the new one.

            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/video/Videos.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/videos/index.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestMedia()
        {
            var url = new ReadOnlyApplicationUrl("~/media/index.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);

            url = new ReadOnlyApplicationUrl("~/media/media/testimonials/employers/index.aspx");
            AssertRedirect(url, HomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestCareer()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/resources");

            var url = new ReadOnlyApplicationUrl("~/career/index.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/career/findingjob.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
            
            url = new ReadOnlyApplicationUrl("~/career/resumes.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/career/interviews.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestDefaultAspx()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Get the job page.

            var url = new ReadOnlyApplicationUrl("~/jobs");
            AssertNoRedirect(url);

            var redirectUrl = url;
            url = new ReadOnlyApplicationUrl("~/jobs/default.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // Individual job.

            url = new ReadOnlyApplicationUrl(true, "~/jobs/" + jobAd.GetJobRelativePath());
            AssertNoRedirect(url);

            redirectUrl = url;
            url = new ReadOnlyApplicationUrl(true, "~/jobs/" + jobAd.GetJobRelativePath() + "/default.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // Browse jobs.

            url = new ReadOnlyApplicationUrl(false, "~/jobs/sydney-jobs/primary-industry-jobs");
            AssertNoRedirect(url);

            redirectUrl = url;
            url = new ReadOnlyApplicationUrl(false, "~/jobs/sydney-jobs/primary-industry-jobs/default.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // Home page.

            url = new ReadOnlyApplicationUrl("~/");
            AssertNoRedirect(url);

            redirectUrl = url;
            url = new ReadOnlyApplicationUrl("~/default.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestQueryString()
        {
            var url = new ReadOnlyApplicationUrl(true, "~/unsubscribe.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/accounts/settings/unsubscribe");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // Make sure it redirects with the query string.

            var userId = Guid.NewGuid();
            const string category = "MemberAlert";
            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString["userId"] = userId.ToString("n");
            newUrl.QueryString["category"] = category;

            var newRedirectUrl = redirectUrl.AsNonReadOnly();
            newRedirectUrl.QueryString["userId"] = userId.ToString("n");
            newRedirectUrl.QueryString["category"] = category;

            AssertRedirect(newUrl, newRedirectUrl, newRedirectUrl);
        }

        [TestMethod]
        public void TestExternalRedirect()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Go to the page.

            var url = new ReadOnlyApplicationUrl("~/employers/login", new ReadOnlyQueryString("returnUrl", "http://www.yahoo.com/"));
            Get(url);

            // Sign in.

            SubmitLogIn(employer);

            // Should be directed to the employer home page and not the external site.

            AssertUrl(new ReadOnlyApplicationUrl(true, "~/search/candidates"));
        }
    }
}