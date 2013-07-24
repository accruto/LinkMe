using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models.JobAds;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class BrowseJobAdsController
        : MembersController
    {
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILocationQuery _locationQuery;

        public BrowseJobAdsController(IJobAdsQuery jobAdsQuery, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
        {
            _jobAdsQuery = jobAdsQuery;
            _industriesQuery = industriesQuery;
            _locationQuery = locationQuery;
        }

        public ActionResult JobAds()
        {
            var country = ActivityContext.Location.Country;

            return View(new JobAdsModel
            {
                Country = country,
                CountrySubdivisions = _locationQuery.GetCountrySubdivisions(country).Where(s => !s.IsCountry).ToList(),
                Regions = _locationQuery.GetRegions(country),
                Industries = _industriesQuery.GetIndustries(),
            });
        }

        public ActionResult IndustryJobAds(string industrySegment)
        {
            var industry = _industriesQuery.GetIndustryByUrlSegment(industrySegment, JobAdsRoutes.SegmentSuffix);
            if (industry == null)
                return RedirectToUrl(JobAdsRoutes.BrowseJobAds.GenerateUrl(), true);

            // Check the url.

            var result = EnsureUrl(industry.GenerateJobAdsUrl());
            if (result != null)
                return result;

            // Return the view for this industry.

            var country = ActivityContext.Location.Country;
            return View(new LocationsJobAdsModel
            {
                Industry = industry,
                CountrySubdivisions = _locationQuery.GetCountrySubdivisions(country).Where(s => !s.IsCountry).ToList(),
                Regions = _locationQuery.GetRegions(country),
            });
        }

        public ActionResult LocationJobAds(string locationSegment)
        {
            var location = _locationQuery.GetLocationByUrlSegment(ActivityContext.Location.Country, locationSegment, JobAdsRoutes.SegmentSuffix);
            if (location == null)
                return RedirectToUrl(JobAdsRoutes.BrowseJobAds.GenerateUrl(), true);

            // Check the url.

            var result = EnsureUrl(location.GenerateJobAdsUrl());
            if (result != null)
                return result;

            // Return the view for this location.

            return View(new IndustriesJobAdsModel
            {
                Location = location,
                Industries = _industriesQuery.GetIndustries(),
            });
        }

        public ActionResult OldJobAd(string jobAdId)
        {
            if (jobAdId == null)
                return NotFound("job ad", "id", "");

            // If reached here then it is through an old url so redirect to the correct one.

            var id = GetJobAdId(jobAdId);
            return id == null
                ? NotFound("job ad", "id", jobAdId)
                : OldJobAdId(id.Value);
        }

        public ActionResult OldJobAdId(Guid? jobAdId)
        {
            // If there is no job ad then simply redirect.

            if (jobAdId == null)
                return RedirectToUrl(JobAdsRoutes.BrowseJobAds.GenerateUrl(), true);

            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId.Value);
            return jobAd == null
                ? NotFound("job ad", "id", jobAdId)
                : RedirectToUrl(jobAd.GenerateJobAdUrl(), true);
        }

        public ActionResult Search()
        {
            return View(new JobSearchReferenceModel());
        }

        private static Guid? GetJobAdId(string jobAdId)
        {
            // Try the whole thing as a guid.

            var id = GetGuid(jobAdId);
            if (id != null)
                return id;

            // Try everything before any '-'.

            var pos = jobAdId.IndexOf("-");
            if (pos != -1)
            {
                id = GetGuid(jobAdId.Substring(0, pos));
                if (id != null)
                    return id;
            }

            // Try everything before any '+'.

            pos = jobAdId.IndexOf("+");
            if (pos != -1)
            {
                id = GetGuid(jobAdId.Substring(0, pos));
                if (id != null)
                    return id;
            }

            return null;
        }

        private static Guid? GetGuid(string jobAdId)
        {
            try
            {
                return new Guid(jobAdId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
