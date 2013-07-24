using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Accounts.Models;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.UI.Registered.Employers;
using MemberSettingsRoutes = LinkMe.Web.Areas.Members.Routes.SettingsRoutes;
using EmployerSettingsRoutes = LinkMe.Web.Areas.Employers.Routes.SettingsRoutes;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    [EnsureHttps]
    public class SettingsController
        : AccountsLoginJoinController
    {
        private readonly IUsersQuery _usersQuery;
        private readonly ISettingsCommand _settingsCommand;
        private readonly ISettingsQuery _settingsQuery;

        public SettingsController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery, IUsersQuery usersQuery, ISettingsCommand settingsCommand, ISettingsQuery settingsQuery)
            : base(accountsManager, loginCredentialsQuery, faqsQuery)
        {
            _usersQuery = usersQuery;
            _settingsCommand = settingsCommand;
            _settingsQuery = settingsQuery;
        }

        public ActionResult Unsubscribe(UnsubscribeRequestModel requestModel)
        {
            var model = new UnsubscribeModel { Login = new Login() };

            try
            {
                Prepare(requestModel, model);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(model);
        }

        [HttpPost, ButtonClicked("Unsubscribe"), ActionName("Unsubscribe")]
        public ActionResult PostUnsubscribe(UnsubscribeRequestModel requestModel)
        {
            var model = new UnsubscribeModel { Login = new Login() };

            try
            {
                Prepare(requestModel, model);

                // Unsubscribe.

                _settingsCommand.SetFrequency(requestModel.UserId.Value, model.Category.Id, Frequency.Never);
                model.HasUnsubscribed = true;

                ModelState.AddModelConfirmation("You have now been unsubscribed from this type of email.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(model);
        }

        [HttpPost, ButtonClicked("Login")]
        public ActionResult Unsubscribe(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var result = TryLogIn(loginModel, rememberMe, GetReturnUrl, () => new ActivationErrorHandler());
            return result ?? View(new UnsubscribeModel { Login = loginModel ?? new Login() });
        }

        private void Prepare(UnsubscribeRequestModel requestModel, UnsubscribeModel unsubscribeModel)
        {
            requestModel.Prepare();
            requestModel.Validate();

            // Get the user.

            var user = _usersQuery.GetUser(requestModel.UserId.Value);
            if (user == null)
                throw new ValidationErrorsException(new NotFoundValidationError("UserId", requestModel.UserId.Value));

            if (CurrentRegisteredUser == null)
                unsubscribeModel.Login.LoginId = _loginCredentialsQuery.GetLoginId(requestModel.UserId.Value);

            // Get the category.

            unsubscribeModel.Category = _settingsQuery.GetCategory(requestModel.Category);
            if (unsubscribeModel.Category == null)
                throw new ValidationErrorsException(new NotFoundValidationError("Category", requestModel.Category));
        }

        private static ReadOnlyUrl GetReturnUrl(AuthenticationResult result)
        {
            return result.Status == AuthenticationStatus.Authenticated && result.User.UserType == UserType.Employer
                ? EmployerSettingsRoutes.Settings.GenerateUrl()
                : MemberSettingsRoutes.Settings.GenerateUrl();
        }
    }
}
