using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class NewOrganisationTests
        : OrganisationsTests
    {
        private const string Name = "OrganisationName";
        private const string Location = "Norlane VIC 3214";
        const string FirstName = "Bob";
        const string LastName = "Boss";
        const string EmailAddress = "bobtheboss@test.linkme.net.au";

        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        private HtmlTextBoxTester _parentFullNameTextBox;
        private HtmlTextBoxTester _nameTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlDropDownListTester _accountManagerIdDropDownList;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlDropDownListTester _communityIdDropDownList;
        private HtmlButtonTester _createButton;
        private HtmlButtonTester _cancelButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _parentFullNameTextBox = new HtmlTextBoxTester(Browser, "ParentFullName");
            _nameTextBox = new HtmlTextBoxTester(Browser, "Name");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _accountManagerIdDropDownList = new HtmlDropDownListTester(Browser, "AccountManagerId");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _communityIdDropDownList = new HtmlDropDownListTester(Browser, "CommunityId");
            _createButton = new HtmlButtonTester(Browser, "create");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
        }

        [TestMethod]
        public void TestCancel()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            LogIn(administrator);
            var url = GetNewOrganisationUrl();
            Get(url);

            // Cancel takes you back home.

            _cancelButton.Click();
            AssertUrl(new ReadOnlyApplicationUrl(true, "~/administrators/home"));
        }

        [TestMethod]
        public void TestName()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            // Try no changes first.

            LogIn(administrator);
            var url = GetNewOrganisationUrl();
            Get(url);

            _nameTextBox.Text = Name;
            _createButton.Click();
            AssertConfirmationMessage("The &#39;" + Name + "&#39; organisation has been created.");

            AssertOrganisation(null, Name, null, administrator, administrator, null, null, null, null);
        }

        [TestMethod]
        public void TestNameErrors()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            LogIn(administrator);
            var url = GetNewOrganisationUrl();
            Get(url);

            // No name.

            _createButton.Click();
            AssertErrorMessage("The name is required.");

            // Bad name.

            _nameTextBox.Text = "%$^&$#(^";
            _createButton.Click();
            AssertErrorMessage("The name must be between 1 and 100 characters in length and not have any invalid characters.");

            // Hierarchy name.

            var parent = new VerifiedOrganisation {Name = "Parent"};
            var child = new VerifiedOrganisation {Name = "Child"};
            child.SetParent(parent);

            _nameTextBox.Text = child.FullName;
            _createButton.Click();
            AssertErrorMessage("The name must be between 1 and 100 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestAccountManager()
        {
            // Disable the second account manager.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            LogIn(administrator1);
            Get(GetNewOrganisationUrl());

            Assert.AreEqual(2, _accountManagerIdDropDownList.Items.Count);
            Assert.AreEqual(0, _accountManagerIdDropDownList.SelectedIndex);
            Assert.AreEqual(administrator1.FullName, _accountManagerIdDropDownList.SelectedItem.Text);
            Assert.AreEqual(administrator1.FullName, _accountManagerIdDropDownList.Items[0].Text);
            Assert.AreEqual(administrator2.FullName, _accountManagerIdDropDownList.Items[1].Text);

            // Change it to administrator2.

            _nameTextBox.Text = Name;
            _accountManagerIdDropDownList.SelectedIndex = 1;
            _createButton.Click();

            AssertOrganisation(null, Name, null, administrator2, administrator1, null, null, null, null);
        }

        [TestMethod]
        public void TestLocation()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            Assert.AreEqual(string.Empty, _locationTextBox.Text);

            // Change it.

            _nameTextBox.Text = Name;
            _locationTextBox.Text = Location;
            _createButton.Click();

            AssertOrganisation(null, Name, Location, administrator, administrator, null, null, null, null);
        }

        [TestMethod]
        public void TestPrimaryContact()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            // Set all the fields.

            _nameTextBox.Text = Name;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _emailAddressTextBox.Text = EmailAddress;
            _createButton.Click();

            AssertOrganisation(null, Name, null, administrator, administrator, FirstName, LastName, EmailAddress, null);
        }

        [TestMethod]
        public void TestParent()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            _parentFullNameTextBox.Text = parent.FullName;
            _nameTextBox.Text = Name;
            _createButton.Click();

            AssertOrganisation(parent.FullName, Name, null, administrator, administrator, null, null, null, null);
        }

        [TestMethod]
        public void TestNameUsed()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            // Create an existing organisation with the name.

            _organisationsCommand.CreateTestVerifiedOrganisation(Name, null, administrator.Id, administrator.Id);

            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            // Set the name.

            _nameTextBox.Text = Name;
            _createButton.Click();

            AssertUrl(GetNewOrganisationUrl());
            AssertErrorMessage("The full name is already being used.");
        }

        [TestMethod]
        public void TestFullNameUsed()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            // Create an existing organisation with the name.

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id, administrator.Id);
            _organisationsCommand.CreateTestVerifiedOrganisation(Name, parent, administrator.Id, administrator.Id);

            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            // Set the names.

            _parentFullNameTextBox.Text = parent.FullName;
            _nameTextBox.Text = Name;
            _createButton.Click();

            AssertUrl(GetNewOrganisationUrl());
            AssertErrorMessage("The full name is already being used.");
        }

        [TestMethod]
        public void TestCommunity()
        {
            // Create some communities.

            var communities = new Community[3];
            for (var index = 0; index < 3; ++index)
                communities[index] = _communitiesCommand.CreateTestCommunity(index);

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            LogIn(administrator);
            Get(GetNewOrganisationUrl());

            // No community selected by default.

            Assert.AreEqual(string.Empty, _communityIdDropDownList.SelectedItem.Text);

            // Update it.

            _nameTextBox.Text = Name;
            _communityIdDropDownList.SelectedIndex = 1;
            _createButton.Click();

            AssertOrganisation(null, Name, null, administrator, administrator, null, null, null, communities[0]);
        }

        private void AssertOrganisation(string parentFullName, string name, string location, IRegisteredUser accountManager, ICommunicationRecipient verifiedBy, string firstName, string lastName, string emailAddress, Community community)
        {
            AssertPageOrganisation(parentFullName, name, location, accountManager, verifiedBy, firstName, lastName, emailAddress, community);
            AssertSavedOrganisation(parentFullName, name, location, accountManager, verifiedBy, firstName, lastName, emailAddress, community);
        }

        private void AssertSavedOrganisation(string parentFullName, string name, string location, IHasId<Guid> accountManager, IHasId<Guid> verifiedBy, string firstName, string lastName, string emailAddress, Community community)
        {
            Guid? parentId = null;
            if (!string.IsNullOrEmpty(parentFullName))
                parentId = _organisationsQuery.GetOrganisation(null, parentFullName).Id;
            var organisation = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(parentId, name);

            if (location == null)
            {
                Assert.IsNull(organisation.Address);
            }
            else
            {
                Assert.IsNotNull(organisation.Address);
                Assert.IsNotNull(organisation.Address.Location);
                Assert.AreEqual(location, organisation.Address.Location.ToString());
            }

            Assert.AreEqual(accountManager.Id, organisation.AccountManagerId);
            Assert.AreEqual(verifiedBy.Id, organisation.VerifiedById);

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(emailAddress))
            {
                Assert.IsNull(organisation.ContactDetails);
            }
            else
            {
                Assert.AreEqual(firstName, organisation.ContactDetails.FirstName);
                Assert.AreEqual(lastName, organisation.ContactDetails.LastName);
                Assert.AreEqual(emailAddress, organisation.ContactDetails.EmailAddress);
            }

            var communityId = organisation.AffiliateId;
            if (community == null)
                Assert.AreEqual(true, communityId == null);
            else
                Assert.AreEqual(community.Id, communityId.Value);
        }

        private void AssertPageOrganisation(string parentFullName, string name, string location, IRegisteredUser accountManager, ICommunicationRecipient verifiedBy, string firstName, string lastName, string emailAddress, Community community)
        {
            Assert.AreEqual(parentFullName ?? "", _parentFullNameTextBox.Text);
            Assert.AreEqual(name, _nameTextBox.Text);
            Assert.AreEqual(location ?? string.Empty, _locationTextBox.Text);

            Assert.AreEqual(accountManager.FullName + (accountManager.IsEnabled ? "" : " (disabled)"), _accountManagerIdDropDownList.SelectedItem.Text);
            AssertPageContains("Verified by " + verifiedBy.FullName);

            Assert.AreEqual(firstName ?? string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(lastName ?? string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(emailAddress ?? string.Empty, _emailAddressTextBox.Text);

            Assert.AreEqual(community == null ? string.Empty : community.Name, _communityIdDropDownList.SelectedItem.Text);
        }
    }
}
