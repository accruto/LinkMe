using System;
using System.Linq;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Services.Test.Employers.Files;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class EmployersTests
        : OrganisationsTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();

        private HtmlButtonTester _downloadButton;
        private HtmlCheckBoxTester _includeChildOrganisationsCheckBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _downloadButton = new HtmlButtonTester(Browser, "download");
            _includeChildOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "IncludeChildOrganisations");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestUnverifiedOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            // Search for the organisation.

            LogIn(administrator);
            Get(GetEmployersUrl(organisation));

            AssertEmployers(employer);
            Assert.IsFalse(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsFalse(_searchButton.IsVisible);
        }

        [TestMethod]
        public void TestDownloadUnverifiedOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);
            var employer = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            // Search for the organisation.

            LogIn(administrator);
            Get(GetEmployersUrl(organisation));

            // Download.

            _downloadButton.Click();
            Assert.AreEqual(new[] { new Tuple<IEmployer, string>(employer, employer.GetLoginId()) }.GetEmployerFileText(), Browser.CurrentPageText);
        }

        [TestMethod]
        public void TestVerifiedOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Create some employers.

            var employers = CreateEmployers(0, 5, organisation);

            // Search for the organisation.

            LogIn(administrator);
            Get(GetEmployersUrl(organisation));

            AssertEmployers(employers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
        }

        [TestMethod]
        public void TestVerifiedParentOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            // Create some employers.

            var childEmployers = CreateEmployers(0, 5, child);
            var parentEmployers = CreateEmployers(5, 4, parent);

            // Search for the organisation.

            LogIn(administrator);

            // Check parent.

            Get(GetEmployersUrl(parent));
            AssertEmployers(parentEmployers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(false, _includeChildOrganisationsCheckBox.IsChecked);

            _searchButton.Click();
            AssertEmployers(parentEmployers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(false, _includeChildOrganisationsCheckBox.IsChecked);

            _includeChildOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();
            AssertEmployers(parentEmployers.Concat(childEmployers).ToArray());
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(true, _includeChildOrganisationsCheckBox.IsChecked);

            // Check child.

            Get(GetEmployersUrl(child));
            AssertEmployers(childEmployers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(false, _includeChildOrganisationsCheckBox.IsChecked);

            _searchButton.Click();
            AssertEmployers(childEmployers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(false, _includeChildOrganisationsCheckBox.IsChecked);

            _includeChildOrganisationsCheckBox.IsChecked = true;
            _searchButton.Click();
            AssertEmployers(childEmployers);
            Assert.IsTrue(_includeChildOrganisationsCheckBox.IsVisible);
            Assert.IsTrue(_searchButton.IsVisible);
            Assert.AreEqual(true, _includeChildOrganisationsCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestDownloadVerifiedOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            // Create some employers.

            var employers = CreateDownloadableEmployers(0, 5, organisation);

            // Search for the organisation.

            LogIn(administrator);
            Get(GetEmployersUrl(organisation));

            // Download.

            _downloadButton.Click();
            Assert.AreEqual(employers.GetEmployerFileText(), Browser.CurrentPageText);
        }

        [TestMethod]
        public void TestDownloadVerifiedParentOrganisation()
        {
            // Create the organisation and employer.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            // Create some employers.

            var childEmployers = CreateDownloadableEmployers(0, 5, child);
            var parentEmployers = CreateDownloadableEmployers(5, 4, parent);

            // Check parent.

            LogIn(administrator);

            Get(GetEmployersUrl(parent));
            _downloadButton.Click();
            Assert.AreEqual(parentEmployers.GetEmployerFileText(), Browser.CurrentPageText);

            Get(GetEmployersUrl(parent));
            _includeChildOrganisationsCheckBox.IsChecked = true;
            _downloadButton.Click();
            Assert.AreEqual((parentEmployers.Concat(childEmployers)).GetEmployerFileText(), Browser.CurrentPageText);

            // Check child.

            Get(GetEmployersUrl(child));
            _downloadButton.Click();
            Assert.AreEqual(childEmployers.GetEmployerFileText(), Browser.CurrentPageText);

            Get(GetEmployersUrl(child));
            _includeChildOrganisationsCheckBox.IsChecked = true;
            _downloadButton.Click();
            Assert.AreEqual(childEmployers.GetEmployerFileText(), Browser.CurrentPageText);
        }

        private Employer[] CreateEmployers(int start, int count, IOrganisation organisation)
        {
            var employers = new Employer[count];
            for (var index = start; index < start + count; ++index)
            {
                var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
                if ((index % 2 == 1))
                {
                    employer.IsEnabled = false;
                    _employerAccountsCommand.UpdateEmployer(employer);
                }

                employers[index - start] = employer;
            }

            return employers;
        }

        private Tuple<IEmployer, string>[] CreateDownloadableEmployers(int start, int count, IOrganisation organisation)
        {
            var employers = new Tuple<IEmployer, string>[count];
            for (var index = start; index < start + count; ++index)
            {
                var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
                if ((index % 2 == 1))
                {
                    employer.IsEnabled = false;
                    _employerAccountsCommand.UpdateEmployer(employer);
                }

                employers[index - start] = new Tuple<IEmployer, string>(employer, employer.GetLoginId());
            }

            return employers;
        }

        private void AssertEmployers(params Employer[] employers)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='search-results']/tbody/tr");
            Assert.AreEqual(employers.Length, nodes.Count);

            for (var index = 0; index < employers.Length; ++index)
            {
                var employer = employers[index];
                var node = nodes[index];

                // Login column

                var a = node.SelectSingleNode("td[position()=1]/a");
                Assert.AreEqual(employer.FullName, a.InnerText);
                var url = GetEmployerUrl(employer);
                Assert.AreEqual(url.PathAndQuery.ToLower(), a.Attributes["href"].Value.ToLower());

                // Organisation column.

                Assert.AreEqual(employer.Organisation.FullName, node.SelectSingleNode("td[position()=2]").InnerText);

                // Status column.

                Assert.AreEqual(employer.IsEnabled ? "Enabled" : "Disabled", node.SelectSingleNode("td[position()=3]").InnerText);
            }
        }
    }
}
