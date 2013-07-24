using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Web.Areas.Api.Models.Location;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class LocationApiController
        : ApiController
    {
        private const int DefaultMaxResults = 10;
        private readonly ILocationQuery _locationQuery;

        public LocationApiController(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
        }

        public ActionResult FindPartialMatchedLocations(int? countryId, string location, int? maxResults)
        {
            var country = countryId == null ? ActivityContext.Location.Country : _locationQuery.GetCountry(countryId.Value);
            maxResults = maxResults ?? DefaultMaxResults;
            var partialMatches = _locationQuery.FindPartialMatchedLocations(country, location, maxResults.Value);
            return Json((from m in partialMatches select m.Key).ToArray());
        }

        public ActionResult FindPartialMatchedPostalSuburbs(int? countryId, string location, int? maxResults)
        {
            var country = countryId == null ? ActivityContext.Location.Country : _locationQuery.GetCountry(countryId.Value);
            maxResults = maxResults ?? DefaultMaxResults;
            var partialMatches = _locationQuery.FindPartialMatchedPostalSuburbs(country, location, maxResults.Value);
            return Json((from m in partialMatches select m.Key).ToArray());
        }

        public ActionResult ResolveLocation(int? countryId, string location)
        {
            try
            {
                if (string.IsNullOrEmpty(location))
                    return ResolveLocation(location ?? string.Empty, string.Empty);

                // If the country is not specified then use the current context.

                var country = countryId == null ? ActivityContext.Location.Country : _locationQuery.GetCountry(countryId.Value);

                // Resolve.

                var locationReference = _locationQuery.ResolveLocation(country, location);
                if (!locationReference.IsFullyResolved)
                {
                    // Not everything could be resolved.

                    return ResolveLocation(location, null);
                }
                
                // Resolve based on what the location was resolved down to.

                if (locationReference.PostalSuburb != null)
                    return ResolveLocation(location, Resolve(country, locationReference.PostalSuburb, location));
                if (locationReference.PostalCode != null)
                    return ResolveLocation(location, ResolvePostalCode(country, locationReference.PostalCode, location));
                if (locationReference.Locality != null)
                    return ResolveLocation(location, ResolveLocality(country, locationReference.Locality, location));
                if (locationReference.Region != null)
                    return ResolveLocation(location, ResolveRegion(country, locationReference.Region, location));
                return ResolveLocation(location, Resolve(country, locationReference.CountrySubdivision, location));
            }
            catch (Exception)
            {
                return ResolveLocation(location, null);
            }
        }

        public ActionResult ClosestLocation(int? countryId, float latitude, float longitude)
        {
            try
            {
                // If the country is not specified then use the current context.

                var country = countryId == null ? ActivityContext.Location.Country : _locationQuery.GetCountry(countryId.Value);

                // Get the closest locality.

                var locality = _locationQuery.GetClosestLocality(country, new GeoCoordinates(latitude, longitude));
                if (locality == null)
                    return ResolveLocation(countryId, null);

                // Return the first postal suburb in the first postal code.

                LocationReference locationReference;

                var postalCodes = _locationQuery.GetPostalCodes(locality).OrderBy(pc => pc.Postcode).ToList();
                if (postalCodes.Count == 0)
                {
                    locationReference = new LocationReference(locality);
                    return ResolveLocation(locationReference.ToString(), locationReference.ToString());
                }

                var postalSuburbs = _locationQuery.GetPostalSuburbs(postalCodes[0]).OrderBy(ps => ps.Name).ToList();
                if (postalSuburbs.Count == 0)
                {
                    locationReference = new LocationReference(postalCodes[0]);
                    return ResolveLocation(locationReference.ToString(), locationReference.ToString());
                }

                locationReference = new LocationReference(postalSuburbs[0]);
                return ResolveLocation(locationReference.ToString(), locationReference.ToString());
            }
            catch (Exception)
            {
                return ResolveLocation(countryId, null);
            }
        }

        private ActionResult ResolveLocation(string location, string resolvedLocation)
        {
            return Json(new ResolvedLocationModel { Location = location, ResolvedLocation = resolvedLocation });
        }

        private string Resolve(Country country, CountrySubdivision subdivision, string location)
        {
            // If the subdivision corresponds to a country then just return the location itself.

            if (subdivision.IsCountry)
                return location;
            
            // If the location is the short display name then don't flip.

            if (string.Compare(location, subdivision.ShortName, StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;

            // If the location is an alias for the subdivision, e.g. Tassie, then don't flip it to TAS.

            if (subdivision.Aliases.Any(alias => string.Compare(location, alias.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
                return location;

            return ResolveNamedLocation(country, subdivision, location);
        }

        private string ResolveRegion(Country country, NamedLocation region, string location)
        {
            return ResolveNamedLocation(country, region, location);
        }

        private string ResolveLocality(Country country, NamedLocation locality, string location)
        {
            return ResolveNamedLocation(country, locality, location);
        }

        private string ResolvePostalCode(Country country, NamedLocation postalCode, string location)
        {
            return ResolveNamedLocation(country, postalCode, location);
        }

        private string Resolve(Country country, PostalSuburb postalSuburb, string location)
        {
            // If the location is postcode first, e.g. 3000 Melbourne VIC, then don't flip it to Melbourne VIC 3000.

            return string.Compare(location, postalSuburb.ToStringPostcodeFirst(), StringComparison.InvariantCultureIgnoreCase) == 0
                ? location
                : ResolveNamedLocation(country, postalSuburb, location);
        }

        private string ResolveNamedLocation(Country country, NamedLocation namedLocation, string location)
        {
            // If the location is the displayName then don't flip.

            if (string.Compare(location, namedLocation.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;

            // If the location is just a case variation on the full string representation then don't flip.

            if (string.Compare(location, namedLocation.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;

            // If it is using one of the location synonyms then don't flip.

            return _locationQuery.IsLocationSynonym(country, location, namedLocation)
                ? location
                : namedLocation.ToString();
        }
    }
}