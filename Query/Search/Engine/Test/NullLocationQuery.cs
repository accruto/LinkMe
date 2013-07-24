using System;
using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Query.Search.Engine.Test
{
    class NullLocationQuery
        : ILocationQuery
    {
        #region Implementation of ILocationQuery

        public IList<Country> GetCountries()
        {
            return new Country[0];
        }

        public Country GetCountry(int id)
        {
            throw new System.NotImplementedException();
        }

        public Country GetCountry(string name)
        {
            return new Country{Id = 0};
        }

        public Country GetCountryByIsoCode(string isoCode)
        {
            throw new NotImplementedException();
        }

        public NamedLocation GetNamedLocation(int id)
        {
            throw new System.NotImplementedException();
        }

        public NamedLocation GetNamedLocation(int id, bool throwIfNotFound)
        {
            throw new System.NotImplementedException();
        }

        public GeographicalArea GetGeographicalArea(int id, bool throwIfNotFound)
        {
            throw new System.NotImplementedException();
        }

        public CountrySubdivision GetCountrySubdivision(int id)
        {
            throw new System.NotImplementedException();
        }

        public CountrySubdivision GetCountrySubdivision(int id, bool throwIfNotFound)
        {
            throw new System.NotImplementedException();
        }

        public CountrySubdivision GetCountrySubdivision(Country country, string nameOrAlias)
        {
            throw new System.NotImplementedException();
        }

        public CountrySubdivision GetCountrySubdivision(GeoCoordinates coordinates)
        {
            throw new System.NotImplementedException();
        }

        public IList<CountrySubdivision> GetCountrySubdivisions(Country country)
        {
            throw new System.NotImplementedException();
        }

        public Region GetRegion(int id)
        {
            throw new System.NotImplementedException();
        }

        public Region GetRegion(Country country, string name)
        {
            throw new System.NotImplementedException();
        }

        public IList<Region> GetRegions(Country country)
        {
            throw new System.NotImplementedException();
        }

        public Locality GetLocality(int id)
        {
            throw new System.NotImplementedException();
        }

        public Locality GetLocality(int id, bool throwIfNotFound)
        {
            throw new System.NotImplementedException();
        }

        public Locality GetLocality(GeoCoordinates coordinates)
        {
            throw new System.NotImplementedException();
        }

        public Locality GetClosestLocality(Country country, GeoCoordinates coordinates)
        {
            throw new NotImplementedException();
        }

        public IList<Locality> GetLocalities(Country country)
        {
            throw new System.NotImplementedException();
        }

        public IList<Locality> GetLocalities(Region region)
        {
            throw new System.NotImplementedException();
        }

        public PostalCode GetPostalCode(Country country, string postcode)
        {
            throw new System.NotImplementedException();
        }

        public IList<PostalCode> GetPostalCodes(Locality locality)
        {
            throw new System.NotImplementedException();
        }

        public PostalSuburb GetPostalSuburb(PostalCode postalCode, string suburb)
        {
            throw new System.NotImplementedException();
        }

        public IList<PostalSuburb> GetPostalSuburbs(PostalCode postalCode)
        {
            throw new System.NotImplementedException();
        }

        public bool IsLocationSynonym(Country country, string location, NamedLocation namedLocation)
        {
            throw new System.NotImplementedException();
        }

        public IList<PartialMatch> FindPartialMatchedLocations(Country country, string location, int maximum)
        {
            throw new System.NotImplementedException();
        }

        public IList<PartialMatch> FindPartialMatchedPostalSuburbs(Country country, string location, int maximum)
        {
            throw new System.NotImplementedException();
        }

        public void ResolveLocation(LocationReference locationReference, Country country)
        {
            throw new System.NotImplementedException();
        }

        public void ResolveLocation(LocationReference locationReference, Country country, string location)
        {
            throw new System.NotImplementedException();
        }

        public LocationReference ResolveLocation(Country country, string location)
        {
            throw new System.NotImplementedException();
        }

        public LocationReference ResolveLocation(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            throw new System.NotImplementedException();
        }

        public void ResolvePostalSuburb(LocationReference locationReference, Country country, string location)
        {
            throw new System.NotImplementedException();
        }

        public LocationReference ResolvePostalSuburb(Country country, string location)
        {
            throw new System.NotImplementedException();
        }

        public LocationReference ResolvePostalSuburb(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            throw new System.NotImplementedException();
        }

        public IUrlNamedLocation ResolveUrlNamedLocation(Country country, string urlName)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(LocationReference location1, LocationReference location2)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(CountrySubdivision subdivision, LocationReference location)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Region region, LocationReference location)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
