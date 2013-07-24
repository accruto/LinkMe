using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Location.Data
{
    public class LocationRepository
        : Repository, ILocationRepository
    {
        private static readonly DataLoadOptions CountrySubdivisionLoadOptions;
        private static readonly DataLoadOptions RegionLoadOptions;
        private static readonly DataLoadOptions LocalityLoadOptions;
        private static readonly DataLoadOptions PostalCodeLoadOptions = DataOptions.CreateLoadOptions<PostalCodeEntity>(s => s.NamedLocationEntity);
        private static readonly DataLoadOptions PostalSuburbLoadOptions = DataOptions.CreateLoadOptions<PostalSuburbEntity>(s => s.NamedLocationEntity);

        private static readonly Func<LocationDataContext, IQueryable<Country>> GetCountriesQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from c in dc.CountryEntities
                   select c.Map());

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, LocationAbbreviation>>> GetLocationAbbreviationsQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from a in dc.LocationAbbreviationEntities
                   select new Tuple<int, LocationAbbreviation>(a.countryId, a.Map()));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, RelativeLocation>>> GetRelativeLocationsQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from l in dc.RelativeLocationEntities
                   select new Tuple<int, RelativeLocation>(l.countryId, l.Map()));

        private static readonly Func<LocationDataContext, IDictionary<int, Country>, IQueryable<CountrySubdivision>> GetCountrySubdivisionsQuery
            = CompiledQuery.Compile((LocationDataContext dc, IDictionary<int, Country> countries)
                => from s in dc.CountrySubdivisionEntities
                   select s.Map(countries));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, CountrySubdivisionAlias>>> GetCountrySubdivisionAliasesQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from s in dc.CountrySubdivisionAliasEntities
                   select new Tuple<int, CountrySubdivisionAlias>(s.countrySubdivisionId, s.Map()));

        private static readonly Func<LocationDataContext, IDictionary<int, IList<int>>, IDictionary<int, Locality>, IQueryable<Region>> GetRegionsQuery
            = CompiledQuery.Compile((LocationDataContext dc, IDictionary<int, IList<int>> regionLocalities, IDictionary<int, Locality> localities)
                => from r in dc.RegionEntities
                   select r.Map(regionLocalities, localities));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, RegionAlias>>> GetRegionAliasesQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from r in dc.RegionAliasEntities
                   select new Tuple<int, RegionAlias>(r.regionId, r.Map()));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, int>>> GetRegionLocalitiesQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from r in dc.LocalityRegionEntities
                   select new Tuple<int, int>(r.regionId, r.localityId));

        private static readonly Func<LocationDataContext, IDictionary<int, CountrySubdivision>, IQueryable<Locality>> GetLocalitiesQuery
            = CompiledQuery.Compile((LocationDataContext dc, IDictionary<int, CountrySubdivision> countrySubdivisions)
                => from l in dc.LocalityEntities
                   select l.Map(countrySubdivisions));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, int>>> GetLocalityPostalCodesQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from c in dc.PostalCodeEntities
                   select new Tuple<int, int>(c.localityId, c.id));

        private static readonly Func<LocationDataContext, IDictionary<int, Locality>, IQueryable<PostalCode>> GetPostalCodesQuery
            = CompiledQuery.Compile((LocationDataContext dc, IDictionary<int, Locality> localities)
                => from c in dc.PostalCodeEntities
                   select c.Map(localities));

        private static readonly Func<LocationDataContext, IQueryable<Tuple<int, int>>> GetPostalCodeSuburbsQuery
            = CompiledQuery.Compile((LocationDataContext dc)
                => from s in dc.PostalSuburbEntities
                   select new Tuple<int, int>(s.postcodeId, s.id));

        private static readonly Func<LocationDataContext, IDictionary<int, CountrySubdivision>, IDictionary<int, PostalCode>, IQueryable<PostalSuburb>> GetPostalSuburbsQuery
            = CompiledQuery.Compile((LocationDataContext dc, IDictionary<int, CountrySubdivision> subdivisions, IDictionary<int, PostalCode> postalCodes)
                => from s in dc.PostalSuburbEntities
                   select s.Map(subdivisions, postalCodes));

        static LocationRepository()
        {
            CountrySubdivisionLoadOptions = new DataLoadOptions();
            CountrySubdivisionLoadOptions.LoadWith<CountrySubdivisionEntity>(s => s.GeographicalAreaEntity);
            CountrySubdivisionLoadOptions.LoadWith<GeographicalAreaEntity>(g => g.NamedLocationEntity);

            RegionLoadOptions = new DataLoadOptions();
            RegionLoadOptions.LoadWith<RegionEntity>(r => r.GeographicalAreaEntity);
            RegionLoadOptions.LoadWith<GeographicalAreaEntity>(g => g.NamedLocationEntity);

            LocalityLoadOptions = new DataLoadOptions();
            LocalityLoadOptions.LoadWith<LocalityEntity>(l => l.GeographicalAreaEntity);
            LocalityLoadOptions.LoadWith<GeographicalAreaEntity>(g => g.NamedLocationEntity);
            LocalityLoadOptions.LoadWith<LocalityEntity>(l => l.LocalityCountrySubdivisionEntities);
        }

        public LocationRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        public virtual LocationData GetLocationData()
        {
            // Get all the objects first.

            var countries = GetCountries();
            var locationAbbreviations = GetLocationAbbreviations();
            var relativeLocations = GetRelativeLocations();
            var regionLocalities = GetRegionLocalities();
            var countrySubdivisions = GetCountrySubdivisions(countries);
            var localities = GetLocalities(countrySubdivisions);
            var localityPostalCodes = GetLocalityPostalCodes();
            var regions = GetRegions(regionLocalities, localities);
            var postalCodes = GetPostalCodes(localities);
            var postalCodeSuburbs = GetPostalCodeSuburbs();
            var postalSuburbs = GetPostalSuburbs(countrySubdivisions, postalCodes);

            return new LocationData
            {
                Countries = countries,
                LocationAbbreviations = locationAbbreviations,
                RelativeLocations = relativeLocations,
                Regions = regions,
                RegionLocalities = regionLocalities,
                CountrySubdivisions = countrySubdivisions,
                Localities = localities,
                LocalityPostalCodes = localityPostalCodes,
                PostalCodes = postalCodes,
                PostalCodeSuburbs = postalCodeSuburbs,
                PostalSuburbs = postalSuburbs,
            };
        }

        private IDictionary<int, IList<int>> GetRegionLocalities()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from r in GetRegionLocalitiesQuery(dc).ToList()
                        group r by r.Item1).ToDictionary(r => r.Key,
                                                         r => (from v in r select v.Item2).ToList() as IList<int>);
            }
        }

        private IDictionary<int, IList<RelativeLocation>> GetRelativeLocations()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from l in GetRelativeLocationsQuery(dc).ToList()
                        group l by l.Item1).ToDictionary(l => l.Key,
                                                         l => (from v in l select v.Item2).ToList() as IList<RelativeLocation>);
            }
        }

        private IDictionary<int, IList<LocationAbbreviation>> GetLocationAbbreviations()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from a in GetLocationAbbreviationsQuery(dc).ToList()
                        group a by a.Item1).ToDictionary(a => a.Key,
                                                         a => (from v in a select v.Item2).ToList() as IList<LocationAbbreviation>);
            }
        }

        private IDictionary<int, Country> GetCountries()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCountriesQuery(dc).ToDictionary(c => c.Id, c => c);
            }
        }

        private IDictionary<int, CountrySubdivision> GetCountrySubdivisions(IDictionary<int, Country> countries)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = CountrySubdivisionLoadOptions;
                var subdivisions = GetCountrySubdivisionsQuery(dc, countries).ToDictionary(s => s.Id, s => s);

                // Get aliases.

                var aliases = GetCountrySubdivisionAliasesQuery(dc);
                foreach (var alias in aliases)
                    subdivisions[alias.Item1].Aliases.Add(alias.Item2);

                return subdivisions;
            }
        }

        private IDictionary<int, Region> GetRegions(IDictionary<int, IList<int>> regionLocalities, IDictionary<int, Locality> localities)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = RegionLoadOptions;
                var regions = GetRegionsQuery(dc, regionLocalities, localities).ToDictionary(r => r.Id, r => r);

                // Get aliases.

                var aliases = GetRegionAliasesQuery(dc);
                foreach (var alias in aliases)
                    regions[alias.Item1].Aliases.Add(alias.Item2);

                return regions;
            }
        }

        private IDictionary<int, Locality> GetLocalities(IDictionary<int, CountrySubdivision> countrySubdivisions)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = LocalityLoadOptions;
                return GetLocalitiesQuery(dc, countrySubdivisions).ToDictionary(l => l.Id, l => l);
            }
        }

        private IDictionary<int, IList<int>> GetLocalityPostalCodes()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from r in GetLocalityPostalCodesQuery(dc).ToList()
                        group r by r.Item1).ToDictionary(r => r.Key,
                                                         r => (from v in r select v.Item2).ToList() as IList<int>);
            }
        }

        private IDictionary<int, PostalCode> GetPostalCodes(IDictionary<int, Locality> localities)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = PostalCodeLoadOptions;
                return GetPostalCodesQuery(dc, localities).ToDictionary(c => c.Id, c => c);
            }
        }

        private IDictionary<int, IList<int>> GetPostalCodeSuburbs()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from r in GetPostalCodeSuburbsQuery(dc).ToList()
                        group r by r.Item1).ToDictionary(r => r.Key,
                                                         r => (from v in r select v.Item2).ToList() as IList<int>);
            }
        }

        private IDictionary<int, PostalSuburb> GetPostalSuburbs(IDictionary<int, CountrySubdivision> subdivisions, IDictionary<int, PostalCode> postalCodes)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                dc.LoadOptions = PostalSuburbLoadOptions;
                return GetPostalSuburbsQuery(dc, subdivisions, postalCodes).ToDictionary(s => s.Id, s => s);
            }
        }

        private LocationDataContext CreateContext()
        {
            return CreateContext(c => new LocationDataContext(c));
        }
    }
}
