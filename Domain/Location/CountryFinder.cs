using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Location
{
    internal class CountryFinder
    {
        private delegate PartialMatch FindPartialMatchFromKvp<T>(KeyValuePair<string, T> kvp);

        #region Member Variables

        private readonly Country _country;

        // Used for resolving.

        private readonly IList<CountrySubdivision> _subdivisions = new List<CountrySubdivision>();
        private readonly SortedList<string, CountrySubdivision> _subdivisionNames = new SortedList<string, CountrySubdivision>(World.StringComparer);
        private readonly SortedList<string, CountrySubdivision> _subdivisionShortNames = new SortedList<string, CountrySubdivision>(World.StringComparer);
        private readonly SortedList<string, CountrySubdivision> _subdivisionAliasNames = new SortedList<string, CountrySubdivision>(World.StringComparer);
        private readonly IDictionary<string, CountrySubdivision> _subdivisionAllNames = new Dictionary<string, CountrySubdivision>(World.StringComparer);

        private readonly IList<Region> _regions = new List<Region>();
        private readonly SortedList<string, Region> _regionNames = new SortedList<string, Region>(World.StringComparer);
        private readonly SortedList<string, Region> _regionAliasNames = new SortedList<string, Region>(World.StringComparer);
        private readonly IDictionary<string, Region> _regionAllNames = new Dictionary<string, Region>(World.StringComparer);

        private readonly IList<Locality> _localities = new List<Locality>();
        private readonly SortedList<string, Locality> _localityNames = new SortedList<string, Locality>(World.StringComparer);
        private readonly IDictionary<int, bool> _closestLocalities = new Dictionary<int, bool>();

        private readonly SortedList<string, KeyValuePair<string, PostalCode>> _postalCodeNames = new SortedList<string, KeyValuePair<string, PostalCode>>(World.StringComparer);
        private SortedList<char, int> _postalCodesFirstCharacters;

        private readonly SortedList<string, IList<PostalSuburb>> _postalSuburbNames = new SortedList<string, IList<PostalSuburb>>(World.StringComparer);
        private SortedList<char, int> _postalSuburbsFirstCharacters;
        private readonly SortedList<string, IList<PostalSuburb>> _postalSuburbsSoundExes = new SortedList<string, IList<PostalSuburb>>(StringComparer.Ordinal);

        // Used for getting partial matching lists.

        private readonly SortedList<string, PartialMatch> _partialMatchedPostalSuburbs = new SortedList<string, PartialMatch>();
        private SortedList<char, int> _partialMatchedPostalSuburbsFirstCharacters;
        private readonly SortedList<string, PartialMatch> _partialMatchedPostalCodes = new SortedList<string, PartialMatch>();
        private SortedList<char, int> _partialMatchedPostalCodesFirstCharacters;

        private readonly SortedList<int, IList<PartialMatch>> _synonymPostalSuburbs = new SortedList<int, IList<PartialMatch>>();

        private readonly SortedList<string, LocationAbbreviation> _locationAbbreviations = new SortedList<string, LocationAbbreviation>();
        private readonly SortedList<string, RelativeLocation> _relativeLocations = new SortedList<string, RelativeLocation>();
        private readonly SortedList<string, string> _prefixSynonyms = new SortedList<string, string>();
        private readonly SortedList<string, string> _suffixSynonyms = new SortedList<string, string>();
        private readonly SortedList<string, string> _flipSynonyms = new SortedList<string, string>();
        private Regex _subdivisionRegex;

        #endregion

        #region Subdivision

        public CountryFinder(Country country)
        {
            _country = country;
        }

        public IList<CountrySubdivision> GetCountrySubdivisions()
        {
            return _subdivisions;
        }

        public CountrySubdivision GetCountrySubdivision(string nameOrAlias)
        {
            // Null or empty string is valid - it means the "default" or "unknown" Subdivision, ie. the whole Country.

            nameOrAlias = nameOrAlias ?? "";
            CountrySubdivision value;
            _subdivisionAllNames.TryGetValue(nameOrAlias, out value);
            return value;
        }

        #endregion

        #region Region

        public IList<Region> GetRegions()
        {
            return _regions;
        }

        public Region GetRegion(string nameOrAlias)
        {
            Region value;
            _regionAllNames.TryGetValue(nameOrAlias, out value);
            return value;
        }

        #endregion

        #region Locality

        public Locality GetLocality(string name)
        {
            Locality value;
            _localityNames.TryGetValue(name, out value);
            return value;
        }

        public IList<Locality> GetLocalities()
        {
            return _localities;
        }

        public Locality GetClosestLocality(GeoCoordinates coordinates)
        {
            if (_localities.Count == 0)
                return null;

            // Just use brute force for now.

            var closestLocality = (from l in _localities where _closestLocalities[l.Id] select l).FirstOrDefault();
            if (closestLocality == null)
                return null;
            var closestDistance = closestLocality.Centroid.Distance(coordinates);

            foreach (var locality in _localities)
            {
                if (_closestLocalities[locality.Id])
                {
                    var distance = locality.Centroid.Distance(coordinates);

                    if (distance < closestDistance)
                    {
                        closestLocality = locality;
                        closestDistance = distance;
                    }
                }
            }
            
            return closestLocality;
        }

        #endregion

        #region PostalCode

        public PostalCode GetPostalCode(string postcode)
        {
            KeyValuePair<string, PostalCode> value;
            return _postalCodeNames.TryGetValue(postcode, out value) ? value.Value : null;
        }

        #endregion

        #region Partial Matches

        internal bool IsLocationSynonym(string location, NamedLocation namedLocation)
        {
            IList<PartialMatch> synonyms;
            if (!_synonymPostalSuburbs.TryGetValue(namedLocation.Id, out synonyms))
                return false;

            foreach (var synonym in synonyms)
            {
                if (string.Equals(location, synonym.Key, World.StringComparison))
                    return true;
            }

            return false;
        }

        internal void FindPartialMatchedSubdivisions(IList<PartialMatch> matches, string location, int maximum)
        {
            Debug.Assert(!string.IsNullOrEmpty(location) && (location.Trim() == location || (location.Trim() + ' ') == location), "!string.IsNullOrEmpty(location) && (location.Trim() == location || (location.Trim() + ' ') == location)");

            // The number of subdivisions per country is low so just scan the list of short names, then display names, then aliases.

            FindStartsWithMatches(
                matches,
                kvp => new PartialMatch(false, kvp.Key, kvp.Value),
                location,
                maximum,
                _subdivisionShortNames,
                _subdivisionNames,
                _subdivisionAliasNames);
        }

        internal void FindPartialMatchedRegions(IList<PartialMatch> matches, string location, int maximum)
        {
            Debug.Assert(!string.IsNullOrEmpty(location) && (location.Trim() == location || (location.Trim() + ' ') == location), "!string.IsNullOrEmpty(location) && (location.Trim() == location || (location.Trim() + ' ') == location)");

            // The number of regions per country is low so just scan the list of display names.

            FindStartsWithMatches(
                matches,
                kvp => new PartialMatch(false, kvp.Key, kvp.Value),
                location,
                maximum,
                _regionNames);
        }

        private static void FindStartsWithMatches<T>(ICollection<PartialMatch> matches, FindPartialMatchFromKvp<T> findMatch, string toMatch, int maximum, params IDictionary<string, T>[] keySets)
        {
            foreach (var keySet in keySets)
            {
                foreach (var kvp in keySet)
                {
                    if (!string.IsNullOrEmpty(kvp.Key))
                    {
                        if (kvp.Key.StartsWith(toMatch, StringComparison.InvariantCultureIgnoreCase))
                        {
                            matches.Add(findMatch(kvp));
                            if (maximum > 0 && matches.Count == maximum)
                                return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list (upto the maximum specified) of postal suburbs, e.g. Armadale VIC 3143 or 3143 Armadale VIC,
        /// sorted by the address location, which partially matches (i.e. starts with) the specified location string.
        /// </summary>
        internal void FindPartialMatchedPostalSuburbs(IList<PartialMatch> matches, string location, int maximum)
        {
            // Suburbs first.

            var limited = maximum > 0;
            var postalSuburbs = FindPartialMatches(location, _partialMatchedPostalSuburbs, _partialMatchedPostalSuburbsFirstCharacters, EmptyFilter<PostalSuburb>.Instance);
            
            // Sort and take only the top ones.

            postalSuburbs.Sort(new PostalSuburbNameComparer());
            foreach (var match in postalSuburbs)
            {
                matches.Add(match);
                if (limited && matches.Count == maximum)
                    break;
            }

            // Postcodes.

            if (!limited || matches.Count < maximum)
            {
                postalSuburbs = FindPartialMatches(location, _partialMatchedPostalCodes, _partialMatchedPostalCodesFirstCharacters, EmptyFilter<PostalSuburb>.Instance);
                postalSuburbs.Sort(new PostalSuburbPostcodeComparer());
                foreach (var match in postalSuburbs)
                {
                    matches.Add(match);
                    if (limited && matches.Count == maximum)
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by Name, whose Name partially matches
        /// (i.e. starts with) the specified suburb string.
        /// </summary>
        private IList<PostalSuburb> FindPartialMatchedSuburbs(string suburb, int limit)
        {
            return FindPartialMatchedSuburbs(suburb, EmptyFilter<PostalSuburb>.Instance, limit);
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by Name, whose Name partially matches
        /// (i.e. starts with) the specified suburb string, and resides within the location as
        /// specified by the filter.
        /// </summary>
        private IList<PostalSuburb> FindPartialMatchedSuburbs(string suburb, IFilter<PostalSuburb> filter, int limit)
        {
            return FindPartialMatches(suburb, limit, _postalSuburbNames, _postalSuburbsFirstCharacters, filter);
        }

        #endregion

        #region Resolve

        internal IList<string> ExtractSubdivisionNames(string location)
        {
            if (_subdivisionRegex == null || string.IsNullOrEmpty(location))
                return new string[0];

            // Use the regex to extract.

            var match = _subdivisionRegex.Match(location);
            if (!match.Success)
                return new string[0];

            var names = new List<string> {match.Groups[0].ToString()};

            // Extract all matches as the location may contain multiple instances.

            match = match.NextMatch();
            while (match.Success)
            {
                names.Add(match.Groups[0].ToString());
                match = match.NextMatch();
            }

            return names;
        }

        /// <summary>
        /// Returns the PostalCode that exactly matches the specified postcode, ensuring that
        /// at least part of the postcode's locality resides within the subdivision.
        /// </summary>
        internal PostalCode FindPostalCode(string postcode, CountrySubdivision subdivision)
        {
            var value = GetPostalCode(postcode);
            if (value != null)
            {
                // Check that the postal code is within the subdivision.

                if (value.Locality.CountrySubdivisions.Contains(subdivision))
                    return value;
            }

            return null;
        }

        internal IList<PostalCode> FindPartialMatchedPostalCodes(string postcode)
        {
            return FindPartialMatchedPostalCodes(postcode, EmptyFilter<PostalCode>.Instance);
        }

        /// <summary>
        /// Returns a list of PostalCodes, sorted by Postcode, whose postcode partially matches
        /// (i.e. starts with) the specified postcode string, and resides within the location as
        /// specified by the filter.
        /// </summary>
        internal IList<PostalCode> FindPartialMatchedPostalCodes(string postcode, IFilter<PostalCode> filter)
        {
            var list = FindPartialMatches(postcode, 0, _postalCodeNames, _postalCodesFirstCharacters, filter);
            var postalCodes = new List<PostalCode>();
            foreach (var pair in list)
                postalCodes.Add(pair.Value.Value);
            return postalCodes;
        }

        internal IList<PostalSuburb> FindSuburbs(string suburb)
        {
            return FindSuburbs(suburb, EmptyFilter<PostalSuburb>.Instance);
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by Name, that exactly matches the suburb
        /// and lies within the location specified by the filter.
        /// </summary>
        internal IList<PostalSuburb> FindSuburbs(string suburb, IFilter<PostalSuburb> filter)
        {
            // Look for exact matches.

            IList<PostalSuburb> values;
            if (!_postalSuburbNames.TryGetValue(suburb, out values))
                return new List<PostalSuburb>();

            // Look for postal suburbs that match the filter.

            var matches = new List<PostalSuburb>();
            foreach (var value in values)
            {
                if (filter.Match(value))
                    matches.Add(value);
            }

            return matches;
        }

        internal IList<PostalSuburb> FindSynonymMatchedSuburbs(string suburb)
        {
            return FindSynonymMatchedSuburbs(suburb, EmptyFilter<PostalSuburb>.Instance);
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by Name, that exactly alias matches the suburb
        /// and lies within the location specified by the filter.
        /// </summary>
        internal IList<PostalSuburb> FindSynonymMatchedSuburbs(string suburb, IFilter<PostalSuburb> filter)
        {
            IList<PostalSuburb> postalSuburbs = new List<PostalSuburb>();

            var synonyms = GetPossibleSuburbs(suburb);
            foreach (var synonym in synonyms)
            {
                foreach (var postalSuburb in FindSuburbs(synonym, filter))
                    postalSuburbs.Add(postalSuburb);
            }

            return postalSuburbs;
        }

        private IEnumerable<string> GetPossibleSuburbs(string synonym)
        {
            IList<string> synonyms = new List<string>();

            // Look at prefixes.

            foreach (var prefixSynonym in _prefixSynonyms)
            {
                if (synonym.StartsWith(prefixSynonym.Key + " "))
                    synonyms.Add(prefixSynonym.Value + " " + synonym.Substring(prefixSynonym.Key.Length + 1));
            }

            // Look at suffixes.

            foreach (var suffixSynonym in _suffixSynonyms)
            {
                if (synonym.EndsWith(" " + suffixSynonym.Key))
                    synonyms.Add(synonym.Substring(0, synonym.Length - suffixSynonym.Key.Length - 1) + " " + suffixSynonym.Value);
            }

            // Look at flips.

            foreach (var flipSynonym in _flipSynonyms)
            {
                if (synonym.StartsWith(flipSynonym.Key + " "))
                    synonyms.Add(synonym.Substring(flipSynonym.Key.Length + 1) + " " + flipSynonym.Value);
                if (synonym.EndsWith(" " + flipSynonym.Key))
                    synonyms.Add(flipSynonym.Value + " " + synonym.Substring(0, synonym.Length - flipSynonym.Key.Length - 1));
            }

            return synonyms;
        }

        private IEnumerable<string> GetSynonymsFor(string suburb)
        {
            var synonyms = new List<string>();

            // Look at prefixes.

            foreach (var prefixSynonym in _prefixSynonyms)
            {
                if (suburb.StartsWith(prefixSynonym.Value + " "))
                    synonyms.Add(prefixSynonym.Key + " " + suburb.Substring(prefixSynonym.Value.Length + 1));
            }

            // Look at suffixes.

            foreach (var suffixSynonym in _suffixSynonyms)
            {
                if (suburb.EndsWith(" " + suffixSynonym.Value))
                    synonyms.Add(suburb.Substring(0, suburb.Length - suffixSynonym.Value.Length - 1) + " " + suffixSynonym.Key);
            }

            // Look at flips.

            foreach (var flipSynonym in _flipSynonyms)
            {
                if (suburb.StartsWith(flipSynonym.Value + " "))
                    synonyms.Add(suburb.Substring(flipSynonym.Value.Length + 1) + " " + flipSynonym.Key);
                if (suburb.EndsWith(" " + flipSynonym.Value))
                    synonyms.Add(flipSynonym.Key + " " + suburb.Substring(0, suburb.Length - flipSynonym.Value.Length - 1));
            }

            return synonyms;
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by DisplayName, that reside within the PostalCode and that
        /// lie within the location specified by the filter.
        /// </summary>
        internal IList<PostalSuburb> FindSuburbs(PostalCode postalCode, IFilter<PostalSuburb> filter)
        {
            var matches = new SortedList<string, PostalSuburb>(World.StringComparer);
            foreach (var postalSuburb in postalCode.GetPostalSuburbs())
            {
                if (filter.Match(postalSuburb))
                    matches.Add(postalSuburb.Name, postalSuburb);
            }

            return matches.Values;
        }

        internal IList<PostalSuburb> FindPartialMatchedSuburbs(string suburb)
        {
            return FindPartialMatchedSuburbs(suburb, 0);
        }

        internal IList<PostalSuburb> FindPartialMatchedSuburbs(string suburb, IFilter<PostalSuburb> filter)
        {
            return FindPartialMatchedSuburbs(suburb, filter, 0);
        }

        internal IList<PostalSuburb> FindContainingMatchedSuburbs(string suburb)
        {
            return FindContainingMatchedSuburbs(suburb, EmptyFilter<PostalSuburb>.Instance);
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by DisplayName, whose DisplayName contains the specified suburb string,
        /// and resides within the location as specified by the filter.
        /// </summary>
        internal IList<PostalSuburb> FindContainingMatchedSuburbs(string suburb, IFilter<PostalSuburb> filter)
        {
            var matches = new List<PostalSuburb>();

            // Brute force for now.

            var compareInfo = CultureInfo.CurrentCulture.CompareInfo;
            var postalSuburbLists = _postalSuburbNames.Values;
            var postalSuburbListsCount = postalSuburbLists.Count;

            for (var i = 0; i < postalSuburbListsCount; ++i)
            {
                var postalSuburbs = postalSuburbLists[i];
                var postalSuburbsCount = postalSuburbs.Count;

                for (var j = 0; j < postalSuburbsCount; ++j)
                {
                    var postalSuburb = postalSuburbs[j];
                    if (compareInfo.IndexOf(postalSuburb.Name, suburb, CompareOptions.IgnoreCase) != -1)
                    {
                        if (filter.Match(postalSuburb))
                            matches.Add(postalSuburb);
                    }
                }
            }

            return matches;
        }

        internal IList<PostalSuburb> FindSoundExMatchedSuburbs(string suburb)
        {
            return FindSoundExMatchedSuburbs(suburb, EmptyFilter<PostalSuburb>.Instance);
        }

        /// <summary>
        /// Returns a list of PostalSuburbs, sorted by DisplayName, whose DisplayName sounds the same
        /// as the specified suburb string, and resides within the location as specified by the filter.
        /// </summary>
        internal IList<PostalSuburb> FindSoundExMatchedSuburbs(string suburb, IFilter<PostalSuburb> filter)
        {
            IList<PostalSuburb> matches = new List<PostalSuburb>();

            // Look for locations that match.

            IList<PostalSuburb> postalSuburbs;
            if (_postalSuburbsSoundExes.TryGetValue(SoundEx.GenerateSoundEx(suburb), out postalSuburbs))
            {
                foreach (var postalSuburb in postalSuburbs)
                {
                    if (filter.Match(postalSuburb))
                        matches.Add(postalSuburb);
                }
            }

            return matches;
        }

        /// <summary>
        /// Returns the first PostalSuburb that exactly matches the suburb and lies within the location specified by the filter.
        /// </summary>
        internal PostalSuburb FindSuburb(string suburb, IFilter<PostalSuburb> filter)
        {
            // Look for exact matches.

            IList<PostalSuburb> values;
            if (!_postalSuburbNames.TryGetValue(suburb, out values))
                return null;

            // Look for the first postal suburb that matches the filter.

            foreach (var value in values)
            {
                if (filter.Match(value))
                    return value;
            }

            // Nothing matches.

            return null;
        }

        /// <summary>
        /// Returns the first PostalSuburb that alias matches the suburb and lies within the location specified by the filter.
        /// </summary>
        internal PostalSuburb FindSynonymMatchedSuburb(string suburb, IFilter<PostalSuburb> filter)
        {
            var synonyms = GetPossibleSuburbs(suburb);
            foreach (var synonym in synonyms)
            {
                var postalSuburb = FindSuburb(synonym, filter);
                if (postalSuburb != null)
                    return postalSuburb;
            }

            return null;
        }

        #endregion

        #region Initialisation

        internal void Add(CountrySubdivision subdivision)
        {
            _subdivisions.Add(subdivision);
        }

        internal void Add(Region region)
        {
            _regions.Add(region);
            _regionNames[region.Name] = region;
        }

        internal void Add(Locality locality)
        {
            _localities.Add(locality);
            _localityNames[locality.Name] = locality;

            // Default is that the locality should not be considered for closest queries.

            _closestLocalities[locality.Id] = false;
        }

        internal void Add(PostalCode postalCode)
        {
            // Add it to the map.

            _postalCodeNames.Add(postalCode.Name, new KeyValuePair<string, PostalCode>(postalCode.Name, postalCode));

            // Also include the postcode without leading zeros.

            var trimmedPostcode = postalCode.Postcode.TrimStart('0');
            if (trimmedPostcode != postalCode.Postcode)
                _postalCodeNames.Add(trimmedPostcode, new KeyValuePair<string, PostalCode>(trimmedPostcode, postalCode));

            // Update the closest status for the locality.

            if (CanFindByClosest(postalCode))
                _closestLocalities[postalCode.Locality.Id] = true;
        }

        internal void Add(PostalSuburb postalSuburb)
        {
            // Add it to the PostalSuburbs map.
            
            IList<PostalSuburb> postalSuburbs;
            if (!_postalSuburbNames.TryGetValue(postalSuburb.Name, out postalSuburbs))
            {
                postalSuburbs = new List<PostalSuburb>();
                _postalSuburbNames[postalSuburb.Name] = postalSuburbs;
            }

            postalSuburbs.Add(postalSuburb);

            // Add it to the SoundEx map.

            var soundEx = SoundEx.GenerateSoundEx(postalSuburb.Name);
            if (soundEx != SoundEx.NoSoundEx)
            {
                if (!_postalSuburbsSoundExes.TryGetValue(soundEx, out postalSuburbs))
                {
                    postalSuburbs = new List<PostalSuburb>();
                    _postalSuburbsSoundExes.Add(soundEx, postalSuburbs);
                }

                postalSuburbs.Add(postalSuburb);
            }
        }

        internal void Add(LocationAbbreviation abbreviation)
        {
            _locationAbbreviations.Add(abbreviation.Abbreviation, abbreviation);
        }

        internal void Add(RelativeLocation location)
        {
            _relativeLocations.Add(location.Name, location);
        }

        internal void Initialise()
        {
            // Initialise each location type.

            InitialiseAliases();
            InitialiseSubdivisions();
            InitialiseRegions();
            InitialisePostalCodes();
            InitialisePostalSuburbs();

            // Set the flag (discount the subdivision for the country itself).

            _country.CanResolveLocations =
                _subdivisionNames.Count > 1
                || _regionNames.Count > 0
                || _localityNames.Count > 0
                || _postalCodeNames.Count > 0
                || _postalSuburbNames.Count > 0;
        }

        private static SortedList<char, int> CreateFirstCharactersPositions(IList<string> list)
        {
            var firstCharacters = new SortedList<char, int>();

            var currentChar = '\0';
            for (var index = 0; index < list.Count; ++index)
            {
                var newChar = char.ToLower(list[index][0]);
                if (newChar != currentChar)
                {
                    currentChar = newChar;
                    firstCharacters[currentChar] = index;
                }
            }

            // Add a last character to support the case where the first character is equal to the last one in the list.

            firstCharacters[char.MaxValue] = list.Count;
            return firstCharacters;
        }

        private void InitialiseAliases()
        {
            // First set up prefix and suffix abbreviations.

            foreach (var abbreviation in _locationAbbreviations.Values)
            {
                // "St X" -> "Saint X"

                if (abbreviation.IsPrefix)
                    _prefixSynonyms[abbreviation.Abbreviation] = abbreviation.Name;

                // "X Nth" - > X North"

                if (abbreviation.IsSuffix)
                    _suffixSynonyms[abbreviation.Abbreviation] = abbreviation.Name;
            }

            // Now set up flips.

            foreach (var relativeLocation in _relativeLocations.Values)
            {
                if (relativeLocation.IsPrefix && relativeLocation.IsSuffix)
                {
                    // "North X" -> "X North"

                    _flipSynonyms[relativeLocation.Name] = relativeLocation.Name;

                    // Look for abbreviations.

                    foreach (var abbreviation in _locationAbbreviations.Values)
                    {
                        if (abbreviation.Name == relativeLocation.Name)
                        {
                            // "Nth X" -> "X North"

                            _flipSynonyms[abbreviation.Abbreviation] = relativeLocation.Name;
                        }
                    }
                }
            }
        }

        private void InitialiseSubdivisions()
        {
            const string regexPartFormat = "\\b{0}\\b|";

            Debug.Assert(_subdivisions != null, "subdivisions != null");

            var sbRegex = new StringBuilder();
            foreach (var subdivision in _subdivisions)
            {
                if (subdivision.ShortName == null)
                {
                    _subdivisionAllNames.Add(string.Empty, subdivision);
                }
                else
                {
                    // Add the subdivision under its short name.

                    _subdivisionShortNames.Add(subdivision.ShortName, subdivision);
                    _subdivisionAllNames.Add(subdivision.ShortName, subdivision);
                    sbRegex.AppendFormat(regexPartFormat, subdivision.ShortName);

                    // Add the subdivision under its display name.

                    _subdivisionNames.Add(subdivision.Name, subdivision);
                    _subdivisionAllNames.Add(subdivision.Name, subdivision);
                    sbRegex.AppendFormat(regexPartFormat, subdivision.Name);

                    // Add the subdivision under its aliases.

                    foreach (var alias in subdivision.Aliases)
                    {
                        _subdivisionAliasNames.Add(alias.Name, subdivision);
                        _subdivisionAllNames.Add(alias.Name, subdivision);
                        sbRegex.AppendFormat(regexPartFormat, alias.Name);
                    }
                }
            }

            // Create the regex for the subdivisions.

            if (sbRegex.Length > 0)
            {
                sbRegex.Replace(" ", @"\s*");
                _subdivisionRegex = new Regex(sbRegex.ToString(0, sbRegex.Length - 1), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        private void InitialiseRegions()
        {
            foreach (var region in _regions)
            {
                _regionAllNames.Add(region.Name, region);

                // Add the region under its aliases.

                foreach (var alias in region.Aliases)
                {
                    _regionAliasNames.Add(alias.Name, region);
                    _regionAllNames.Add(alias.Name, region);
                }
            }
        }

        private void InitialisePostalCodes()
        {
            // Extract the first characters for quick lookup.

            _postalCodesFirstCharacters = CreateFirstCharactersPositions(_postalCodeNames.Keys);
        }

        private void InitialisePostalSuburbs()
        {
            // Sort the list of PostalSuburbs for each member of the list.

            foreach (var pair in _postalSuburbNames)
            {
                var postalSuburbs = (List<PostalSuburb>) pair.Value;
                postalSuburbs.Sort(PostalSuburbComparison);
            }

            foreach (var pair in _postalSuburbsSoundExes)
            {
                var postalSuburbs = (List<PostalSuburb>)pair.Value;
                postalSuburbs.Sort(PostalSuburbComparison);
            }

            // Extract the first characters for quick lookup.

            _postalSuburbsFirstCharacters = CreateFirstCharactersPositions(_postalSuburbNames.Keys);

            // Create synonyms list.

            foreach (var pair in _postalSuburbNames)
            {
                var postalSuburbs = (List<PostalSuburb>)pair.Value;
                foreach (var postalSuburb in postalSuburbs)
                {
                    var synonyms = new List<PartialMatch>();
                    _synonymPostalSuburbs[postalSuburb.Id] = synonyms;

                    var key = postalSuburb.ToString();
                    var partialMatch = new PartialMatch(false, key, postalSuburb);
                    _partialMatchedPostalSuburbs[key] = partialMatch;
                    synonyms.Add(partialMatch);

                    key = postalSuburb.ToStringPostcodeFirst();
                    partialMatch = new PartialMatch(false, key, postalSuburb);
                    _partialMatchedPostalCodes[key] = partialMatch;
                    synonyms.Add(partialMatch);

                    // Add a name for each synonym.

                    foreach (var synonym in GetSynonymsFor(postalSuburb.Name))
                        AddSynonym(synonyms, synonym, postalSuburb);
                }
            }

            // Extract the first characters for quick lookup.

            _partialMatchedPostalSuburbsFirstCharacters = CreateFirstCharactersPositions(_partialMatchedPostalSuburbs.Keys);
            _partialMatchedPostalCodesFirstCharacters = CreateFirstCharactersPositions(_partialMatchedPostalCodes.Keys);
        }

        private void AddSynonym(ICollection<PartialMatch> synonyms, string name, PostalSuburb postalSuburb)
        {
            var newPostalSuburb = new PostalSuburb {Id = postalSuburb.Id, Name = name, PostalCode = postalSuburb.PostalCode, CountrySubdivision = postalSuburb.CountrySubdivision};

            var key = newPostalSuburb.ToString();
            var partialMatch = new PartialMatch(true, key, newPostalSuburb);
            _partialMatchedPostalSuburbs[key] = partialMatch;
            synonyms.Add(partialMatch);

            key = newPostalSuburb.ToStringPostcodeFirst();
            partialMatch = new PartialMatch(true, key, newPostalSuburb);
            _partialMatchedPostalCodes[key] = partialMatch;
            synonyms.Add(partialMatch);
        }

        private static int PostalSuburbComparison(PostalSuburb ps1, PostalSuburb ps2)
        {
            return string.Compare(ps1.Name, ps2.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region Partial Matching

        private static int FindFirstPartialMatch(string arg, IList<string> keys, SortedList<char, int> firstCharacters)
        {
            // Use the lookup to determine where to start.

            var firstCharacter = char.ToLower(arg[0]);
            var firstCharacterPos = firstCharacters.IndexOfKey(firstCharacter);
            if (firstCharacterPos != -1)
            {
                // Shortcut. Because this can be called in response to a user typing in an input field the
                // case where there is just one letter will be the most common, and since we already have that
                // information just return it rather than do the further searching which is just going to get
                // back to here in any case.

                if (arg.Length == 1)
                    return firstCharacters.Values[firstCharacterPos];

                // Binary search down to the first partial match.

                var start = firstCharacters.Values[firstCharacterPos];
                var end = firstCharacters.Values[firstCharacterPos + 1] - 1;
                return FindFirstPartialMatch(arg, keys, start, end);
            }

            return -1;
        }

        private static int FindFirstPartialMatch(string arg, IList<string> keys, int start, int end)
        {
            // Look at the mid point of the range.

            var pos = start + (end - start)/2;
            if (keys[pos].StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                // Found a partial match, work backwards to get to the first partial match in the list.

                for (var index = pos - 1; index >= start; --index)
                {
                    if (!keys[index].StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
                        return index + 1;
                }

                return start;
            }

            // Try again using a smaller range.

            if (string.Compare(keys[pos], arg, StringComparison.CurrentCultureIgnoreCase) < 0)
                start = pos + 1;
            else
                end = pos - 1;

            if (start > end)
                return -1;
            return FindFirstPartialMatch(arg, keys, start, end);
        }

        private static List<KeyValuePair<string, KeyValuePair<string, T>>> FindPartialMatches<T>(string partialKey, int maximum, SortedList<string, KeyValuePair<string, T>> map, SortedList<char, int> firstCharacters, IFilter<T> filter)
        {
            var matches = new List<KeyValuePair<string, KeyValuePair<string, T>>>();
            FindPartialMatches(matches, partialKey, maximum, map, firstCharacters, filter);
            return matches;
        }

        private List<PartialMatch> FindPartialMatches(string partialKey, SortedList<string, PartialMatch> map, SortedList<char, int> firstCharacters, IFilter<PostalSuburb> filter)
        {
            var partialMatches = new List<PartialMatch>();
            FindPartialMatches(partialMatches, partialKey, map, firstCharacters, filter);

            // If any synonym postal suburbs are still in the list check that something better doesn't exist.

            var bestPartialMatches = new List<PartialMatch>();
            foreach (var partialMatch in partialMatches)
            {
                var bestPartialMatch = partialMatch;

                if (bestPartialMatch.IsSynonym)
                {
                    IList<PartialMatch> synonyms;
                    if (_synonymPostalSuburbs.TryGetValue(partialMatch.NamedLocation.Id, out synonyms))
                    {
                        // Look for the best synonym that matches the partial key.

                        foreach (var synonym in synonyms)
                        {
                            if (synonym.Key.StartsWith(partialKey, StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (!synonym.IsSynonym)
                                {
                                    bestPartialMatch = synonym;
                                    break;
                                }

                                // Whichever has the longer name is better.

                                if (synonym.Key.Length > bestPartialMatch.Key.Length)
                                    bestPartialMatch = synonym;
                            }
                        }
                    }
                }

                bestPartialMatches.Add(bestPartialMatch);
            }

            return bestPartialMatches;
        }

        private static void FindPartialMatches<T>(ICollection<KeyValuePair<string, KeyValuePair<string, T>>> matches, string partialKey, int maximum, SortedList<string, KeyValuePair<string, T>> map, SortedList<char, int> firstCharacters, IFilter<T> filter)
        {
            if (!string.IsNullOrEmpty(partialKey))
            {
                // Find the position of the first partial match.

                var pos = FindFirstPartialMatch(partialKey, map.Keys, firstCharacters);
                if (pos != -1)
                {
                    // If it matches the filter add it.

                    var t = map.Values[pos].Value;
                    if (filter.Match(t))
                    {
                        var key = map.Keys[pos];
                        matches.Add(new KeyValuePair<string, KeyValuePair<string, T>>(key, new KeyValuePair<string, T>(map.Values[pos].Key, t)));
                        if (maximum > 0 && matches.Count == maximum)
                            return;
                    }

                    // Iterate through the following partial matches.

                    ++pos;
                    while (pos < map.Count)
                    {
                        if (map.Keys[pos].StartsWith(partialKey, StringComparison.CurrentCultureIgnoreCase))
                        {
                            t = map.Values[pos].Value;
                            if (filter.Match(t))
                            {
                                var key = map.Keys[pos];
                                matches.Add(new KeyValuePair<string, KeyValuePair<string, T>>(key, new KeyValuePair<string, T>(map.Values[pos].Key, t)));
                                if (maximum > 0 && matches.Count == maximum)
                                    return;
                            }
                        }
                        else
                        {
                            break;
                        }

                        ++pos;
                    }
                }
            }
        }

        private static void FindPartialMatches(IList<PartialMatch> matches, string partialKey, SortedList<string, PartialMatch> map, SortedList<char, int> firstCharacters, IFilter<PostalSuburb> filter)
        {
            if (!string.IsNullOrEmpty(partialKey))
            {
                // Find the position of the first partial match.

                var pos = FindFirstPartialMatch(partialKey, map.Keys, firstCharacters);
                if (pos != -1)
                {
                    // If it matches the filter add it.

                    var partialMatch = map.Values[pos];
                    var postalSuburb = (PostalSuburb) partialMatch.NamedLocation;
                    if (filter.Match(postalSuburb))
                        Add(matches, partialMatch);

                    // Iterate through the following partial matches.

                    ++pos;
                    while (pos < map.Count)
                    {
                        if (map.Keys[pos].StartsWith(partialKey, StringComparison.CurrentCultureIgnoreCase))
                        {
                            partialMatch = map.Values[pos];
                            postalSuburb = (PostalSuburb) partialMatch.NamedLocation;
                            if (filter.Match(postalSuburb))
                                Add(matches, partialMatch);
                        }
                        else
                        {
                            break;
                        }

                        ++pos;
                    }
                }
            }
        }

        private static void Add(IList<PartialMatch> matches, PartialMatch partialMatch)
        {
            // If the match is not a synonym then it needs to go into the list.

            if (!partialMatch.IsSynonym)
            {
                // This means that all other matches are synonyms which need to be removed first.

                var id = partialMatch.NamedLocation.Id;
                for (var index = 0; index < matches.Count; )
                {
                    var matchedNamedLocation = matches[index].NamedLocation;
                    if (matchedNamedLocation.Id == id)
                        matches.RemoveAt(index);
                    else
                        ++index;
                }

                // Add it to the end.

                matches.Add(partialMatch);
            }
            else
            {
                // It is a synonym. If the postal suburb is not already represented then add it.

                var id = partialMatch.NamedLocation.Id;
                foreach (var match in matches)
                {
                    var matchedNamedLocation = match.NamedLocation;
                    if (matchedNamedLocation.Id == id)
                        return;
                }

                // No other, so add this one.

                matches.Add(partialMatch);
            }
        }

        private static IList<T> FindPartialMatches<T>(string partialKey, int maximum, SortedList<string, IList<T>> map, SortedList<char, int> firstCharacters, IFilter<T> filter)
        {
            var matches = new List<T>();
            if (!string.IsNullOrEmpty(partialKey))
            {
                // Find the position of the first partial match.

                var pos = FindFirstPartialMatch(partialKey, map.Keys, firstCharacters);
                if (pos != -1)
                {
                    // Iterate through list for the first partial match.

                    var list = map.Values[pos];
                    foreach (var t in list)
                    {
                        // If it matches the liter then add it.

                        if (filter.Match(t))
                        {
                            matches.Add(t);
                            if (maximum > 0 && matches.Count == maximum)
                                return matches;
                        }
                    }

                    // Iterate through the following partial matches.

                    ++pos;
                    while (pos < map.Count)
                    {
                        if (map.Keys[pos].StartsWith(partialKey, StringComparison.CurrentCultureIgnoreCase))
                        {
                            list = map.Values[pos];
                            foreach (var t in list)
                            {
                                if (filter.Match(t))
                                {
                                    matches.Add(t);
                                    if (maximum > 0 && matches.Count == maximum)
                                        return matches;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }

                        ++pos;
                    }
                }
            }

            return matches;
        }

        private class PostalSuburbNameComparer
            : IComparer<PartialMatch>
        {
            public int Compare(PartialMatch p1, PartialMatch p2)
            {
                var ps1 = (PostalSuburb) p1.NamedLocation;
                var ps2 = (PostalSuburb) p2.NamedLocation;

                // Compare postal suburb name first.

                var compare = string.Compare(ps1.Name, ps2.Name, true);
                if (compare != 0)
                    return compare;

                // Compare state.

                compare = string.Compare(ps1.CountrySubdivision.ShortName, ps2.CountrySubdivision.ShortName, true);
                if (compare != 0)
                    return compare;

                // Compare postcodes.

                return string.Compare(ps1.PostalCode.Postcode, ps2.PostalCode.Postcode, true);
            }
        }

        private class PostalSuburbPostcodeComparer
            : IComparer<PartialMatch>
        {
            public int Compare(PartialMatch p1, PartialMatch p2)
            {
                var ps1 = (PostalSuburb) p1.NamedLocation;
                var ps2 = (PostalSuburb) p2.NamedLocation;

                // Compare postcodes first.

                var compare = string.Compare(ps1.PostalCode.Postcode, ps2.PostalCode.Postcode, true);
                if (compare != 0)
                    return compare;

                // Compare suburbs.

                compare = string.Compare(ps1.Name, ps2.Name, true);
                if (compare != 0)
                    return compare;

                // Compare states.

                return string.Compare(ps1.CountrySubdivision.ShortName, ps2.CountrySubdivision.ShortName, true);
            }
        }

        #endregion

        private static bool CanFindByClosest(PostalCode postalCode)
        {
            // Must be a non-PO Box post code, which means it falls outside the following ranges.
            // Sourced from http://en.wikipedia.org/wiki/Postcodes_in_Australia

            int ipostcode;
            if (!int.TryParse(postalCode.Postcode, out ipostcode))
                return false;

            if (ipostcode >= 1000 && ipostcode <= 1999)
                return false;

            if (ipostcode >= 200 && ipostcode <= 299)
                return false;

            if (ipostcode >= 8000 && ipostcode <= 8999)
                return false;

            if (ipostcode >= 9000 && ipostcode <= 9999)
                return false;

            if (ipostcode >= 5800 && ipostcode <= 5999)
                return false;

            if (ipostcode >= 6800 && ipostcode <= 6999)
                return false;

            if (ipostcode >= 7800 && ipostcode <= 7999)
                return false;

            if (ipostcode >= 900 && ipostcode <= 999)
                return false;

            return true;
        }
    }
}
