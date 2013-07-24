using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Users.Custodians.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Administrators.Models.Custodians;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Custodians
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class MaintainCustodiansController
        : AdministratorsController
    {
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ICustodiansQuery _custodiansQuery;
        private readonly ICommunitiesQuery _communitiesQuery;

        public MaintainCustodiansController(IUserAccountsCommand userAccountsCommand, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, ICustodiansQuery custodiansQuery, ICommunitiesQuery communitiesQuery)
        {
            _userAccountsCommand = userAccountsCommand;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _custodiansQuery = custodiansQuery;
            _communitiesQuery = communitiesQuery;
        }

        public ActionResult Edit(Guid id)
        {
            var custodian = _custodiansQuery.GetCustodian(id);
            if (custodian == null)
                return NotFound("custodian", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(custodian.Id);
            if (credentials == null)
                return NotFound("custodian", "id", id);

            return View(new CustodianUserModel
                            {
                                User = custodian,
                                UserLogin = new CustodianLoginModel { LoginId = credentials.LoginId },
                                Community = _communitiesQuery.GetCommunity(custodian.AffiliateId.Value),
                            });
        }

        [HttpPost, ButtonClicked("Enable")]
        public ActionResult Enable(Guid id)
        {
            var custodian = _custodiansQuery.GetCustodian(id);
            if (custodian == null)
                return NotFound("custodian", "id", id);

            _userAccountsCommand.EnableUserAccount(custodian, User.Id().Value);
            return RedirectToRoute(CustodiansRoutes.Edit, new { id });
        }

        [HttpPost, ActionName("Enable"), ButtonClicked("Disable")]
        public ActionResult Disable(Guid id)
        {
            var custodian = _custodiansQuery.GetCustodian(id);
            if (custodian == null)
                return NotFound("custodian", "id", id);

            _userAccountsCommand.DisableUserAccount(custodian, User.Id().Value);
            return RedirectToRoute(CustodiansRoutes.Edit, new { id });
        }

        [HttpPost]
        public ActionResult ChangePassword(Guid id, CustodianLoginModel custodianLogin)
        {
            var custodian = _custodiansQuery.GetCustodian(id);
            if (custodian == null)
                return NotFound("custodian", "id", id);

            var credentials = _loginCredentialsQuery.GetCredentials(custodian.Id);
            if (credentials == null)
                return NotFound("custodian", "id", id);

            try
            {
                // Validate.

                custodianLogin.Validate();

                // Update.

                credentials.PasswordHash = LoginCredentials.HashToString(custodianLogin.Password);
                _loginCredentialsCommand.UpdateCredentials(custodian.Id, credentials, User.Id().Value);
                const string message = "The password has been reset.";

                return RedirectToRouteWithConfirmation(CustodiansRoutes.Edit, new { id }, message);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            custodianLogin.LoginId = credentials.LoginId;
            return View("Edit", new CustodianUserModel
                                    {
                                        User = _custodiansQuery.GetCustodian(id),
                                        UserLogin = custodianLogin,
                                        Community = _communitiesQuery.GetCommunity(custodian.AffiliateId.Value),
                                    });
        }
    }
}