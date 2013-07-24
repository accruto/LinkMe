using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Users.Custodians.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Administrators.Models.Communities;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Communities
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class CommunitiesController
        : AdministratorsController
    {
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly ICustodianAccountsCommand _custodianAccountsCommand;
        private readonly ICustodiansQuery _custodiansQuery;
        private readonly IVerticalsCommand _verticalsCommand;
        private readonly ILocationQuery _locationQuery;

        public CommunitiesController(ICommunitiesQuery communitiesQuery, ICustodianAccountsCommand custodianAccountsCommand, ICustodiansQuery custodiansQuery, IVerticalsCommand verticalsCommand, ILocationQuery locationQuery)
        {
            _communitiesQuery = communitiesQuery;
            _custodianAccountsCommand = custodianAccountsCommand;
            _custodiansQuery = custodiansQuery;
            _verticalsCommand = verticalsCommand;
            _locationQuery = locationQuery;
        }

        public ActionResult Index()
        {
            return View(_communitiesQuery.GetCommunities());
        }

        public ActionResult Edit(Guid id)
        {
            var community = _communitiesQuery.GetCommunity(id);
            if (community == null)
                return NotFound("community", "id", id);

            var vertical = _verticalsCommand.GetVertical(id);
            if (vertical == null)
                return NotFound("vertical", "id", id);

            return View(new CommunityModel
                            {
                                Community = community,
                                Vertical = vertical,
                                Countries = _locationQuery.GetCountries()
                            });
        }

        public ActionResult Custodians(Guid id)
        {
            var community = _communitiesQuery.GetCommunity(id);
            if (community == null)
                return NotFound("community", "id", id);

            return View(new CustodiansModel
                            {
                                Community = community,
                                Custodians = _custodiansQuery.GetAffiliationCustodians(community.Id)
                            });
        }

        public ActionResult NewCustodian(Guid id)
        {
            var community = _communitiesQuery.GetCommunity(id);
            if (community == null)
                return NotFound("community", "id", id);

            return View(new NewCustodianModel
                            {
                                Community = community,
                                Custodian = new CreateCustodianModel(),
                            });
        }

        [HttpPost, ActionName("NewCustodian"), ButtonClicked("Create")]
        public ActionResult NewCustodian(Guid id, [Bind(Include = "LoginId,Password,EmailAddress,FirstName,LastName")] CreateCustodianModel createCustodian)
        {
            var community = _communitiesQuery.GetCommunity(id);
            if (community == null)
                return NotFound("community", "id", id);

            if (createCustodian == null)
                createCustodian = new CreateCustodianModel();

            try
            {
                // Look for errors.

                createCustodian.Validate();

                // Create the login.

                CreateCustodian(community, createCustodian);

                // Get ready to create another.

                return RedirectToRouteWithConfirmation(CommunitiesRoutes.NewCustodian, new { id }, HttpUtility.HtmlEncode("The account for " + createCustodian.FirstName + " " + createCustodian.LastName + " has been created."));
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View(new NewCustodianModel
                            {
                                Community = community,
                                Custodian = createCustodian
                            });
        }

        [HttpPost, ActionName("NewCustodian"), ButtonClicked("Cancel")]
        public ActionResult CancelNewCustodian(Guid id)
        {
            return RedirectToRoute(CommunitiesRoutes.Custodians, new { id });
        }

        private void CreateCustodian(Community community, CreateCustodianModel model)
        {
            // For now use the old way of doing things.

            var custodian = new Custodian
            {
                EmailAddress = new EmailAddress { Address = model.EmailAddress },
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var credentials = new LoginCredentials
            {
                LoginId = model.LoginId,
                PasswordHash = LoginCredentials.HashToString(model.Password),
            };

            // Create the account.

            _custodianAccountsCommand.CreateCustodian(custodian, credentials, community.Id);
        }
    }
}