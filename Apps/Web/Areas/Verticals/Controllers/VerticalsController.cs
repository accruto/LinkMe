using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Errors.Controllers;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Areas.Verticals.Models;
using LinkMe.Web.Areas.Verticals.Routes;

namespace LinkMe.Web.Areas.Verticals.Controllers
{
    public class VerticalsController
        : ViewController
    {
        private readonly IVerticalsCommand _verticalsCommand;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IWebSiteQuery _webSiteQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IMemberAccountsCommand _memberAccountsCommand;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;

        public VerticalsController(IVerticalsCommand verticalsCommand, IVerticalsQuery verticalsQuery, ICommunitiesQuery communitiesQuery, IWebSiteQuery webSiteQuery, IMembersQuery membersQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, IMemberAccountsCommand memberAccountsCommand, IAccountVerificationsCommand accountVerificationsCommand)
        {
            _verticalsCommand = verticalsCommand;
            _verticalsQuery = verticalsQuery;
            _communitiesQuery = communitiesQuery;
            _webSiteQuery = webSiteQuery;
            _membersQuery = membersQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _memberAccountsCommand = memberAccountsCommand;
            _accountVerificationsCommand = accountVerificationsCommand;
        }

        public ActionResult Home(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, Public.Routes.HomeRoutes.Home);
        }

        public ActionResult Join(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, JoinRoutes.Join);
        }

        public ActionResult LogIn(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, LoginsRoutes.LogIn);
        }

        public ActionResult EmployerHome(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, Employers.Routes.HomeRoutes.Home);
        }

        public ActionResult EmployerJoin(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, AccountsRoutes.Join);
        }

        public ActionResult EmployerLogin(string verticalUrl, string verticalUrl2)
        {
            return RedirectToNonVerticalRoute(verticalUrl, verticalUrl2, AccountsRoutes.LogIn);
        }

        public ActionResult Convert(string verticalUrl)
        {
            var community = GetCommunity(verticalUrl);
            if (community == null)
                return RedirectToRoute(Public.Routes.HomeRoutes.Home);

            return View(new ConvertModel { Community = community });
        }

        [HttpPost]
        public ActionResult Convert(string verticalUrl, ConvertModel convertModel)
        {
            var community = GetCommunity(verticalUrl);
            if (community == null)
                return RedirectToRoute(Public.Routes.HomeRoutes.Home);
            convertModel.Community = community;

            try
            {
                convertModel.Prepare();
                convertModel.Validate();

                // Find the member.

                var member = _membersQuery.GetMember(convertModel.EmailAddress);
                if (member == null)
                    throw new ValidationErrorsException(new NotFoundValidationError("Account", null));

                // Must be a member of the community and the details must match.

                if (!MatchAccount(member, community, convertModel))
                    throw new ValidationErrorsException(new NotFoundValidationError("Account", null));

                // Create the credentials.

                var credentials = new LoginCredentials
                {
                    LoginId = convertModel.NewEmailAddress,
                    PasswordHash = LoginCredentials.HashToString(convertModel.Password),
                };
                _memberAccountsCommand.CreateCredentials(member, credentials);

                // Send an email if needed.

                if (!member.IsActivated)
                    _accountVerificationsCommand.SendActivation(member, member.GetPrimaryEmailAddress().Address);

                return RedirectToRoute(VerticalsRoutes.Converted);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new VerticalsErrorHandler());
            }

            return View(convertModel);
        }

        public ActionResult Converted(string verticalUrl)
        {
            var community = GetCommunity(verticalUrl);
            if (community == null)
                return RedirectToRoute(Public.Routes.HomeRoutes.Home);

            return View(new ConvertedModel { Community = community });
        }

        public ActionResult Reset()
        {
            // Reset the context.

            ActivityContext.Reset();

            // Redirect home.

            return RedirectToRoute(Public.Routes.HomeRoutes.Home);
        }

        private bool MatchAccount(IRegisteredUser member, Community community, ConvertModel convertModel)
        {
            // Must be an exact match.

            if (member.AffiliateId != community.Id
                || member.FirstName != convertModel.FirstName
                || member.LastName != convertModel.LastName)
                return false;

            // Job title and company must be part of the resume titles and companies.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            if (candidate == null || candidate.ResumeId == null)
                return false;

            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            if (resume == null || resume.Jobs == null || resume.Jobs.Count == 0)
                return false;

            return resume.Jobs.Any(j => !string.IsNullOrEmpty(j.Title) && j.Title.Contains(convertModel.JobTitle))
                && resume.Jobs.Any(j => !string.IsNullOrEmpty(j.Company) && j.Company.Contains(convertModel.JobCompany));
        }

        private Community GetCommunity(string verticalUrl)
        {
            // Ensure this is a vertical that an account can be reclaimed from.

            var vertical = _verticalsQuery.GetVerticalByUrl(verticalUrl);
            if (vertical == null || !vertical.RequiresExternalLogin)
                return null;

            return _communitiesQuery.GetCommunity(vertical.Id);
        }

        private ActionResult RedirectToNonVerticalRoute(string verticalUrl, string verticalUrl2, RouteReference route)
        {
            try
            {
                // Set the current context based upon the URL parameter.

                var vertical = string.IsNullOrEmpty(verticalUrl2)
                    ? _verticalsCommand.GetVerticalByUrl(verticalUrl)
                    : _verticalsCommand.GetVerticalByUrl(verticalUrl + "/" + verticalUrl2);

                if (vertical != null)
                {
                    // If it is not deleted then set everything up for the vertical, else simply redirect to the non-vertical page.

                    if (!vertical.IsDeleted)
                    {
                        if (!string.IsNullOrEmpty(vertical.Host))
                        {
                            // Redirect to the vertical domain.

                            var redirectUrl = route.GenerateUrl();
                            var redirectPermanentlyUrl = _webSiteQuery.GetUrl(WebSite.LinkMe, vertical.Id, redirectUrl.Scheme == "https", "~/".AddUrlSegments(redirectUrl.AppRelativePath));
                            return RedirectToUrl(redirectPermanentlyUrl, true);
                        }

                        ActivityContext.Set(vertical);
                    }

                    // Redirect to the associated web site page.  For SEO purposes
                    // make this a permanent redirection.

                    return RedirectToUrl(route.GenerateUrl(), true);
                }
            }
            catch (Exception)
            {
                // Ignore.
            }

            // Not found. Since this is somewhat of a catch all return a "not found" result.

            HandleNotFoundError();
            return null;
        }

        private void HandleNotFoundError()
        {
            string requestedUrl = null;
            var clientUrl = HttpContext.GetClientUrl();
            if (clientUrl != null)
                requestedUrl = clientUrl.PathAndQuery;
            var referrerUrl = Request.Headers["Referer"];

            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            if (HttpContext.Request.AcceptTypes != null && HttpContext.Request.AcceptTypes.Contains(ContentTypes.Json))
                HttpContext.ExecuteController<ErrorsApiController, string>(c => c.NotFound, requestedUrl);
            else
                HttpContext.ExecuteController<ErrorsController, string, string>(c => c.NotFound, requestedUrl, referrerUrl);
        }
    }
}
