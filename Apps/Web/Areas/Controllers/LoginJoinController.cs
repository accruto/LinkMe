using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;
using AccountsRoutes=LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes;
using EmployerJoin = LinkMe.Web.Models.Accounts.EmployerJoin;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Controllers
{
    public abstract class LoginJoinController
        : WebViewController
    {
        private static readonly ReadOnlyUrl JoinUrl = JoinRoutes.Join.GenerateUrl();

        private readonly IAccountsManager _accountsManager;
        protected readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IFaqsQuery _faqsQuery;

        private static readonly Guid DisableFaqId = new Guid("7B7FAD42-E027-4586-843B-4D422F39E7EA");

        private AnonymousUserContext _context;

        protected AnonymousUserContext AnonymousUserContext
        {
            get { return _context ?? (_context = new AnonymousUserContext(HttpContext)); }
        }

        protected LoginJoinController(IAccountsManager accountsManager, ILoginCredentialsQuery loginCredentialsQuery, IFaqsQuery faqsQuery)
        {
            _accountsManager = accountsManager;
            _loginCredentialsQuery = loginCredentialsQuery;
            _faqsQuery = faqsQuery;
        }

        protected ActionResult TryLogIn(Login login, CheckBoxValue rememberMe, Func<AuthenticationResult, ReadOnlyUrl> getReturnUrl, Func<IErrorHandler> createErrorHandler)
        {
            try
            {
                login = login ?? new Login();
                login.RememberMe = rememberMe != null && rememberMe.IsChecked;
                var result = _accountsManager.LogIn(HttpContext, login);

                // Go to the next page.

                switch (result.Status)
                {
                    case AuthenticationStatus.AuthenticatedMustChangePassword:
                        return RedirectToRoute(AccountsRoutes.MustChangePassword, new RouteValueDictionary(new Dictionary<string, object> { { Apps.Asp.Constants.ReturnUrlParameter, getReturnUrl(result) ?? HttpContext.GetReturnUrl() } }));

                    case AuthenticationStatus.Disabled:

                        var faq = _faqsQuery.GetFaq(DisableFaqId);
                        return RedirectToUrl(faq.GenerateUrl(_faqsQuery.GetCategories()));

                    case AuthenticationStatus.Deactivated:
                        return GetDeactivatedResult(result, getReturnUrl);

                    case AuthenticationStatus.Failed:
                        throw new AuthenticationFailedException();

                    default:
                        return GetAuthenticatedResult(result, getReturnUrl);
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, createErrorHandler());
            }

            return null;
        }

        protected ActionResult TryLogIn(Login login, CheckBoxValue rememberMe)
        {
            return TryLogIn(login, rememberMe, r => HttpContext.GetReturnUrl(), () => new StandardErrorHandler());
        }

        protected ActionResult TryJoin(MemberJoin join, CheckBoxValue acceptTerms, Func<IErrorHandler> createErrorHandler)
        {
            try
            {
                join = join ?? new MemberJoin();

                // Process the post to check validations.

                if (acceptTerms == null || !acceptTerms.IsChecked)
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, createErrorHandler());

                // Try to join.

                if (acceptTerms != null && acceptTerms.IsChecked)
                {
                    var account = new MemberAccount
                    {
                        FirstName = join.FirstName,
                        LastName = join.LastName,
                        EmailAddress = join.EmailAddress,
                    };

                    var credentials = new AccountLoginCredentials
                    {
                        LoginId = join.EmailAddress,
                        Password = join.JoinPassword,
                        ConfirmPassword = join.JoinConfirmPassword,
                    };

                    _accountsManager.Join(HttpContext, account, credentials, true);
                    return RedirectToUrl(JoinUrl);
                }

                // Not accepting terms so cannot proceed but also check whether any other fields fail validation.

                join.Prepare();
                join.Validate();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, createErrorHandler());
            }

            return null;
        }
        
        protected ActionResult TryJoin(MemberJoin join, CheckBoxValue acceptTerms)
        {
            return TryJoin(join, acceptTerms, () => new StandardErrorHandler());
        }

        protected ActionResult TryJoin(EmployerJoin join, CheckBoxValue acceptTerms, Func<IErrorHandler> createErrorHandler)
        {
            try
            {
                join = join ?? new EmployerJoin();

                // Process the post to check validations.

                if (acceptTerms == null || !acceptTerms.IsChecked)
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, createErrorHandler());

                // Try to join.

                if (acceptTerms != null && acceptTerms.IsChecked)
                {
                    var account = new EmployerAccount
                    {
                        FirstName = join.FirstName,
                        LastName = join.LastName,
                        OrganisationName = join.OrganisationName,
                        EmailAddress = join.EmailAddress,
                        PhoneNumber = join.PhoneNumber,
                        SubRole = join.SubRole,
                        Location = join.Location,
                        IndustryIds = join.IndustryIds,
                    };

                    var credentials = new AccountLoginCredentials
                    {
                        LoginId = join.JoinLoginId,
                        Password = join.JoinPassword,
                        ConfirmPassword = join.JoinConfirmPassword,
                    };

                    _accountsManager.Join(HttpContext, account, credentials);
                    return RedirectToReturnUrl();
                }

                // Not accepting terms so cannot proceed but also check whether any other fields fail validation.

                join.Prepare();
                join.Validate();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, createErrorHandler());
            }

            return null;
        }

        protected ActionResult TryJoin(EmployerJoin join, CheckBoxValue acceptTerms)
        {
            return TryJoin(join, acceptTerms, () => new StandardErrorHandler());
        }

        protected void TryLogOut()
        {
            _accountsManager.LogOut(HttpContext);
        }

        protected string GetLoginId()
        {
            // The current user may have been set up elsewhere.
            // If so, use it to populate the login id.

            var requestUserId = AnonymousUserContext.RequestUserId;
            if (requestUserId != null)
            {
                var credentials = _loginCredentialsQuery.GetCredentials(requestUserId.Value);
                if (credentials != null)
                {
                    AnonymousUserContext.RequestUserId = null;
                    return credentials.LoginId;
                }
            }

            return null;
        }

        protected virtual ReadOnlyUrl GetLoggedInMemberUrl(IUser user)
        {
            return HttpContext.GetReturnUrl();
        }

        private ActionResult GetDeactivatedResult(AuthenticationResult result, Func<AuthenticationResult, ReadOnlyUrl> getReturnUrl)
        {
            // Only members are affected.

            if (result.User.UserType == UserType.Member)
                return GetDeactivatedMemberResult();

            var returnUrl = getReturnUrl(result);
            return returnUrl != null ? RedirectToUrl(returnUrl) : RedirectToReturnUrl();
        }

        private ActionResult GetDeactivatedMemberResult()
        {
            return RedirectToRoute(AccountsRoutes.NotActivated);
        }

        private ActionResult GetAuthenticatedResult(AuthenticationResult result, Func<AuthenticationResult, ReadOnlyUrl> getReturnUrl)
        {
            var returnUrl = getReturnUrl(result);

            if (result.User.UserType == UserType.Member)
                return GetAuthenticatedMemberResult(result.User, returnUrl);
            return returnUrl != null ? RedirectToUrl(returnUrl) : RedirectToReturnUrl();
        }

        private ActionResult GetAuthenticatedMemberResult(IUser user, ReadOnlyUrl returnUrl)
        {
            // Need to check whether all email addresses are verified.

            var member = user as IMember;
            if (member != null)
            {
                if (member.EmailAddresses != null && member.EmailAddresses.All(e => !e.IsVerified))
                    return RedirectToRoute(AccountsRoutes.NotVerified, new { returnUrl = (returnUrl ?? HttpContext.GetReturnUrl()).ToString() } );
            }

            return RedirectToUrl(returnUrl ?? GetLoggedInMemberUrl(user));
        }
    }
}
