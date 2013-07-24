using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Employers.Models.LinkedIn;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Controllers;
using HomeRoutes = LinkMe.Web.Areas.Employers.Routes.HomeRoutes;

namespace LinkMe.Web.Areas.Employers.Controllers.LinkedIn
{
    [EnsureNotAuthorized]
    public class LinkedInController
        : EmployersController
    {
        private readonly ILinkedInCommand _linkedInCommand;
        private readonly ILinkedInQuery _linkedInQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly IAccountsManager _accountsManager;
        private readonly IFaqsQuery _faqsQuery;

        private static readonly Guid DisableFaqId = new Guid("7B7FAD42-E027-4586-843B-4D422F39E7EA");

        public LinkedInController(IUserAccountsCommand userAccountsCommand, IAccountsManager accountsManager, IFaqsQuery faqsQuery, ILinkedInCommand linkedInCommand, ILinkedInQuery linkedInQuery, IIndustriesQuery industriesQuery)
        {
            _userAccountsCommand = userAccountsCommand;
            _accountsManager = accountsManager;
            _faqsQuery = faqsQuery;
            _linkedInCommand = linkedInCommand;
            _linkedInQuery = linkedInQuery;
            _industriesQuery = industriesQuery;
        }

        public ActionResult Account()
        {
            // Should have been redirected here as a result of a LinkedIn login so check there is a profile.

            var profile = _linkedInQuery.GetProfile(CurrentAnonymousUser.Id);
            if (profile == null)
                return RedirectToRoute(HomeRoutes.Home);

            return View(GetAccountModel(new Login(), profile));
        }

        [EnsureHttps, HttpPost, ActionName("Account"), ButtonClicked("Login")]
        public ActionResult LogIn(Login loginModel, [Bind(Include = "RememberMe")] CheckBoxValue rememberMe)
        {
            var profile = _linkedInQuery.GetProfile(CurrentAnonymousUser.Id);
            if (profile == null)
                return RedirectToRoute(HomeRoutes.Home);

            try
            {
                loginModel = loginModel ?? new Login();
                loginModel.RememberMe = rememberMe != null && rememberMe.IsChecked;
                var result = _accountsManager.LogIn(HttpContext, loginModel);

                // Go to the next page.

                switch (result.Status)
                {
                    case AuthenticationStatus.Disabled:

                        // If they are disabled, simply redirect them.

                        var faq = _faqsQuery.GetFaq(DisableFaqId);
                        return RedirectToUrl(faq.GenerateUrl(_faqsQuery.GetCategories()));

                    case AuthenticationStatus.Failed:
                        throw new AuthenticationFailedException();

                    case AuthenticationStatus.Deactivated:

                        // Activate them.

                        _userAccountsCommand.ActivateUserAccount(result.User, result.User.Id);
                        break;
                }

                // The user has been authenticated so associate them with the LinkedIn profile.

                profile.UserId = result.User.Id;
                _linkedInCommand.UpdateProfile(profile);

                return RedirectToReturnUrl();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Account", GetAccountModel(loginModel, profile));
        }

        [EnsureHttps, HttpPost, ActionName("Account"), ButtonClicked("Join")]
        public ActionResult Join(LinkedInJoin joinModel, [Bind(Include = "AcceptTerms")] CheckBoxValue acceptTerms)
        {
            var profile = _linkedInQuery.GetProfile(CurrentAnonymousUser.Id);
            if (profile == null)
                return RedirectToRoute(HomeRoutes.Home);

            try
            {
                joinModel = joinModel ?? new LinkedInJoin();

                // Process the post to check validations.

                if (acceptTerms == null || !acceptTerms.IsChecked)
                    ModelState.AddModelError(new[] { new TermsValidationError("AcceptTerms") }, new StandardErrorHandler());

                // Try to join.

                if (acceptTerms != null && acceptTerms.IsChecked)
                {
                    var account = new EmployerAccount
                    {
                        FirstName = joinModel.FirstName,
                        LastName = joinModel.LastName,
                        EmailAddress = joinModel.EmailAddress,
                        PhoneNumber = joinModel.PhoneNumber,
                        OrganisationName = joinModel.OrganisationName,
                        Location = joinModel.Location,
                        SubRole = joinModel.SubRole,
                        IndustryIds = joinModel.IndustryIds
                    };

                    _accountsManager.Join(HttpContext, account, profile);
                    return RedirectToReturnUrl();
                }

                // Not accepting terms so cannot proceed but also check whether any other fields fail validation.

                joinModel.Prepare();
                joinModel.Validate();
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Account", GetAccountModel(joinModel));
        }

        private LinkedInAccountModel GetAccountModel(Login login, LinkedInProfile profile)
        {
            return GetAccountModel(
                login,
                new LinkedInJoin
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    IndustryIds = profile.Industries == null ? null : (from i in profile.Industries select i.Id).ToList(),
                    Location = profile.Location == null ? null : profile.Location.ToString(),
                    OrganisationName = profile.OrganisationName,
                },
                false);
        }

        private LinkedInAccountModel GetAccountModel(LinkedInJoin join)
        {
            return GetAccountModel(
                new Login(),
                join,
                false);
        }

        private LinkedInAccountModel GetAccountModel(Login login, LinkedInJoin join, bool acceptTerms)
        {
            return new LinkedInAccountModel
            {
                Login = login ?? new Login(),
                Join = join ?? new LinkedInJoin(),
                AcceptTerms = acceptTerms,
                Industries = _industriesQuery.GetIndustries()
            };
        }
    }
}