using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain;
using LinkMe.Domain.Location.Queries;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Employers.Controllers.Candidates
{
    public class BrowseCandidatesController
        : EmployersController
    {
        private readonly ILocationQuery _locationQuery;

        public BrowseCandidatesController(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
        }

        public ActionResult Candidates()
        {
            var country = ActivityContext.Location.Country;

            return View(new CandidatesModel
            {
                Country = country,
                CountrySubdivisions = _locationQuery.GetCountrySubdivisions(country).Where(s => !s.IsCountry).ToList(),
                Regions = _locationQuery.GetRegions(country),
                SalaryBands = GetSalaryBands(),
            });
        }

        [EnsureHttp]
        public ActionResult SalaryBandCandidates(string salarySegment)
        {
            var salary = salarySegment.GetSalaryByUrlSegment(CandidatesRoutes.SegmentSuffix);
            if (salary == null)
                return RedirectToUrl(CandidatesRoutes.BrowseCandidates.GenerateUrl(), true);

            // Check the url.

            var result = EnsureUrl(salary.GenerateCandidatesUrl());
            if (result != null)
                return result;

            var country = ActivityContext.Location.Country;
            return View(new LocationsCandidatesModel
            {
                SalaryBand = salary,
                CountrySubdivisions = _locationQuery.GetCountrySubdivisions(country).Where(s => !s.IsCountry).ToList(),
                Regions = _locationQuery.GetRegions(country),
            });
        }

        [EnsureHttp]
        public ActionResult LocationCandidates(string locationSegment)
        {
            var location = _locationQuery.GetLocationByUrlSegment(ActivityContext.Location.Country, locationSegment, CandidatesRoutes.SegmentSuffix);
            if (location == null)
                return RedirectToUrl(CandidatesRoutes.BrowseCandidates.GenerateUrl(), true);

            // Check the url.

            var result = EnsureUrl(location.GenerateCandidatesUrl());
            if (result != null)
                return result;

            // Return the view for this location.

            return View(new SalaryBandsCandidatesModel
            {
                Location = location,
                SalaryBands = GetSalaryBands(),
            });
        }

        private static IList<Salary> GetSalaryBands()
        {
            var bands = new List<Salary> { new Salary { UpperBound = 30000 } };

            for (var i = 30000; i < 200000; i += 10000)
            {
                bands.Add(new Salary { LowerBound = i, UpperBound = i + 10000 });
            }

            bands.Add(new Salary { LowerBound = 200000 });
            return bands;
        }
    }
}
