using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Affiliations.Commands;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Employers.Controllers.Enquiries
{
    public class EnquiriesController
        : EmployersController
    {
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IOrganisationAffiliationsCommand _organisationAffiliationsCommand;

        public EnquiriesController(ICommunitiesQuery communitiesQuery, IOrganisationAffiliationsCommand organisationAffiliationsCommand)
        {
            _communitiesQuery = communitiesQuery;
            _organisationAffiliationsCommand = organisationAffiliationsCommand;
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Apply()
        {
            return View(new AffiliationEnquiry());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Apply([Bind(Include = "CompanyName,EmailAddress,FirstName,LastName,JobTitle,PhoneNumber")] AffiliationEnquiry enquiry)
        {
            try
            {
                // Create a new enquiry.

                var community = _communitiesQuery.GetCommunity("Monash University Business and Economics");
                _organisationAffiliationsCommand.CreateEnquiry(community.Id, enquiry);

                // Move to the next page.

                return RedirectToRoute(EnquiriesRoutes.Confirm);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the user the errors.

            return View(enquiry);
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}