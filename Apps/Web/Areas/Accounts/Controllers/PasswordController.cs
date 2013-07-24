using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Accounts.Models;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Controllers;
using LinkMe.Web.Mvc;
using MemberSettingsRoutes = LinkMe.Web.Areas.Members.Routes.SettingsRoutes;
using EmployerSettingsRoutes = LinkMe.Web.Areas.Employers.Routes.SettingsRoutes;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public class PasswordController
        : AccountsController
    {
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand;
        private readonly IUsersQuery _usersQuery;
        private readonly IEmployersQuery _employersQuery;

        public PasswordController(ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, ILoginAuthenticationCommand loginAuthenticationCommand, IUsersQuery usersQuery, IEmployersQuery employersQuery)
        {
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _loginAuthenticationCommand = loginAuthenticationCommand;
            _usersQuery = usersQuery;
            _employersQuery = employersQuery;
        }

        [EnsureHttps, EnsureAuthorized, NoExternalLogin]
        public ActionResult MustChangePassword()
        {
            return View("ChangePassword", new ChangePasswordModel { IsAdministrator = CurrentRegisteredUser.UserType == UserType.Administrator, MustChange = true });
        }

        [EnsureHttps, EnsureAuthorized, NoExternalLogin, HttpPost, ButtonClicked("Save")]
        public ActionResult MustChangePassword(ChangePasswordModel changePassword)
        {
            return ChangePassword(true, changePassword);
        }

        [EnsureHttps, EnsureAuthorized, NoExternalLogin]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword", new ChangePasswordModel { IsAdministrator = CurrentRegisteredUser.UserType == UserType.Administrator, MustChange = false });
        }

        [EnsureHttps, EnsureAuthorized, NoExternalLogin, HttpPost, ButtonClicked("Save")]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            return ChangePassword(false, changePassword);
        }

        [EnsureHttps, EnsureAuthorized, NoExternalLogin, HttpPost, ActionName("ChangePassword"), ButtonClicked("Cancel")]
        public ActionResult CancelChangePassword()
        {
            switch (CurrentRegisteredUser.UserType)
            {
                case UserType.Employer:
                    return RedirectToRoute(EmployerSettingsRoutes.Settings);

                case UserType.Member:
                    return RedirectToRoute(MemberSettingsRoutes.Settings);

                default:
                    return RedirectToUrl(HttpContext.GetHomeUrl());
            }
        }

        [EnsureNotAuthorized, NoExternalLogin]
        public ActionResult NewPassword(string userId)
        {
            return View(new NewPasswordModel { LoginId = userId });
        }

        [EnsureNotAuthorized, NoExternalLogin, HttpPost, ButtonClicked("Send")]
        public ActionResult NewPassword(NewPasswordModel newPassword)
        {
            try
            {
                // Make sure everything is in order.

                newPassword.Validate();

                // First look for the login id.

                IRegisteredUser user = null;
                var userId = _loginCredentialsQuery.GetUserId(newPassword.LoginId);
                if (userId != null)
                {
                    user = _usersQuery.GetUser(userId.Value);
                }
                else
                {
                    // Look for an employer treating it as an email address.

                    var employers = _employersQuery.GetEmployers(newPassword.LoginId);
                    if (employers.Count > 1)
                    {
                        ModelState.AddModelError(string.Format("There is more than one user with the specified email address. Please enter one of the usernames or <a href=\"{0}\">contact us</a> for assistance.", SupportRoutes.ContactUs.GenerateUrl()));
                        return View("NewPasswordSent", newPassword);
                    }

                    if (employers.Count == 1)
                        user = employers[0];
                }
                
                if (user == null || user.UserType == UserType.Administrator)
                {
                    ModelState.AddModelError("The user cannot be found. Please try again.");
                }
                else
                {
                    // Now reset the password.

                    var credentials = _loginCredentialsQuery.GetCredentials(user.Id);
                    _loginCredentialsCommand.ResetPassword(user.Id, credentials);

                    return View("NewPasswordSent", newPassword);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(newPassword);
        }

        [EnsureNotAuthorized, NoExternalLogin, HttpPost, ActionName("NewPassword"), ButtonClicked("Done")]
        public ActionResult DoneNewPassword(UserType? userType)
        {
            return userType == null ? RedirectToReturnUrl() : RedirectToReturnUrl(userType.Value);
        }

        [EnsureNotAuthorized, NoExternalLogin, HttpPost, ActionName("NewPassword"), ButtonClicked("Cancel")]
        public ActionResult CancelNewPassword(UserType? userType)
        {
            return userType == null ? RedirectToReturnUrl() : RedirectToReturnUrl(userType.Value);
        }

        private ActionResult ChangePassword(bool mustChange, ChangePasswordModel changePassword)
        {
            changePassword.MustChange = mustChange;
            changePassword.IsAdministrator = CurrentRegisteredUser.UserType == UserType.Administrator;

            try
            {
                // Make sure everything is in order.

                changePassword.Validate();

                // Check the current credentials.

                var userId = CurrentRegisteredUser.Id;
                var loginId = _loginCredentialsQuery.GetLoginId(userId);
                var credentials = new LoginCredentials { LoginId = loginId, Password = changePassword.Password };

                var result = _loginAuthenticationCommand.AuthenticateUser(credentials);
                switch (result.Status)
                {
                    case AuthenticationStatus.Failed:
                        throw new AuthenticationFailedException();
                }

                // Check that the password has been changed.

                if (changePassword.Password == changePassword.NewPassword)
                    throw new ValidationErrorsException(new NotChangedValidationError("Password", ""));

                // Change it.

                _loginCredentialsCommand.ChangePassword(userId, credentials, changePassword.NewPassword);

                // Redirect.

                return RedirectToUrlWithConfirmation(HttpContext.GetReturnUrl(), "Your password has been changed.");
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("ChangePassword", changePassword);
        }
    }
}
