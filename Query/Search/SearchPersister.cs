using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Query.Search
{
    internal interface ISearchCriteriaLocation
    {
        int? CountryId { get; }
        string LocationText { get; }
        int? Area { get; }
        string Postcode { get; }
        string State { get; }
        LocationReference Location { get; set; }
    }

    internal interface ISearchCriteriaIndustries
    {
        string Industries { get; }
        Guid? IndustryId { get; }
        IList<Guid> IndustryIds { get; set; }
    }

    public abstract class SearchPersister
    {
        private readonly ILocationQuery _locationQuery;
        private readonly IIndustriesQuery _industriesQuery;

        protected SearchPersister(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            _locationQuery = locationQuery;
            _industriesQuery = industriesQuery;
        }

        protected void OnIndustriesLoaded(SearchCriteria criteria)
        {
            var criteriaIndustries = criteria as ISearchCriteriaIndustries;
            if (criteriaIndustries == null)
                return;

            if (!string.IsNullOrEmpty(criteriaIndustries.Industries))
                ConvertIndustries(criteriaIndustries);
            else if (!criteriaIndustries.IndustryIds.IsNullOrEmpty() || criteriaIndustries.IndustryId != null)
                FilterIndustryIds(criteriaIndustries);
        }

        protected void OnLocationLoaded(SearchCriteria criteria)
        {
            var criteriaLocation = criteria as ISearchCriteriaLocation;
            if (criteriaLocation == null)
                return;

            // "255" is reserved for "anywhere"

            if (criteriaLocation.CountryId == 255)
                return;

            // Old alerts store location in different ways so try to use them.

            if (criteriaLocation.Area != null)
            {
                var area = _locationQuery.GetGeographicalArea(criteriaLocation.Area.Value, true);
                criteriaLocation.Location = new LocationReference(area);
                return;
            }

            // Country, default to Australia if not found.

            var country = criteriaLocation.CountryId != null
                ? _locationQuery.GetCountry(criteriaLocation.CountryId.Value)
                : null;

            var postcode = criteriaLocation.Postcode;
            var state = criteriaLocation.State;
            var location = criteriaLocation.LocationText;

            if (!string.IsNullOrEmpty(postcode))
            {
                // If only the postcode is set then use that.

                if (string.IsNullOrEmpty(state) && string.IsNullOrEmpty(location))
                {
                    var postalCode = _locationQuery.GetPostalCode(country ?? _locationQuery.GetCountry("Australia"), criteriaLocation.Postcode);
                    if (postalCode != null)
                    {
                        criteriaLocation.Location = new LocationReference(postalCode.Locality);
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(criteriaLocation.State))
            {
                // If only the state is set then use that.

                if (string.IsNullOrEmpty(postcode) && string.IsNullOrEmpty(location))
                {
                    var countrySubdivision = _locationQuery.GetCountrySubdivision(country ?? _locationQuery.GetCountry("Australia"), criteriaLocation.State);
                    if (countrySubdivision != null)
                    {
                        criteriaLocation.Location = new LocationReference(countrySubdivision);
                        return;
                    }
                }
            }

            // Standard way.

            if (country == null && string.IsNullOrEmpty(postcode) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(location))
                return;

            // Put in everything.

            if (string.IsNullOrEmpty(location))
                location = string.Empty;
            if (!string.IsNullOrEmpty(postcode))
                location += " " + postcode;
            if (!string.IsNullOrEmpty(state))
                location += " " + state;

            criteriaLocation.Location = _locationQuery.ResolveLocation(country ?? _locationQuery.GetCountry("Australia"), location);
        }

        private void ConvertIndustries(ISearchCriteriaIndustries criteriaIndustries)
        {
            // Convert a list of names into a list of ids.

            criteriaIndustries.IndustryIds = null;
            var names = criteriaIndustries.Industries.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var list = new List<Guid>();
            foreach (var name in names)
            {
                // Try the name first.

                var industry = _industriesQuery.GetIndustryByAnyName(name.Trim());
                if (industry != null)
                {
                    list.Add(industry.Id);
                }
                else
                {
                    // Try as a Guid.

                    try
                    {
                        var id = new Guid(name);
                        industry = _industriesQuery.GetIndustry(id);
                        if (industry != null)
                            list.Add(industry.Id);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            if (list.Count > 0)
                criteriaIndustries.IndustryIds = list;
        }

        private void FilterIndustryIds(ISearchCriteriaIndustries criteriaIndustries)
        {
            // If an industry id is not found then don't include it.

            var list = (from industryId in criteriaIndustries.IndustryIds
                        let industry = _industriesQuery.GetIndustry(industryId)
                        where industry != null
                        select industryId).ToList();

            if (criteriaIndustries.IndustryId != null)
            {
                var industry = _industriesQuery.GetIndustry(criteriaIndustries.IndustryId.Value);
                if (industry != null)
                    list.Add(criteriaIndustries.IndustryId.Value);
            }

            criteriaIndustries.IndustryIds = list.Count == 0 ? null : list;
        }
    }
}
