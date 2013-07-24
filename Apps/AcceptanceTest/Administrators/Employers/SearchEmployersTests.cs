using System;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Services.Test.Employers.Files;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public class SearchEmployersTests
        : EmployersTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        private HtmlButtonTester _downloadButton;
        private HtmlButtonTester _searchButton;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlCheckBoxTester _matchOrganisationNameExactlyCheckBox;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlCheckBoxTester _isEnabledCheckBox;
        private HtmlCheckBoxTester _isDisabledCheckBox;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _downloadButton = new HtmlButtonTester(Browser, "download");
            _searchButton = new HtmlButtonTester(Browser, "search");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _matchOrganisationNameExactlyCheckBox = new HtmlCheckBoxTester(Browser, "MatchOrganisationNameExactly");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _isEnabledCheckBox = new HtmlCheckBoxTester(Browser, "IsEnabled");
            _isDisabledCheckBox = new HtmlCheckBoxTester(Browser, "IsDisabled");
        }

        [TestMethod]
        public void TestDefaults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            Get(GetSearchEmployersUrl());
            AssertControls(false);
        }

        [TestMethod]
        public void TestSearchWithNoCriteria()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            for (var index = 0; index < 3; ++index)
                _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);

            Get(GetSearchEmployersUrl());
            _searchButton.Click();

            AssertResults(3);
        }

        [TestMethod]
        public void TestEnabledDisabled()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);
            for (var index = 0; index < 4; ++index)
            {
                employers[index].IsEnabled = false;
                _employerAccountsCommand.UpdateEmployer(employers[index]);
            }

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            Assert.AreEqual(true, _isEnabledCheckBox.IsChecked);
            Assert.AreEqual(true, _isDisabledCheckBox.IsChecked);

            // All.

            _searchButton.Click();
            AssertResults(10);

            // Enabled.

            _isEnabledCheckBox.IsChecked = true;
            _isDisabledCheckBox.IsChecked = false;
            _searchButton.Click();
            AssertResults(6);

            // Disabled.

            _isEnabledCheckBox.IsChecked = false;
            _isDisabledCheckBox.IsChecked = true;
            _searchButton.Click();
            AssertResults(4);

            // None.

            _isEnabledCheckBox.IsChecked = false;
            _isDisabledCheckBox.IsChecked = false;
            _searchButton.Click();
            AssertResults(0);
        }

        [TestMethod]
        public void TestSearchOrganisation()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            // All.

            _searchButton.Click();
            AssertResults(10);

            _organisationNameTextBox.Text = organisation.FullName;
            _searchButton.Click();
            AssertResults(10);

            // Change some.

            var newOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(2, null, administrator.Id);
            for (var index = 0; index < 4; ++index)
            {
                employers[index].Organisation = newOrganisation;
                _employerAccountsCommand.UpdateEmployer(employers[index]);
            }

            _organisationNameTextBox.Text = organisation.FullName;
            _searchButton.Click();
            AssertResults(6);

            _organisationNameTextBox.Text = newOrganisation.FullName;
            _searchButton.Click();
            AssertResults(4);
        }

        [TestMethod]
        public void TestSearchChildOrganisation()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var parentEmployers = new Employer[10];
            for (var index = 0; index < parentEmployers.Length; ++index)
                parentEmployers[index] = _employerAccountsCommand.CreateTestEmployer(index, parent);

            // Create the child organisation.

            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);
            var childEmployers = new Employer[8];
            for (var index = 0; index < childEmployers.Length; ++index)
                childEmployers[index] = _employerAccountsCommand.CreateTestEmployer(parentEmployers.Length + index, child);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            // All.

            _searchButton.Click();
            AssertResults(parentEmployers.Length + childEmployers.Length);

            // Parent + child.

            _organisationNameTextBox.Text = parent.FullName;
            _searchButton.Click();
            AssertResults(parentEmployers.Length + childEmployers.Length);

            // Just parent.

            _organisationNameTextBox.Text = parent.FullName;
            _matchOrganisationNameExactlyCheckBox.IsChecked = true;
            _searchButton.Click();
            AssertResults(parentEmployers.Length);

            // Child.

            _organisationNameTextBox.Text = child.FullName;
            _searchButton.Click();
            AssertResults(childEmployers.Length);

            _organisationNameTextBox.Text = child.FullName;
            _matchOrganisationNameExactlyCheckBox.IsChecked = true;
            _searchButton.Click();
            AssertResults(childEmployers.Length);
        }

        [TestMethod]
        public void TestSearchFirstName()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            _firstNameTextBox.Text = employers[0].FirstName;
            _searchButton.Click();
            AssertResults(1);
        }

        [TestMethod]
        public void TestSearchLastName()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            _lastNameTextBox.Text = employers[0].LastName;
            _searchButton.Click();
            AssertResults(1);
        }

        [TestMethod]
        public void TestSearchEmailAddress()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            _emailAddressTextBox.Text = employers[0].EmailAddress.Address;
            _searchButton.Click();
            AssertResults(1);
        }

        [TestMethod]
        public void TestSearchLoginId()
        {
            // Create some employers.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employers = new Employer[10];
            for (var index = 0; index < 10; ++index)
                employers[index] = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            _loginIdTextBox.Text = employers[0].GetLoginId();
            _searchButton.Click();
            AssertResults(1);
        }

        [TestMethod]
        public void TestDownload()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            LogIn(administrator);
            Get(GetSearchEmployersUrl());

            _searchButton.Click();
            AssertResults(1);

            // Download.

            _downloadButton.Click();
            Assert.AreEqual(new[] { new Tuple<IEmployer, string>(employer, employer.GetLoginId()) }.GetEmployerFileText(), Browser.CurrentPageText);
        }

        private void AssertControls(bool hasSearchResults)
        {
            Assert.AreEqual(_downloadButton.IsVisible, hasSearchResults);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.IsTrue(_organisationNameTextBox.IsVisible);
            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.IsTrue(_isEnabledCheckBox.IsVisible);
            Assert.IsTrue(_isDisabledCheckBox.IsVisible);
        }

        private void AssertResults(int expectedResults)
        {
            Assert.AreEqual(_downloadButton.IsVisible, expectedResults > 0);
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='search-results']/tbody/tr");
            Assert.AreEqual(expectedResults, nodes == null ? 0 : nodes.Count);
        }
    }
}