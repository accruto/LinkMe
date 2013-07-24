using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Web.Areas.Administrators.Models.Organisations;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Organisations
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class OrganisationCreditsController
        : AdministratorsController
    {
        private const int DefaultUsageDays = 30;

        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly IAllocationsCommand _allocationsCommand;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery;
        private readonly IOrdersQuery _ordersQuery;

        public OrganisationCreditsController(IOrganisationsQuery organisationsQuery, IEmployersQuery employersQuery, IMembersQuery membersQuery, IJobAdsQuery jobAdsQuery, IAllocationsQuery allocationsQuery, IAllocationsCommand allocationsCommand, ICreditsQuery creditsQuery, IExercisedCreditsQuery exercisedCreditsQuery, IOrdersQuery ordersQuery)
        {
            _organisationsQuery = organisationsQuery;
            _employersQuery = employersQuery;
            _membersQuery = membersQuery;
            _jobAdsQuery = jobAdsQuery;
            _allocationsQuery = allocationsQuery;
            _allocationsCommand = allocationsCommand;
            _creditsQuery = creditsQuery;
            _exercisedCreditsQuery = exercisedCreditsQuery;
            _ordersQuery = ordersQuery;
        }

        public ActionResult Index(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var credits = _creditsQuery.GetCredits();
            return View(new OrganisationCreditsModel
                            {
                                Organisation = organisation,
                                Allocations = new Dictionary<Guid, IList<Allocation>> {{organisation.Id, GetAllocations(organisation.Id)}},
                                Credits = credits,
                                CreditId = credits[0].Id,
                                Orders = _ordersQuery.GetOrders(organisation.Id)
                            });
        }

        [HttpPost, ActionName("Index")]
        public ActionResult PostIndex(Guid id, [Bind(Include="CreditId")] Guid creditId, [Bind(Include="Quantity")] int? quantity, [Bind(Include="ExpiryDate")] DateTime? expiryDate)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            // Allocate.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = creditId, ExpiryDate = expiryDate, InitialQuantity = quantity });

            return RedirectToRoute(OrganisationsRoutes.Credits, new { id });
        }

        public ActionResult Usage(Guid id, [Bind(Include = "StartDate")] DateTime? startDate, [Bind(Include = "EndDate")] DateTime? endDate)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            if (startDate == null)
                startDate = DateTime.Now.Date.AddDays(-1 * DefaultUsageDays);
            if (endDate == null)
                endDate = DateTime.Now.Date.AddDays(1);

            // Get credits exercised by this employer.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCreditsByOwnerId(id, new DateTimeRange(startDate.Value, endDate.Value));

            // Get all associated data.

            return View(new OrganisationExercisedCreditsModel
            {
                Organisation = organisation,
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                ExercisedCredits = exercisedCredits,
                Credits = _creditsQuery.GetCredits().ToDictionary(c => c.Id, c => c),
                Allocations = _allocationsQuery.GetAllocations(exercisedCredits).ToDictionary(a => a.Id, a => a),
                JobAds = _jobAdsQuery.GetJobAds(exercisedCredits),
                Members = _membersQuery.GetMembers(exercisedCredits),
                Employers = _employersQuery.GetEmployers(exercisedCredits),
            });
        }

        public ActionResult AllocationUsage(Guid id, Guid allocationId)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var allocation = _allocationsQuery.GetAllocation(allocationId);
            if (allocation == null)
                return NotFound("allocation", "id", allocationId);

            // Get credits exercised for this allocation.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits(allocationId);

            // Get all associated data.

            var credit = _creditsQuery.GetCredit(allocation.CreditId);

            return View(new OrganisationAllocationExercisedCreditsModel
            {
                Organisation = organisation,
                ExercisedCredits = exercisedCredits,
                Allocations = new Dictionary<Guid, Allocation> { { allocation.Id, allocation } },
                Credits = new Dictionary<Guid, Credit> {{credit.Id, credit}},
                JobAds = _jobAdsQuery.GetJobAds(exercisedCredits),
                Members = _membersQuery.GetMembers(exercisedCredits),
                Employers = _employersQuery.GetEmployers(exercisedCredits),
            });
        }

        private IList<Allocation> GetAllocations(Guid organisationId)
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(organisationId);

            // Put the never expiring credits first.

            return (from a in allocations where a.ExpiryDate == null select a)
                .Concat(from a in allocations where a.ExpiryDate != null orderby a.ExpiryDate descending select a).ToList();
        }
    }
}