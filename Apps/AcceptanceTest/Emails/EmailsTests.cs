using System;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Emails
{
    [TestClass]
    public abstract class EmailsTests
        : WebTestClass
    {
        protected readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        protected ReadOnlyUrl _unsubscribeUrl;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlButtonTester _unsubscribeButton;

        private static readonly Regex TinyUrlRegex = new Regex("^/url/[a-zA-Z0-9]{32}$");
        private static readonly Regex TinyUrlTrackingRegex = new Regex(@"^/url/[a-zA-Z0-9]{32}\.aspx$");

        protected ReadOnlyUrl _searchUrl;
        protected ReadOnlyUrl _resultsUrl;

        [TestInitialize]
        public void EmailsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _unsubscribeUrl = new ReadOnlyApplicationUrl(true, "~/accounts/settings/unsubscribe");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _unsubscribeButton = new HtmlButtonTester(Browser, "Unsubscribe");

            _searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _resultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
        }

        protected void AssertLink(string definition, ReadOnlyUrl expectedUrl, ReadOnlyUrl url)
        {
            AssertLinkPath(url);
            Get(url);
            AssertUrl(GetEmailUrl(definition, expectedUrl));
        }

        protected void AssertLink(string definition, IRegisteredUser user, ReadOnlyUrl expectedLoginUrl, ReadOnlyUrl expectedUrl, ReadOnlyUrl url)
        {
            AssertLinkPath(url);

            // Get the page.

            Get(url);

            // Should get sign in form so log in.

            var emailUrl = GetEmailUrl(definition, expectedLoginUrl);
            AssertUrl(user.UserType == UserType.Member ? GetLoginUrl(emailUrl) : GetEmployerLoginUrl(emailUrl));

            // Log in should be pre-populated.

            Assert.AreEqual(user.GetLoginId(), _loginIdTextBox.Text);
            SubmitLogIn(user.GetLoginId(), user.GetPassword());

            AssertUrl(GetEmailUrl(definition, expectedUrl));
            LogOut();
        }

        protected void AssertLink(string definition, IRegisteredUser user, ReadOnlyUrl expectedUrl, ReadOnlyUrl url)
        {
            AssertLink(definition, user, expectedUrl, expectedUrl, url);
        }

        protected void AssertUnsubscribeLink(IUser user, Category category, ReadOnlyUrl url)
        {
            // Get the page.

            Get(url);

            AssertUrlWithoutQuery(_unsubscribeUrl);
            AssertPageContains("Please confirm that you would like to unsubscribe");
            _unsubscribeButton.Click();

            // Now that the user is unsubscribed check their settings.

            AssertSettings(user, category, Frequency.Never);
        }

        protected void AssertSettings(IHasId<Guid> user, Category category, Frequency frequency)
        {
            var settings = _settingsQuery.GetSettings(user.Id);
            if (settings == null)
            {
                Assert.AreEqual(category.DefaultFrequency, frequency);
            }
            else
            {
                var categorySettings = settings.CategorySettings.SingleOrDefault(c => c.CategoryId == category.Id);
                if (categorySettings != null)
                    Assert.AreEqual(frequency, categorySettings.Frequency);
                else
                    Assert.AreEqual(category.DefaultFrequency, frequency);
            }
        }

        private static void AssertLinkPath(ReadOnlyUrl url)
        {
            var path = url.Path;
            var applicationPath = new ApplicationUrl("~/").Path;
            Assert.IsTrue(path.StartsWith(applicationPath));
            var applicationRelativePath = path.Substring(applicationPath.Length - 1);

            if (!TinyUrlRegex.Match(applicationRelativePath).Success)
                Assert.Fail("Link '" + applicationRelativePath + "' does not correspond to a tiny url, '~/url/<guid>'.");
        }

        protected void AssertTrackingLink(ReadOnlyUrl url)
        {
            var path = url.Path;
            var applicationPath = new ReadOnlyApplicationUrl("~/").Path;
            Assert.IsTrue(path.StartsWith(applicationPath));
            var applicationRelativePath = path.Substring(applicationPath.Length - 1);

            if (!TinyUrlTrackingRegex.Match(applicationRelativePath).Success)
                Assert.Fail("Link '" + applicationRelativePath + "' does not correspond to a tracking url, '~/url/<guid>.aspx'.");

            // Get the page.

            Get(url);
            Assert.AreEqual(0, Browser.CurrentPageText.Length);
        }
    }
}
