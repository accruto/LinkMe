using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility;
using LinkMe.Utility.Validation;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.Service
{
    /// <summary>
    /// This handler is used by WillingnessToRelocate control
    /// </summary>
    public class GetStructuredLocations : SimpleWebServiceHandler
    {
        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        public const char RecordsDivider = ';';
        public const char NameDivider = '=';
        public const char TypeDivider = ':';
        private const string AnywherePrefix = "Anywhere in ";

        // This uses short type names to minimize data transfer to client via Ajax calls.

        public static readonly string CountryType = "c";
        public static readonly string CountrySubdivisionType = "s";
        public static readonly string RegionType = "r";
        public static readonly string PostalSuburbType = "p";

        public const string LocationParameter = "location";
        public const string CountryParameter = "country";
        public const string MaximumParameter = "maximum";
        private const int MaximumDefault = 20;

        protected override string GetOutputErrorMessage(string errorMessage, bool showErrorToEndUser)
        {
            return string.Empty;
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override void ProcessRequestImpl(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            // Extract the parameters.

            string location = context.Request.QueryString[LocationParameter];
            int maximum = ParseUtil.ParseUserInputInt32Optional(context.Request.QueryString[MaximumParameter], MaximumParameter, MaximumDefault);
            int countryId = ParseUtil.ParseUserInputInt32Optional(context.Request.QueryString[CountryParameter], CountryParameter, /*RequestContext.Current.LocationContext.Country*/ _locationQuery.GetCountry("Australia").Id);

            Country country = _locationQuery.GetCountry(countryId);
            if (country == null)
                throw new ServiceEndUserException(String.Format(ValidationErrorMessages.INVALID_COUNTRY_ID_SPECIFIED, countryId));

            // Get the matches.

            IList<PartialMatch> locations;
            if (String.IsNullOrEmpty(location))
            {
                // An empty location means get all subdivisions and regions with no limitation.

                locations = new List<PartialMatch>();
                Add(_locationQuery.GetCountrySubdivisions(country), locations);
                Add(_locationQuery.GetRegions(country), locations);
            }
            else
            {
                // Get the partially matching locations.

                locations = _locationQuery.FindPartialMatchedLocations(country, location, maximum);
            }

            context.Response.Write(GetRecords(locations));
        }

        private static void Add<T>(IEnumerable<T> source, ICollection<PartialMatch> target) where T : NamedLocation
        {
            foreach (T t in source)
                target.Add(new PartialMatch(t.Name, t));
        }

        private static string GetRecords(IList<PartialMatch> locations)
        {
            var records = new string[locations.Count];
            for (int index = 0; index < records.Length; index++)
                records[index] = GetRecord(locations[index].Key, locations[index].NamedLocation, true, true);
            return string.Join(RecordsDivider.ToString(), records);
        }

        public static string GetRecords(IList<LocationReference> locations)
        {
            var records = new string[locations.Count];
            for (int index = 0; index < records.Length; index++)
                records[index] = GetRecord(locations[index].ToString(), locations[index].NamedLocation, true, false);
            return string.Join(RecordsDivider.ToString(), records);
        }
        
        public static string GetRecord(string displayName, NamedLocation namedLocation, bool includeId, bool includeType)
        {
            var sb = new StringBuilder();
            if (includeId)
                sb.Append(namedLocation.Id);

            string type;
            if (namedLocation is CountrySubdivision)
            {
                var cs = (CountrySubdivision)namedLocation;
                if (cs.IsCountry)
                {
                    type = CountryType;
                    displayName = AnywherePrefix + cs.Country.Name;
                }
                else
                {
                    type = CountrySubdivisionType;
                    if (displayName == null)
                        displayName = cs.Name;
                }
            }
            else if (namedLocation is Region)
            {
                type = RegionType;
            }
            else if (namedLocation is PostalSuburb)
            {
                type = PostalSuburbType;
            }
            else
            {
                throw new ArgumentException("List item of type '" + namedLocation.GetType() + "' is unknown.");
            }

            if (includeType)
                sb.Append(TypeDivider).Append(type);
            if (includeId)
                sb.Append(NameDivider);
            if (displayName == null)
                displayName = namedLocation.ToString();
            sb.Append(displayName);
            return sb.ToString();
        }

        public static IList<NamedLocation> ParseRecords(string value)
        {
            IList<NamedLocation> namedLocations = new List<NamedLocation>();

            string[] locations = value.Split(RecordsDivider);
            foreach (string location in locations)
            {
                // This has the form '<id>=<type>:<displayName>'.  Just use the id to look up the named location.

                string[] sliced = location.Split(NameDivider);
                if (sliced.Length < 1 || sliced.Length > 2)
                    throw new LinkMeApplicationException(ValidationErrorMessages.INVALID_FORMAT + " String: '" + location + "'. " + "User-supplied string: " + value);

                int namedLocationId = ParseUtil.ParseUserInputInt32(sliced[0], "id");
                namedLocations.Add(_locationQuery.GetNamedLocation(namedLocationId, true));
            }

            return namedLocations;
        }
    }
}
