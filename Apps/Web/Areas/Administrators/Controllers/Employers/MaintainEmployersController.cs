using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.Employers.Files;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Search.Employers;
using LinkMe.Web.Areas.Administrators.Models;
using LinkMe.Web.Areas.Administrators.Models.Employers;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Employers
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class MaintainEmployersController
        : AdministratorsController
    {
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IEmployerAccountsCommand _employerAccountsCommand;
        private readonly IEmployersQuery _employersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IPhoneNumbersQuery _phoneNumbersQuery;
        private readonly IExecuteEmployerSearchCommand _executeEmployerSearchCommand;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IAccountReportsQuery _accountReportsQuery;

        public MaintainEmployersController(IUserAccountsCommand userAccountsCommand, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, IEmployerAccountsCommand employerAccountsCommand, IEmployersQuery employersQuery, IOrganisationsQuery organisationsQuery, IPhoneNumbersQuery phoneNumbersQuery, IExecuteEmployerSearchCommand executeEmployerSearchCommand, IEmailsCommand emailsCommand, IAccountReportsQuery accountReportsQuery)
        {
            _userAccountsCommand = userAccountsCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _employerAccountsCommand = employerAccountsCommand;
            _employersQuery = employersQuery;
            _organisationsQuery = organisationsQuery;
            _phoneNumbersQuery = phoneNumbersQuery;
            _executeEmployerSearchCommand = executeEmployerSearchCommand;
            _emailsCommand = emailsCommand;
            _accountReportsQuery = accountReportsQuery;
        }

        public ActionResult Search()
        {
            return View(new EmployerSearchModel
            {
                Criteria = new AdministrativeEmployerSearchCriteria { IsEnabled = true, IsDisabled = true },
            });
        }

        [HttpPost, ButtonClicked("Search")]
        public ActionResult Search(AdministrativeEmployerSearchCriteria criteria, [Bind(Include = "MatchOrganisationNameExactly")] CheckBoxValue matchOrganisationNameExactly, [Bind(Include = "IsEnabled")] CheckBoxValue isEnabled, [Bind(Include = "IsDisabled")] CheckBoxValue isDisabled)
        {
            // Search.

            criteria.IsEnabled = isEnabled.IsChecked;
            criteria.IsDisabled = isDisabled.IsChecked;
            criteria.MatchOrganisationNameExactly = matchOrganisationNameExactly.IsChecked;
            var employers = _executeEmployerSearchCommand.Search(criteria);

            // Get logins.

            var loginIds = _loginCredentialsQuery.GetLoginIds(from e in employers select e.Id);

            return View(new EmployerSearchModel
            {
                Criteria = criteria,
                Employers = (from e in employers
                             select new EmployerModel
                             {
                                 Employer = e,
                                 LoginId = loginIds.ContainsKey(e.Id) ? loginIds[e.Id] : null
                             }).ToList()});
        }

        [HttpPost, ActionName("Search"), ButtonClicked("Download")]
        public ActionResult Download(AdministrativeEmployerSearchCriteria criteria, [Bind(Include = "MatchOrganisationNameExactly")] CheckBoxValue matchOrganisationNameExactly, [Bind(Include = "IsEnabled")] CheckBoxValue isEnabled, [Bind(Include = "IsDisabled")] CheckBoxValue isDisabled)
        {
            // Search.

            criteria.IsEnabled = isEnabled.IsChecked;
            criteria.IsDisabled = isDisabled.IsChecked;
            criteria.MatchOrganisationNameExactly = matchOrganisationNameExactly.IsChecked;
            var employers = _executeEmployerSearchCommand.Search(criteria);

            // Login ids.

            var loginIds = _loginCredentialsQuery.GetLoginIds(from e in employers select e.Id);

            // Write them out.

            var fileStream = (from e in employers
            select new Tuple<IEmployer, string>(
                e,
                loginIds.ContainsKey(e.Id) ? loginIds[e.Id] : null)).ToFileStream();

            var fileName = FileSystem.GetValidFileName("LinkMeSearchResults.csv");
            return new FileStreamResult(fileStream, MediaType.Csv) { FileDownloadName = fileName };
        }

        public ActionResult Edit(Guid id)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
            if (credentials == null)
                return NotFound("employer", "id", id);

            return View(new UserModel<IEmployer, EmployerLoginModel>
                            {
                                User = employer,
                                UserLogin = new EmployerLoginModel { LoginId = credentials.LoginId },
                            });
        }

        [HttpPost]
        public ActionResult Edit(Guid id, string firstName, string lastName, string emailAddress, string phoneNumber, string organisationName, string loginId)
        {
            Employer employer = null;
            LoginCredentials credentials = null;

            try
            {
                employer = _employersQuery.GetEmployer(id);
                if (employer == null)
                    return NotFound("employer", "id", id);

                credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
                if (credentials == null)
                    return NotFound("employer", "id", id);

                // Update the employer.

                employer.FirstName = firstName;
                employer.LastName = lastName;
                employer.EmailAddress = string.IsNullOrEmpty(emailAddress) ? null : new EmailAddress { Address = emailAddress };
                employer.PhoneNumber = _phoneNumbersQuery.GetPhoneNumber(phoneNumber, ActivityContext.Location.Country);

                // Update the organisation but only for verified organisations.

                if (employer.Organisation.IsVerified && organisationName != employer.Organisation.FullName)
                    employer.Organisation = _organisationsQuery.GetVerifiedOrganisation(organisationName);

                _employerAccountsCommand.UpdateEmployer(employer);

                // Update the credentials.

                credentials.LoginId = loginId;
                _loginCredentialsCommand.UpdateCredentials(employer.Id, credentials, User.Id().Value);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(new UserModel<IEmployer, EmployerLoginModel>
            {
                User = employer,
                UserLogin = new EmployerLoginModel { LoginId = credentials == null ? null : credentials.LoginId },
            });
        }

        [AcceptVerbs(HttpVerbs.Post), ButtonClicked("Enable")]
        public ActionResult Enable(Guid id)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            _userAccountsCommand.EnableUserAccount(employer, User.Id().Value);
            return RedirectToRoute(EmployersRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Disable")]
        public ActionResult Disable(Guid id)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            _userAccountsCommand.DisableUserAccount(employer, User.Id().Value);
            return RedirectToRoute(EmployersRoutes.Edit, new { id });
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, EmployerLoginModel employerLogin, [Bind(Include = "SendPasswordEmail")] CheckBoxValue sendPasswordEmail)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
            if (credentials == null)
                return NotFound("employer", "id", id);

            try
            {
                // Validate.

                employerLogin.SendPasswordEmail = sendPasswordEmail.IsChecked;
                employerLogin.Validate();

                // Update.

                credentials.PasswordHash = LoginCredentials.HashToString(employerLogin.Password);
                credentials.MustChangePassword = true;
                _loginCredentialsCommand.UpdateCredentials(employer.Id, credentials, User.Id().Value);

                string message;
                if (employerLogin.SendPasswordEmail)
                {
                    var members = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now);
                    _emailsCommand.TrySend(new NewEmployerWelcomeEmail(employer, credentials.LoginId, employerLogin.Password, members));
                    message = "The password has been reset and an email has been sent.";
                }
                else
                {
                    message = "The password has been reset.";
                }

                return RedirectToRouteWithConfirmation(EmployersRoutes.Edit, new { id }, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            employerLogin.LoginId = credentials.LoginId;
            return View("Edit", new UserModel<IEmployer, EmployerLoginModel>
                                    {
                                        User = _employersQuery.GetEmployer(id),
                                        UserLogin = employerLogin
                                    });
        }
    }
}