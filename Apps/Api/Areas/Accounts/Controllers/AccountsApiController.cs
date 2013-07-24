using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Api.Areas.Accounts.Models;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Devices.Apple;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Api.Areas.Accounts.Controllers
{
    public class AccountsApiController
        : ApiController
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IAppleDevicesQuery _appleDevicesQuery;
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery;

        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly IAppleDevicesCommand _appleDevicesCommand;

        public AccountsApiController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IEmployersQuery employersQuery, IAppleDevicesQuery appleDevicesQuery, IMemberSearchAlertsQuery memberSearchAlertsQuery, ILoginAuthenticationCommand loginAuthenticationCommand, ILoginCredentialsCommand loginCredentialsCommand, IAppleDevicesCommand appleDevicesCommand)
        {
            _accountsManager = accountsManager;
            _loginCredentialsQuery = loginCredentialsQuery;
            _employersQuery = employersQuery;
            _appleDevicesQuery = appleDevicesQuery;
            _memberSearchAlertsQuery = memberSearchAlertsQuery;

            _loginAuthenticationCommand = loginAuthenticationCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _appleDevicesCommand = appleDevicesCommand;
        }

        public ActionResult LogIn(LoginModel loginModel)
        {
            try
            {
                var login = loginModel == null ? new Login() : new Login { LoginId = loginModel.LoginId, Password = loginModel.Password };
                login.RememberMe = true;
                
                var result = _accountsManager.LogIn(HttpContext, login);

                AuthenticateUser(result);

                switch (result.Status)
                {
                    case AuthenticationStatus.AuthenticatedMustChangePassword:
                        throw new UserMustChangePasswordException();

                    case AuthenticationStatus.Disabled:
                        throw new UserDisabledException();

                    case AuthenticationStatus.Failed:
                        throw new AuthenticationFailedException();

                    default:
                        return Json(new JsonConfirmationModel { Id = result.User.Id });
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer)]
        public ActionResult LogOut()
        {
            try
            {
                _accountsManager.LogOut(HttpContext);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, HttpPut]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                // Make sure everything is in order.

                changePassword.Validate();

                // Check the passed-in credentials.

                var credentials = new LoginCredentials { LoginId = changePassword.LoginId, Password = changePassword.Password };

                var result = _loginAuthenticationCommand.AuthenticateUser(credentials);
                if (result.Status == AuthenticationStatus.Failed)
                    throw new AuthenticationFailedException();

                // Check that the password has been changed.

                if (changePassword.Password == changePassword.NewPassword)
                    throw new ValidationErrorsException(new NotChangedValidationError("Password", ""));

                // Change it.

                _loginCredentialsCommand.ChangePassword(result.User.Id, credentials, changePassword.NewPassword);

            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPut]
        public ActionResult RegisterDevice(string deviceToken)
        {
            try
            {
                // strip spaces and brackets
                var deviceTokenToStore = deviceToken.Replace(" ", string.Empty).Replace("<", string.Empty).Replace(">",
                    string.Empty);

                var employer = CurrentEmployer;
                var devices = _appleDevicesQuery.GetDevices(employer.Id);

                if (devices == null || devices.Count == 0 || !devices.Select(s => s.DeviceToken).Contains(deviceTokenToStore))
                {
                    var newDevice = new AppleDevice
                    {
                        OwnerId = employer.Id,
                        Active = true,
                        DeviceToken = deviceTokenToStore,
                    };

                    _appleDevicesCommand.CreateDevice(newDevice);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer, true), HttpGet]
        public ActionResult UnviewedAlertedCandidates()
        {
            try
            {
                var unviewedCandidates = _memberSearchAlertsQuery.GetUnviewedCandidates(CurrentEmployer.Id);

                var results = unviewedCandidates.Select(c => c.SavedResumeSearchAlertId).Distinct().Select(i =>
                    new SavedSearchResultsModel
                        {
                            SavedSearchId =
                                _memberSearchAlertsQuery.GetMemberSearchAlert(i).MemberSearchId,
                            CandidateIds =
                                (from u in unviewedCandidates
                                where u.SavedResumeSearchAlertId == i
                                select u.CandidateId).ToList(),
                        }).ToList();

                var model = new SavedSearchResultsResponseModel
                                {
                                    TotalSearches = results.Count(),
                                    Searches = results,
                                };
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        public ActionResult NewPassword(string emailAddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailAddress))
                    ModelState.AddModelError("You must supply an email address to send the new password to");

                var employers = _employersQuery.GetEmployers(emailAddress);

                if (employers == null || employers.Count == 0)
                {
                    ModelState.AddModelError("The user cannot be found. Please try again.");
                }
                else if (employers.Count == 1)
                {
                    // Now reset the password.
                    var employer = employers[0];

                    var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
                    _loginCredentialsCommand.ResetPassword(employer.Id, credentials);
                }
                else if (employers.Count > 1)
                {
                    ModelState.AddModelError(string.Format("There is more than one user with the specified email address. Please reset your password on the website"));
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private void AuthenticateUser(AuthenticationResult result)
        {
            // Only allow employers.

            if (result.User != null && result.User.UserType != UserType.Employer)
            {
                _accountsManager.LogOut(HttpContext);
                throw new AuthenticationFailedException();
            }
        }
    }
}