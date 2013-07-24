using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class CommunicationsTests
        : OrganisationsTests
    {
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();

        private HtmlCheckBoxTester _employerUpdateCheckBox;
        private HtmlCheckBoxTester _campaignCheckBox;
        private HtmlButtonTester _saveButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();

            _employerUpdateCheckBox = new HtmlCheckBoxTester(Browser, "EmployerUpdate");
            _campaignCheckBox = new HtmlCheckBoxTester(Browser, "Campaign");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }

        [TestMethod]
        public void TestDefault()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1);

            LogIn(administrator);
            Get(GetCommunicationsUrl(organisation));

            AssertCategory("EmployerUpdate", null, _settingsQuery.GetSettings(organisation.Id));
            AssertCategory("Campaign", null, _settingsQuery.GetSettings(organisation.Id));
            AssertCategory("PartnerNotification", null, _settingsQuery.GetSettings(organisation.Id));

            Assert.AreEqual(true, _employerUpdateCheckBox.IsChecked);
            Assert.AreEqual(true, _campaignCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestEmployerUpdate()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1);

            LogIn(administrator);
            var url = GetCommunicationsUrl(organisation);
            Get(url);

            // Turn it off.

            _employerUpdateCheckBox.IsChecked = false;
            _saveButton.Click();

            AssertCategory("EmployerUpdate", Frequency.Never, _settingsQuery.GetSettings(organisation.Id));
            Assert.AreEqual(false, _employerUpdateCheckBox.IsChecked);

            // Turn it on again.

            _employerUpdateCheckBox.IsChecked = true;
            _saveButton.Click();

            AssertCategory("EmployerUpdate", Frequency.Immediately, _settingsQuery.GetSettings(organisation.Id));
            Assert.AreEqual(true, _employerUpdateCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestCampaign()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1);

            LogIn(administrator);
            var url = GetCommunicationsUrl(organisation);
            Get(url);

            // Turn it off.

            _campaignCheckBox.IsChecked = false;
            _saveButton.Click();

            AssertCategory("Campaign", Frequency.Never, _settingsQuery.GetSettings(organisation.Id));
            Assert.AreEqual(false, _campaignCheckBox.IsChecked);

            // Turn it on again.

            _campaignCheckBox.IsChecked = true;
            _saveButton.Click();

            AssertCategory("Campaign", Frequency.Immediately, _settingsQuery.GetSettings(organisation.Id));
            Assert.AreEqual(true, _campaignCheckBox.IsChecked);
        }

        private void AssertCategory(string name, Frequency? expectedFrequency, RecipientSettings settings)
        {
            var category = _settingsQuery.GetCategory(name);
            if (expectedFrequency == null)
            {
                if (settings != null)
                    Assert.IsNull((from c in settings.CategorySettings where c.CategoryId == category.Id select c).SingleOrDefault());
            }
            else
            {
                Assert.IsNotNull(settings);
                var categorySettings = (from c in settings.CategorySettings where c.CategoryId == category.Id select c).SingleOrDefault();
                Assert.IsNotNull(categorySettings);
                Assert.AreEqual(expectedFrequency.Value, categorySettings.Frequency);
            }
        }
    }
}
