using System;
using AjaxPro;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common
{
    public class ResolveLocation
    {
        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        [AjaxMethod]
        public string ResolveNamedLocation(int countryId, string location)
        {
            try
            {
                if (string.IsNullOrEmpty(location))
                    return string.Empty;

                // If the country is not specified then use the current context.

                Country country = _locationQuery.GetCountry(countryId);
                if (country == null)
                    country = /*RequestContext.Current.LocationContext.Country*/ _locationQuery.GetCountry("Australia");

                // Resolve.

                var locationReference = _locationQuery.ResolveLocation(country, location);

                if (!locationReference.IsFullyResolved)
                {
                    // Not everything could be resolved.

                    return string.Empty;
                }
                else
                {
                    // Resolve based on what the location was resolved down to.

                    if (locationReference.PostalSuburb != null)
                        return Resolve(country, locationReference.PostalSuburb, location);
                    else if (locationReference.PostalCode != null)
                        return Resolve(country, locationReference.PostalCode, location);
                    else if (locationReference.Locality != null)
                        return Resolve(country, locationReference.Locality, location);
                    else if (locationReference.Region != null)
                        return Resolve(country, locationReference.Region, location);
                    else
                        return Resolve(country, locationReference.CountrySubdivision, location);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [AjaxMethod]
        public string ResolvePostalSuburb(int countryId, string location)
        {
            try
            {
                // If the country is not specified then use the current context.

                Country country = _locationQuery.GetCountry(countryId);
                if (country == null)
                    country = /*RequestContext.Current.LocationContext.Country*/ _locationQuery.GetCountry("Australia");

                // Try to resolve.

                var locationReference = _locationQuery.ResolvePostalSuburb(country, location);

                if (locationReference.IsFullyResolved)
                {
                    // Fully resolved.  At least as resolved as it is going to get,
                    // for those countries which don't actually have suburbs defined
                    // this will indicate it is resolved but there won't actually be a postal suburb set.

                    if (locationReference.PostalSuburb != null)
                        return Resolve(country, locationReference.PostalSuburb, location);
                    else 
                        return location;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string Resolve(Country country, CountrySubdivision subdivision, string location)
        {
            // If the subdivision corresponds to a country then just return the location itself.

            if (subdivision.IsCountry)
            {
                return location;
            }
            else
            {

                // If the location is the short display name then don't flip.

                if (string.Compare(location, subdivision.ShortName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    return location;

                // If the location is an alias for the subdivision, e.g. Tassie, then don't flip it to TAS.

                foreach (CountrySubdivisionAlias alias in subdivision.Aliases)
                {
                    if (string.Compare(location, alias.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                        return location;
                }

                return ResolveNamedLocation(country, subdivision, location);
            }
        }

        private static string Resolve(Country country, Region region, string location)
        {
            return ResolveNamedLocation(country, region, location);
        }

        private static string Resolve(Country country, Locality locality, string location)
        {
            return ResolveNamedLocation(country, locality, location);
        }

        private static string Resolve(Country country, PostalCode postalCode, string location)
        {
            return ResolveNamedLocation(country, postalCode, location);
        }

        private static string Resolve(Country country, PostalSuburb postalSuburb, string location)
        {
            // If the location is postcode first, e.g. 3000 Melbourne VIC, then don't flip it to Melbourne VIC 3000.
            
            if (string.Compare(location, postalSuburb.ToStringPostcodeFirst(), StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;
            else
                return ResolveNamedLocation(country, postalSuburb, location);
        }

        private static string ResolveNamedLocation(Country country, NamedLocation namedLocation, string location)
        {
            // If the location is the displayName then don't flip.

            if (string.Compare(location, namedLocation.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;

            // If the location is just a case variation on the full string representation then don't flip.

            if (string.Compare(location, namedLocation.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0)
                return location;

            // If it is using one of the location synonyms then don't flip.

            if (_locationQuery.IsLocationSynonym(country, location, namedLocation))
                return location;

            return namedLocation.ToString();
        }
    }
}
