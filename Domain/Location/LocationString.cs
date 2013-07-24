namespace LinkMe.Domain.Location
{
    internal static class LocationString
    {
        public static string ToString(NamedLocation namedLocation, CountrySubdivision subdivision, bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            if (namedLocation is Locality)
                return ToString((Locality)namedLocation, subdivision, hasSubdivisionAccess, hasSubSubdivisionAccess);
            if (namedLocation is PostalCode)
                return ToString((PostalCode)namedLocation, subdivision, hasSubdivisionAccess, hasSubSubdivisionAccess);
            if (namedLocation is Region)
                return ToString((Region)namedLocation);
            if (namedLocation is PostalSuburb)
                return ToString((PostalSuburb)namedLocation, hasSubdivisionAccess, hasSubSubdivisionAccess);
            if (namedLocation is CountrySubdivision)
                return ToString((CountrySubdivision)namedLocation, hasSubdivisionAccess);
            return string.Empty;
        }

        public static string ToString(NamedLocation namedLocation)
        {
            return ToString(namedLocation, World.GetCountrySubdivision(namedLocation), true, true);
        }

        private static string ToString(Locality locality, CountrySubdivision subdivision, bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            if (hasSubdivisionAccess)
            {
                if (hasSubSubdivisionAccess)
                {
                    // Access to everything.

                    return locality.Name + (subdivision.ShortName == null ? string.Empty : " " + subdivision.ShortName);
                }

                // Access to only the subdivision.

                return ToString(subdivision, true);
            }
            
            if (hasSubSubdivisionAccess)
            {
                // Access to only the locality.

                return locality.Name;
            }

            // Access to nothing.
            
            return string.Empty;
        }

        private static string ToString(PostalCode postalCode, CountrySubdivision subdivision, bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            if (hasSubdivisionAccess)
            {
                if (hasSubSubdivisionAccess)
                {
                    // Access to everything.

                    return postalCode.Name + (subdivision.ShortName == null ? string.Empty : " " + subdivision.ShortName);
                }

                // Access to only the subdivision.

                return ToString(subdivision, true);
            }

            if (hasSubSubdivisionAccess)
            {
                // Access to only the postcode.

                return postalCode.Name;
            }

            // Access to nothing.
            
            return string.Empty;
        }

        private static string ToString(Region region)
        {
            // Just return the display name.

            return region.Name;
        }

        private static string ToString(PostalSuburb postalSuburb, bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            return ToString(postalSuburb, true, hasSubdivisionAccess, hasSubSubdivisionAccess);
        }

        public static string ToString(PostalSuburb postalSuburb, bool suburbFirst)
        {
            return ToString(postalSuburb, suburbFirst, true, true);
        }

        private static string ToString(PostalSuburb postalSuburb, bool suburbFirst, bool hasSubdivisionAccess, bool hasSubSubdivisionAccess)
        {
            if (hasSubdivisionAccess)
            {
                if (hasSubSubdivisionAccess)
                {
                    // Access to everything.

                    if (suburbFirst)
                        return postalSuburb.Name + " " + postalSuburb.CountrySubdivision.ShortName + " " + postalSuburb.PostalCode.Postcode;
                    return postalSuburb.PostalCode.Postcode + " " + postalSuburb.Name + " " + postalSuburb.CountrySubdivision.ShortName;
                }

                // Access to only the subdivision.

                return postalSuburb.CountrySubdivision.ShortName;
            }

            if (hasSubSubdivisionAccess)
            {
                // Access to only the suburb and postcode.

                if (suburbFirst)
                    return postalSuburb.Name + " " + postalSuburb.PostalCode.Postcode;
                return postalSuburb.PostalCode.Postcode + " " + postalSuburb.Name;
            }

            // Access to nothing.

            return string.Empty;
        }

        private static string ToString(CountrySubdivision subdivision, bool hasSubdivisionAccess)
        {
            if (hasSubdivisionAccess)
                return subdivision.ShortName ?? string.Empty;
            return string.Empty;
        }
    }
}
