using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Location;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public static class RelocationExtensions
    {
        public static IList<int> GetRelocationCountryIds(this ICollection<LocationReference> relocationLocations)
        {
            if (relocationLocations == null || relocationLocations.Count == 0)
                return null;

            return (from l in relocationLocations
                    where l.IsCountry
                    select l.Country.Id).ToList();
        }

        public static IList<int> GetRelocationCountryLocationIds(this ICollection<LocationReference> relocationLocations, Country country)
        {
            if (relocationLocations == null || relocationLocations.Count == 0)
                return null;

            // Grab all locations that are within this country.

            return (from l in relocationLocations
                    where !l.IsCountry
                    && l.NamedLocation != null
                    && l.Country.Id == country.Id
                    select l.NamedLocation.Id).ToList();
        }
    }
}
