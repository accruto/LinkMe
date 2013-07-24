using System.Collections.Generic;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class SearchOrganisationsTests
        : OrganisationsTests
    {
        private const char FullNameSeparator = '\u2192';

        private HtmlTextBoxTester _fullNameTextBox;
        private HtmlDropDownListTester _accountManagerIdDropDownList;
        private HtmlCheckBoxTester _verifiedOrganisationsCheckBox;
        private HtmlCheckBoxTester _unverifiedOrganisationsCheckBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _fullNameTextBox = new HtmlTextBoxTester(Browser, "FullName");
            _accountManagerIdDropDownList = new HtmlDropDownListTester(Browser, "AccountManagerId");
            _verifiedOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "VerifiedOrganisations");
            _unverifiedOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "UnverifiedOrganisations");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestSearchByName()
        {
            // Create an account manager.

            var accountManager = _administratorAccountsCommand.CreateTestAdministrator(1);

            // Create some organisations.

            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Parent Company", null, accountManager.Id, accountManager.Id);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Child Company", parentOrganisation, accountManager.Id, accountManager.Id);
            var grandchildOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Grandchild Company", childOrganisation, accountManager.Id, accountManager.Id);

            var unverifiedOrganisation1 = _organisationsCommand.CreateTestOrganisation(1);
            var unverifiedOrganisation2 = _organisationsCommand.CreateTestOrganisation(2);

            // Assert defaults.

            LogIn(accountManager);
            Get(GetSearchOrganisationsUrl());
            Assert.AreEqual(string.Empty, _fullNameTextBox.Text);

            // The whole tree

            _fullNameTextBox.Text = parentOrganisation.Name.Substring(0, 5);
            _searchButton.Click();

            AssertOrganisations(true, false, new[] { parentOrganisation, childOrganisation, grandchildOrganisation }, new[] { unverifiedOrganisation1, unverifiedOrganisation2 });

            // Can't search for a child company name only...

            _fullNameTextBox.Text = childOrganisation.Name;
            _searchButton.Click();

            AssertOrganisations(true, false, new Organisation[0], new[] { parentOrganisation, childOrganisation, grandchildOrganisation, unverifiedOrganisation1, unverifiedOrganisation2 });

            // .. you must start with the parent name

            _fullNameTextBox.Text = parentOrganisation.Name + FullNameSeparator + childOrganisation.Name.Substring(0, 3);
            _searchButton.Click();

            AssertOrganisations(true, false, new[] { childOrganisation, grandchildOrganisation }, new[] { parentOrganisation, unverifiedOrganisation1, unverifiedOrganisation2 });

            // Unverified.

            _fullNameTextBox.Text = unverifiedOrganisation1.Name.Substring(0, 4);
            _searchButton.Click();

            AssertOrganisations(true, false, new Organisation[0], new[] { parentOrganisation, childOrganisation, grandchildOrganisation, unverifiedOrganisation1, unverifiedOrganisation2 });

            _verifiedOrganisationsCheckBox.IsChecked = false;
            _unverifiedOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();

            AssertOrganisations(false, true, new[] { unverifiedOrganisation1, unverifiedOrganisation2 }, new[] { parentOrganisation, childOrganisation, grandchildOrganisation });

            // Both verified and unverified

            _verifiedOrganisationsCheckBox.IsChecked = true;
            _unverifiedOrganisationsCheckBox.IsChecked = true;
            _fullNameTextBox.Text = parentOrganisation.Name.Substring(0, 5);
            _searchButton.Click();

            AssertOrganisations(true, true, new[] { parentOrganisation, childOrganisation, grandchildOrganisation }, new[] { unverifiedOrganisation1, unverifiedOrganisation2 });

            _fullNameTextBox.Text = unverifiedOrganisation1.Name;
            _searchButton.Click();

            AssertOrganisations(true, true, new[] { unverifiedOrganisation1 }, new[] { parentOrganisation, childOrganisation, grandchildOrganisation, unverifiedOrganisation2 });
        }

        [TestMethod]
        public void TestSearchByStatus()
        {
            // Create an account manager.

            var accountManager = _administratorAccountsCommand.CreateTestAdministrator(1);

            // Create some organisations.

            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Parent Company", null, accountManager.Id, accountManager.Id);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Child Company", parentOrganisation, accountManager.Id, accountManager.Id);
            var grandchildOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Grandchild Company", childOrganisation, accountManager.Id, accountManager.Id);

            var unverifiedOrganisation1 = _organisationsCommand.CreateTestOrganisation(1);
            var unverifiedOrganisation2 = _organisationsCommand.CreateTestOrganisation(2);

            // Assert defaults.

            LogIn(accountManager);
            Get(GetSearchOrganisationsUrl());
            Assert.AreEqual(true, _verifiedOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(false, _unverifiedOrganisationsCheckBox.IsChecked);

            // Verified

            _verifiedOrganisationsCheckBox.IsChecked = true;
            _unverifiedOrganisationsCheckBox.IsChecked = false;
            _searchButton.Click();

            AssertOrganisations(true, false, new[] { parentOrganisation, childOrganisation, grandchildOrganisation }, new[] { unverifiedOrganisation1, unverifiedOrganisation2 });

            // Unverified.

            _verifiedOrganisationsCheckBox.IsChecked = false;
            _unverifiedOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();

            AssertOrganisations(false, true, new[] { unverifiedOrganisation1, unverifiedOrganisation2 }, new[] { parentOrganisation, childOrganisation, grandchildOrganisation });

            // All

            _verifiedOrganisationsCheckBox.IsChecked = true;
            _unverifiedOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();

            AssertOrganisations(true, true, new[] { parentOrganisation, childOrganisation, grandchildOrganisation, unverifiedOrganisation1, unverifiedOrganisation2 }, new Organisation[0]);
        }

        [TestMethod]
        public void TestSearchByAccountManager()
        {
            // Create a couple of account managers, disabling the second.

            var accountManager1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var accountManager2 = _administratorAccountsCommand.CreateTestAdministrator(2);
            accountManager2.IsEnabled = false;
            _administratorAccountsCommand.UpdateAdministrator(accountManager2);

            // Create some organisations.
            //                   Parent (Account Manager 1)
            //                             |
            //                   Child (Account Manager 1)
            //                             |
            //                 Grand child (Account Manager 2)

            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Parent Company", null, accountManager1.Id, accountManager1.Id);
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Child Company", parentOrganisation, accountManager1.Id, accountManager1.Id);
            var grandchildOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation("Grandchild Company", childOrganisation, accountManager2.Id, accountManager2.Id);
            var unverifiedOrganisation = _organisationsCommand.CreateTestOrganisation(1);

            // Check account manager list

            LogIn(accountManager1);
            Get(GetSearchOrganisationsUrl());

            Assert.AreEqual(true, _verifiedOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(false, _unverifiedOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(3, _accountManagerIdDropDownList.Items.Count);
            Assert.AreEqual("", _accountManagerIdDropDownList.Items[0].Text);
            Assert.AreEqual(accountManager1.FullName, _accountManagerIdDropDownList.Items[1].Text);
            Assert.AreEqual(accountManager2.FullName + " (disabled)", _accountManagerIdDropDownList.Items[2].Text);

            // First account manager.

            _accountManagerIdDropDownList.SelectedIndex = 1;
            _searchButton.Click();

            AssertOrganisations(true, false, new[] { parentOrganisation, childOrganisation }, new[] { grandchildOrganisation, unverifiedOrganisation }); 

            // Second account manager.

            _accountManagerIdDropDownList.SelectedIndex = 2;
            _searchButton.Click();

            AssertOrganisations(true, false, new[] { grandchildOrganisation }, new[] { parentOrganisation, childOrganisation, unverifiedOrganisation });

            // Include unverified.

            _unverifiedOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();

            AssertOrganisations(true, true, new[] { grandchildOrganisation }, new[] { parentOrganisation, childOrganisation, unverifiedOrganisation });

            // Only unverified.

            _verifiedOrganisationsCheckBox.IsChecked = false;
            _searchButton.Click();

            AssertOrganisations(false, true, new Organisation[0], new[] { parentOrganisation, childOrganisation, grandchildOrganisation, unverifiedOrganisation });
        }

        private void AssertOrganisations(bool verified, bool unverified, ICollection<Organisation> expected, IEnumerable<Organisation> notExpected)
        {
            Assert.AreEqual(verified, _verifiedOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(unverified, _unverifiedOrganisationsCheckBox.IsChecked);
            AssertPageContains(expected.Count == 0 ? "No results found" : (expected.Count == 1 ? "1 result found" : expected.Count + " results found"));

            foreach (var organisation in expected)
                AssertLink(organisation);
            foreach (var organisation in notExpected)
                AssertNoLink(organisation);
        }

        private void AssertLink(IOrganisation organisation)
        {
            AssertPageContains(GetLink(organisation), true);
        }

        private void AssertNoLink(IOrganisation organisation)
        {
            AssertPageDoesNotContain(GetLink(organisation));
        }

        private static string GetLink(IOrganisation organisation)
        {
            var url = new ReadOnlyApplicationUrl("~/administrators/organisations/" + organisation.Id);
            var sb = new StringBuilder();
            sb.Append("<a href=\"")
                .Append(url.PathAndQuery)
                .Append("\">")
                .Append(organisation.FullName)
                .Append("</a>");
            return sb.ToString();
        }
    }
}