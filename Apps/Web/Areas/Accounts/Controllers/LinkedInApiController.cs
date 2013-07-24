using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.LinkedIn.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Accounts.Models;

namespace LinkMe.Web.Areas.Accounts.Controllers
{
    public class LinkedInApiController
        : ApiController
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ILinkedInAuthenticationCommand _linkedInAuthenticationCommand;
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly ILinkedInCommand _linkedInCommand;
        private readonly ILinkedInQuery _linkedInQuery;

        public LinkedInApiController(IAuthenticationManager authenticationManager, ILinkedInAuthenticationCommand linkedInAuthenticationCommand, IUserAccountsCommand userAccountsCommand, ILinkedInCommand linkedInCommand, ILinkedInQuery linkedInQuery)
        {
            _authenticationManager = authenticationManager;
            _linkedInAuthenticationCommand = linkedInAuthenticationCommand;
            _userAccountsCommand = userAccountsCommand;
            _linkedInCommand = linkedInCommand;
            _linkedInQuery = linkedInQuery;
        }

        [EnsureHttps, HttpPost]
        public ActionResult LogIn(LinkedInApiProfile profile)
        {
            try
            {
                // If the user is already logged in then associate the profile with the user.

                if (CurrentRegisteredUser != null)
                {
                    // Only support employers at the moment.

                    if (CurrentEmployer == null)
                        return Json(new LinkedInAuthenticationModel { Status = AuthenticationStatus.Failed.ToString() });

                    var linkedInProfile = new LinkedInProfile { Id = profile.Id, UserId = CurrentEmployer.Id };
                    _linkedInCommand.UpdateProfile(linkedInProfile);

                    return Json(new LinkedInAuthenticationModel { Status = AuthenticationStatus.Authenticated.ToString() });
                }
                else
                {
                    if (LogIn(profile.Id))
                        return Json(new LinkedInAuthenticationModel { Status = AuthenticationStatus.Authenticated.ToString() });

                    // Save the profile.

                    var linkedInProfile = CreateLinkedInProfile(profile);
                    _linkedInCommand.UpdateProfile(linkedInProfile);

                    return Json(new LinkedInAuthenticationModel { Status = AuthenticationStatus.Failed.ToString() });
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        private LinkedInProfile CreateLinkedInProfile(LinkedInApiProfile profile)
        {
            // Use the id of the current anonymous user as the UserId.

            return new LinkedInProfile
            {
                Id = profile.Id,
                UserId = CurrentAnonymousUser.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                OrganisationName = profile.OrganisationName,
                Industries = _linkedInQuery.GetIndustries(profile.Industry),
                Location = _linkedInQuery.GetLocation(profile.Country, profile.Location),
            };
        }

        private bool LogIn(string linkedInProfileId)
        {
            var result = _linkedInAuthenticationCommand.AuthenticateUser(linkedInProfileId);

            switch (result.Status)
            {
                case AuthenticationStatus.Authenticated:
                    _authenticationManager.LogIn(HttpContext, result.User, AuthenticationStatus.Authenticated);
                    return true;
                    
                case AuthenticationStatus.Deactivated:

                    // If they are logging in with their LinkedIn credentials assume they are activated.

                    _userAccountsCommand.ActivateUserAccount(result.User, result.User.Id);
                    _authenticationManager.LogIn(HttpContext, result.User, AuthenticationStatus.Authenticated);
                    return true;

                default:
                    return false;
            }
        }
    }
}