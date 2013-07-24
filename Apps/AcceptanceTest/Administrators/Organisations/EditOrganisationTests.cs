using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class EditOrganisationTests
        : OrganisationsTests
    {
        const string FirstName = "Bob";
        const string LastName = "Boss";
        const string EmailAddress1 = "bobtheboss@test.linkme.net.au";
        const string EmailAddress2 = "bobsnewemail@test.linkme.net.au";
        const string EmailAddress3 = "bobsotheremail@test.linkme.net.au";

        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        private HtmlTextBoxTester _parentFullNameTextBox;
        private HtmlTextBoxTester _nameTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlDropDownListTester _accountManagerIdDropDownList;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlDropDownListTester _communityIdDropDownList;
        private HtmlButtonTester _saveButton;
        private HtmlButtonTester _verifyButton;

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
            _saveButton = new HtmlButtonTester(Browser, "save");
            _verifyButton = new HtmlButtonTester(Browser, "verify");
        }

        [TestMethod]
        public void TestViewUnverified()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);

            // Get the page.

            LogIn(administrator);
            var url = GetOrganisationUrl(organisation);
            Get(url);
            AssertUrl(url);

            AssertUnverifiedDetails(organisation);
        }

        [TestMethod]
        public void TestViewVerified()
        {
            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator1.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator1.Id);
            var grandChild = _organisationsCommand.CreateTestVerifiedOrganisation(3, child, administrator1.Id, administrator2.Id);

            // View the parent.

            LogIn(administrator1);
            var url = GetOrganisationUrl(parent);
            Get(url);
            AssertUrl(url);

            AssertVerifiedDetails(parent, administrator1, administrator1);
            AssertNoLink(parent);
            AssertLink(child);
            AssertLink(grandChild);

            // View the child.

            url = GetOrganisationUrl(child);
            Get(url);
            AssertUrl(url);

            AssertVerifiedDetails(child, administrator1, administrator1);
            AssertLink(parent);
            AssertNoLink(child);
            AssertLink(grandChild);

            // View the grandchild.

            url = GetOrganisationUrl(grandChild);
            Get(url);
            AssertUrl(url);

            AssertVerifiedDetails(grandChild, administrator1, administrator2);
            AssertLink(parent);
            AssertLink(child);
            AssertNoLink(grandChild);
        }

        [TestMethod]
        public void TestEditName()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            const string newName = "New child company name";

            // Try no changes first.

            LogIn(administrator);
            var url = GetOrganisationUrl(child);
            Get(url);

            _saveButton.Click();
            AssertVerifiedDetails(child, administrator, administrator);

            // Change the name

            _nameTextBox.Text = newName;
            _saveButton.Click();

            Assert.AreEqual(newName, _nameTextBox.Text);

            // Reload the page and check that it's really changed

            Get(url);
            Assert.AreEqual(newName, _nameTextBox.Text);

            child.Name = newName;
            AssertOrganisation(child, _organisationsQuery.GetOrganisation(child.Id));
        }

        [TestMethod]
        public void TestEditNameErrors()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            // Try no changes first.

            LogIn(administrator);
            var url = GetOrganisationUrl(child);
            Get(url);

            // No name.

            _nameTextBox.Text = string.Empty;
            _saveButton.Click();
            AssertErrorMessage("The name is required.");

            // Bad characters.

            _nameTextBox.Text = "%$^&$#(^";
            _saveButton.Click();
            AssertErrorMessage("The name must be between 1 and 100 characters in length and not have any invalid characters.");

            // Hierarchy name.

            _nameTextBox.Text = child.FullName;
            _saveButton.Click();
            AssertErrorMessage("The name must be between 1 and 100 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestEditLocation()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            const string country = "Australia";
            const string newLocation1 = "Norlane VIC 3214";
            const string newLocation2 = "South Yarra VIC 3141";

            // Try no changes first.

            LogIn(administrator);
            var url = GetOrganisationUrl(child);
            Get(url);

            _saveButton.Click();
            AssertVerifiedDetails(child, administrator, administrator);

            // Change the location

            _locationTextBox.Text = newLocation1;
            _saveButton.Click();
            Assert.AreEqual(newLocation1, _locationTextBox.Text);

            // Reload the page and check that it's really changed

            Get(url);
            Assert.AreEqual(newLocation1, _locationTextBox.Text);
            child.Address = new Address {Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(country), newLocation1)};
            AssertOrganisation(child, _organisationsQuery.GetOrganisation(child.Id));

            // Change it again.

            _locationTextBox.Text = newLocation2;
            _saveButton.Click();
            Assert.AreEqual(newLocation2, _locationTextBox.Text);

            // Reload the page and check that it's really changed

            Get(url);
            Assert.AreEqual(newLocation2, _locationTextBox.Text);
            child.Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(country), newLocation2) };
            AssertOrganisation(child, _organisationsQuery.GetOrganisation(child.Id));

            // Remove it.

            _locationTextBox.Text = string.Empty;
            _saveButton.Click();
            Assert.AreEqual(string.Empty, _locationTextBox.Text);

            // Reload the page and check that it's really changed

            Get(url);
            Assert.AreEqual(string.Empty, _locationTextBox.Text);
            child.Address = null;
            AssertOrganisation(child, _organisationsQuery.GetOrganisation(child.Id));
        }

        [TestMethod]
        public void TestEditAccountManager()
        {
            // Disable the second account manager.

            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);
            administrator2.IsEnabled = false;
            _administratorAccountsCommand.UpdateAdministrator(administrator2);

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator1.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator2.Id);

            LogIn(administrator1);
            var url = GetOrganisationUrl(child);
            Get(url);

            Assert.AreEqual(2, _accountManagerIdDropDownList.Items.Count);
            Assert.AreEqual(0, _accountManagerIdDropDownList.SelectedIndex);
            Assert.AreEqual(administrator2.FullName + " (disabled)", _accountManagerIdDropDownList.SelectedItem.Text);

            // Change it to administrator1.

            _accountManagerIdDropDownList.SelectedIndex = 1;
            _saveButton.Click();

            Assert.AreEqual(0, _accountManagerIdDropDownList.SelectedIndex);
            Assert.AreEqual(1, _accountManagerIdDropDownList.Items.Count);
            Assert.AreEqual(administrator1.FullName, _accountManagerIdDropDownList.SelectedItem.Text);
            AssertPageContains("Verified by " + administrator2.FullName); // "Verified by" hasn't changed

            // Load the page again.

            Get(url);
            Assert.AreEqual(administrator1.FullName, _accountManagerIdDropDownList.SelectedItem.Text);
            Assert.AreEqual(1, _accountManagerIdDropDownList.Items.Count);
            Assert.AreEqual(administrator1.FullName, _accountManagerIdDropDownList.Items[0].Text);

            child.AccountManagerId = administrator1.Id;
            AssertOrganisation(child, _organisationsQuery.GetOrganisation(child.Id));
        }

        [TestMethod]
        public void TestEditPrimaryContact()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            LogIn(administrator);
            var url = GetOrganisationUrl(child);

            // Set.

            EditPrimaryContact(url, string.Empty, string.Empty, string.Empty, FirstName, LastName, EmailAddress1, child);

            // Update.

            EditPrimaryContact(url, FirstName, LastName, EmailAddress1, FirstName, LastName, EmailAddress2, child);

            // Clear everything.

            EditPrimaryContact(url, FirstName, LastName, EmailAddress2, string.Empty, string.Empty, string.Empty, child);
        }

        [TestMethod]
        public void TestMultipleEmailAddresses()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            LogIn(administrator);
            var url = GetOrganisationUrl(child);
            Get(url);

            Assert.AreEqual(string.Empty, _firstNameTextBox.Text);
            Assert.AreEqual(string.Empty, _lastNameTextBox.Text);
            Assert.AreEqual(string.Empty, _emailAddressTextBox.Text);

            // Set all the fields.

            EditPrimaryContact(url, string.Empty, string.Empty, string.Empty, FirstName, LastName, EmailAddress1 + ";" + EmailAddress2, child);

            // Update.

            EditPrimaryContact(url, FirstName, LastName, EmailAddress1 + ";" + EmailAddress2, FirstName, LastName, EmailAddress2, child);

            // Clear everything.

            EditPrimaryContact(url, FirstName, LastName, EmailAddress2, string.Empty, string.Empty, string.Empty, child);

            // Add 3 email address.

            EditPrimaryContact(url, string.Empty, string.Empty, string.Empty, FirstName, LastName, EmailAddress1 + ";" + EmailAddress2 + ";" + EmailAddress3, child);
        }

        [TestMethod]
        public void TestEditParent()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);
            var grandChild = _organisationsCommand.CreateTestVerifiedOrganisation(3, child, administrator.Id, administrator.Id);
            var otherParent = _organisationsCommand.CreateTestVerifiedOrganisation(4, null, administrator.Id);

            LogIn(administrator);
            var url = GetOrganisationUrl(grandChild);
            Get(url);

            // Check the initial state.

            Assert.AreEqual(child.FullName, _parentFullNameTextBox.Text);

            AssertLink(parent);
            AssertLink(child);
            AssertNoLink(grandChild);
            AssertNoLink(otherParent);

            // Move it up a level.

            _parentFullNameTextBox.Text = parent.FullName;
            _saveButton.Click();

            Assert.AreEqual(parent.FullName, _parentFullNameTextBox.Text);
            AssertPageDoesNotContain(child.FullName);
            grandChild.SetParent(parent);
            AssertOrganisation(grandChild, _organisationsQuery.GetOrganisation(grandChild.Id));

            AssertLink(parent);
            AssertLink(child);
            AssertNoLink(grandChild);
            AssertNoLink(otherParent);

            // Clear the parent.

            _parentFullNameTextBox.Text = "";
            _saveButton.Click();

            Assert.AreEqual("", _parentFullNameTextBox.Text);

            AssertNoLink(parent);
            AssertNoLink(child);
            AssertNoLink(grandChild);
            AssertNoLink(otherParent);

            grandChild.SetParent(null);
            AssertOrganisation(grandChild, _organisationsQuery.GetOrganisation(grandChild.Id));

            // Set to another parent.

            _parentFullNameTextBox.Text = otherParent.FullName;
            _saveButton.Click();

            Assert.AreEqual(otherParent.FullName, _parentFullNameTextBox.Text);

            AssertNoLink(parent);
            AssertNoLink(child);
            AssertNoLink(grandChild);
            AssertLink(otherParent);

            grandChild.SetParent(otherParent);
            AssertOrganisation(grandChild, _organisationsQuery.GetOrganisation(grandChild.Id));

            // Change it back to original.

            _parentFullNameTextBox.Text = child.FullName;
            _saveButton.Click();

            Assert.AreEqual(child.FullName, _parentFullNameTextBox.Text);

            AssertLink(parent);
            AssertLink(child);
            AssertNoLink(grandChild);
            AssertNoLink(otherParent);

            grandChild.SetParent(child);
            AssertOrganisation(grandChild, _organisationsQuery.GetOrganisation(grandChild.Id));
        }

        [TestMethod]
        public void TestCircularHierarchy()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);

            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);
            var grandChild = _organisationsCommand.CreateTestVerifiedOrganisation(3, child, administrator.Id, administrator.Id);

            LogIn(administrator);
            var url = GetOrganisationUrl(parent);
            Get(url);

            // Try to set the organisation as its own parent.

            _parentFullNameTextBox.Text = parent.FullName;
            _saveButton.Click();

            AssertErrorMessage("Setting the parent will result in a circular dependency.");
            AssertOrganisation(parent, _organisationsQuery.GetOrganisation(parent.Id));

            // Try to set an immediate child of the organisation as its parent

            _parentFullNameTextBox.Text = child.FullName;
            _saveButton.Click();

            AssertErrorMessage("Setting the parent will result in a circular dependency.");
            AssertOrganisation(parent, _organisationsQuery.GetOrganisation(parent.Id));

            // Try to set a grandchild of the organisation as its parent

            _parentFullNameTextBox.Text = grandChild.FullName;
            _saveButton.Click();

            AssertErrorMessage("Setting the parent will result in a circular dependency.");
            AssertOrganisation(parent, _organisationsQuery.GetOrganisation(parent.Id));
        }

        [TestMethod]
        public void TestEditCommunity()
        {
            // Create some communities.

            var communities = new Community[3];
            for (var index = 0; index < 3; ++index)
                communities[index] = _communitiesCommand.CreateTestCommunity(index);

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetOrganisationUrl(organisation);
            Get(url);

            // No community selected by default.

            AssertCommunity(organisation.Id, null, communities);

            // Update it.

            _communityIdDropDownList.SelectedIndex = 1;
            _saveButton.Click();

            AssertCommunity(organisation.Id, communities[0].Id, communities);

            // Update it.

            _communityIdDropDownList.SelectedIndex = 2;
            _saveButton.Click();

            AssertCommunity(organisation.Id, communities[1].Id, communities);

            // Remove it.

            _communityIdDropDownList.SelectedIndex = 0;
            _saveButton.Click();

            AssertCommunity(organisation.Id, null, communities);
        }

        [TestMethod]
        public void TestVerifyOrganisation()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestOrganisation(1);

            // Get the page.

            LogIn(administrator);
            var url = GetOrganisationUrl(organisation);
            Get(url);
            AssertUrl(url);

            AssertUnverifiedDetails(organisation);

            // Verify it.

            _verifyButton.Click();

            // Check the page.

            AssertUrl(url);
            AssertVerifiedDetails(organisation, administrator, administrator);

            // Check what is saved.

            var verifiedOrganisation = new VerifiedOrganisation
            {
                Id = organisation.Id,
                Name = organisation.Name,
                AccountManagerId = administrator.Id,
                VerifiedById = administrator.Id
            };
            AssertOrganisation(verifiedOrganisation, _organisationsQuery.GetOrganisation(organisation.Id));
        }

        private void EditPrimaryContact(ReadOnlyUrl url, string existingFirstName, string existingLastName, string existingEmailAddress, string newFirstName, string newLastName, string newEmailAddress, VerifiedOrganisation organisation)
        {
            Get(url);
            Assert.AreEqual(existingFirstName, _firstNameTextBox.Text);
            Assert.AreEqual(existingLastName, _lastNameTextBox.Text);
            Assert.AreEqual(existingEmailAddress, _emailAddressTextBox.Text);

            // Set all the fields.

            _firstNameTextBox.Text = newFirstName;
            _lastNameTextBox.Text = newLastName;
            _emailAddressTextBox.Text = newEmailAddress;
            _saveButton.Click();

            Assert.AreEqual(newFirstName, _firstNameTextBox.Text);
            Assert.AreEqual(newLastName, _lastNameTextBox.Text);
            Assert.AreEqual(newEmailAddress, _emailAddressTextBox.Text);

            // Load the page again and check

            Get(url);
            Assert.AreEqual(newFirstName, _firstNameTextBox.Text);
            Assert.AreEqual(newLastName, _lastNameTextBox.Text);
            Assert.AreEqual(newEmailAddress, _emailAddressTextBox.Text);

            organisation.ContactDetails = string.IsNullOrEmpty(newFirstName) && string.IsNullOrEmpty(newLastName) && string.IsNullOrEmpty(newEmailAddress)
                ? null
                : GetContactDetails(newFirstName, newLastName, newEmailAddress);
            AssertOrganisation(organisation, _organisationsQuery.GetOrganisation(organisation.Id));
        }

        private static ContactDetails GetContactDetails(string firstName, string lastName, string emailAddress)
        {
            var contactDetails = new ContactDetails { FirstName = firstName, LastName = lastName };
           
            var splitEmailAddresses = emailAddress.Split(';');
            contactDetails.EmailAddress = splitEmailAddresses.Length > 0 ? splitEmailAddresses[0] : null;
            contactDetails.SecondaryEmailAddresses = splitEmailAddresses.Length > 1
                ? string.Join(";", splitEmailAddresses.Skip(1).ToArray())
                : null;

            return contactDetails;
        }

        private void AssertCommunity(Guid organisationId, Guid? expectedCommunityId, IList<Community> communities)
        {
            AssertPageCommunity(expectedCommunityId, communities);
            AssertSavedCommunity(organisationId, expectedCommunityId);
        }

        private void AssertSavedCommunity(Guid organisationId, Guid? expectedCommunityId)
        {
            Assert.AreEqual(expectedCommunityId, _organisationsQuery.GetOrganisation(organisationId).AffiliateId);
        }

        private void AssertPageCommunity(Guid? expectedCommunityId, IList<Community> communities)
        {
            // Check the items, first is "no community".

            Assert.AreEqual(string.Empty, _communityIdDropDownList.Items[0].Text);
            for (var index = 0; index < communities.Count; ++index)
                Assert.AreEqual(communities[index].Name, _communityIdDropDownList.Items[index + 1].Text);

            // Check the expected community.

            Assert.AreEqual(expectedCommunityId == null ? string.Empty : (from c in communities where c.Id == expectedCommunityId.Value select c.Name).Single(), _communityIdDropDownList.SelectedItem.Text);
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
                .Append(organisation.Name)
                .Append("</a>");
            return sb.ToString();
        }

        private void AssertUnverifiedDetails(IOrganisation organisation)
        {
            // Organisation details

            Assert.AreEqual(_nameTextBox.Text, organisation.Name);
            AssertPageContains("Not verified");
            AssertPageDoesNotContain("Verified by");

            // Verified-only stuff is not visible

            Assert.IsFalse(_parentFullNameTextBox.IsVisible);
            Assert.IsFalse(_accountManagerIdDropDownList.IsVisible);
            Assert.IsFalse(_firstNameTextBox.IsVisible);
            Assert.IsFalse(_lastNameTextBox.IsVisible);
            Assert.IsFalse(_emailAddressTextBox.IsVisible);
        }

        private void AssertVerifiedDetails(Organisation organisation, IRegisteredUser accountManager, ICommunicationRecipient verifiedBy)
        {
            Assert.AreEqual(organisation.ParentFullName ?? "", _parentFullNameTextBox.Text);
            Assert.AreEqual(organisation.Name, _nameTextBox.Text);
            Assert.AreEqual(organisation.Address == null || organisation.Address.Location == null ? string.Empty : organisation.Address.Location.ToString(), _locationTextBox.Text);

            Assert.AreEqual(accountManager.FullName + (accountManager.IsEnabled ? "" : " (disabled)"), _accountManagerIdDropDownList.SelectedItem.Text);
            AssertPageContains("Verified by " + verifiedBy.FullName);
        }

        private static void AssertOrganisation(Organisation expected, Organisation organisation)
        {
            Assert.AreEqual(expected.Id, organisation.Id);
            Assert.AreEqual(expected.Name, organisation.Name);
            Assert.AreEqual(expected.FullName, organisation.FullName);
            Assert.AreEqual(expected.ParentId, organisation.ParentId);
            Assert.AreEqual(expected.ParentFullName, organisation.ParentFullName);
            Assert.AreEqual(expected.IsVerified, organisation.IsVerified);
            Assert.AreEqual(expected.AffiliateId, organisation.AffiliateId);

            if (expected.IsVerified)
                AssertVerifiedOrganisation(expected as VerifiedOrganisation, organisation as VerifiedOrganisation);
        }

        private static void AssertVerifiedOrganisation(VerifiedOrganisation expected, VerifiedOrganisation organisation)
        {
            Assert.AreEqual(expected.AccountManagerId, organisation.AccountManagerId);
            Assert.AreEqual(expected.VerifiedById, organisation.VerifiedById);
            if (expected.ContactDetails == null)
            {
                Assert.IsNull(organisation.ContactDetails);
            }
            else
            {
                Assert.IsNotNull(organisation.ContactDetails);
                Assert.AreEqual(expected.ContactDetails.EmailAddress, organisation.ContactDetails.EmailAddress);
                Assert.AreEqual(expected.ContactDetails.FaxNumber, organisation.ContactDetails.FaxNumber);
                Assert.AreEqual(expected.ContactDetails.FirstName, organisation.ContactDetails.FirstName);
                Assert.AreEqual(expected.ContactDetails.LastName, organisation.ContactDetails.LastName);
                Assert.AreEqual(expected.ContactDetails.PhoneNumber, organisation.ContactDetails.PhoneNumber);
                Assert.AreEqual(expected.ContactDetails.SecondaryEmailAddresses, organisation.ContactDetails.SecondaryEmailAddresses);
            }
        }
    }
}
