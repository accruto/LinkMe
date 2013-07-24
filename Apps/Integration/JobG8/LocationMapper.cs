using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Utility;

namespace LinkMe.Apps.Integration.JobG8
{
    public class LocationMapper
    {
        private readonly ILocationQuery _locationQuery;

        public LocationMapper(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;
        }

        public LocationReference Map(string jobg8Country, string jobg8Location, string jobg8Area, string jobg8Postcode)
        {
            // Resolve the Country.

            var country = _locationQuery.GetCountry(MapCountry(jobg8Country));
            if (country == null)
                throw new ServiceEndUserException("Invalid country.", jobg8Country);

            // Try to resolve by the postcode.

            if (!string.IsNullOrEmpty(jobg8Postcode))
            {
                var postcodeLocation = _locationQuery.ResolveLocation(country, jobg8Postcode);
                if (postcodeLocation.IsFullyResolved)
                    return postcodeLocation;
            }

            // Check whether jobg8Location refers to the Region.

            if (!string.IsNullOrEmpty(jobg8Location))
            {
                var regionLocation = _locationQuery.ResolveLocation(country, jobg8Location);
                if (regionLocation.IsFullyResolved && regionLocation.Region != null)
                    return regionLocation;
            }

            // Try the full resolution.

            var locationParts = new string[3];
            var partsCount = 0;

            if (!string.IsNullOrEmpty(jobg8Area))
                locationParts[partsCount++] = MapArea(jobg8Area);

            if (!string.IsNullOrEmpty(jobg8Location))
                locationParts[partsCount++] = MapLocation(jobg8Location);

            if (!string.IsNullOrEmpty(jobg8Postcode))
                locationParts[partsCount++] = jobg8Postcode;

            var location = string.Join(", ", locationParts, 0, partsCount);

            var namedLocations = new List<NamedLocation>(1);
            var locationReference = _locationQuery.ResolveLocation(country, location, namedLocations, 1);

            if (namedLocations.Count > 0)
                locationReference = new LocationReference(namedLocations[0]);

            return locationReference;
        }

        private static string MapCountry(string jobg8Country)
        {
            switch (jobg8Country)
            {
                case "United States":
                    return "USA";

                default:
                    return jobg8Country;
            }
        }

        private static string MapLocation(string jobg8Location)
        {
            switch (jobg8Location)
            {
                case "NSW Other":
                    return "NSW";

                case "VIC Other":
                    return "VIC";

                case "QLD Other":
                    return "QLD";

                case "SA Other":
                    return "SA";

                case "WA Other":
                    return "WA";

                case "TAS Other":
                    return "TAS";

                default:
                    return jobg8Location;
            }
        }

        private static string MapArea(string jobg8Area)
        {
            switch (jobg8Area)
            {
                case "Albury Wodonga":
                    return "Albury";

                default:
                    return jobg8Area;
            }
        }
    }
}
