using System;
using System.Net;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    /// <summary>
    /// This test case is specifically testing the redirection of external urls.
    /// The functionality of the pages that are returned is not being tested.
    /// </summary>
    [TestClass]
    public class SecurityRedirectTests : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestMemberSecurePage()
        {
            // Will be redirected to login.

            var url = new ReadOnlyApplicationUrl(true, "~/members/profile");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, false);

            url = new ReadOnlyApplicationUrl(false, "~/ui/registered/networkers/FindFriends.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/FindFriends.aspx");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/members/friends/FindFriends.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, false);

            // Log in and do it again.

            var member = CreateMember();
            LogIn(member);

            // Should be redirected to https.

            url = new ReadOnlyApplicationUrl(false, "~/members/profile");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/members/profile");
            Test(url, HttpStatusCode.OK, null, true);

            url = new ReadOnlyApplicationUrl(false, "~/ui/registered/networkers/FindFriends.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/FindFriends.aspx");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/members/friends/FindFriends.aspx");
            Test(url, HttpStatusCode.OK, null, true);
        }

        [TestMethod]
        public void TestEmployerSecurePage()
        {
            // Will be redirected to login.

            var url = new ReadOnlyApplicationUrl(true, "~/employers/settings");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/employers/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, false);

            // redirect to the secure version
            url = new ReadOnlyApplicationUrl(false, "~/ui/registered/employers/JobAdCandidates.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/JobAdCandidates.aspx");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            //Redirect from secure to new manageCandidates page
            url = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/JobAdCandidates.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/manage");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            // Log in and do it again.

            var employer = CreateEmployer();
            LogIn(employer);

            // Should be redirected to https.

            url = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
            Test(url, HttpStatusCode.OK, null, true);

            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var path = "~/employers/candidates/manage/" + jobAd.Id;

            url = new ReadOnlyApplicationUrl(false, path);
            redirectUrl = new ReadOnlyApplicationUrl(true, path);
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, path);
            Test(url, HttpStatusCode.OK, null, true);
        }

        [TestMethod]
        public void TestAdministratorSecurePage()
        {
            // Will be redirected to login.

            var url = new ReadOnlyApplicationUrl(false, "~/administrators/home");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/administrators/home");
            Test(url, HttpStatusCode.Found, redirectUrl, false);

            // Log in and do it again.

            var administrator = CreateAdministrator();
            LogIn(administrator);

            // Should be redirected to https.

            url = new ReadOnlyApplicationUrl(false, "~/administrators/home");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/administrators/home");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/administrators/home");
            Test(url, HttpStatusCode.OK, null, true);
        }

        [TestMethod]
        public void TestCustodianSecurePage()
        {
            // Will be redirected to login.

            var url = new ReadOnlyApplicationUrl(false, "~/custodians/home");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            // Will be redirected to login.

            url = new ReadOnlyApplicationUrl(true, "~/custodians/home");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", url.PathAndQuery));
            Test(url, HttpStatusCode.Found, redirectUrl, false);

            // Log in and do it again.

            var custodian = CreateCustodian();
            LogIn(custodian);

            // Should be redirected to https.

            url = new ReadOnlyApplicationUrl(false, "~/custodians/home");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/custodians/home");
            Test(url, HttpStatusCode.Found, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/custodians/home");
            Test(url, HttpStatusCode.OK, null, true);
        }

        [TestMethod]
        public void TestIgnorePage()
        {
            var url = new ReadOnlyApplicationUrl(true, "~/login");
            Test(url, HttpStatusCode.OK, null, false);

            // No redirection should happen.

            url = new ReadOnlyApplicationUrl(false, "~/guests/Friends.aspx");
            Test(url, HttpStatusCode.OK, null, false);

            url = new ReadOnlyApplicationUrl(true, "~/guests/Friends.aspx");
            Test(url, HttpStatusCode.OK, null, false);

            // Log in and do it again.

            var member = CreateMember();
            LogIn(member);

            url = new ReadOnlyApplicationUrl(true, "~/login");
            var redirectUrl = new ReadOnlyApplicationUrl("~/members/profile");
            Test(url, HttpStatusCode.Found, redirectUrl, false);
        }

        [TestMethod]
        public void TestSearchJobs()
        {
            // Will be redirected to http.

            var url = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.OK, null, false);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.OK, null, true);

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs/SimpleSearch.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/SimpleSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            // Log in and do it again.

            var member = CreateMember();
            LogIn(member);

            // Should be redirected to http.

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.OK, null, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.OK, null, true);

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs/SimpleSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/SimpleSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);
        }

        [TestMethod]
        public void TestAdvancedSearchJobs()
        {
            // Will be redirected to http.

            var url = new ReadOnlyApplicationUrl(false, "~/search/jobs/advanced");
            var redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/advanced");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs/AdvancedSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/AdvancedSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            // Log in and do it again.

            var member = CreateMember();
            LogIn(member);

            // Should be redirected to http.

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs/advanced");
            redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/advanced");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(false, "~/search/jobs/AdvancedSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(false, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/jobs/AdvancedSearch.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);
        }

        [TestMethod]
        public void TestSearchResumes()
        {
            // Will be redirected to https.

            var url = new ReadOnlyApplicationUrl(false, "~/search/resumes/SimpleSearch.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/resumes/SimpleSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            // Log in and do it again.

            var employer = CreateEmployer();
            LogIn(employer);

            url = new ReadOnlyApplicationUrl(false, "~/search/resumes/SimpleSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/resumes/SimpleSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);
        }

        [TestMethod]
        public void TestAdvancedSearchResumes()
        {
            // Will be redirected to https.

            var url = new ReadOnlyApplicationUrl(false, "~/search/resumes/AdvancedSearch.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/resumes/AdvancedSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            // Log in and do it again.

            var employer = CreateEmployer();
            LogIn(employer);

            // Will be redirected to https.

            url = new ReadOnlyApplicationUrl(false, "~/search/resumes/AdvancedSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);

            url = new ReadOnlyApplicationUrl(true, "~/search/resumes/AdvancedSearch.aspx");
            Test(url, HttpStatusCode.MovedPermanently, redirectUrl, true);
        }

        [TestMethod]
        public void TestServices()
        {
            // JobAds should not be redirected.

            var url = new ReadOnlyApplicationUrl(false, "~/jobads");
            Test(url, HttpStatusCode.OK, null, false);

            url = new ReadOnlyApplicationUrl(true, "~/jobads");
            Test(url, HttpStatusCode.OK, null, false);
            
            // GetSuggestedLocations should not be redirected.

            url = new ReadOnlyApplicationUrl(false, "~/service/GetSuggestedLocations.ashx");
            Test(url, HttpStatusCode.OK, null, false);

            url = new ReadOnlyApplicationUrl(true, "~/service/GetSuggestedLocations.ashx");
            Test(url, HttpStatusCode.OK, null, false);
        }

        private void Test(ReadOnlyUrl url, HttpStatusCode statusCode, ReadOnlyUrl expectedRedirectUrl, bool expectedToBeAbsolute)
        {
            // Do a simple GET and make sure the page is found.

            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            request.Method = "GET";
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri)));

            // Get the response and check.

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != statusCode)
                {
                    var message = "The page at\r\n'" + url + "'"
                        + " was expected to return a status code of " + statusCode
                        + " but instead returned " + response.StatusCode;
                    if (response.StatusCode == HttpStatusCode.Found)
                        message += ". Redirected to '" + response.Headers["Location"] + "'.";
                    Assert.Fail(message);
                }
                else if (response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    // A redirect really shouldn't be using a path and query but we seem to be sending it.
                    // Investigate what it would take to make sure this is absolute.

                    var expectedUrl = expectedToBeAbsolute ? expectedRedirectUrl.AbsoluteUri : expectedRedirectUrl.PathAndQuery;
                    if (string.Compare(response.Headers["Location"], expectedUrl, StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        var message = "The page at '" + url + "'"
                            + " was redirected properly (status code " + response.StatusCode + ")"
                            + " but it was redirected to '" + response.Headers["Location"] + "'"
                            + " instead of the expected '" + expectedUrl + "'";
                        Assert.Fail(message);
                    }
                }
            }
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private Administrator CreateAdministrator()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(0);
        }

        private Custodian CreateCustodian()
        {
            var community = _communitiesCommand.CreateTestCommunity(1);
            return _custodianAccountsCommand.CreateTestCustodian(0, community.Id);
        }
    }
}
