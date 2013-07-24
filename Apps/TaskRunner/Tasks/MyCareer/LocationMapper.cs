using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.TaskRunner.Tasks.MyCareer
{
    public class LocationMapper
    {
        private readonly ILocationQuery _locationQuery;

        private class Address
        {
            public string Postcode;
            public string State;
            public string Area;
        }

        private readonly NamedLocation _australia;
        private readonly NamedLocation _canberra;
        private readonly NamedLocation _sydney;
        private readonly NamedLocation _darwin;
        private readonly NamedLocation _brisbane;
        private readonly NamedLocation _adelaide;
        private readonly NamedLocation _hobart;
        private readonly NamedLocation _melbourne;
        private readonly NamedLocation _perth;
        private readonly NamedLocation _goldCoast;

        private readonly NamedLocation _newcastle;
        private readonly NamedLocation _shepparton;
        private readonly NamedLocation _sutherland;
        private readonly NamedLocation _gosford;
        private readonly NamedLocation _townsville;
        private readonly NamedLocation _bathurst;
        private readonly NamedLocation _cairns;
        private readonly NamedLocation _rockhampton;
        private readonly NamedLocation _noosa;

        private readonly Dictionary<string, NamedLocation> _knownAreasVIC = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasTAS = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasNSW = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasACT = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasQLD = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasSA = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasWA = new Dictionary<string, NamedLocation>();
        private readonly Dictionary<string, NamedLocation> _knownAreasNT = new Dictionary<string, NamedLocation>();

        private readonly Dictionary<string, Dictionary<string, NamedLocation>> _knownAreasByState;

        public LocationMapper(ILocationQuery locationQuery)
        {
            _locationQuery = locationQuery;

            _knownAreasByState = new Dictionary<string, Dictionary<string, NamedLocation>>
            {
                {"VIC", _knownAreasVIC},
                {"TAS", _knownAreasTAS},
                {"NSW", _knownAreasNSW},
                {"ACT", _knownAreasACT},
                {"QLD", _knownAreasQLD},
                {"SA", _knownAreasSA},
                {"WA", _knownAreasWA},
                {"NT", _knownAreasNT},
            };

            _australia = ResolveLocation(string.Empty);
            _canberra = ResolveLocation("Canberra");
            _sydney = ResolveLocation("Sydney");
            _darwin = ResolveLocation("Darwin");
            _brisbane = ResolveLocation("Brisbane");
            _adelaide = ResolveLocation("Adelaide");
            _hobart = ResolveLocation("Hobart");
            _melbourne = ResolveLocation("Melbourne");
            _perth = ResolveLocation("Perth");
            _goldCoast = ResolveLocation("Gold Coast");

            _newcastle = ResolveLocation("2300");
            _shepparton = ResolveLocation("3630");
            _sutherland = ResolveLocation("1499");
            _gosford = ResolveLocation("2250");
            _townsville = ResolveLocation("4810");
            _bathurst = ResolveLocation("2795");
            _cairns = ResolveLocation("4870");
            _rockhampton = ResolveLocation("4700");
            _noosa = ResolveLocation("4567");

            _knownAreasNSW.Add("Sydney Metro", _sydney);
            _knownAreasNSW.Add("Sydney", _sydney);
            _knownAreasNSW.Add("Sydney Inner Suburbs", _sydney);
            _knownAreasNSW.Add("Northern Beaches", _sydney);
            _knownAreasNSW.Add("Newcastle & Region", _newcastle);
            _knownAreasNSW.Add("Newcastle & Hunter", _newcastle);
            _knownAreasNSW.Add("Sutherland Shire", _sutherland);
            _knownAreasNSW.Add("Gosford & Central Coast", _gosford);
            _knownAreasNSW.Add("Bathurst & Central West NSW", _bathurst);

            _knownAreasVIC.Add("Melbourne Metro", _melbourne);
            _knownAreasVIC.Add("Melbourne", _melbourne);
            _knownAreasVIC.Add("Melbourne Inner Suburbs", _melbourne);
            _knownAreasVIC.Add("Bayside", _melbourne);
            _knownAreasVIC.Add("Mornington Peninsula", _melbourne);
            _knownAreasVIC.Add("Shepparton & Central North", _shepparton);

            _knownAreasQLD.Add("Brisbane Metro", _brisbane);
            _knownAreasQLD.Add("Brisbane Inner Suburbs", _brisbane);
            _knownAreasQLD.Add("Gold Coast", _goldCoast);
            _knownAreasQLD.Add("Gold Coast & Hinterland", _goldCoast);
            _knownAreasQLD.Add("Townsville & Region", _townsville);
            _knownAreasQLD.Add("Cairns & Region", _cairns);
            _knownAreasQLD.Add("Rockhampton & Region", _rockhampton);
            _knownAreasQLD.Add("Sunshine Coast", _noosa);
            _knownAreasQLD.Add("Sunshine Coast & Region", _noosa);

            _knownAreasWA.Add("Perth Metro", _perth);
            _knownAreasWA.Add("Perth Inner Suburbs", _perth);

            _knownAreasSA.Add("Adelaide Metro", _adelaide);
            _knownAreasSA.Add("Adelaide Inner Suburbs", _adelaide);

            _knownAreasTAS.Add("Hobart Metro", _hobart);

            _knownAreasNT.Add("Darwin Metro", _darwin);
            _knownAreasNT.Add("Darwin Metro Area", _darwin);

            _knownAreasACT.Add("Canberra & ACT", _canberra);
        }

        public bool TryMap(string location, out LocationReference locationReference)
        {
            var address = ParseLocation(location);

            // Use the postcode whenever possible.

            if (!string.IsNullOrEmpty(address.Postcode))
            {
                locationReference = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), address.Postcode);
                if (locationReference.IsFullyResolved)
                    return true;
            }

            // Try well-known area names used by MyCarrer.

            if (!string.IsNullOrEmpty(address.State) && !string.IsNullOrEmpty(address.Area))
            {
                Dictionary<string, NamedLocation> knownAreas;
                NamedLocation namedLocation;
                if (_knownAreasByState.TryGetValue(address.State, out knownAreas) &&
                    knownAreas.TryGetValue(address.Area, out namedLocation))
                {
                    locationReference = new LocationReference(namedLocation);
                    return true;
                }
            }

            // Resolve the state only.

            locationReference = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), address.State);
            return false;
        }

        private NamedLocation ResolveLocation(string location)
        {
            var reference = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location);
            return reference.NamedLocation;
        }

        /// <summary>
        /// Parses My Career location string.
        /// </summary>
        /// <remarks>The string is parsed into the postcode, state and 
        /// most significant geographical area name.</remarks>
        /// <example>Horsham,VIC 3400</example>
        /// <example>Sydney Metro,NSW     </example>
        /// <example>Perth CBD,WA  6000</example>
        /// <example>Southeastern Suburbs, Melbourne,VIC    </example>
        /// <example>Melbourne Inner Suburbs,VIC     </example>
        /// <example>Gosford & Central Coast,NSW     </example>
        private static Address ParseLocation(string location)
        {
            var address = new Address();

            string[] locationParts = location.Split(',');
            for (int i = 0; i < locationParts.Length; i++)
                locationParts[i] = locationParts[i].Trim();

            string lastPart = locationParts[locationParts.Length - 1];

            int spacePos = lastPart.LastIndexOf(' ');
            if (spacePos != -1)
            {
                address.State = lastPart.Substring(0, spacePos).TrimEnd();
                address.Postcode = lastPart.Substring(spacePos + 1);
            }
            else
            {
                address.State = lastPart;
            }

            if (locationParts.Length > 1)
                address.Area = locationParts[locationParts.Length - 2];

            return address;
        }
    }
}
