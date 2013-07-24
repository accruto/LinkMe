using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Location
{
    internal static class LocationResolver
    {
        // It is assumed that all postcodes are numeric.

        private static readonly Regex PostcodeRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);

        [Flags]
        private enum MatchCondition
        {
            Exact = 0x1,
            Partial = 0x2,
            Containing = 0x4,
            SoundEx = 0x8,
            Synonym = 0x10,
            All = Exact | Partial | Containing | SoundEx | Synonym,
        }

        public static void ResolveLocation(LocationReference locationReference, Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            location = location == null ? null : location.Trim();
            if (string.IsNullOrEmpty(location))
            {
                locationReference.Set(country, null);
                return;
            }

            // Search for an exact match.

            var finder = World.GetCountryFinder(country.Id);
            if (ResolveCountrySubdivisionRegion(finder, locationReference, location))
                return;

            // See if the location contains the country name (or is the country name)

            var strippedLocation = StripCountry(finder, country, location);
            if (strippedLocation == null)
            {
                locationReference.Set(country, null);
                return;
            }

            // Search for an exact match again.

            if (ResolveCountrySubdivisionRegion(finder, locationReference, strippedLocation))
                return;

            // Extract the location which will be used for resolving from the passed in string.

            var unstructuredLocation = ExtractUnstructuredLocation(strippedLocation);
            var parseLocation = ExtractParseLocation(unstructuredLocation);

            // Parse the location to determine what has been supplied out of the Suburb, CountrySubdivision and Postcode.

            string suburb, postcode;
            IList<KeyValuePair<string, CountrySubdivision>> subdivisions;
            ParseLocation(finder, country, parseLocation, out postcode, out subdivisions, out suburb);

            // Search for an exact match within the localities.

            var locality = ResolveLocality(finder, country, postcode, subdivisions, suburb);
            if (locality != null)
            {
                locationReference.Set(locality);
                return;
            }

            // Search for an exact match within the postal codes.

            var postalCode = ResolvePostalCode(finder, postcode, subdivisions, suburb);
            if (postalCode != null)
            {
                locationReference.Set(postalCode);
                return;
            }

            // Treat it as an address location and try to resolve it.

            ResolvePostalSuburb(locationReference, finder, country, unstructuredLocation, postcode, subdivisions, suburb, suggestions, maximumSuggestions);
        }

        public static void ResolvePostalSuburb(LocationReference locationReference, Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            if (country == null)
                throw new ArgumentNullException("country");

            // Extract the location which will be used for resolving from the passed in string.

            var unstructuredLocation = ExtractUnstructuredLocation(location);
            var parseLocation = ExtractParseLocation(unstructuredLocation);

            // Parse the location to determine what has been supplied out of the Suburb, CountrySubdivision and Postcode.

            string suburb, postcode;
            IList<KeyValuePair<string, CountrySubdivision>> subdivisions;
            var finder = World.GetCountryFinder(country.Id);
            ParseLocation(finder, country, parseLocation, out postcode, out subdivisions, out suburb);

            // Resolve.

            ResolvePostalSuburb(locationReference, finder, country, unstructuredLocation, postcode, subdivisions, suburb, suggestions, maximumSuggestions);
        }

        private static bool ResolveCountrySubdivisionRegion(CountryFinder finder, LocationReference locationReference, string location)
        {
            // Search for an exact match within the subdivisions.

            var subdivision = finder.GetCountrySubdivision(location);
            if (subdivision != null)
            {
                locationReference.Set(subdivision);
                return true;
            }

            // Search for an exact match within the regions.

            var region = finder.GetRegion(location);
            if (region != null)
            {
                locationReference.Set(region);
                return true;
            }

            return false;
        }

        private static Locality ResolveLocality(CountryFinder finder, Country country, string postcode, ICollection<KeyValuePair<string, CountrySubdivision>> subdivisions, string suburb)
        {
            // Use the postcode to look up a locality.

            var locality = finder.GetLocality(postcode);
            if (locality == null)
                return null;

            // If no subdivisions were supplied then check the suburb.  If that is also not supplied then it is a match.

            if (subdivisions.Count == 0)
                return string.IsNullOrEmpty(suburb) ? locality : null;

            // Check that one of the subdivisions from the location matches one of the subdivisions of the locality.

            foreach (var pair in subdivisions)
            {
                // If the suburb is set then it can't be a match.

                if (string.IsNullOrEmpty(pair.Key))
                {
                    var subdivision = pair.Value;
                    foreach (var localitySubdivision in locality.CountrySubdivisions)
                    {
                        if (subdivision.Equals(localitySubdivision))
                            return locality;
                    }
                }
            }

            return null;
        }

        private static PostalCode ResolvePostalCode(CountryFinder finder, string postcode, ICollection<KeyValuePair<string, CountrySubdivision>> subdivisions, string suburb)
        {
            // Use the postcode to look up a postal code.

            var postalCode = finder.GetPostalCode(postcode);
            if (postalCode == null)
                return null;

            // If no subdivisions were supplied then check the suburb.  If that is also not supplied then it is a match.

            if (subdivisions.Count == 0)
                return string.IsNullOrEmpty(suburb) ? postalCode : null;

            // Check that one of the subdivisions from the location matches one of the subdivisions of the locality.

            foreach (var pair in subdivisions)
            {
                // If the suburb is set then it can't be a match.

                if (string.IsNullOrEmpty(pair.Key))
                {
                    var subdivision = pair.Value;
                    foreach (var localitySubdivision in postalCode.Locality.CountrySubdivisions)
                    {
                        if (subdivision.Equals(localitySubdivision))
                            return postalCode;
                    }
                }
            }

            return null;
        }

        private static void ResolvePostalSuburb(LocationReference locationReference, CountryFinder finder, Country country, string unstructuredLocation, string postcode, IList<KeyValuePair<string, CountrySubdivision>> subdivisions, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Resolve based on how many subdivisions were found in the location.

            LocationReference resolvedLocationReference;

            switch (subdivisions.Count)
            {
                case 0:
                    resolvedLocationReference = Resolve(MatchCondition.All, finder, country, unstructuredLocation, postcode, suburb, suggestions, maximumSuggestions);
                    break;

                case 1:
                    resolvedLocationReference = Resolve(MatchCondition.All, finder, country, unstructuredLocation, subdivisions[0].Value, postcode, subdivisions[0].Key, suggestions, maximumSuggestions);
                    break;

                default:
                    resolvedLocationReference = Resolve(finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions);
                    break;
            }

            if (resolvedLocationReference == null)
                resolvedLocationReference = Resolve(country, unstructuredLocation);

            // Copy over the result.

            locationReference.UnstructuredLocation = resolvedLocationReference.UnstructuredLocation;
            locationReference.NamedLocation = resolvedLocationReference.NamedLocation;
            locationReference.CountrySubdivision = resolvedLocationReference.CountrySubdivision;
            locationReference.Locality = resolvedLocationReference.Locality;
        }

        #region Parsing

        private static string ExtractUnstructuredLocation(string location)
        {
            location = location ?? string.Empty;
            foreach (var c in TextUtil.WhitespaceChars)
                location.Replace(c, ' ');
            return TextUtil.StripExtraWhiteSpace(location);
        }

        private static string ExtractParseLocation(string location)
        {
            // Remove anything that is not alpha numeric or space.

            return TextUtil.StripExtraWhiteSpace(TextUtil.ReduceToAlphaNumericAndWhiteSpace(location, ' '));
        }

        private static void ParseLocation(CountryFinder finder, Country country, string location, out string postcode, out IList<KeyValuePair<string, CountrySubdivision>> subdivisions, out string suburb)
        {
            // Extract the postcode and subdivision.

            postcode = ExtractPostcode(ref location);
            location = StripCountry(finder, country, location);
            subdivisions = ExtractSubdivisions(finder, ref location);

            // Treat whatever is left as the suburb.

            suburb = location;
        }

        private static string ExtractPostcode(ref string location)
        {
            var postcode = string.Empty;
            var match = PostcodeRegex.Match(location);
            if (match.Success)
            {
                postcode = match.Groups[0].ToString();
                location = PostcodeRegex.Replace(location, string.Empty).Trim();
            }

            return postcode;
        }

        private static IList<KeyValuePair<string, CountrySubdivision>> ExtractSubdivisions(CountryFinder finder, ref string location)
        {
            // The name of a subdivision may be used within an suburb, e.g. the subdivision Victoria is part of the name
            // Victoria Gardens, so does Victoria Gardens refer to the suburb Victoria Gardens or does it refer to the suburb
            // Gardens in the Victoria subdivision.  Need to check both so return all combinations that could be valid. 

            IList<KeyValuePair<string, CountrySubdivision>> subdivisions = new List<KeyValuePair<string, CountrySubdivision>>();

            // Extract all subdivision names from the location.

            var aliases = finder.ExtractSubdivisionNames(location);
            if (aliases.Count == 0)
                return subdivisions;

            // Iterate through each match.

            foreach (var alias in aliases)
            {
                var subdivision = finder.GetCountrySubdivision(alias);
                var subdivisionLocation = location.Replace(alias, string.Empty).Trim();
                subdivisions.Add(new KeyValuePair<string, CountrySubdivision>(subdivisionLocation, subdivision));
            }

            location = null;
            return subdivisions;
        }

        private static string StripCountry(CountryFinder finder, Country country, string location)
        {
            Debug.Assert(location != null && country != null, "location != null && country != null");

            if (location.EndsWith(country.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                if (location.Length == country.Name.Length)
                    return null;

                var beforeCountry = location.Length - country.Name.Length - 1;
                if (char.IsWhiteSpace(location[beforeCountry]) && !EndsWithLocation(location, finder))
                    return location.Remove(beforeCountry).TrimEnd();
            }
            else if (location.StartsWith(country.Name))
            {
                var afterCountry = country.Name.Length;
                if (char.IsWhiteSpace(location[afterCountry]))
                    return location.Substring(afterCountry).TrimStart();
            }

            return location;
        }

        private static bool EndsWithLocation(string location, CountryFinder finder)
        {
            foreach (var countrySubdivision in finder.GetCountrySubdivisions())
            {
                if (EndsWith(location, countrySubdivision))
                    return true;
            }

            foreach (var region in finder.GetRegions())
            {
                if (EndsWith(location, region))
                    return true;
            }

            return false;
        }

        private static bool EndsWith(string location, CountrySubdivision subdivision)
        {
            if (subdivision.IsCountry)
                return false;

            if (EndsWith(location, subdivision.Name))
                return true;
            if (EndsWith(location, subdivision.ShortName))
                return true;
            foreach (var alias in subdivision.Aliases)
            {
                if (EndsWith(location, alias.Name))
                    return true;
            }

            return false;
        }

        private static bool EndsWith(string location, Region region)
        {
            return EndsWith(location, region.Name);
        }

        private static bool EndsWith(string location, string ending)
        {
            if (location.EndsWith(ending, StringComparison.CurrentCultureIgnoreCase))
            {
                if (location.Length == ending.Length)
                    return true;

                var before = location.Length - ending.Length - 1;
                if (char.IsWhiteSpace(location[before]))
                    return true;
            }

            return false;
        }

        #endregion

        #region Resolve

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, string postcode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            return Resolve(condition, finder, country, unstructuredLocation, null, postcode, suburb, suggestions, maximumSuggestions);
        }

        private static LocationReference Resolve(CountryFinder finder, Country country, string unstructuredLocation, string postcode, IList<KeyValuePair<string, CountrySubdivision>> subdivisions, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            bool isSubdivisions;
            IList<LocationReference> locationReferences;

            // Check for exact matches.

            Resolve(MatchCondition.Exact, finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions, out isSubdivisions, out locationReferences);
            if (!isSubdivisions)
                return Resolve(locationReferences, suggestions);

            // Check for synonym matches.

            Resolve(MatchCondition.Synonym, finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions, out isSubdivisions, out locationReferences);
            if (!isSubdivisions)
                return Resolve(locationReferences, suggestions);

            // Check for partial matches.

            Resolve(MatchCondition.Partial, finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions, out isSubdivisions, out locationReferences);
            if (!isSubdivisions)
                return Resolve(locationReferences, suggestions);

            // Check for containing matches.

            Resolve(MatchCondition.Containing, finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions, out isSubdivisions, out locationReferences);
            if (!isSubdivisions)
                return Resolve(locationReferences, suggestions);

            // Check for soundex matches.

            Resolve(MatchCondition.SoundEx, finder, country, unstructuredLocation, postcode, subdivisions, suggestions, maximumSuggestions, out isSubdivisions, out locationReferences);
            if (!isSubdivisions)
                return Resolve(locationReferences, suggestions);

            return null;
        }

        private static LocationReference Resolve(IList<LocationReference> locationReferences, ICollection<NamedLocation> suggestions)
        {
            switch (locationReferences.Count)
            {
                case 0:
                    // Nothing found.

                    return null;

                case 1:
                    // Only one found so return it.

                    if (suggestions != null)
                        suggestions.Clear();
                    return locationReferences[0];

                default:
                    // All the references may have been resolved to exactly the same thing so return it if it is unique.

                    var locationReference = locationReferences[0];
                    for (var index = 1; index < locationReferences.Count; index++)
                    {
                        if (!locationReference.Equals(locationReferences[index]))
                            return null;
                    }

                    // All references are the same.

                    if (suggestions != null)
                        suggestions.Clear();
                    return locationReference;
            }
        }

        private static void Resolve(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, string postcode, IList<KeyValuePair<string, CountrySubdivision>> subdivisions, ICollection<NamedLocation> suggestions, int maximumSuggestions, out bool isSubdivisions, out IList<LocationReference> locationReferences)
        {
            locationReferences = new List<LocationReference>();
            isSubdivisions = true;

            // Work through each subdivision found.  Move backwards because most locations will
            // contain the subdivision at the end, and so are more likely to match.

            for (var index = subdivisions.Count - 1; index >= 0; --index)
            {
                var suburb = subdivisions[index].Key;
                var subdivision = subdivisions[index].Value;

                // If there is any sort of match then return that.

                var locationReference = Resolve(condition, finder, country, unstructuredLocation, subdivision, postcode, suburb, suggestions, maximumSuggestions);
                var subdivisionLocationReference = Resolve(unstructuredLocation, subdivision);

                if (isSubdivisions)
                {
                    if (locationReference.Equals(subdivisionLocationReference))
                    {
                        // Just another subdivision reference so add it.

                        locationReferences.Add(locationReference);
                    }
                    else
                    {
                        // Something more than a subdivision.

                        locationReferences.Clear();
                        locationReferences.Add(locationReference);
                        isSubdivisions = false;
                    }
                }
                else
                {
                    // Check that it is more than just a subdivision reference.

                    if (!locationReference.Equals(subdivisionLocationReference))
                        locationReferences.Add(locationReference);
                }
            }
        }

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, CountrySubdivision subdivision, string postcode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Break the resolution down into what was actually supplied.

            if (subdivision == null)
            {
                if (postcode == string.Empty)
                {
                    return string.IsNullOrEmpty(suburb)
                        ? null
                        : ResolveSuburb(condition, finder, country, unstructuredLocation, suburb, suggestions, maximumSuggestions);
                }
                
                return string.IsNullOrEmpty(suburb)
                           ? ResolvePostcode(finder, country, unstructuredLocation, postcode, suggestions, maximumSuggestions)
                           : ResolvePostcodeSuburb(condition, finder, country, unstructuredLocation, postcode, suburb, suggestions, maximumSuggestions);
            }

            if (postcode == string.Empty)
            {
                return suburb == string.Empty
                    ? ResolveSubdivision(unstructuredLocation, subdivision)
                    : ResolveSubdivisionSuburb(condition, finder, unstructuredLocation, subdivision, suburb, suggestions, maximumSuggestions);
            }
            
            return string.IsNullOrEmpty(suburb)
                ? ResolveSubdivisionPostcode(finder, unstructuredLocation, subdivision, postcode, suggestions, maximumSuggestions)
                : ResolveSubdivisionPostcodeSuburb(condition, finder, unstructuredLocation, subdivision, postcode, suburb, suggestions, maximumSuggestions);
        }

        private static LocationReference ResolveSuburb(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            PostalSuburb postalSuburb;
            var continueResolving = true;

            // Look for exact matches.

            if ((condition & MatchCondition.Exact) != 0)
            {
                var postalSuburbs = finder.FindSuburbs(suburb);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for synonym matches.

            if ((condition & MatchCondition.Synonym) != 0)
            {
                var postalSuburbs = finder.FindSynonymMatchedSuburbs(suburb);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                }
            }

            // Cannot resolve.

            return null;
        }

        private static LocationReference ResolvePostcode(CountryFinder finder, Country country, string unstructuredLocation, string postcode, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Look for an exact match.

            var postalCode = finder.GetPostalCode(postcode);
            if (postalCode == null)
            {
                // If the postcode is the only thing supplied and it does not match exactly then it cannot be resolved.

                return null;
            }

            // If the postal code belongs to more than one subdivision then cannot resolve.

            if (postalCode.Locality.CountrySubdivisions.Count != 1)
            {
                AddToSuggestions(postalCode.GetPostalSuburbs(), suggestions, maximumSuggestions);
                return Resolve(country, unstructuredLocation, postalCode);
            }

            // Look for suburbs that match the postcode.

            var postalSuburbs = GetSuburbs(postalCode);
            if (postalSuburbs.Count > 0)
            {
                var postalSuburb = SelectSuburb(postalSuburbs, true, suggestions, maximumSuggestions);
                return Resolve(country, unstructuredLocation, postalCode, postalSuburb);
            }

            // Postcode within a single subdivision with no suburbs.

            return Resolve(country, unstructuredLocation, postalCode);
        }

        private static LocationReference ResolvePostcodeSuburb(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, string postcode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Look for exact match for the postcode filtered by the subdivision.

            var postalCode = finder.GetPostalCode(postcode);
            if (postalCode != null)
                return Resolve(condition, finder, country, unstructuredLocation, postalCode, suburb, suggestions, maximumSuggestions);
            
            // Look for postal codes that partially match.

            var postalCodes = finder.FindPartialMatchedPostalCodes(postcode);
            if (postalCodes.Count == 0)
            {
                // Cannot resolve.

                return null;
            }

            // There are postal codes that match so try to resolve.

            return Resolve(condition, finder, country, unstructuredLocation, postalCodes, suburb, suggestions, maximumSuggestions);
        }

        private static LocationReference ResolveSubdivision(string unstructuredLocation, CountrySubdivision subdivision)
        {
            return Resolve(unstructuredLocation, subdivision);
        }

        private static LocationReference ResolveSubdivisionSuburb(MatchCondition condition, CountryFinder finder, string unstructuredLocation, CountrySubdivision subdivision, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var filter = CreatePostalSuburbFilter(subdivision);
            PostalSuburb postalSuburb;
            var continueResolving = true;

            // Look for exact matches.

            if ((condition & MatchCondition.Exact) != 0)
            {
                var postalSuburbs = finder.FindSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for synonym matches.

            if ((condition & MatchCondition.Synonym) != 0)
            {
                var postalSuburbs = finder.FindSynonymMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                }
            }

            // Cannot resolve.

            return Resolve(unstructuredLocation, subdivision);
        }

        private static LocationReference ResolveSubdivisionPostcode(CountryFinder finder, string unstructuredLocation, CountrySubdivision subdivision, string postcode, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Look for exact match for the postcode filtered by the subdivision.

            var postalCode = finder.FindPostalCode(postcode, subdivision);
            if (postalCode == null)
            {
                // If the postcode does not match exactly then it cannot be resolved.

                return Resolve(unstructuredLocation, subdivision);
            }

            // Look for suburbs that match the subdivision and postcode.

            var postalSuburbs = finder.FindSuburbs(postalCode, CreatePostalSuburbFilter(subdivision));
            if (postalSuburbs.Count > 0)
            {
                var postalSuburb = SelectSuburb(postalSuburbs, true, suggestions, maximumSuggestions);
                return Resolve(unstructuredLocation, subdivision, postalCode, postalSuburb);
            }

            // Postcode in a subdivision with no suburbs.

            return Resolve(unstructuredLocation, subdivision, postalCode);
        }

        private static LocationReference ResolveSubdivisionPostcodeSuburb(MatchCondition condition, CountryFinder finder, string unstructuredLocation, CountrySubdivision subdivision, string postcode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Look for exact match for the postcode filtered by the subdivision.

            var postalCode = finder.FindPostalCode(postcode, subdivision);
            if (postalCode != null)
                return Resolve(condition, finder, unstructuredLocation, subdivision, postalCode, suburb, suggestions, maximumSuggestions);

            // Look for postal codes that partially match.

            var postalCodes = finder.FindPartialMatchedPostalCodes(postcode, CreatePostalCodeFilter(subdivision));
            return postalCodes.Count == 0
                ? Resolve(unstructuredLocation, subdivision)
                : Resolve(condition, finder, unstructuredLocation, subdivision, postalCodes, suburb, suggestions, maximumSuggestions);
        }

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, PostalCode postalCode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var filter = CreatePostalSuburbFilter(postalCode);

            // Look for an exact match.

            PostalSuburb postalSuburb;
            if ((condition & MatchCondition.Exact) != 0)
            {
                postalSuburb = finder.FindSuburb(suburb, filter);
                if (postalSuburb != null)
                    return Resolve(postalSuburb);
            }

            // Look for synonym match.

            var continueResolving = true;
            if ((condition & MatchCondition.Synonym) != 0)
            {
                postalSuburb = finder.FindSynonymMatchedSuburb(suburb, filter);
                if (postalSuburb != null)
                    return Resolve(postalSuburb);
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalCode(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalCode, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalCode(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalCode, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalCode(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalCode, postalSuburb);
                }
            }

            // Cannot resolve.

            return Resolve(country, unstructuredLocation, postalCode);
        }

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, Country country, string unstructuredLocation, IList<PostalCode> postalCodes, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var filter = CreatePostalSuburbFilter(postalCodes);
            PostalSuburb postalSuburb;
            var continueResolving = true;

            // Look for an exact match.

            if ((condition & MatchCondition.Exact) != 0)
            {
                var postalSuburbs = finder.FindSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for alias matches.

            if ((condition & MatchCondition.Synonym) != 0)
            {
                var postalSuburbs = finder.FindSynonymMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinPostalSuburbs(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(country, unstructuredLocation, postalSuburb);
                }
            }

            // Cannot resolve.

            return null;
        }

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, string unstructuredLocation, CountrySubdivision subdivision, PostalCode postalCode, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var filter = CreatePostalSuburbFilter(subdivision, postalCode);
            PostalSuburb postalSuburb;
            var continueResolving = true;

            // Look for an exact match.

            if ((condition & MatchCondition.Exact) != 0)
            {
                postalSuburb = finder.FindSuburb(suburb, filter);
                if (postalSuburb != null)
                    return Resolve(postalSuburb);
            }

            // Look for alias match.

            if ((condition & MatchCondition.Synonym) != 0)
            {
                postalSuburb = finder.FindSynonymMatchedSuburb(suburb, filter);
                if (postalSuburb != null)
                    return Resolve(postalSuburb);
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count != 0)
                {
                    postalSuburb = SelectSuburb(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalCode, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count != 0)
                {
                    postalSuburb = SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalCode, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count != 0)
                {
                    postalSuburb = SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalCode, postalSuburb);
                }
            }

            // Cannot resolve.

            return Resolve(unstructuredLocation, subdivision, postalCode);
        }

        private static LocationReference Resolve(MatchCondition condition, CountryFinder finder, string unstructuredLocation, CountrySubdivision subdivision, IList<PostalCode> postalCodes, string suburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var filter = CreatePostalSuburbFilter(subdivision, postalCodes);
            PostalSuburb postalSuburb;
            var continueResolving = true;

            // Look for an exact match.

            if ((condition & MatchCondition.Exact) != 0)
            {
                var postalSuburbs = finder.FindSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, true, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for alias matches.

            if ((condition & MatchCondition.Synonym) != 0)
            {
                var postalSuburbs = finder.FindSynonymMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for partial matches.

            if ((condition & MatchCondition.Partial) != 0)
            {
                var postalSuburbs = finder.FindPartialMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for containing matches.

            if ((condition & MatchCondition.Containing) != 0)
            {
                var postalSuburbs = finder.FindContainingMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                    continueResolving = false;
                }
            }

            // Look for soundex matches.

            if ((condition & MatchCondition.SoundEx) != 0)
            {
                var postalSuburbs = finder.FindSoundExMatchedSuburbs(suburb, filter);
                if (postalSuburbs.Count > 0)
                {
                    postalSuburb = ResolveWithinSubdivision(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                    if (ResolvingIsComplete(postalSuburb, suggestions, maximumSuggestions))
                        return Resolve(unstructuredLocation, subdivision, postalSuburb);
                }
            }

            // Cannot resolve.

            return Resolve(unstructuredLocation, subdivision);
        }

        private static PostalSuburb ResolveWithinSubdivision(IList<PostalSuburb> postalSuburbs, bool continueResolving, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Even though subdivision is not used it is kept as a parameter just to indicate that the
            // subdivision was originally passed in and that the suburbs have all been filtered so that
            // they belong to the subdivision.

            // Shortcut.

            if (postalSuburbs.Count == 1)
                return SelectSuburb(postalSuburbs[0], continueResolving, suggestions);

            // Determine whether all suburbs belong to the same postal code.

            var postalCode = AreAllSuburbsInSamePostalCode(postalSuburbs);
            if (postalCode != null)
            {
                // All suburbs belong to the same postal code so use it.

                return SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
            }

            AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
            return null;
        }

        private static PostalSuburb ResolveWithinPostalCode(IList<PostalSuburb> postalSuburbs, bool continueResolving, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Even though postalCode is not used it is kept as a parameter just to indicate that the
            // postcode was originally passed in and that the suburbs have all been filtered so that
            // they belong to the postal code.

            // Shortcut.

            if (postalSuburbs.Count == 1)
                return SelectSuburb(postalSuburbs[0], continueResolving, suggestions);

            // Determine whether all suburbs belong to the same subdivision.

            var subdivision = AreAllSuburbsInSameSubdivision(postalSuburbs);
            if (subdivision != null)
            {
                // All suburbs belong to the same subdivision so use it.

                return SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
            }

            // The suburbs belong to different subdivisions and therefore cannot be resolved.

            AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
            return null;
        }

        private static PostalSuburb ResolveWithinPostalSuburbs(IList<PostalSuburb> postalSuburbs, bool continueResolving, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Shortcut.

            if (postalSuburbs.Count == 1)
                return SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);

            // Resolve the subdivision and postcode.

            var subdivision = AreAllSuburbsInSameSubdivision(postalSuburbs);
            var postalCode = AreAllSuburbsInSamePostalCode(postalSuburbs);

            if (subdivision == null)
            {
                // Multiple subdivisions, check whether the postal code can resolve it.

                if (postalCode != null && postalCode.Locality.CountrySubdivisions.Count == 1)
                {
                    // Postal code resolves the subdivision - although with the current domain model this
                    // is not actually possible.  The postal suburbs are confined within the postal code,
                    // so that if they span multiple subdivisions then the postal code will as well.
                    // Will keep this here in case that restriction is ever relaxed.
                    // Put the assert to indicate that if this is ever hit further investigation is
                    // required to determine why it was hit.

                    Debug.Assert(false);
                    return SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);
                }

                // Either the postalcode hasn't been resolved or the number of subdivisions cannot be resolved.
                // In any case cannot resolve.

                AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
                return null;
            }

            // Same subdivision and postcode.

            if (postalCode != null)
                return SelectSuburb(postalSuburbs, continueResolving, suggestions, maximumSuggestions);

            // Same subdivision but different postal codes.
            // If the names of all postal suburbs are the same then choose based on postal code.

            if ((from s in postalSuburbs select s.Name).Distinct().Count() == 1)
                return SelectSuburbByPostalCode(postalSuburbs, continueResolving, suggestions, maximumSuggestions);

            AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
            return null;
        }

        private static LocationReference Resolve(PostalSuburb postalSuburb)
        {
            var locationReference = new LocationReference();
            locationReference.Set(postalSuburb);
            return locationReference;
        }

        private static LocationReference Resolve(Country country, string unstructuredLocation, PostalSuburb postalSuburb)
        {
            return postalSuburb != null
                ? Resolve(postalSuburb)
                : Resolve(country, unstructuredLocation);
        }

        private static LocationReference Resolve(Country country, string unstructuredLocation)
        {
            var locationReference = new LocationReference();
            locationReference.Set(country, unstructuredLocation);
            return locationReference;
        }

        private static LocationReference Resolve(string unstructuredLocation, CountrySubdivision subdivision, PostalSuburb postalSuburb)
        {
            return postalSuburb != null
                ? Resolve(postalSuburb)
                : Resolve(unstructuredLocation, subdivision);
        }

        private static LocationReference Resolve(string unstructuredLocation, CountrySubdivision subdivision)
        {
            var locationReference = new LocationReference();
            locationReference.Set(unstructuredLocation, subdivision);
            return locationReference;
        }

        private static LocationReference Resolve(Country country, string unstructuredLocation, PostalCode postalCode, PostalSuburb postalSuburb)
        {
            return postalSuburb != null
                ? Resolve(postalSuburb)
                : Resolve(country, unstructuredLocation, postalCode);
        }

        private static LocationReference Resolve(Country country, string unstructuredLocation, PostalCode postalCode)
        {
            var locationReference = new LocationReference();
            locationReference.Set(country, unstructuredLocation, postalCode);
            return locationReference;
        }

        private static LocationReference Resolve(string unstructuredLocation, CountrySubdivision subdivision, PostalCode postalCode, PostalSuburb postalSuburb)
        {
            return postalSuburb != null
                ? Resolve(postalSuburb)
                : Resolve(unstructuredLocation, subdivision, postalCode);
        }

        private static LocationReference Resolve(string unstructuredLocation, CountrySubdivision subdivision, PostalCode postalCode)
        {
            var locationReference = new LocationReference();
            locationReference.Set(unstructuredLocation, subdivision, postalCode);
            return locationReference;
        }

        private static PostalSuburb SelectSuburb(PostalSuburb postalSuburb, bool continueResolving, ICollection<NamedLocation> suggestions)
        {
            if (continueResolving)
            {
                // Simply return the instance.

                return postalSuburb;
            }

            AddToSuggestions(postalSuburb, suggestions);
            return null;
        }

        private static PostalSuburb SelectSuburb(IList<PostalSuburb> postalSuburbs, bool continueResolving, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            if (continueResolving)
            {
                // It is assumed that the list is in alphabetical order.
                // Simply choose the first instance.

                return postalSuburbs[0];
            }

            AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
            return null;
        }

        private static PostalSuburb SelectSuburbByPostalCode(ICollection<PostalSuburb> postalSuburbs, bool continueResolving, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            if (continueResolving && postalSuburbs.Count > 0)
            {
                // Choose the postal suburb based on the order of the postal code.

                return postalSuburbs.OrderBy(s => s.PostalCode.Name).First();
            }

            AddToSuggestions(postalSuburbs, suggestions, maximumSuggestions);
            return null;
        }

        private static void AddToSuggestions(IEnumerable<PostalSuburb> postalSuburbs, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            if (suggestions != null)
            {
                foreach (var postalSuburb in postalSuburbs)
                {
                    if (!suggestions.Contains(postalSuburb))
                    {
                        suggestions.Add(postalSuburb);
                        if (maximumSuggestions > 0 && suggestions.Count == maximumSuggestions)
                            break;
                    }
                }
            }
        }

        private static void AddToSuggestions(PostalSuburb postalSuburb, ICollection<NamedLocation> suggestions)
        {
            if (suggestions != null)
            {
                if (!suggestions.Contains(postalSuburb))
                    suggestions.Add(postalSuburb);
            }
        }

        private static bool ResolvingIsComplete(PostalSuburb postalSuburb, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            // Do not continue if any of the following is true:
            // - a postal suburb has been resolved (postalSuburb != null)
            // - the caller is not interested in getting any suggestions (suggestions == null)
            // - the caller is interested in getting suggestions, has specified a maximum and that maximum has been reached

            if (postalSuburb != null)
                return true;
            if (suggestions == null)
                return true;
            if (maximumSuggestions > 0 && suggestions.Count == maximumSuggestions)
                return true;
            return false;
        }

        private static PostalCode AreAllSuburbsInSamePostalCode(IList<PostalSuburb> suburbs)
        {
            Debug.Assert(suburbs != null && suburbs.Count > 0);

            var postalCode = suburbs[0].PostalCode;
            for (var index = 1; index < suburbs.Count; ++index)
            {
                if (suburbs[index].PostalCode.Id != postalCode.Id)
                    return null;
            }

            return postalCode;
        }

        private static CountrySubdivision AreAllSuburbsInSameSubdivision(IList<PostalSuburb> suburbs)
        {
            Debug.Assert(suburbs != null && suburbs.Count > 0);

            var subdivision = suburbs[0].CountrySubdivision;
            for (var index = 1; index < suburbs.Count; ++index)
            {
                if (suburbs[index].CountrySubdivision.Id != subdivision.Id)
                    return null;
            }

            return subdivision;
        }

        private static IList<PostalSuburb> GetSuburbs(PostalCode postalCode)
        {
            var matches = new SortedList<string, PostalSuburb>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var postalSuburb in postalCode.GetPostalSuburbs())
                matches.Add(postalSuburb.Name, postalSuburb);
            return matches.Values;
        }

        #endregion

        #region Filters

        private static IFilter<PostalCode> CreatePostalCodeFilter(CountrySubdivision subdivision)
        {
            return new SubdivisionFilter(subdivision);
        }

        private static IFilter<PostalSuburb> CreatePostalSuburbFilter(CountrySubdivision subdivision)
        {
            return new SubdivisionFilter(subdivision);
        }

        private static IFilter<PostalSuburb> CreatePostalSuburbFilter(CountrySubdivision subdivision, PostalCode postalCode)
        {
            return new SubdivisionPostalCodeFilter(subdivision, postalCode);
        }

        private static IFilter<PostalSuburb> CreatePostalSuburbFilter(CountrySubdivision subdivision, IList<PostalCode> postalCodes)
        {
            return new SubdivisionPostalCodesFilter(subdivision, postalCodes);
        }

        private static IFilter<PostalSuburb> CreatePostalSuburbFilter(PostalCode postalCode)
        {
            return new PostalCodeFilter(postalCode);
        }

        private static IFilter<PostalSuburb> CreatePostalSuburbFilter(IList<PostalCode> postalCodes)
        {
            return new PostalCodesFilter(postalCodes);
        }

        private class SubdivisionFilter
            : IFilter<PostalSuburb>, IFilter<PostalCode>
        {
            public SubdivisionFilter(CountrySubdivision subdivision)
            {
                _subdivision = subdivision;
            }

            public static bool Match(PostalSuburb postalSuburb, CountrySubdivision subdivision)
            {
                return subdivision == null || postalSuburb.CountrySubdivision.Equals(subdivision);
            }

            public bool Match(PostalSuburb postalSuburb)
            {
                return Match(postalSuburb, _subdivision);
            }

            private static bool Match(PostalCode postalCode, CountrySubdivision subdivision)
            {
                return subdivision == null || postalCode.Locality.CountrySubdivisions.Contains(subdivision);
            }

            public bool Match(PostalCode postalCode)
            {
                return Match(postalCode, _subdivision);
            }

            private readonly CountrySubdivision _subdivision;
        }

        private class SubdivisionPostalCodeFilter
            : IFilter<PostalSuburb>
        {
            public SubdivisionPostalCodeFilter(CountrySubdivision subdivision, PostalCode postalCode)
            {
                _subdivision = subdivision;
                _postalCode = postalCode;
            }

            public static bool Match(PostalSuburb postalSuburb, CountrySubdivision subdivision, PostalCode postalCode)
            {
                return SubdivisionFilter.Match(postalSuburb, subdivision) && PostalCodeFilter.Match(postalSuburb, postalCode);
            }

            public bool Match(PostalSuburb postalSuburb)
            {
                return Match(postalSuburb, _subdivision, _postalCode);
            }

            private readonly CountrySubdivision _subdivision;
            private readonly PostalCode _postalCode;
        }

        private class SubdivisionPostalCodesFilter
            : IFilter<PostalSuburb>
        {
            public SubdivisionPostalCodesFilter(CountrySubdivision subdivision, IList<PostalCode> postalCodes)
            {
                _subdivision = subdivision;
                _postalCodes = postalCodes;
            }

            private static bool Match(PostalSuburb postalSuburb, CountrySubdivision subdivision, IEnumerable<PostalCode> postalCodes)
            {
                foreach (var postalCode in postalCodes)
                {
                    if (SubdivisionPostalCodeFilter.Match(postalSuburb, subdivision, postalCode))
                        return true;
                }

                return false;
            }

            public bool Match(PostalSuburb postalSuburb)
            {
                return Match(postalSuburb, _subdivision, _postalCodes);
            }

            private readonly CountrySubdivision _subdivision;
            private readonly IList<PostalCode> _postalCodes;
        }

        private class PostalCodeFilter
            : IFilter<PostalSuburb>
        {
            public PostalCodeFilter(PostalCode postalCode)
            {
                _postalCode = postalCode;
            }

            public static bool Match(PostalSuburb postalSuburb, PostalCode postalCode)
            {
                return postalCode == null || postalSuburb.PostalCode.Equals(postalCode);
            }

            public bool Match(PostalSuburb postalSuburb)
            {
                return Match(postalSuburb, _postalCode);
            }

            private readonly PostalCode _postalCode;
        }

        private class PostalCodesFilter
            : IFilter<PostalSuburb>
        {
            public PostalCodesFilter(IList<PostalCode> postalCodes)
            {
                _postalCodes = postalCodes;
            }

            private static bool Match(PostalSuburb postalSuburb, IEnumerable<PostalCode> postalCodes)
            {
                foreach (var postalCode in postalCodes)
                {
                    if (PostalCodeFilter.Match(postalSuburb, postalCode))
                        return true;
                }

                return false;
            }

            public bool Match(PostalSuburb postalSuburb)
            {
                return Match(postalSuburb, _postalCodes);
            }

            private readonly IList<PostalCode> _postalCodes;
        }

        #endregion
    }
}
