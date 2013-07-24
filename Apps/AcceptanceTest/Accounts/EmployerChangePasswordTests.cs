using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class EmployerChangePasswordTests
        : ChangePasswordTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IProfilesQuery _profilesQuery = Resolve<IProfilesQuery>();

        private ReadOnlyUrl _settingsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");
        }

        [TestMethod]
        public void TestChangePasswordWithOtherConfirmation()
        {
            var user = CreateUser();
            LogIn(user);

            // Set state so the user gets the terms reminder when they get redirected back to the home page.

            var profile = _profilesQuery.GetEmployerProfile(user.Id);
            Assert.IsNull(profile);
            /* Turned off at the moment.
            state.UpdatedTermsReminder.Hide = false;
            state.UpdatedTermsReminder.FirstShownTime = null;
            _stateCommand.UpdateEmployerState(user.Id, state);
            */

            Get(_changePasswordUrl);
            _passwordTextBox.Text = Password;
            _newPasswordTextBox.Text = NewPassword;
            _confirmNewPasswordTextBox.Text = NewPassword;
            _saveButton.Click();

            // Check.

            AssertUrlWithoutQuery(GetHomeUrl());
            AssertNoErrorMessages();
            AssertConfirmationMessages("Your password has been changed.");
            // AssertConfirmationMessages("Your password has been changed.", "We've made some changes to our terms and conditions. You can review them here.");

            // Try to login with the old password.

            LogOut();
            LogIn(user);
            AssertNotLoggedIn();

            LogIn(user, NewPassword);
            AssertUrl(GetHomeUrl());
        }

        protected override RegisteredUser CreateUser()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected override ReadOnlyUrl GetHomeUrl()
        {
            return LoggedInEmployerHomeUrl;
        }

        protected override ReadOnlyUrl GetCancelUrl()
        {
            return _settingsUrl;
        }
    }
}