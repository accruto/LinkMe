using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Users.Custodians.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communities
{
    [TestClass]
    public class NewCustodianTests
        : CommunitiesTests
    {
        private const string LoginIdFormat = "custodian{0}";
        private const string EmailAddressFormat = "custodian{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";

        private readonly ICustodiansQuery _custodiansQuery = Resolve<ICustodiansQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

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
            var community = CreateCommunity();

            // Get the page.

            LogIn(administrator);
            var url = GetNewCustodianUrl(community);
            Get(url);

            // Should be an error message.

            Assert.IsTrue(_createButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
            _cancelButton.Click();

            AssertUrl(GetCustodiansUrl(community));
        }

        [TestMethod]
        public void TestErrors()
        {
            var administrator = CreateAdministrator(1);
            var community = CreateCommunity();

            // Get the page.

            LogIn(administrator);
            var url = GetNewCustodianUrl(community);
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
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestNewCustodians()
        {
            var administrator = CreateAdministrator(1);
            var community = CreateCommunity();

            // Get the page.

            LogIn(administrator);
            var url = GetNewCustodianUrl(community);
            Get(url);

            var custodian1 = CreateLogin(1);
            AssertLogins(community, custodian1);

            Get(url);
            var custodian2 = CreateLogin(2);
            AssertLogins(community, custodian1, custodian2);

            Get(url);
            var custodian3 = CreateLogin(3);
            AssertLogins(community, custodian1, custodian2, custodian3);
        }

        private void AssertLogins(Community community, params Custodian[] expectedCustodians)
        {
            AssertSavedLogins(community, expectedCustodians);
            AssertPageLogins(community, expectedCustodians);
        }

        private void AssertSavedLogins(Community community, ICollection<Custodian> expectedCustodians)
        {
            var custodians = _custodiansQuery.GetAffiliationCustodians(community.Id);
            Assert.AreEqual(expectedCustodians.Count, custodians.Count);

            foreach (var expectedCustodian in expectedCustodians)
            {
                var id = _loginCredentialsQuery.GetUserId(expectedCustodian.GetLoginId());
                var custodian = _custodiansQuery.GetCustodian(id.Value);
                Assert.IsNotNull(custodian);

                // Check one of the recrutiers for the organisation.

                Assert.AreEqual(true, (from r in custodians where r.Id == custodian.Id select r).Any());

                // Check one of the recrutiers for the community.

                Assert.AreEqual(expectedCustodian.EmailAddress, custodian.EmailAddress);
                Assert.AreEqual(expectedCustodian.FirstName, custodian.FirstName);
                Assert.AreEqual(expectedCustodian.LastName, custodian.LastName);
                Assert.AreEqual(expectedCustodian.GetLoginId(), custodian.GetLoginId());
            }
        }

        private void AssertPageLogins(Community community, IList<Custodian> expectedCustodians)
        {
            // Go to the login page.

            Get(GetCustodiansUrl(community));

            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            Assert.AreEqual(expectedCustodians.Count, trNodes.Count);

            for (var index = 0; index < trNodes.Count; ++index)
            {
                var expectedCustodian = expectedCustodians[index];
                var trNode = trNodes[index];
                Assert.AreEqual(expectedCustodian.FullName, trNode.SelectSingleNode("td[position()=1]/a").InnerText);

                var id = _loginCredentialsQuery.GetUserId(expectedCustodian.GetLoginId());
                var custodian = _custodiansQuery.GetCustodian(id.Value);
                Assert.AreEqual(GetCustodianUrl(custodian).PathAndQuery.ToLower(), trNode.SelectSingleNode("td[position()=1]/a").Attributes["href"].Value.ToLower());
            }
        }

        private Custodian CreateLogin(int index)
        {
            var firstName = string.Format(FirstNameFormat, index);
            var lastName = string.Format(LastNameFormat, index);

            _loginIdTextBox.Text = string.Format(LoginIdFormat, index);
            _emailAddressTextBox.Text = string.Format(EmailAddressFormat, index);
            _firstNameTextBox.Text = firstName;
            _lastNameTextBox.Text = lastName;
            _passwordTextBox.Text = "password";
            _createButton.Click();

            AssertConfirmationMessage("The account for " + firstName + " " + lastName + " has been created.");

            return new Custodian
            {
                EmailAddress = new EmailAddress { Address = string.Format(EmailAddressFormat, index), IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
            };
        }
    }
}