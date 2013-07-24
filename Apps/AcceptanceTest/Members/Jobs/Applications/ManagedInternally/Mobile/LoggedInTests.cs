using System;
using System.Net;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Mobile
{
    [TestClass]
    public class LoggedInTests
        : ManagedInternallyTests
    {
        [TestMethod]
        public void TestLoggedIn()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertView(jobAd.Id));
            var applicationId = AssertApply(Apply(employer, jobAd, true, CoverLetterText));

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            AssertEmail(member, employer, CoverLetterText, GetProfileResumeFileName(member));
            AssertPreviousApplication(jobAd, application);
        }

        [TestMethod]
        public void TestRememberMeLoggedIn()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member, true);

            // Get the cookies.

            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");
            LogOut();

            // Set the cookies.

            Browser.Cookies = new CookieContainer();
            var uri = new Uri(new ReadOnlyApplicationUrl("/").AbsoluteUri);
            Browser.Cookies.Add(uri, new Cookie("user_cookie", userCookieValue));
            Browser.Cookies.Add(uri, new Cookie("passwordCookie", passwordCookieValue));

            // Apply for the job.

            View(jobAd.Id, () => AssertView(jobAd.Id));
            var applicationId = AssertApply(Apply(employer, jobAd, true, CoverLetterText));
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            AssertEmail(member, employer, CoverLetterText, GetProfileResumeFileName(member));
            AssertPreviousApplication(jobAd, application);
        }

        private string GetCookieValue(string name)
        {
            var cookies = Browser.Cookies.GetCookies(Browser.CurrentUrl);
            var cookie = cookies[name];
            return cookie == null ? null : cookie.Value;
        }
    }
}