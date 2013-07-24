using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Administrators.Models;
using LinkMe.Web.Areas.Administrators.Models.Administrators;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Administrators
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class MaintainAdministratorsController
        : AdministratorsController
    {
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand;
        private readonly IAdministratorsQuery _administratorsQuery;

        public MaintainAdministratorsController(IUserAccountsCommand userAccountsCommand, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IAdministratorAccountsCommand administratorAccountsCommand, IAdministratorsQuery administratorsQuery)
        {
            _userAccountsCommand = userAccountsCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _administratorAccountsCommand = administratorAccountsCommand;
            _administratorsQuery = administratorsQuery;
        }

        public ActionResult Index()
        {
            return View(_administratorsQuery.GetAdministrators());
        }

        public ActionResult Edit(Guid id)
        {
            var administrator = _administratorsQuery.GetAdministrator(id);
            if (administrator == null)
                return NotFound("administrator", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(id);
            if (credentials == null)
                return NotFound("administrator", "id", id);

            return View(new UserModel<Administrator, AdministratorLoginModel>
                            {
                                User = administrator,
                                UserLogin = new AdministratorLoginModel { LoginId = credentials.LoginId }
                            });
        }

        [HttpPost, ButtonClicked("Enable")]
        public ActionResult Enable(Guid id)
        {
            var administrator = _administratorsQuery.GetAdministrator(id);
            if (administrator == null)
                return NotFound("administrator", "id", id);

            _userAccountsCommand.EnableUserAccount(administrator, User.Id().Value);
            return RedirectToRoute(AdministratorsRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Disable")]
        public ActionResult Disable(Guid id)
        {
            var administrator = _administratorsQuery.GetAdministrator(id);
            if (administrator == null)
                return NotFound("administrator", "id", id);

            _userAccountsCommand.DisableUserAccount(administrator, User.Id().Value);
            return RedirectToRoute(AdministratorsRoutes.Edit, new { id });
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, AdministratorLoginModel administratorLogin)
        {
            var administrator = _administratorsQuery.GetAdministrator(id);
            if (administrator == null)
                return NotFound("administrator", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(id);
            if (credentials == null)
                return NotFound("administrator", "id", id);

            try
            {
                // Validate.

                administratorLogin.Validate();

                // Update.

                credentials.PasswordHash = LoginCredentials.HashToString(administratorLogin.Password);
                _loginCredentialsCommand.UpdateCredentials(administrator.Id, credentials, User.Id().Value);
                const string message = "The password has been reset.";

                return RedirectToRouteWithConfirmation(AdministratorsRoutes.Edit, new { id }, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            administratorLogin.LoginId = credentials.LoginId;
            return View("Edit", new UserModel<Administrator, AdministratorLoginModel>
                                    {
                                        User = _administratorsQuery.GetAdministrator(id),
                                        UserLogin = administratorLogin,
                                    });
        }

        public ActionResult New()
        {
            return View(new CreateAdministratorModel());
        }

        [HttpPost, ActionName("New"), ButtonClicked("Create")]
        public ActionResult New([Bind(Include = "LoginId,Password,EmailAddress,FirstName,LastName")] CreateAdministratorModel createAdministrator)
        {
            if (createAdministrator == null)
                createAdministrator = new CreateAdministratorModel();

            try
            {
                // Look for errors.

                createAdministrator.Validate();

                // Create the login.

                CreateAdministrator(createAdministrator);

                // Get ready to create another.

                return RedirectToRouteWithConfirmation(AdministratorsRoutes.New, null, HttpUtility.HtmlEncode("The account for " + createAdministrator.FirstName + " " + createAdministrator.LastName + " has been created."));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(createAdministrator);
        }

        [HttpPost, ActionName("New"), ButtonClicked("Cancel")]
        public ActionResult CancelNew()
        {
            return RedirectToRoute(AdministratorsRoutes.Index);
        }

        private void CreateAdministrator(CreateAdministratorModel model)
        {
            var administrator = new Administrator
            {
                EmailAddress = new EmailAddress { Address = model.EmailAddress, IsVerified = true },
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var credentials = new LoginCredentials
            {
                LoginId = model.LoginId,
                PasswordHash = LoginCredentials.HashToString(model.Password),
            };

            // Create the account.

            _administratorAccountsCommand.CreateAdministrator(administrator, credentials);
        }
    }
}