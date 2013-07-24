using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.Service
{
    public class GetSuggestedLocations : AutoSuggestWebServiceHandler
    {
        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        public const int DefaultMaximum = 10;
        public const string LocationParameter = "location";
        public const string CountryParameter = "country";
        public const string MaximumParameter = "maximum";
        public const string MethodParameter = "method";

        protected override int DefaultMaxResults
        {
            get { return DefaultMaximum; }
        }

        protected override string MaxResultsParam
        {
            get { return MaximumParameter; }
        }

        protected override IList<string> GetSuggestionList(HttpContext context, int maxResults)
        {
            // Extract the parameters.

            string location = context.Request.Params[LocationParameter];
            if (string.IsNullOrEmpty(location))
                return null;

            string method = context.Request.Params[MethodParameter];

            // At the moment only Australia is supported.

            int countryId = ParseUtil.ParseUserInputInt32(context.Request.Params[CountryParameter], "country ID");
            if (countryId != /*RequestContext.Current.LocationContext.Country*/ _locationQuery.GetCountry("Australia").Id)
                return null;

            // Get the partial matches.

            var country = /*RequestContext.Current.LocationContext.Country*/ _locationQuery.GetCountry("Australia");

            var matches = string.Equals(method, UI.Controls.Common.Location.ResolutionMethod.NamedLocation.ToString(), StringComparison.InvariantCultureIgnoreCase)
                ? _locationQuery.FindPartialMatchedLocations(country, location, maxResults)
                : _locationQuery.FindPartialMatchedPostalSuburbs(country, location, maxResults);

            return matches.Select(p => p.Key).ToArray();
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }
    }
}
