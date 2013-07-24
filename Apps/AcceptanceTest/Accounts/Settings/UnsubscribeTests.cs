using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts.Settings
{
    [TestClass]
    public class UnsubscribeTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;
        private HtmlButtonTester _unsubscribeButton;

        private ReadOnlyUrl _settingsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _unsubscribeButton = new HtmlButtonTester(Browser, "Unsubscribe");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
        }

        [TestMethod]
        public void TestInvalidParameters()
        {
            var member = CreateMember();
            var category = GetCategory();

            // No parameters.

            Get(GetUnsubscribeUrl(null, null));

            Assert.IsFalse(_unsubscribeButton.IsVisible);
            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessages("The user id is required.", "The category is required.");

            // Invalid user id.

            var userId = Guid.NewGuid();
            Get(GetUnsubscribeUrl(userId, category.Name));

            Assert.IsFalse(_unsubscribeButton.IsVisible);
            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The user id with value '" + userId + "' cannot be found.");

            // Invalid category.

            Get(GetUnsubscribeUrl(member.Id, "invalidcategory"));

            Assert.IsFalse(_unsubscribeButton.IsVisible);
            Assert.IsTrue(_loginButton.IsVisible);
            AssertErrorMessage("The category with value 'invalidcategory' cannot be found.");

            // Login and confirm redirect.

            AssertPageContains("The details for unsubscribing you from this email are not recognised.");
            _loginIdTextBox.Text = member.GetLoginId();
            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();
            AssertUrl(_settingsUrl);
        }

        [TestMethod]
        public void TestUnsubscribe()
        {
            // Create the member.

            var member = CreateMember();
            var category = GetCategory();

            // Check the current settings.

            var settings = _settingsQuery.GetSettings(member.Id);
            Assert.IsFalse(settings.CategorySettings.Any(c => c.CategoryId == category.Id));
            Assert.IsTrue(category.DefaultFrequency != Frequency.Never);

            // Get the page.

            Get(GetUnsubscribeUrl(member.Id, category.Name));
            Assert.IsTrue(_unsubscribeButton.IsVisible);
            AssertPageContains("Please confirm that you would like to unsubscribe");

            // Unsubscribe.

            _unsubscribeButton.Click();
            Assert.IsFalse(_unsubscribeButton.IsVisible);
            AssertPageContains("You have now been unsubscribed");

            // Check the current settings.

            settings = _settingsQuery.GetSettings(member.Id);
            var categorySettings = settings.CategorySettings.SingleOrDefault(c => c.CategoryId == category.Id);
            Assert.AreEqual(Frequency.Never, categorySettings.Frequency);

            // Login and confirm redirect.

            _loginIdTextBox.Text = member.GetLoginId();
            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();
            AssertUrl(_settingsUrl);
        }

        [TestMethod]
        public void TestEarlyLogin()
        {
            // Create the member.

            var member = CreateMember();
            var category = GetCategory();

            // Check the current settings.

            var settings = _settingsQuery.GetSettings(member.Id);
            Assert.IsFalse(settings.CategorySettings.Any(c => c.CategoryId == category.Id));
            Assert.IsTrue(category.DefaultFrequency != Frequency.Never);

            // Get the page.

            Get(GetUnsubscribeUrl(member.Id, category.Name));
            Assert.IsTrue(_unsubscribeButton.IsVisible);
            AssertPageContains("Please confirm that you would like to unsubscribe");

            // Login and confirm redirect.

            _loginIdTextBox.Text = member.GetLoginId();
            _passwordTextBox.Text = member.GetPassword();
            _loginButton.Click();
            AssertUrl(_settingsUrl);

            // Check the current settings.

            settings = _settingsQuery.GetSettings(member.Id);
            Assert.IsFalse(settings.CategorySettings.Any(c => c.CategoryId == category.Id));
            Assert.IsTrue(category.DefaultFrequency != Frequency.Never);
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private static ReadOnlyUrl GetUnsubscribeUrl(Guid? userId, string category)
        {
            var queryString = new QueryString();
            if (userId != null)
                queryString.Add("userId", userId.Value.ToString("n"));
            if (category != null)
                queryString.Add("category", category);
            return new ReadOnlyApplicationUrl(true, "~/accounts/settings/unsubscribe", queryString);
        }

        private Category GetCategory()
        {
            return _settingsQuery.GetCategory("MemberToMemberNotification");
        }
    }
}