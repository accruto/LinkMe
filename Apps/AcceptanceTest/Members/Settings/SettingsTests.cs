using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public abstract class SettingsTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        protected ReadOnlyUrl _settingsUrl;
        protected ReadOnlyUrl _changePasswordUrl;
        protected ReadOnlyUrl _deactivateUrl;

        protected HtmlTextBoxTester _emailAddressTextBox;
        protected HtmlTextBoxTester _secondaryEmailAddressTextBox;
        protected HtmlTextBoxTester _firstNameTextBox;
        protected HtmlTextBoxTester _lastNameTextBox;
        protected HtmlButtonTester _saveButton;

        [TestInitialize]
        public void SettingsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/members/settings");
            _changePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changepassword");
            _deactivateUrl = new ReadOnlyApplicationUrl(true, "~/members/settings/deactivate");

            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _secondaryEmailAddressTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddress");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }
    }
}