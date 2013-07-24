using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Administrators.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Administrators
{
    [TestClass]
    public class NewAdministratorTests
        : AdministratorsTests
    {
        private const string LoginIdFormat = "administrator{0}";
        private const string EmailAddressFormat = "administrator{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";
        private const string GoodPassword = "newPassword3";

        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IAdministratorsQuery _administratorsQuery = Resolve<IAdministratorsQuery>();

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlPasswordTester _passwordTextBox;

        private HtmlButtonTester _createButton;
        private HtmlButtonTester _cancelButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");

            _createButton = new HtmlButtonTester(Browser, "create");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
        }

        [TestMethod]
        public void TestCancel()
        {
            var administrator = CreateAdministrator(1);

            // Get the page.

            LogIn(administrator);
            var url = GetNewAdministratorUrl();
            Get(url);

            // Should be an error message.

            Assert.IsTrue(_createButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();

            AssertUrl(GetAdministratorsUrl());
        }

        [TestMethod]
        public void TestErrors()
        {
            var administrator = CreateAdministrator(1);

            // Get the page.

            LogIn(administrator);
            var url = GetNewAdministratorUrl();
            Get(url);

            Assert.AreEqual(string.Empty, _loginIdTextBox.Text);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);
            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(string.Empty, _passwordTextBox.Text);

            // Nothing filled in.

            _createButton.Click();

            AssertErrorMessages(
                "The username is required.",
                "The email address is required.",
                "The first name is required.",
                "The last name is required.",
                "The password is required.");

            // Bad values.

            _loginIdTextBox.Text = new string('a', 350);
            _emailAddressTextBox.Text = "sdffsdf";
            _firstNameTextBox.Text = "%$^&^*()";
            _lastNameTextBox.Text = "%$&$*&(&";
            _passwordTextBox.Text = "password";
            _createButton.Click();

            AssertErrorMessages(
                "The username must be no more than 320 characters in length.",
                "The email address must be valid and have less than 320 characters.",
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The password must be between 8 and 50 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestNewAdministrators()
        {
            var administrator1 = CreateAdministrator(1);

            // Get the page.

            LogIn(administrator1);
            var url = GetNewAdministratorUrl();
            Get(url);

            var administrator2 = CreateLogin(2);
            AssertLogins(administrator2, administrator1);

            Get(url);
            var administrator3 = CreateLogin(3);
            AssertLogins(administrator2, administrator3, administrator1);

            Get(url);
            var administrator4 = CreateLogin(4);
            AssertLogins(administrator2, administrator3, administrator4, administrator1);
        }

        private void AssertLogins(params Administrator[] expectedAdministrators)
        {
            AssertSavedLogins(expectedAdministrators);
            AssertPageLogins(expectedAdministrators);
        }

        private void AssertSavedLogins(ICollection<Administrator> expectedAdministrators)
        {
            var administrators = _administratorsQuery.GetAdministrators();
            Assert.AreEqual(expectedAdministrators.Count, administrators.Count);

            foreach (var expectedAdministrator in expectedAdministrators)
            {
                var id = _loginCredentialsQuery.GetUserId(expectedAdministrator.GetLoginId());
                var administrator = _administratorsQuery.GetAdministrator(id.Value);
                Assert.IsNotNull(administrator);

                // Check one of the recrutiers for the organisation.

                Assert.AreEqual(true, (from a in administrators where a.Id == administrator.Id select a).Any());

                // Check one of the recrutiers for the community.

                Assert.AreEqual(expectedAdministrator.EmailAddress, administrator.EmailAddress);
                Assert.AreEqual(expectedAdministrator.FirstName, administrator.FirstName);
                Assert.AreEqual(expectedAdministrator.LastName, administrator.LastName);
                Assert.AreEqual(expectedAdministrator.GetLoginId(), administrator.GetLoginId());
            }
        }

        private void AssertPageLogins(IList<Administrator> expectedAdministrators)
        {
            // Go to the login page.

            Get(GetAdministratorsUrl());

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            Assert.AreEqual(expectedAdministrators.Count, trNodes.Count);

            for (var index = 0; index < trNodes.Count; ++index)
            {
                var expectedAdministrator = expectedAdministrators[index];
                var trNode = trNodes[index];
                Assert.AreEqual(expectedAdministrator.FullName, trNode.SelectSingleNode("td[position()=1]/a").InnerText);

                var id = _loginCredentialsQuery.GetUserId(expectedAdministrator.GetLoginId());
                var administrator = _administratorsQuery.GetAdministrator(id.Value);
                Assert.AreEqual(GetAdministratorUrl(administrator).PathAndQuery.ToLower(), trNode.SelectSingleNode("td[position()=1]/a").Attributes["href"].Value.ToLower());
            }
        }

        private Administrator CreateLogin(int index)
        {
            var firstName = string.Format(FirstNameFormat, index);
            var lastName = string.Format(LastNameFormat, index);

            _loginIdTextBox.Text = string.Format(LoginIdFormat, index);
            _emailAddressTextBox.Text = string.Format(EmailAddressFormat, index);
            _firstNameTextBox.Text = firstName;
            _lastNameTextBox.Text = lastName;
            _passwordTextBox.Text = GoodPassword;
            _createButton.Click();

            AssertConfirmationMessage("The account for " + firstName + " " + lastName + " has been created.");

            return new Administrator
            {
                EmailAddress = new EmailAddress { Address = string.Format(EmailAddressFormat, index), IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
            };
        }
    }
}