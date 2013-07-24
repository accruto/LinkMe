using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Api.Areas.Employers.Models.Accounts;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Accounts
{
    public class AccountsApiController
        : ApiController
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;

        public AccountsApiController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IEmployerCreditsQuery employerCreditsQuery)
        {
            _accountsManager = accountsManager;
            _loginCredentialsQuery = loginCredentialsQuery;
            _employerCreditsQuery = employerCreditsQuery;
        }

        [HttpPost]
        public ActionResult Join(EmployerJoinModel joinModel)
        {
            try
            {
                joinModel.Validate();

                // Try to join.

                var account = new EmployerAccount
                {
                    FirstName = joinModel.FirstName,
                    LastName = joinModel.LastName,
                    EmailAddress = joinModel.EmailAddress,
                    Location = joinModel.Location,
                    OrganisationName = joinModel.OrganisationName,
                    PhoneNumber = joinModel.PhoneNumber,
                    SubRole = joinModel.SubRole,
                };

                var credentials = new AccountLoginCredentials
                {
                    LoginId = joinModel.LoginId,
                    Password = joinModel.Password,
                    ConfirmPassword = joinModel.Password,
                };

                _accountsManager.Join(HttpContext, account, credentials);
                return Json(new JsonResponseModel());
            }
            catch (DuplicateUserException ex)
            {
                ModelState.AddModelError("LoginId", ex.Message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer, true), HttpGet]
        public new ActionResult Profile()
        {
            var employer = CurrentEmployer;
            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);

            return Json(new ProfileModel
            {
                Id = employer.Id,
                LoginId = _loginCredentialsQuery.GetLoginId(employer.Id),
                EmailAddress = employer.EmailAddress.Address,
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                OrganisationName = employer.Organisation == null ? string.Empty : employer.Organisation.Name,
                Location = employer.Organisation == null || employer.Organisation.Address == null || employer.Organisation.Address.Location == null
                    ? null
                    : employer.Organisation.Address.Location.ToString(),
                PhoneNumber = employer.PhoneNumber.Number,
                Credits = allocation == null
                    ? 0
                    : allocation.RemainingQuantity.HasValue
                        ? allocation.RemainingQuantity.Value
                        : -1
            }, JsonRequestBehavior.AllowGet);
        }
    }
}