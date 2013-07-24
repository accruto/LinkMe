using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Administrators.Models.Employers;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Employers
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class EmployerCreditsController
        : AdministratorsController
    {
        private const int DefaultUsageDays = 30;

        private readonly IEmployersQuery _employersQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly IEmployerAllocationsCommand _employerAllocationsCommand;
        private readonly ICreditsQuery _creditsQuery;
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery;
        private readonly IOrdersQuery _ordersQuery;

        public EmployerCreditsController(IEmployersQuery employersQuery, IMembersQuery membersQuery, IJobAdsQuery jobAdsQuery, IOrganisationsQuery organisationsQuery, IAllocationsQuery allocationsQuery, IEmployerAllocationsCommand employerAllocationsCommand, ICreditsQuery creditsQuery, IExercisedCreditsQuery exercisedCreditsQuery, IOrdersQuery ordersQuery)
        {
            _employersQuery = employersQuery;
            _membersQuery = membersQuery;
            _jobAdsQuery = jobAdsQuery;
            _organisationsQuery = organisationsQuery;
            _allocationsQuery = allocationsQuery;
            _employerAllocationsCommand = employerAllocationsCommand;
            _creditsQuery = creditsQuery;
            _exercisedCreditsQuery = exercisedCreditsQuery;
            _ordersQuery = ordersQuery;
        }

        public ActionResult Index(Guid id)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            var credits = _creditsQuery.GetCredits();
            return View(new EmployerCreditsModel
            {
                Employer = employer,
                Allocations = new Dictionary<Guid, IList<Allocation>> {{employer.Id, GetAllocations(employer.Id)}},
                Credits = credits,
                CreditId = _creditsQuery.GetCredit<ContactCredit>().Id,
                Orders = _ordersQuery.GetOrders(employer.Id)
            });
        }

        [HttpPost]
        public ActionResult Index(Guid id, Guid creditId, int? quantity, DateTime? expiryDate)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            try
            {
                // Allocate.

                _employerAllocationsCommand.CreateAllocation(new Allocation
                {
                    OwnerId = employer.Id,
                    CreditId = creditId,
                    ExpiryDate = expiryDate,
                    InitialQuantity = quantity
                });

                return RedirectToRoute(EmployersRoutes.Credits, new { id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            var credits = _creditsQuery.GetCredits();
            return View(new EmployerCreditsModel
            {
                Employer = employer,
                Allocations = new Dictionary<Guid, IList<Allocation>> { { employer.Id, GetAllocations(employer.Id) } },
                Credits = credits,
                CreditId = creditId,
                Quantity = quantity,
                ExpiryDate = expiryDate,
                Orders = _ordersQuery.GetOrders(employer.Id)
            });
        }

        public ActionResult Usage(Guid id, [Bind(Include = "StartDate")] DateTime? startDate, [Bind(Include = "EndDate")] DateTime? endDate)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            if (startDate == null)
                startDate = DateTime.Now.Date.AddDays(-1 * DefaultUsageDays);
            if (endDate == null)
                endDate = DateTime.Now.Date.AddDays(1);

            // Get credits exercised by this employer.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCreditsByExerciserId(id, new DateTimeRange(startDate.Value, endDate.Value));

            // Get all associated data.

            var allocations = _allocationsQuery.GetAllocations(exercisedCredits);
            var organisations = _organisationsQuery.GetOrganisations((from a in allocations select a.OwnerId).Distinct());

            return View(new EmployerExercisedCreditsModel
            {
                Employer = employer,
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                ExercisedCredits = exercisedCredits,
                Credits = _creditsQuery.GetCredits().ToDictionary(c => c.Id, c => c),
                Allocations = allocations.ToDictionary(a => a.Id, a => a),
                JobAds = _jobAdsQuery.GetJobAds(exercisedCredits),
                Members = _membersQuery.GetMembers(exercisedCredits),
                Organisations = organisations.ToDictionary(o => o.Id, o => o),
            });
        }

        public ActionResult AllocationUsage(Guid id, Guid allocationId)
        {
            var employer = _employersQuery.GetEmployer(id);
            if (employer == null)
                return NotFound("employer", "id", id);

            var allocation = _allocationsQuery.GetAllocation(allocationId);
            if (allocation == null)
                return NotFound("allocation", "id", allocationId);

            // Get credits exercised for this allocation.

            var exercisedCredits = _exercisedCreditsQuery.GetExercisedCredits(allocationId);

            // Get all associated data.

            var credit = _creditsQuery.GetCredit(allocation.CreditId);

            return View(new EmployerAllocationExercisedCreditsModel
            {
                Employer = employer,
                ExercisedCredits = exercisedCredits,
                Allocations = new Dictionary<Guid, Allocation> { { allocation.Id, allocation } },
                Credits = new Dictionary<Guid, Credit> { { credit.Id, credit } },
                JobAds = _jobAdsQuery.GetJobAds(exercisedCredits),
                Members = _membersQuery.GetMembers(exercisedCredits),
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